using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PhotoGram.Data;
using PhotoGram.Interface;
using PhotoGram.Models;
using System.Security.Principal;

namespace PhotoGram.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public readonly ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context)
        {
            _context= context;
        }

        public bool AddAccount(Account account)
        {
            _context.Accounts.Add(account);

            return Save();
        }
        public bool DeleteAccount(Account account)
        {
            _context.Accounts.Remove(account);

            return Save();
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts.Include(a => a.Following).Include(a => a.Followers).Include(a => a.Posts).ToListAsync();
        }

        public async Task<Account> GetByScreenNameAsync(string ScreenName)
        {
            return await _context.Accounts.Where(a => a.ScreenName == ScreenName).FirstOrDefaultAsync();
        }
        public async Task<Account> GetByScreenName_IncludeFollowerAsync(string ScreenName)
        {
            return await _context.Accounts.Where(a => a.ScreenName == ScreenName).Include(a => a.Followers).FirstOrDefaultAsync();
        }
        public async Task<Account> GetByScreenName_IncludeFollowingAsync(string ScreenName)
        {
            throw new NotImplementedException();
        }
        public async Task<Account> GetByIdAsync_IncludeAll(int id)
        {
            return await _context.Accounts
                .Where(a => a.Id == id)
                .Include(a => a.Posts)
                .Include(a => a.Following)
                .Include(a => a.Followers)
                .FirstOrDefaultAsync();

        }
        public async Task<Account> GetByIdNTAsync_IncludeAll(int id)
        {
            return await _context.Accounts
                .Where(a => a.Id == id)
                .Include(a => a.Posts)
                .Include(a => a.Following)
                .Include(a => a.Followers)
                .AsNoTracking()
                .FirstOrDefaultAsync();

        }
        public async Task<Account> GetByIdAsync(int id)
        {
            return await _context.Accounts
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<Account> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Accounts
                .Where(a => a.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
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
