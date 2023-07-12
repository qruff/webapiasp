using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapiasp.Models;
using webapiasp.DTO;
using Microsoft.EntityFrameworkCore;

namespace webapiasp.Controllers
{
    [ApiController]
    [Route("api/exploiter/")]
    public class CartController : ControllerBase
    {
        private readonly TravelContext _context;

        public CartController(TravelContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавление в корзину
        /// </summary>
        /// <param name="addToCartRequest"></param>
        /// <returns></returns>
        [HttpPost("cart/add")]
        public IActionResult Add([FromBody] AddToCartRequest addToCartRequest)
        {
            Console.WriteLine("add tour to cart");
            Console.WriteLine(addToCartRequest);
            Exploiter exploiter = _context.Exploiters.FirstOrDefault(e => e.Id == addToCartRequest.ExploiterId);
            Tour tour = _context.Tours.FirstOrDefault(t=>t.Id == addToCartRequest.TourId);

            if(exploiter == null || tour == null)
            {
                return BadRequest("Invalid exploiter Id or tour Id");
            }

            Cart cart = new Cart
            {
                Tour = tour,
                Quantity = addToCartRequest.Quantity,
                Exploiter = exploiter
            };
            _context.Carts.Add(cart);
            _context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Получение туров своей корзины
        /// </summary>
        /// <param name="exploiterId"></param>
        /// <returns></returns>
        [HttpGet("mycart")]
        public IActionResult GetMyCart([FromQuery] int exploiterId)
        {
            Console.WriteLine("My cart for exploiter id: " + exploiterId);

            List<CartDataResponse> cartDatas = new List<CartDataResponse>();
            List<Cart> exploiterCarts = _context.Carts
                .Include(c => c.Tour)
                .Where(c => c.Exploiter.Id == exploiterId)
                .ToList();

            double totalCartPrice = 0;

            foreach (Cart cart in exploiterCarts)
            {
                CartDataResponse cartData = new CartDataResponse
                {
                    CartId = cart.Id,
                    TourDescription = cart.Tour.Description,
                    TourName = cart.Tour.Title,
                    TourImage = cart.Tour.ImageName,
                    Quantity = (int)cart.Quantity,
                    TourId = cart.Tour.Id
                };

                cartDatas.Add(cartData);    
                totalCartPrice += (int)cart.Quantity * double.Parse(cart.Tour.Price.ToString());
            }

            CartResponse cartResponse = new CartResponse
            {
                TotalCartPrice = totalCartPrice.ToString(),
                CartData = cartDatas
            };

            return Ok(cartResponse);
        }

        /// <summary>
        /// Удаление тура из корзины
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        [HttpGet("mycart/remove")]
        public IActionResult RemoveCartItem([FromQuery] int cartId)
        {
            Console.WriteLine("DELETE CART ITEM WHOSE ID IS: " + cartId);

            Cart cart = _context.Carts.FirstOrDefault(c => c.Id == cartId);

            if (cart == null)
            {
                return BadRequest("Invalid cart ID");
            }

            _context.Carts.Remove(cart);
            _context.SaveChanges();

            return Ok("SUCCESS");
        }
    }
}
