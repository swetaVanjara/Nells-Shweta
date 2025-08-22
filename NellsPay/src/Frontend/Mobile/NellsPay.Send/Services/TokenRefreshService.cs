
namespace NellsPay.Send.Services;

public class TokenRefreshService : ITokenRefreshService, IAsyncDisposable
{
    private readonly IUserService _authService;
    private readonly ISettingsProvider _settingsProvider;
    private readonly ICountriesService _countriesService;
    private readonly IFxService _fxService;
    private CancellationTokenSource _cts;
    private Task _timerTask;

    public TokenRefreshService(IUserService authService, ICountriesService countriesService, ISettingsProvider settingsProvider, IFxService fxService)
    {
        _authService = authService;
        _settingsProvider = settingsProvider;
        _fxService = fxService;
        _countriesService = countriesService;
    }

    public void Start()
    {
        if (_timerTask != null && !_timerTask.IsCompleted) return;

        _cts = new CancellationTokenSource();
        _timerTask = RunPeriodicRefreshAsync(_cts.Token);
        Console.WriteLine("Token refresh task started");
    }

    public void Stop()
    {
        _cts?.Cancel();
        Console.WriteLine("Token refresh task stopped");
    }

    private async Task RunPeriodicRefreshAsync(CancellationToken token)
    {
        var timer = new PeriodicTimer(TimeSpan.FromMinutes(10));

        try
        {
            while (await timer.WaitForNextTickAsync(token))
            {
                await TryRefreshTokenAsync();
            }
        }
        catch (OperationCanceledException)
        {
            // Task was canceled — expected on Stop
        }
    }

    private async Task TryRefreshTokenAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(_settingsProvider.AccessToken) ||
                string.IsNullOrWhiteSpace(_settingsProvider.RefreshToken))
            {
                Console.WriteLine("Skipping token refresh: Missing tokens.");
                return;
            }

            Console.WriteLine("Attempting token refresh...");
            var result = await _authService.RefreshToken();
            Console.WriteLine($"Token refresh result: {result}");
            await LoadCurrencyDetailsAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Token refresh failed: {ex.Message}");
        }
    }
    private async Task LoadCurrencyDetailsAsync()
    {
      
        var countriesResponse = await _countriesService.GetCountries(0, 60);
        var  allCountries = countriesResponse?.countries?.data ?? [];


        string? nationalityCode = _settingsProvider?.CustomerId != null
            ? _settingsProvider?.country2Code : null;

        var senderCountry = allCountries.FirstOrDefault(c => c.country2Code == nationalityCode)
                            ?? allCountries.FirstOrDefault(c => c.country2Code == "FR");

        var receiverCountry = allCountries.FirstOrDefault(c => c.country2Code == "CM");

        if (senderCountry == null || receiverCountry == null)
            return;

        await _fxService.ConvertCurrency(senderCountry.currencyCode, receiverCountry.currencyCode, 1.0, forceRefresh: true);

    }

    public async ValueTask DisposeAsync()
    {
        Stop();
        if (_timerTask != null)
        {
            try { await _timerTask; } catch { /* ignore */ }
        }
    }
}