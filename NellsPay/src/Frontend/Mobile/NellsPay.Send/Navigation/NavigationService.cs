using NellsPay.Send.Views.LoginPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.Navigation;
public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private INavigation? GetNavigation()
    {
        if (Application.Current.MainPage is NavigationPage navPage)
            return navPage.Navigation;

        if (Application.Current.MainPage is Shell shell && shell.CurrentPage is Page page)
            return page.Navigation;

        if (Application.Current.MainPage is Page p)
            return p.Navigation;

        return null;
    }

    public async Task PushAsync<TPage>() where TPage : Page
    {
        var page = _serviceProvider.GetRequiredService<TPage>();
        await GetNavigation()?.PushAsync(page)!;
    }

    public async Task PushAsync<TPage>(object parameter) where TPage : Page
    {
        var page = _serviceProvider.GetRequiredService<TPage>();

        if (page.BindingContext is IInitializeWithParameter vm)
            vm.Initialize(parameter);

        await GetNavigation()?.PushAsync(page)!;
    }

    public async Task PopAsync()
    {
        await GetNavigation()?.PopAsync()!;
    }

    public async Task PopToRootAsync()
    {
        await GetNavigation()?.PopToRootAsync()!;
    }
}