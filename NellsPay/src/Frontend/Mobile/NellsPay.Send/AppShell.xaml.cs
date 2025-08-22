using NellsPay.Send.ViewModels.SendingReceivingCurrencyVM;
using NellsPay.Send.Views.LoginPages;
using NellsPay.Send.Views.MoneyTransferFlowPages;
using NellsPay.Send.Views.Notification;
using NellsPay.Send.Views.PaymentSettingsPages;
using NellsPay.Send.Views.PaymentsFlow;
using NellsPay.Send.Views.ProfilePages;
using NellsPay.Send.Views.RecipientsPages;
using NellsPay.Send.Views.TransactionPages;
using NellsPay.Send.Views.Verifyidentity;

namespace NellsPay.Send
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //   _ = new MauiIcon();

            // Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            // Routing.RegisterRoute(nameof(SendMoneyPage), typeof(SendMoneyPage));
            // Routing.RegisterRoute(nameof(RecipientPage), typeof(RecipientPage));
            // Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(TransactionPage), typeof(TransactionPage));
            Routing.RegisterRoute("Recipients/Money", typeof(RecipientPage));
            Routing.RegisterRoute(nameof(SendingReceivingCurrencyPage), typeof(SendingReceivingCurrencyPage));
            Routing.RegisterRoute(nameof(PaymentMethodPage), typeof(PaymentMethodPage));
            Routing.RegisterRoute(nameof(ChooseReasonPage), typeof(ChooseReasonPage));
            Routing.RegisterRoute(nameof(EditProfilePage), typeof(EditProfilePage));
            Routing.RegisterRoute(nameof(SelectCountryRecipient), typeof(SelectCountryRecipient));
            Routing.RegisterRoute(nameof(AddEditRecipientPage), typeof(AddEditRecipientPage));
            Routing.RegisterRoute(nameof(ReviewTransictionPage), typeof(ReviewTransictionPage));
            Routing.RegisterRoute(nameof(TransferPinPage), typeof(TransferPinPage));
            Routing.RegisterRoute(nameof(PaymentSettingsPage), typeof(PaymentSettingsPage));
            Routing.RegisterRoute(nameof(AddManuallyPage), typeof(AddManuallyPage));
            Routing.RegisterRoute(nameof(ChangePasswordPage), typeof(ChangePasswordPage));
            Routing.RegisterRoute(nameof(TermsOrPolicyPage), typeof(TermsOrPolicyPage));
            Routing.RegisterRoute(nameof(FaqsPage), typeof(FaqsPage));
            Routing.RegisterRoute(nameof(TermsOrPolicyPage), typeof(TermsOrPolicyPage));

            Routing.RegisterRoute(nameof(ChooserecipientsPage), typeof(ChooserecipientsPage));
            Routing.RegisterRoute(nameof(OnboardingPage), typeof(OnboardingPage));
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(PINPage), typeof(PINPage));

            Routing.RegisterRoute(nameof(NotificatiosPage), typeof(NotificatiosPage));
            Routing.RegisterRoute(nameof(TransactionDetailsPage), typeof(TransactionDetailsPage));


            Routing.RegisterRoute(nameof(AddBankAccountPage), typeof(AddBankAccountPage));
            Routing.RegisterRoute(nameof(CustomKYCPage), typeof(CustomKYCPage));
            Routing.RegisterRoute(nameof(GenderPage), typeof(GenderPage));
            Routing.RegisterRoute(nameof(ConfirmPaymentPage), typeof(ConfirmPaymentPage));
            Routing.RegisterRoute(nameof(SelectBankAccountPage), typeof(SelectBankAccountPage));
            Routing.RegisterRoute(nameof(SelectBankPage), typeof(SelectBankPage));
            Routing.RegisterRoute(nameof(SelectCardPage), typeof(SelectCardPage));
            Routing.RegisterRoute(nameof(CameraPage), typeof(CameraPage));
            Routing.RegisterRoute(nameof(ProccessingVerficationPage), typeof(ProccessingVerficationPage));
            Routing.RegisterRoute(nameof(SelectDocumentPage), typeof(SelectDocumentPage));
            Routing.RegisterRoute(nameof(ChooseDeliveryMethodPage), typeof(ChooseDeliveryMethodPage));
            Routing.RegisterRoute(nameof(PaymentWebview), typeof(PaymentWebview));
            Routing.RegisterRoute(nameof(ScanCard), typeof(ScanCard));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var tokenRefreshService = App.Services?.GetService<ITokenRefreshService>();
            tokenRefreshService?.Start();
        }
        
        
    }
}
