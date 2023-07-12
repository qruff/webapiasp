using Microsoft.AspNetCore.Mvc;
using webapiasp.Models;
namespace webapiasp.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : Controller
    {
        private readonly TravelContext _context;
        
        public CategoryController(TravelContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Получение всех категорий туров
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            Console.WriteLine("get all categories");
            List<Category> categories = _context.Categories.ToList();
            Console.WriteLine("response sent");
            return Ok(categories);
        }

        /// <summary>
        /// Добавление новой категории
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public ActionResult Add([FromBody] Category category)
        {
            Console.WriteLine("add category");
            _context.Categories.Add(category);
            _context.SaveChanges();

            if (_context.SaveChanges()>0)
            {
                Console.WriteLine("response sent");
                return Ok(category);
            }
            else
            {
                Console.WriteLine("response sent");
                return StatusCode(500, "Failed to add category");
            }
        }
    }
}
