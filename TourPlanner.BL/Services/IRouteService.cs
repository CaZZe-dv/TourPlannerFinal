using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BL.Services
{
    public interface IRouteService
    {
        Task<RouteResponse?> GetRouteResponse(string transportType, string start, string end);
    }
}
