using Microsoft.AspNetCore.Mvc;
using Web.Api;
using WEB.Models;

namespace WEB.Controllers
{
    public class TrailerController : Controller
    {

        private readonly CommentService _commentService;
        public TrailerController(CommentService comment)
        {
            this._commentService = comment;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string MaPhim, string TenPhim, string Video)
        {

            if (string.IsNullOrEmpty(MaPhim))
            {
                return NotFound("Mã phim không được để trống.");
            }
            var movie = new MovieModel
            {
                MaPhim = MaPhim,
                TenPhim = TenPhim,
                Video = Video
            };


            var listComment = await _commentService.GetAllComment(MaPhim) ?? new List<CommentModel>();

            var model = new TrailerViewModel
            {
                movie = movie,
                Comments = listComment
            };

            return View(model);
        }



        // Thêm bình luận
        [HttpPost]
        public async Task<IActionResult> AddComment(CommentRequest comment)
        {
            var maKH = HttpContext.Session.GetString("UserId");

            var newComment = new CommentModel
            {
                maKH = maKH,
                NoiDung = comment.noiDung,
                Gio = DateTime.Now,
                MaPhim = comment.MaPhim
            };

            var result = await _commentService.PostComment(newComment);

            if (result != null)
            {
                return RedirectToAction("Index", new { MaPhim = comment.MaPhim, TenPhim = comment.TenPhim, Video = comment.Video });
            }
            else
            {
                ModelState.AddModelError("", "Không thể thêm bình luận.");
                return View("Error");
            }
        }



    }

    public class CommentRequest
    {
        public string MaPhim { get; set; }
        public string TenPhim { get; set; }
        public string Video { get; set; }
        public string noiDung { get; set; }
    }
}
