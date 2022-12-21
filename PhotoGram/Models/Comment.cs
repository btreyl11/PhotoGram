using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoGram.Models
{
    public class Comment
    {
        public Comment() 
        {
            Likes = new HashSet<Account>();
        }
        public int Id { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account account { get; set; }
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public string Text { get; set; } 
        public ICollection<Account> Likes { get; set; }
    }
}
