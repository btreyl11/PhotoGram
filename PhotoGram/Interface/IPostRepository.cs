using PhotoGram.Models;

namespace PhotoGram.Interface
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();

        Task<IEnumerable<Post>> GetAccountPostsAsync(string ScreenName);

        Task<Post> GetPostsAsync(int id);

        bool AddLike(Post post, Account account);

        bool RemoveLike(Post post, Account account);

        bool Add(Post post);
        bool Update(Post post);
        bool Remove(Post post);
        bool Save();
    }
}
