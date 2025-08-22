using SkiaSharp;

namespace NellsPay.Send.Services;

public class CaptureImageService : ICaptureImageService
{
    public async Task<string?> CaptureAndSaveAsync(string context)
    {
        try
        {
            var status = await Permissions.RequestAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
                return null;

            var photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo == null) return null;

            var filePath = Path.Combine(FileSystem.AppDataDirectory, $"{context}.png");

            await using var sourceStream = await photo.OpenReadAsync();
            var resizedBytes = ResizeImage(sourceStream, 800, 800);
            return Convert.ToBase64String(resizedBytes);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Image capture error: " + ex);
            return null;
        }
    }

    private byte[] ResizeImage(Stream imageStream, int width, int height)
    {
        using var managedStream = new SKManagedStream(imageStream);
        using var original = SKBitmap.Decode(managedStream);

        if (original == null)
            throw new Exception("Failed to decode image stream.");

        var resized = original.Resize(new SKImageInfo(width, height), SKFilterQuality.Medium);
        if (resized == null)
            throw new Exception("Failed to resize image.");

        using var image = SKImage.FromBitmap(resized);
        using var outputStream = new MemoryStream();
        image.Encode(SKEncodedImageFormat.Jpeg, 80).SaveTo(outputStream);

        return outputStream.ToArray();
    }

}

