using PhotoGram.Models;

namespace PhotoGram.Interface
{
    public interface IAccountRepository
    {
        /* -- Account Getters -- */
        public Task<IEnumerable<Account>> GetAllAsync();
        public Task<Account> GetByScreenNameAsync(string ScreenName);
        public  Task<Account> GetByScreenName_IncludeFollowerAsync(string ScreenName);
        public Task<Account> GetByScreenName_IncludeFollowingAsync(string ScreenName);
        public Task<Account> GetByIdAsync(int id);
        public Task<Account> GetByIdAsyncNoTracking(int id);
        public Task<Account> GetByIdAsync_IncludeAll(int id);

        /* -- Followers & Follow Getters -- */
        public ICollection<Account> GetFollowerListAsync(Account account);
        public ICollection<Account> GetFollowListAsync(Account account);


        /* -- Account Modifiers -- */

        /// <summary>
        /// AddAccount adds Account to the database context
        /// </summary>
        /// <param name="account"> The account to be added</param>
        /// <returns>Returns true if successful</returns>
        public bool AddAccount(Account account);
        /// <summary>
        /// Update an Account
        /// </summary>
        /// <param name="account">The account to be added</param>
        /// <returns>Returns true if account was added</returns>
        public bool UpdateAccount(Account account);
        /// <summary>
        /// Delete an Account
        /// </summary>
        /// <param name="account">Account to be deleted</param>
        /// <returns>Returns true if successful and false if the deletion failed</returns>
        public bool DeleteAccount(Account account);


        /* -- Follower & Follow Modifiers -- */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="fid"></param>
        /// <returns></returns>
        public bool RemoveFollower(Account account, Account followerAccount);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="fid"></param>
        /// <returns></returns>
        public bool RemoveFollow(Account account, Account followAccount);

        /* -- Account Post Modifiers -- */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public bool AddFollower(Account account, Account followerAccount);
        public bool AddFollow(Account account, Account followAccount);

        public bool Save();


        
    }
}
