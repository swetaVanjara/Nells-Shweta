namespace NellsPay.Send.Views;

public partial class SendMoneyPage : ContentPage
{
	public SendMoneyPage(SendMoneyViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void Backbutton_Clicked(object sender, EventArgs e)
    {

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is SendMoneyViewModel viewModel)
        {
            viewModel.CheckUserIsVerifiedOrNot();
        }
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        if (entry == null) return;

        string newText = e.NewTextValue;
        if (string.IsNullOrWhiteSpace(newText)) return;

        entry.TextChanged -= Entry_TextChanged; // Prevent infinite loop

        int cursorPosition = entry.CursorPosition; // Save cursor position

        if (decimal.TryParse(newText, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out decimal number))
        {
            entry.Text = number.ToString("N2", CultureInfo.InvariantCulture);
            cursorPosition = Math.Min(entry.Text.Length, cursorPosition); // Ensure cursor position is valid
        }

        entry.CursorPosition = cursorPosition; // Restore cursor position

        entry.TextChanged += Entry_TextChanged;
    }
}