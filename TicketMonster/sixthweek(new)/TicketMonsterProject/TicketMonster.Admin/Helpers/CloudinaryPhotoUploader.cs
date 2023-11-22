using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using TicketMonster.Admin.Interface;

namespace TicketMonster.Admin.Helpers
{
public class CloudinaryPhotoUploader : IPhotoUploader
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryPhotoUploader(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<string> UploadPhoto(IFormFile photo)
    {
        if (photo == null || photo.Length == 0)
        {
            return null;
        }

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(photo.FileName, photo.OpenReadStream()),        
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        return uploadResult.SecureUrl.ToString();
    }
}
}

