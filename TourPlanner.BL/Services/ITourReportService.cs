using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BL.Services
{
    public interface ITourReportService
    {
        Task<string> getResponse(string start, string end);
    }
}
