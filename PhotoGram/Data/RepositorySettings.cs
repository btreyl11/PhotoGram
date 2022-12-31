namespace PhotoGram.Data
{
    public enum Includes
    {
        All,
        None,
        Posts,
        Follows,
        Comments,
        Likes
    };
    public class RepositorySettings
    {
        private  Includes  _include { get; set; }
        private bool _isTracking { get; set; }

        public RepositorySettings(Includes includes, bool tracking) 
        {
            _include = includes;
            _isTracking = tracking;
        }

        public Includes include()
        {
            return _include;
        }
        public bool isTracking()
        {
            return _isTracking;
        }
    };
}
