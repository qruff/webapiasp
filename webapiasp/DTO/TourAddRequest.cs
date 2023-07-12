using webapiasp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.FileProviders;
namespace webapiasp.DTO
{
    public class TourAddRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        
        public static Tour ToEntity(TourAddRequest dto)
        {
            var entity = new Tour
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                Quantity = dto.Quantity,
                Price = dto.Price,
                CategoryId = dto.CategoryId
            };
            return entity;
        }

        public override string ToString()
        {
            return $"TourAddRequest [id={Id}, title={Title}, description={Description}, quantity={Quantity}, price={Price}, categoryId={CategoryId}]";
        }
    }
}
