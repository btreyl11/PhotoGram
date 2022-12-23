using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using PhotoGram.Interface;
using PhotoGram.Models;

namespace PhotoGram.Controllers
{
    public class PostController : Controller
    {
        public readonly IPostRepository _postRepository;
        public readonly ICommentRepository _commentRepository;
        public readonly IAccountRepository _accountRepository;
        public readonly IPhotoPostService _photoPostService;
        public PostController(IPostRepository postRepository
            , ICommentRepository commentRepository
            , IAccountRepository accountRepository
            ,IPhotoPostService photoPostService)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _accountRepository = accountRepository;
            _photoPostService = photoPostService;
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
        [HttpPost]
        public async Task<IActionResult> Delete(int id, Post post)
        {
            int accId = post.AccountId;
            Account account = await _accountRepository.GetByIdAsync_IncludeAll(accId);
            Post fullPost = await _postRepository.GetByIdAsync(post.Id);
            if (account == null || post == null)
            {
                return View("Error");
            }
            var commentsList = await _commentRepository.GetComentsByPostIdAsync(post.Id);
            if (commentsList != null && commentsList.Any())
            {
                foreach (Comment comment in commentsList)
                {
                    if(!_commentRepository.Delete(comment))
                    {
                        throw new Exception("Failed to Delete Comment: " + comment.Id);
                    }
                }
            }
            var DelResult = await _photoPostService.DeletePhotoAsync(post.ImgUrl);
            if(DelResult == null) {
                DelResult = null;
            }
            account.Posts.Remove(fullPost);
            if(_postRepository.Remove(fullPost))
            {
                return RedirectToAction("Details", "Profile", new {id = accId});
            }
            
            return NotFound();

            
        }
    }
}
