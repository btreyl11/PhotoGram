using Microsoft.AspNetCore.Mvc;
using PhotoGram.Interface;
using PhotoGram.Models;
using PhotoGram.ModelView;

namespace PhotoGram.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPhotoPostService _photoPostService;

        public ProfileController(IAccountRepository accountRepository, IPhotoPostService photoPostService)
        {
            _accountRepository = accountRepository;
            _photoPostService = photoPostService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            
            var model = await _accountRepository.GetByIdAsync_IncludeAll(id);

            if (model == null)
            {
                return View("Error");
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var profile = await _accountRepository.GetByIdAsync(id);

            EditProfileModelView model = new EditProfileModelView()
            {
                Id = profile.Id,
                ScreenName= profile.ScreenName,
                About= profile.About,
                ProfileImgUrl = profile.ProfilImgURL
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProfileModelView profMV)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", profMV);
            }
            var usrProfile = await _accountRepository.GetByIdAsyncNoTracking(id);
            if(usrProfile== null)
            {
                return View("Error");
            }
            var profile = new Account()
            {
                Id = id,
                About = profMV.About,
                ScreenName = profMV.ScreenName,
                ProfilImgURL = profMV.ProfileImgUrl,
            };
            _accountRepository.UpdateAccount(profile);

            return RedirectToAction("Details");
        }

        public  async Task<IActionResult> CreatePost(int id)
        {
            
            Account acc = await _accountRepository.GetByIdAsync_IncludeAll(id);
            if (acc == null)
                return View("Error");
            var postMV = new CreatePostModelView();

            return View(postMV);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost(int id, CreatePostModelView postMv)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            var acc = await _accountRepository.GetByIdNTAsync_IncludeAll(id);

            if(acc== null)
            {
                return View("Error");
            }
            var result = await _photoPostService.PostPhotoAsync(postMv.Image);
            if (result == null)
                throw new Exception("Error uploading image!");
            Post post = new Post()
            {
                AccountId = id,
                Caption = postMv.Caption,
                ImgUrl = result.Url.ToString()

            };

            acc.Posts.Add(post);

            _accountRepository.UpdateAccount(acc);
            
            return RedirectToAction("Details", new {id = id});

        }
        
    }
}
