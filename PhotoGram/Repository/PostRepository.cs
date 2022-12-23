using Microsoft.EntityFrameworkCore;
using PhotoGram.Data;
using PhotoGram.Interface;
using PhotoGram.Models;

namespace PhotoGram.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Post post)
        {
            _context.Posts.Add(post);

            return Save();
        }

        public bool AddLike(Post post, Account account)
        {
            
            post.Likes.Add(account);
            return Update(post);
        }

        public async Task<IEnumerable<Post>> GetAccountPostsAsync(string ScreenName)
        {
            var posts = await _context.Posts.Include(p => p.account).Where(p => p.account.ScreenName == ScreenName).ToListAsync();
            return posts;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts.Include(p => p.account).Include(p=>p.Likes).ToListAsync();
        }
        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.account)
                .Include(p => p.Likes)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<Post> GetPostsAsync(int id)
        {
            return await _context.Posts.Where( p=> p.Id == id).FirstOrDefaultAsync();
        }

        public bool Remove(Post post)
        {
            _context.Posts.Remove(post);
            return Save();
        }

        public bool RemoveLike(Post post, Account account)
        {
            post.Likes.Remove(account);
            return Update(post);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();

            return save > 0;
        }

        public bool Update(Post post)
        {
           _context.Update(post);
            return Save();
        }
    }
}
