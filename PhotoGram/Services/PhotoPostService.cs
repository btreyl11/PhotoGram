using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using PhotoGram.Helper;
using PhotoGram.Interface;

namespace PhotoGram.Services
{
    public class PhotoPostService : IPhotoPostService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoPostService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }
        public async Task<DeletionResult> DeletePhotoAsync(string publicUrl)
        {
            var publicId = publicUrl.Split('/').Last().Split('.')[0];
            var deleteParams = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deleteParams);
        }

        public async Task<ImageUploadResult> PostPhotoAsync(IFormFile file)
        {
            var result = new ImageUploadResult();
            if(file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation()
                   .Height(500)
                   .Width(500)
                   .Crop("fill")
                   .Gravity("face")
                };

                result = await _cloudinary.UploadAsync(uploadParams);
            }
            return result;
        }
    }
}
