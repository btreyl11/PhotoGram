using Microsoft.AspNetCore.Mvc;
using PhotoGram.Interface;
using PhotoGram.Models;

namespace PhotoGram.Controllers
{
    public class PostController : Controller
    {
        public readonly IPostRepository _postRepository;
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<IActionResult> Detail(int id)
        {
            Post post =  await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
    }
}
