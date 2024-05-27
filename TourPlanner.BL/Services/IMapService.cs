using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BL.Services
{
    public interface IMapService
    {
        Task<string?> getResponse(float zoom, float x, float y);
    }
}
