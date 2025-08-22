using CommunityToolkit.Mvvm.Messaging;
using NellsPay.Send.Contracts;
using NellsPay.Send.Views.Verifyidentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NellsPay.Send.ViewModels.Verifyidentity
{
    public class SelectDocumentVM : BaseViewModel
    {
        #region Fields
        private ObservableCollection<DocumentModel> _Documents { get; set; } = new ObservableCollection<DocumentModel>();
        public AsyncRelayCommand<DocumentModel> ChoosetCommand { get; private set; }
        private readonly IKycService _Service;
        private readonly ISettingsProvider _settingsProvider;

        #endregion
        #region Property
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<DocumentModel> Documents
        {
            get { return _Documents; }
            set
            {
                if (_Documents != value)
                {
                    _Documents = value;

                    OnPropertyChanged();

                }
            }
        }
        #endregion
        #region Extra

        #endregion
        public SelectDocumentVM(ISettingsProvider settingsProvider, IKycService serviceProvider)
        {
            _settingsProvider = settingsProvider;
            _Service = serviceProvider;
            ChoosetCommand = new AsyncRelayCommand<DocumentModel>(NavigateToUploadPicture);
        }
        public async Task OnPageAppearing()
        { 
            // if (int.Parse(_settingsProvider.SelectedDocumentId) >= 0)
            // {
            //     await Shell.Current.GoToAsync($"{nameof(CameraPage)}?SelectdocumentID={_settingsProvider.SelectedDocumentId}");
            //     return;
            // }
            await GetData();
        }
        private async Task NavigateToUploadPicture(DocumentModel selectdocument)
        {
            try
            {
                IsLoading = true;
                _settingsProvider.SelectedDocumentId = selectdocument.Id;
                await Shell.Current.GoToAsync($"{nameof(CameraPage)}?SelectdocumentID={selectdocument.Id}");
                // if (!string.IsNullOrEmpty(_settingsProvider.Session))
                // {
                //     if (!string.IsNullOrEmpty(_settingsProvider.SelectedDocumentId))
                //     {
                //         _ = Shell.Current.GoToAsync($"{nameof(CameraPage)}?SelectdocumentID={_settingsProvider.SelectedDocumentId}");
                //         return;
                //     }
                // }
                // var ResSession = await _Service.GetSessionByProfileId(_settingsProvider.ProfileId);
                // if (ResSession == null)
                // {
                //     var obj = new SessionWrapper
                //     {
                //         Session = new Session
                //         {
                //             Callback = new Uri("https://www.nellspay.com"),
                //             VendorData = _settingsProvider.ProfileId,
                //         }
                //     };
                //     Console.Write("Sission Start Jason:**-*-*" + obj.ToString());
                //     var response = await _Service.PostSessionStart(obj);
                //     if (response?.Url != null)
                //     {
                //         _settingsProvider.Session = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                //         _settingsProvider.SelectedDocumentId = selectdocument.Id;
                //     }
                //     else
                //     {
                //         Console.Write(obj.ToString());
                //         Console.Write("*-*-*-*-*- Session not start. *-*-*-*-*-*");
                //     }

                //     await Shell.Current.GoToAsync($"{nameof(CameraPage)}?SelectdocumentID={selectdocument.Id}");
                // }
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());
            }
            finally
            {
                IsLoading = false;
            }
        }

        #region Methods
        public async Task GetData()
        {
            Documents = new ObservableCollection<DocumentModel>();
            Documents.Add(new DocumentModel { Id = "1", DocumentName = "Passport", DocumentDescription = "Most countries accepted.", DocumentIcon = "passport.png" });
            Documents.Add(new DocumentModel { Id = "2", DocumentName = "Driving license", DocumentDescription = "Valid for local KYC and AML checks.", DocumentIcon = "drivinglicense.png" });
            Documents.Add(new DocumentModel { Id = "3", DocumentName = "Government ID card", DocumentDescription = "US, Canada and Maxico", DocumentIcon = "governmentid.png" });
            Documents.Add(new DocumentModel { Id = "4", DocumentName = "Resident card", DocumentDescription = "Proof of legal residency status.", DocumentIcon = "residentcard.png" });
        }

        #endregion

        #region Command

        public ICommand BackCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("..");
        });
        public ICommand ContinueCommand => new Command(async () =>
        {
            // await Shell.Current.GoToAsync("..");
        });
        // public ICommand ChoosetCommand
        // {
        //     get
        //     {
        //         return new Command(async (e) =>
        //         {
        //             var item = (e as Recipient);
        //             if (item != null)
        //             {
        //                 await Shell.Current.GoToAsync(nameof(CameraPage));
        //             }
        //         });
        //     }
        // }
        #endregion
    }
}
