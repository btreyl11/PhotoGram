using CloudinaryDotNet.Core;
using CloudinaryDotNet.Actions;

namespace PhotoGram.Interface
{
    public interface IPhotoPostService
    {
        public Task<ImageUploadResult> PostPhotoAsync(IFormFile file);

        public Task<DeletionResult> DeletePhotoAsync(string photoId);
    }
}
