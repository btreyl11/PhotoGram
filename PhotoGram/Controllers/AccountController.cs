using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using PhotoGram.Interface;
using PhotoGram.Models;
using PhotoGram.ModelView;

namespace PhotoGram.Controllers
{
    public class AccountController : Controller
    {
        public readonly IAccountRepository _accountRepo;
        public readonly IPhotoPostService _photoPostService;
        public readonly IPostRepository _postRepository;

        public AccountController(IAccountRepository accountRepo, IPhotoPostService photoPostService, IPostRepository postRepository)
        {
            _accountRepo = accountRepo;
            _photoPostService = photoPostService;
            _postRepository = postRepository;
        }
        public async Task<IActionResult> Index()
        {
            var accounts = await _accountRepo.GetAllAsync();
            return View(accounts);
        }
        public IActionResult Add()
        {
            CreateAccountModelView AccountMV = new CreateAccountModelView();

            return View(AccountMV);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateAccountModelView accountMV)
        {
            if (!ModelState.IsValid)
            {
                return View(accountMV);
            }
            ImageUploadResult ProfileImage = await _photoPostService.PostPhotoAsync(accountMV.ProfileImage);

            if(ProfileImage == null)
            {
                throw new Exception("Failed to upload image!");
            }

            Account acc = new Account()
            {
                ScreenName = accountMV.ScreenName,
                ProfilImgURL = ProfileImage.Url.ToString(),
                About = accountMV.About
            };

            if (!_accountRepo.AddAccount(acc))
                throw new Exception("Error adding acount!");

            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var account = await _accountRepo.GetByIdAsync(id);

            if (account == null)
            {
                return NotFound();
            }
            
            _accountRepo.DeleteAccount(account);
            return RedirectToAction("Index");
        }

    }

}
