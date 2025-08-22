using NellsPay.Send.Navigation;
using NellsPay.Send.Repository;
using NellsPay.Send.RestApi;
using NellsPay.Send.Services.Contracts;
using NellsPay.Send.ViewModels.EditProfileVM;
using NellsPay.Send.ViewModels.LoginViewModels;
using NellsPay.Send.ViewModels.MoneyTransferFlowViewModels;
using NellsPay.Send.ViewModels.Notification;
using NellsPay.Send.ViewModels.PaymentSettingsViewModels;
using NellsPay.Send.ViewModels.PaymentsFlow;
using NellsPay.Send.ViewModels.ProfileViewModels;
using NellsPay.Send.ViewModels.RecipientsViewModels;
using NellsPay.Send.ViewModels.SendingReceivingCurrencyVM;
using NellsPay.Send.ViewModels.TransactionViewModels;
using NellsPay.Send.ViewModels.Verifyidentity;
using NellsPay.Send.Views.LoginPages;
using NellsPay.Send.Views.MoneyTransferFlowPages;
using NellsPay.Send.Views.Notification;
using NellsPay.Send.Views.PaymentSettingsPages;
using NellsPay.Send.Views.PaymentsFlow;
using NellsPay.Send.Views.PopUpPages;
using NellsPay.Send.Views.ProfilePages;
using NellsPay.Send.Views.RecipientsPages;
using NellsPay.Send.Views.TransactionPages;
using NellsPay.Send.Views.Verifyidentity;

namespace NellsPay.Send.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSendServices(this IServiceCollection services)
        {
            //Service Registration
            services.AddSingleton<IRecipientService, RecipientService>();
            services.AddSingleton<ICurrencyTransferService, CurrencyTransferService>();
            services.AddSingleton<ITransactionService, TransactionService>();
            services.AddSingleton<INotificatiosService, NotificatiosService>();
            services.AddSingleton<IPaymentFlowService, PaymentFlowService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserApi>(provider => HttpClientProvider.Instance.GetApi<IUserApi>());
            services.AddSingleton<ICustomerApi>(provider => HttpClientProvider.Instance.GetApi<ICustomerApi>());
            services.AddSingleton<ICountriesAPI>(provider => HttpClientProvider.Instance.GetApi<ICountriesAPI>());
            services.AddSingleton<IKycApi>(provider => HttpClientProvider.Instance.GetApi<IKycApi>());
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IToastService, ToastService>();
            services.AddSingleton<ISignInService, SignInService>();
            services.AddSingleton<ISignUpValidationService, SignUpValidationService>();
            services.AddSingleton<IRecipientValidationService, RecipientValidationService>();
            services.AddTransient<ISettingsProvider, SettingsProvider>();
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<ICountriesService, CountriesService>();
            services.AddSingleton<ILogoutService, LogoutService>();
            services.AddSingleton<IKycService, KycService>();
            services.AddSingleton<IFxService, FxService>();
            services.AddSingleton<ITokenRefreshService, TokenRefreshService>();
            services.AddSingleton<IDbContext, MobileDbContext>();
            services.AddSingleton<ICountryRepository, CountryRepository>();
            services.AddSingleton<IFxRepository, FxRepository>();
            services.AddSingleton<IRecipientRepository, RecipientRepository>();
            //UI Registration
            services.AddTransient<HomePage>();
            services.AddTransient<TransactionPage>();
            services.AddTransient<SendMoneyPage>();
            services.AddTransient<RecipientPage>();
            services.AddTransient<ProfilePage>();
            services.AddTransient<SendingReceivingCurrencyPage>();
            services.AddTransient<SelectCountryRecipient>();
            services.AddTransient<PaymentMethodPage>();
            services.AddTransient<ChooseReasonPage>();
            services.AddTransient<EditProfilePage>();
            services.AddTransient<AddEditRecipientPage>();
            services.AddTransient<ReviewTransictionPage>();
            services.AddTransient<TransferPinPage>();
            services.AddTransient<PaymentSettingsPage>();
            services.AddTransient<AddManuallyPage>();
            services.AddTransient<ChangePasswordPage>();
            services.AddTransient<FaqsPage>();
            services.AddTransient<TermsOrPolicyPage>();
            services.AddTransient<SuccessPage>();
            services.AddTransient<ChooserecipientsPage>();
            services.AddTransient<CameraPage>();

            services.AddTransient<OnboardingPage>();
            services.AddTransient<SignUpPage>();
            services.AddTransient<LoginPage>();
            services.AddTransient<PINPage>();
            services.AddTransient<NotificatiosPage>();
            services.AddTransient<TransactionDetailsPage>();
            services.AddTransient<ProccessingVerficationPage>();
            services.AddTransient<ProccessingVerficationVM>();

            services.AddTransient<AddBankAccountPage>();
            services.AddTransient<ConfirmPaymentPage>();
            services.AddTransient<SelectBankAccountPage>();
            services.AddTransient<SelectBankPage>();
            services.AddTransient<SelectCardPage>();


            services.AddTransient<SelectDocumentPage>();
            services.AddTransient<ChooseDeliveryMethodPage>();
            services.AddTransient<PaymentWebview>();
            services.AddTransient<CustomKYCPage>();
            services.AddTransient<GenderPage>();
            //ViewModel Registration
            services.AddTransient<BaseViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<TransactionViewModel>();
            services.AddTransient<SendMoneyViewModel>();
            services.AddTransient<RecipientViewModel>();
            services.AddTransient<ProfileViewModel>();
            services.AddTransient<SendingReceivingCurrencyVM>();
            services.AddTransient<PaymentMethodVM>();
            services.AddTransient<ChooseReasonVM>();
            services.AddTransient<EditProfileVM>();
            services.AddTransient<AddEditRecipientVM>();
            services.AddTransient<ReviewTransictionVM>();
            services.AddTransient<TransferPinVM>();
            services.AddTransient<PaymentSettingsVM>();
            services.AddTransient<AddManuallyVM>();
            services.AddTransient<ChangePasswordVM>();
            services.AddTransient<FaqsVM>();
            services.AddTransient<ChooserecipientsVM>();
            services.AddTransient<PINVM>();
            services.AddTransient<SignUpVM>();
            services.AddTransient<OnboardingVM>();
            services.AddTransient<LoginVM>();
            services.AddTransient<CameraVM>();

            services.AddTransient<NotificatiosVM>();
            services.AddTransient<TransactionDetailsVM>();


            services.AddTransient<AddBankAccountVM>();
            services.AddTransient<ConfirmPaymentVM>();
            services.AddTransient<SelectBankAccountVM>();
            services.AddTransient<SelectBankVM>();
            services.AddTransient<SelectCardVM>();
            services.AddTransient<ChooseDeliveryMethodVM>();
            services.AddTransient<CustomKYCViewModel>();
            services.AddTransient<GenderVM>();


            services.AddTransient<SelectDocumentVM>();
            services.AddTransient<PaymentWebViewModel>();
            services.AddTransient<SelectCountryRecipientVM>();
            services.AddTransient<BaseViewModel>();;
            services.AddTransient<RecipientDataStore>();
            services.AddSingleton<ICaptureImageService, CaptureImageService>();
            return services;
        }
    }
}
