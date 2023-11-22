using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketMonster.Admin.Interface;

namespace TicketMonster.Web.WebApi
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoUploader _photoUploader;

        public PhotoController(IPhotoUploader photoUploader)
        {
            _photoUploader = photoUploader;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var imageUrl = await _photoUploader.UploadPhoto(file);

            if (imageUrl != null)
            {
                return Ok(new { imageUrl });
            }
            else
            {
                return BadRequest("圖片上傳失敗");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadMultiple(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("请至少选择一张图片！");
            }

            var imageUrls = new List<string>();

            foreach (var file in files)
            {
                var imageUrl = await _photoUploader.UploadPhoto(file);
                if (imageUrl != null)
                {
                    imageUrls.Add(imageUrl);
                }
            }

            if (imageUrls.Count > 0)
            {
                return Ok(new { imageUrls });
            }
            else
            {
                return BadRequest("图片上传失败");
            }
        }

    }
}
