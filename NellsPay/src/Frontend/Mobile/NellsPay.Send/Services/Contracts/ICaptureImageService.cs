namespace NellsPay.Send.Services.Contracts;
public interface ICaptureImageService
{
    Task<string?> CaptureAndSaveAsync(string context);
}