using Microsoft.AspNetCore.Components.Web;
using PhotoGram.Models;

namespace PhotoGram.Interface
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment> GetByIdAsync(int id);

        Task<IEnumerable<Comment>> GetAllPostCommentsAsync(int postId);

        bool AddLike(Comment comment, Account account);
        bool RemoveLike(Comment comment, Account account);

        bool Add(Comment comment);
        bool Update(Comment comment);
        bool Delete(Comment comment);
        bool Save();
    }
}
