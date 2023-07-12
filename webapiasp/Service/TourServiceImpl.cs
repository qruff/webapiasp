using System;
using webapiasp.Models;
using webapiasp.Utility;
namespace webapiasp.Service
{
    public class TourServiceImpl : TourService
    {
        private readonly TravelContext _context;
        private readonly StorageService _storageService;
        public TourServiceImpl(TravelContext context, StorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        /// <summary>
        /// Добавление тура
        /// </summary>
        /// <param name="tour"></param>
        /// <param name="tourImage"></param>
        public void AddTour(Tour tour, IFormFile tourImage)
        {
            string tourImageName = _storageService.Store(tourImage);
            tour.ImageName = tourImageName;

        }
    }
}
