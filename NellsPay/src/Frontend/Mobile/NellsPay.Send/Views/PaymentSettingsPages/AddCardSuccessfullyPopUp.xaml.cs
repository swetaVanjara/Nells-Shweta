using CommunityToolkit.Maui.Views;

namespace NellsPay.Send.Views.PaymentSettingsPages;

public partial class AddCardSuccessfullyPopUp  : Popup, INotifyPropertyChanged
{
    private CardsModel _Card { get; set; } = new CardsModel();
    public CardsModel Card
    {
        get { return _Card; }
        set
        {
            if (_Card != value)
            {
                _Card = value;

                OnPropertyChanged();
            }
        }
    }
    
    public AddCardSuccessfullyPopUp(CardsModel cardsModel)
	{
		InitializeComponent();
        Card = cardsModel;

        Opened += async (s, e) => await AnimateOpen();
    }
    private async Task AnimateOpen()
    {
        PopupContent.TranslationY = 300; // Start below
        await PopupContent.TranslateTo(0, 0, 300, Easing.CubicOut);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await AnimateClose();
    }

    private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
    {
        if (e.Direction == SwipeDirection.Down)
        {
            await AnimateClose();
        }

    }
    private async Task AnimateClose()
    {
        await PopupContent.TranslateTo(0, 300, 300, Easing.CubicIn);
        Dispatcher.Dispatch(() => Close());
    }

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        var changed = PropertyChanged;
        if (changed == null)
            return;

        changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}