using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoGram.Models
{
    public class Post
    {
        public Post()
        {
            Likes = new HashSet<Account>();
        }
        public int Id { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account account { get; set; }
        
        public string ImgUrl { get; set; }
        public string Caption { get; set; }

        public ICollection<Account> Likes { get; set; }
    }
}
