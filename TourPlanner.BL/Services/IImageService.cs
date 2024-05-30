using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TourPlanner.BL.Services
{
    public interface IImageService
    {
        Task<string> SaveImage(BitmapSource image, string tourId);
        Task<BitmapSource> GetImage(string tourId);
        Task RemoveImage(string tourId);
    }
}
