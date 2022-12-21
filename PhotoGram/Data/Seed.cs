using PhotoGram.Models;
namespace PhotoGram.Data {
    public class Seed
    {
        public static Account acc1 { get; set; }
        public static Account acc2 { get; set; }
        public static Account acc3 { get; set; }
        public static void SeedData(IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();
            
                if (!context.Accounts.Any())
                {
                    BuildAccounts();

                    foreach(Post p in acc1.Posts)
                    {
                        context.Posts.Add(p);
                    }
                    foreach (Post p in acc2.Posts)
                    {
                        context.Posts.Add(p);
                    }
                    foreach (Post p in acc3.Posts)
                    {
                        context.Posts.Add(p);
                    }

                    context.Accounts.AddRange(acc1,acc2,acc3);
                    
                    context.SaveChanges();
                }
            }
        }
        public static void BuildAccounts()
        {
            acc1 = new Account("Account1", "https://www.pinkvilla.com/files/styles/amp_metadata_content_image/public/vicky_kaushal_shares_cool_pic_amid_wedding.jpg");
            acc1.About = "About Account 1";

            acc1 = AddPosts(acc1);
            

            acc2 = new Account("Account2", "https://www.boredpanda.com/blog/wp-content/uploads/2017/11/My-most-popular-pic-since-I-started-dog-photography-5a0b38cbd5e1e__880.jpg");
            acc2.About = "About Account 2";

            acc2 = AddPosts(acc2);

            acc3 = new Account("Account3", "https://images.unsplash.com/photo-1527960669566-f882ba85a4c6?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MXx8YXdlc29tZSUyMHBpY3xlbnwwfHwwfHw%3D&w=1000&q=80");
            acc3.About = "About Account 3";
            acc3= AddPosts(acc3);
        }
        public static Account AddPosts(Account acc)
        {
            Account account = acc;
            if(acc.Posts == null)
                acc.Posts = new HashSet<Post>();
            for (int i = 0; i < 5; i++)
            {
                acc.Posts.Add(new Post()
                {
                    AccountId = acc.Id,
                    account = acc,
                    Caption = "Post" + i + "For" + acc.ScreenName,
                    ImgUrl = "https://media.istockphoto.com/id/1297349747/photo/hot-air-balloons-flying-over-the-botan-canyon-in-turkey.jpg?b=1&s=170667a&w=0&k=20&c=1oQ2rt0FfJFhOcOgJ8hoaXA5gY4225BA4RdOP1RRBz4="
                }) ;
            }
            return account;
        }
    }
}