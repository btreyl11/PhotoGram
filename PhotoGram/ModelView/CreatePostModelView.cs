using PhotoGram.Models;

namespace PhotoGram.ModelView
{
    public class CreatePostModelView
    {
        public string Caption { get; set; }
        public IFormFile Image { get; set; }
    }
}
