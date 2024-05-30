﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TourPlanner.BL.Services
{
    public interface IMapService
    {
        Task<BitmapSource?> GenerateMapAsync(string zoomStr, string start, string end);
    }
}
