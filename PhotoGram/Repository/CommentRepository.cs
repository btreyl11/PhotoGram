using Microsoft.EntityFrameworkCore;
using PhotoGram.Data;
using PhotoGram.Interface;
using PhotoGram.Models;
using System.Security.Cryptography.Xml;

namespace PhotoGram.Repository
{
    public class CommentRepository : ICommentRepository
    {
        public readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Comment comment)
        {
           _context.Comments.Add(comment);
            return Save();
        }

        public bool AddLike(Comment comment, Account account)
        {
            if(!(comment.Likes == null) && !comment.Likes.Contains(account))
            {
                comment.Likes.Add(account);
                return Save();
            }
            return false;
        }

        public bool Delete(Comment comment)
        {
            _context.Comments.Remove(comment);
            return Save();
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllPostCommentsAsync(int postId)
        {
            return await _context.Comments.Where(c=> c.PostId== postId).ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public bool RemoveLike(Comment comment, Account account)
        {
            if (comment.Likes == null)
                throw new Exception("Likes equal to Null");
            if (comment.Likes.Contains(account))
            {
                comment.Likes.Remove(account);
                return Update(comment);
            }
            return false;
        }

        public bool Save()
        {
            var save = _context.SaveChanges();

            return save > 0;
        }

        public bool Update(Comment comment)
        {
            _context.Comments.Update(comment);
            return Save();
        }
    }
}
