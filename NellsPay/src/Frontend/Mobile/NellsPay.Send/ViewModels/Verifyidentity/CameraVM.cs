using System.Windows.Input;
using NellsPay.Send.ResponseModels;
using NellsPay.Send.RestApi;
using NellsPay.Send.ViewModels.Verifyidentity;
using NellsPay.Send.Views.Verifyidentity;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace NellsPay.Send.ViewModels.LoginViewModels
{
    [QueryProperty(nameof(SelectdocumentID), "SelectdocumentID")]

    public partial class CameraVM : BaseViewModel
    {
        private readonly ICaptureImageService _captureImageService;
        private readonly IKycService _kycService;
        private readonly ISettingsProvider _settingsProvider;
        private readonly IToastService _toastService;
        [ObservableProperty]
        private ImageSource? capturedImage;

        private readonly List<string> _captureContexts = new() { "document-front", "document-back", "face" };

        [ObservableProperty]
        private bool isTorchOn;

        [ObservableProperty]
        private bool isDocumentVisible = true;

        [ObservableProperty]
        private bool isSelfieVisible = false;

        [ObservableProperty]
        private string label1 = "Capture Document";

        [ObservableProperty]
        private string label2 = "Step 1";

        [ObservableProperty]
        private bool isCircleOverlay = false;

        [ObservableProperty]
        private string selectdocumentID = "";

        private int captureStep = 0;
        private int captureindexStep = 0;

        public ICommand BackCommand { get; }

        public CameraVM(
            ICaptureImageService captureImageService,
            IKycService kycService,
            ISettingsProvider settingsProvider, IToastService toastService)
        {
            _captureImageService = captureImageService;
            _kycService = kycService;
            _settingsProvider = settingsProvider;
            _toastService = toastService;
            BackCommand = new RelayCommand(BackPage);
        }

        [RelayCommand]
        public async Task CaptureAsync()
        {
            try
            {
                if (_settingsProvider.ProfileId != null)
                {
                    var context = _captureContexts[captureindexStep];
                    var base64Image = await _captureImageService.CaptureAndSaveAsync(context);
                    CapturedImage = ImageSource.FromStream(() =>
                        new MemoryStream(Convert.FromBase64String(base64Image)));
                    if (string.IsNullOrWhiteSpace(base64Image))
                    {
                        await Shell.Current.DisplayAlert("Error", "Failed to capture image.", "OK");
                        return;
                    }

                    var wrapper = new DocUploadWrapper
                    {
                        ImageUploadRequest = new ImageUploadRequest
                        {
                            Image = new NellsPay.Send.RestApi.Image
                            {
                                Context = context,
                                Content = base64Image
                            }
                        }
                    };

                    var response = await _kycService.PostUploadImage(wrapper, _settingsProvider.ProfileId);

                    // Update step indexes
                    captureStep++;
                    captureindexStep++;

                    // PASSPORT (2 steps: front + selfie)
                    if (selectdocumentID == "1")
                    {
                        if (captureStep == 1)
                        {
                            // Show selfie step
                            IsDocumentVisible = false;
                            IsSelfieVisible = true;
                            Label1 = "Take a selfie";
                            Label2 = "Step 2: Please make sure you are taking photo in a well lighted area and face in the circular frame.";
                            IsCircleOverlay = true;
                        }
                        else if (captureStep == 2)
                        {
                            // All steps done
                            _settingsProvider.SelectedDocumentId = "0";

                            await Shell.Current.GoToAsync(nameof(ProccessingVerficationPage));
                        }
                    }
                    else // OTHER DOCUMENTS (3 steps: front, back, selfie)
                    {
                        if (captureStep == 1)
                        {
                            IsDocumentVisible = true;
                            IsSelfieVisible = false;
                            Label1 = "Capture back side of document";
                            Label2 = "Step 2";
                            IsCircleOverlay = false;
                        }
                        else if (captureStep == 2)
                        {
                            IsDocumentVisible = false;
                            IsSelfieVisible = true;
                            Label1 = "Take a selfie";
                            Label2 = "Step 3: Please make sure you are taking photo in a well lighted area and face in the circular frame.";
                            IsCircleOverlay = true;
                        }
                        else if (captureStep == 3)
                        {
                            _settingsProvider.SelectedDocumentId = "0";
                            await Shell.Current.GoToAsync(nameof(ProccessingVerficationPage));
                        }
                    }
                }
                else
                {
                    _toastService.ShowToast("Profile ID Is Null.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Capture failed: {e}");
            }

        }
        private async void BackPage()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}