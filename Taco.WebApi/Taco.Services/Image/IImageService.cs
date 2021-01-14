using System.Threading.Tasks;
using Taco.Core.DTO;

namespace Taco.Services.Image
{
    public interface IImageService
    {
        Task<ImageDto> GetImage(string folder, string imageId);
    }
}
