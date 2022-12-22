namespace PhotoGram.ModelView
{
    public class CreateAccountModelView
    {
        public string ScreenName { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string About { get; set; }
    }
}
