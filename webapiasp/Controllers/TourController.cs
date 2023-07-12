using Microsoft.AspNetCore.Mvc;
using webapiasp.DTO;
using webapiasp.Models;
using webapiasp.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace webapiasp.Controllers
{
    [ApiController]
    [Route("api/tour")]
    public class TourController : ControllerBase
    {
        private readonly TourService tourService;
        private readonly TravelContext context;

        public TourController(TourService tourService, TravelContext context)
        {
            this.tourService = tourService;
            this.context = context;
        }

        /// <summary>
        /// Получение всех туров
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public IActionResult GetAllTours()
        {
            Console.WriteLine("getting all tours");
            List<Tour> tours = context.Tours.ToList();
            Console.WriteLine("response sent");
            return Ok(tours);
        }

        /// <summary>
        /// Получение тура по id
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public IActionResult GetTourById([FromQuery] int tourId)
        {
            Console.WriteLine("get tour by id");
            Tour tour = context.Tours.FirstOrDefault(c => c.Id == tourId);
            if (tour != null)
            {
                Console.WriteLine("response sent");
                return Ok(tour);
            }
            else
            {
                Console.WriteLine("response sent");
                return NotFound("tour not found");
            }
        }

        /// <summary>
        /// Получение туров по категории
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("category")]
        public IActionResult GetToursByCategories([FromQuery] int categoryId)
        {
            Console.WriteLine("get all tours by category");
            List<Tour> tours = context.Tours.Where(t => t.CategoryId == categoryId).ToList();
            Console.WriteLine("response sent");
            return Ok(tours);
        }

        /// <summary>
        /// Получение изображений туров
        /// </summary>
        /// <param name="tourImageName"></param>
        /// <returns></returns>
        [HttpGet("{tourImageName}")]
        public IActionResult FetchTourImage(string tourImageName)
        {
            Console.WriteLine("for fetching tour pic");
            Console.WriteLine("Loading file: " + tourImageName);
            string filePath = Path.Combine("I:/imagesfortravel", tourImageName);
            if (System.IO.File.Exists(filePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "image/*");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
