using Microsoft.Ajax.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using PhotoGram.Data;
using PhotoGram.Interface;
using PhotoGram.Models;
using System.Dynamic;
using System.Security.Principal;

namespace PhotoGram.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public readonly ApplicationDbContext _context;
        private RepositorySettings _default;
        public AccountRepository(ApplicationDbContext context)
        {
            _context= context;
            _default = new RepositorySettings(Includes.All, true);
        }

        public bool AddAccount(Account account)
        {
            _context.Accounts.Add(account);

            return Save();
        }
        public bool DeleteAccount(Account account)
        {
            //Remove Following
            foreach(Account following in account.Following)
            {
                following.Followers.Remove(account);
            }
            //Remove Followers
            foreach(Account follower in account.Followers)
            {
                follower.Following.Remove(account);
            }
            //Delete Post Comments
            IEnumerable<Comment> Comments = _context.Comments.Where(c => c.AccountId == account.Id).ToList();
            foreach (Comment comment in Comments)
            {
                _context.Comments.Remove(comment);
            }
            //Remove Posts
            if (account.Posts != null && account.Posts.Any())
            {
                foreach(Post post in account.Posts)
                {
                    //Delete Post Comments
                    IEnumerable<Comment> p_comments = _context.Comments.Where(c => c.PostId== post.Id).ToList();
                    foreach(Comment comment in p_comments)
                        _context.Comments.Remove(comment);

                    _context.Posts.Remove(post);
                    
                }
            }
            _context.Accounts.Remove(account);

            return Save();
        }

        public async Task<IEnumerable<Account>> GetAllAsync(RepositorySettings settings = null)
        {
            if (settings == null)
                settings = _default;
            if (settings.include() == Includes.None)
            {
                if (settings.isTracking())
                    return await _context.Accounts.ToListAsync();
                else
                {
                    return await _context.Accounts.AsNoTracking().ToListAsync();
                }
            }
            else
            {
                IIncludableQueryable<Account, ICollection<Account>> query = null;

                switch (settings.include())
                {
                    case Includes.All:
                        query = _context.Accounts
                            .Include(a => a.Followers)
                            .Include(a => a.Posts)
                            .Include(a => a.Following);
                        break;
                    case Includes.Follows:
                       query = _context.Accounts.Include(a => a.Following)
                            .Include(a => a.Followers);
                        break;
                }
                if(query == null)
                {
                    if(settings.include() == Includes.Posts)
                    {
                        if(settings.isTracking())
                            return await _context.Accounts.Include("Account.Posts").ToListAsync();
                        return await _context.Accounts.Include("Account.Posts").AsNoTracking().ToListAsync();
                    }
                    throw new Exception("Error Loading accounts with provided settings");
                }
                if (settings.isTracking())
                    return await query.ToListAsync();
                return await query.AsNoTracking().ToListAsync();
            }
        }

        public async Task<Account> GetByScreenNameAsync(string ScreenName, RepositorySettings settings = null)
        {
            if (settings == null)
                settings = _default;
            if (settings.include() == Includes.None)
            {
                if (settings.isTracking())
                    return await _context.Accounts.Where(a => a.ScreenName == ScreenName).FirstOrDefaultAsync();
                else
                {
                    return await _context.Accounts.AsNoTracking().Where(a => a.ScreenName == ScreenName).FirstOrDefaultAsync();
                }
            }
            else
            {
                IIncludableQueryable<Account, ICollection<Account>> query = null;

                switch (settings.include())
                {
                    case Includes.All:
                        query = _context.Accounts
                            .Include(a => a.Followers)
                            .Include(a => a.Posts)
                            .Include(a => a.Following);
                        break;
                    case Includes.Follows:
                        query = _context.Accounts.Include(a => a.Following)
                             .Include(a => a.Followers);
                        break;
                }
                if (query == null)
                {
                    if (settings.include() == Includes.Posts)
                    {
                        if (settings.isTracking())
                            return await _context.Accounts.Include("Account.Posts").Where(a => a.ScreenName == ScreenName).FirstOrDefaultAsync();
                        return await _context.Accounts.Include("Account.Posts").AsNoTracking().Where(a => a.ScreenName == ScreenName).FirstOrDefaultAsync();
                    }
                    throw new Exception("Error Loading Account: " + ScreenName + " with provided settings");
                }
                if (settings.isTracking())
                    return await query.Where(a => a.ScreenName == ScreenName).FirstOrDefaultAsync();
                return await query.AsNoTracking().Where(a => a.ScreenName == ScreenName).FirstOrDefaultAsync();
            }
            
        }
        public async Task<Account> GetByIdAsync(int id, RepositorySettings settings = null)
        {
            if (settings == null)
                settings = _default;
            if (settings.include() == Includes.None)
            {
                if (settings.isTracking())
                    return await _context.Accounts.Where(a => a.Id == id).FirstOrDefaultAsync();
                else
                {
                    return await _context.Accounts.AsNoTracking().Where(a => a.Id == id).FirstOrDefaultAsync();
                }
            }
            else
            {
                IIncludableQueryable<Account, ICollection<Account>> query = null;

                switch (settings.include())
                {
                    case Includes.All:
                        query = _context.Accounts
                            .Include(a => a.Followers)
                            .Include(a => a.Posts)
                            .Include(a => a.Following);
                        break;
                    case Includes.Follows:
                        query = _context.Accounts.Include(a => a.Following)
                             .Include(a => a.Followers);
                        break;
                }
                if (query == null)
                {
                    if (settings.include() == Includes.Posts)
                    {
                        if (settings.isTracking())
                            return await _context.Accounts.Include(a => a.Posts).Where(a => a.Id == id).FirstOrDefaultAsync();
                        return await _context.Accounts.Include(a => a.Posts).AsNoTracking().Where(a => a.Id == id).FirstOrDefaultAsync();
                    }
                    throw new Exception("Error Loading Account: " + id + " with provided settings");
                }
                if (settings.isTracking())
                    return await query.Where(a => a.Id == id).FirstOrDefaultAsync();
                return await query.AsNoTracking().Where(a => a.Id == id).FirstOrDefaultAsync();
            }
        }
        public ICollection<Account> GetFollowerListAsync(Account account)
        { 
            return account.Followers;
        }
       
        public ICollection<Account> GetFollowListAsync(Account account)
        {
            return new HashSet<Account>();
        }

        public bool RemoveFollow(Account account, Account followAccount)
        {


            //if (account.Following.Contains(followAccount)) 
            //{ 
            //    account.Following.Remove(followAccount);
            //    return Save();
            //}
            return false;

        }

        public bool RemoveFollower(Account account, Account followerAccount)
        {
          

           

            
            if(account.Followers.Contains(followerAccount))
            {
                account.Followers.Remove(followerAccount);

                return Save();
            }
            return false;
        }

        public bool AddFollower(Account account, Account followerAccount)
        {

            if(account == null)
            {
                throw new Exception("Account does not exist!");
            }
            account.Followers.Add(followerAccount);

            return UpdateAccount(account);

        }
        public bool AddFollow(Account account, Account followAccount)
        {

            return false;
            //if (account == null)
            //{
            //    throw new Exception("Account does not exist!");
            //}

           
            //account.Following.Add(followAccount);

            //return UpdateAccount(account);
        }
        public bool UpdateAccount(Account account)
        {
            _context.Accounts.Update(account);

            return Save();
        }
        public bool Save()
        {
            var save = _context.SaveChanges();

            return save > 0;
        }
    }
}
