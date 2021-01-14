using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using Taco.Services.Image;

namespace Pholk.Frontend.WebAPI.Controllers.Public
{
    [Route("image")]
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{folder}/{imageId}")]
        public async Task<IActionResult> ImageResult([FromRoute] string folder, [FromRoute] string imageId)
        {
            try
            {
                var result = await _imageService.GetImage(folder, imageId);
                return File(result.File, result.ContentType);
            }
            catch (AmazonS3Exception e)
            {
                return NotFound($"Error encountered ***. Message:'{e.Message}' when reading object");
            }
        }
    }
}