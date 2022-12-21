using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace PhotoGram.Models
{
    public class Account
    {
        public Account()
        {
            this.LikedPosts = new HashSet<Post>();
            this.LikedComments= new HashSet<Comment>();
            this.Followers = new HashSet<Account>();
            this.Following = new HashSet<Account>();


            this.About = "";
        }
        public Account(string sname, string img)
        {
            this.LikedPosts = new HashSet<Post>();
            this.LikedComments = new HashSet<Comment>();
            this.Followers = new HashSet<Account>();
            this.Following = new HashSet<Account>();


            this.About = "";
            this.ScreenName = sname;
            this.ProfilImgURL= img;
        }
        public int Id { get; set; }
       
        public string ScreenName { get; set; }
        public string ProfilImgURL { get; set; }
        public string About { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Account> Followers { get; set; }
        public virtual ICollection<Account> Following { get; set; }

        public ICollection<Post> LikedPosts { get; set; }
        public ICollection<Comment> LikedComments { get; set; }
    }
}
