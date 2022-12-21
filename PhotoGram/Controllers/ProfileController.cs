using Microsoft.AspNetCore.Mvc;
using PhotoGram.Interface;
using PhotoGram.Models;
using PhotoGram.ModelView;

namespace PhotoGram.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public ProfileController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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

        
    }
}
