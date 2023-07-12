using webapiasp.Models;
namespace webapiasp.Service
{
    public interface TourService
    {
        void AddTour(Tour tour, IFormFile tourImage);
    }
}
