
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using NellsPay.Send.Contracts;
using NellsPay.Send.Helpers;
using NellsPay.Send.Views.PopUpPages;

namespace NellsPay.Send.ViewModels.Verifyidentity
{
    public class ProccessingVerficationVM : BaseViewModel
    {
        #region Fields
        private ProcesseModel _Proccess1 { get; set; } = new ProcesseModel();
        private ProcesseModel _Proccess2 { get; set; } = new ProcesseModel();
        private ProcesseModel _Proccess3 { get; set; } = new ProcesseModel();
        public ICommand BackCommand { get; }
        private readonly ISettingsProvider _settingsProvider;
        private readonly IKycService _kycService;
        private readonly ISettingsProvider _settingProvider;
        #endregion
        #region Property
        public ProcesseModel Proccess1
        {
            get { return _Proccess1; }
            set
            {
                if (_Proccess1 != value)
                {
                    _Proccess1 = value;
                    OnPropertyChanged();

                }
            }
        }
        public ProcesseModel Proccess2
        {
            get { return _Proccess2; }
            set
            {
                if (_Proccess2 != value)
                {
                    _Proccess2 = value;
                    OnPropertyChanged();

                }
            }
        }
        public ProcesseModel Proccess3
        {
            get { return _Proccess3; }
            set
            {
                if (_Proccess3 != value)
                {
                    _Proccess3 = value;
                    OnPropertyChanged();

                }
            }
        }
        #endregion
        #region Extra

        #endregion
        public ProccessingVerficationVM(IKycService kycService, ISettingsProvider settingsProvider)
        {
            _settingProvider = settingsProvider;
            _kycService = kycService;
            BackCommand = new RelayCommand(BackPage);
            Task.Run(async () =>
            {
                await StartProccess();
            });
        }
        private async void BackPage()
        {
            await Shell.Current.GoToAsync("..");
        }
        #region Methods
        public async Task StartProccess()
        {
            Proccess1 = new ProcesseModel { ProccessTitle = "Photos processing", isProccessing = 0 };
            Proccess2 = new ProcesseModel { ProccessTitle = "Image quality processing", isProccessing = 0 };
            Proccess3 = new ProcesseModel { ProccessTitle = "Photos processing", isProccessing = 0 };

            Proccess1.isProccessing = 1;
            await Task.Delay(3000);
            Proccess1.isProccessing = 2;
            Proccess1.ProccessTitle = "Photos processed";
            Proccess2.isProccessing = 1;
            await Task.Delay(3000);
            Proccess2.isProccessing = 2;
            Proccess2.ProccessTitle = "Image quality checked";
            Proccess3.isProccessing = 1;
            await Task.Delay(3000);
            Proccess3.isProccessing = 2;
            Proccess3.ProccessTitle = "Photos processed";
            var SubmitSession = new SubmitSessionWrapper()
            {
                status = "submitted",
            };
            var response = await _kycService.PatchSubmitSession(SubmitSession, _settingProvider.ProfileId);
            if (response?.Message?.Equals("Session submitted successfully.", StringComparison.OrdinalIgnoreCase) == true)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Shell.Current.CurrentPage.ShowPopupAsync(new SuccessPage("Verification", "Thank you, your verification is recorded. Will notify you once verification is completed.", "Ok",true));
                    await Shell.Current.GoToAsync("//HomePage");
                    await Task.Delay(300);
                    WeakReferenceMessenger.Default.Send<object, string>(this, HomePageRefreshMessage.VerifiedData);
                });
            }
        }

        #endregion

        #region Command

        public ICommand ExitCommand => new Command(() =>
        {
            //
        });

        #endregion
    }
}
