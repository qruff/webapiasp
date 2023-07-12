using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using webapiasp.DTO;
using webapiasp.Models;
using webapiasp.Utility;

namespace webapiasp.Controllers
{
    [ApiController]
    [Route("api/exploiter")]
    public class OrderController : ControllerBase
    {
        private readonly TravelContext context;
        public OrderController(TravelContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <param name="exploiterId"></param>
        /// <returns></returns>
        [HttpPost("order")]
        public IActionResult CustomerOrder([FromQuery(Name = "exploiterId")] int exploiterId)
        {
            Console.WriteLine("ORDER FOR CUSTOMER ID: " + exploiterId);
            string orderId = Helper.GetAlphaNumericOrderId();
            List<Cart> exploiterCarts = context.Carts.Where(c => c.ExploiterId == exploiterId).Include(o => o.Tour).Include(o => o.Exploiter).ToList();
            DateTime currentDateTime = DateTime.Now;
            string formatDateTime = currentDateTime.ToString("dd-MM-yyyy HH:mm");

            foreach (Cart cart in exploiterCarts)
            {
                Order order = new Order();
                order.OrderId = orderId;
                order.Exploiter = cart.Exploiter;
                order.Tour = cart.Tour;
                order.Quantity = cart.Quantity;
                order.OrderDate = formatDateTime;
                order.GuideDate = Constants.GuideStatus.PENDING.ToString();
                order.GuideStatus = Constants.GuideStatus.PENDING.ToString();
                order.GuideTime = Constants.GuideTime.DEFAULT.ToString();
                order.GuideAssigned = Constants.IsGuideAssigned.NO.ToString();
                context.Orders.Add(order);
                context.Carts.Remove(cart);
            }
            context.SaveChanges();
            Console.WriteLine("response sent");
            return Ok("ORDER SUCCESS");
        }

        /// <summary>
        /// Получение данных своего заказа
        /// </summary>
        /// <param name="exploiterId"></param>
        /// <returns></returns>
        [HttpGet("myorder")]
        public IActionResult GetMyOrder([FromQuery(Name = "exploiterId")] int exploiterId)
        {
            Console.WriteLine("MY ORDER FOR EXPLOITER ID: " + exploiterId);
            List<Order> exploiterOrder = context.Orders.Where(o => o.ExploiterId == exploiterId).Include(o => o.Tour).ToList();
            OrderDataResponse orderResponse = new OrderDataResponse();
            List<MyOrderResponse> orderDatas = new List<MyOrderResponse>();
            foreach (Order order in exploiterOrder)
            {
                MyOrderResponse orderData = new MyOrderResponse();
                orderData.OrderId = order.OrderId;
                orderData.TourDescription = order.Tour.Description;
                orderData.TourName = order.Tour.Title;
                orderData.TourImage = order.Tour.ImageName;
                orderData.Quantity = (int)order.Quantity;
                orderData.OrderDate = order.OrderDate;
                orderData.TourId = order.Tour.Id;
                orderData.GuideDate = order.GuideDate + " " + order.GuideTime;
                orderData.GuideStatus = order.GuideStatus;
                orderData.TotalPrice = (order.Quantity * double.Parse(order.Tour.Price.ToString())).ToString();
                if (order.GuidePersonId == 0)
                {
                    orderData.GuidePersonContact = Constants.GuideStatus.PENDING.ToString();
                    orderData.GuidePersonName = Constants.GuideStatus.PENDING.ToString();
                }
                else
                {
                    Exploiter guidePerson = context.Exploiters.FirstOrDefault(x => x.Id == order.GuidePersonId);
                    if (guidePerson != null)
                    {
                        orderData.GuidePersonContact = guidePerson.Phone;
                        orderData.GuidePersonName = guidePerson.Firstname;
                    }
                }
                orderDatas.Add(orderData);
            }
            string json = JsonConvert.SerializeObject(orderDatas);
            Console.WriteLine(json);
            return Ok(orderDatas);
        }

        /// <summary>
        /// Получение всех заказаов
        /// </summary>
        /// <returns></returns>
        [HttpGet("admin/allorder")]
        public IActionResult GetAllOrder()
        {
            Console.WriteLine("FETCH ALL ORDERS");
            List<Order> exploiterOrder = context.Orders.Include(o => o.Tour).Include(o => o.Exploiter).ToList();
            OrderDataResponse orderResponse = new OrderDataResponse();
            List<MyOrderResponse> orderDatas = new List<MyOrderResponse>();
            foreach (Order order in exploiterOrder)
            {
                MyOrderResponse orderData = new MyOrderResponse();
                orderData.OrderId = order.OrderId;
                orderData.TourDescription = order.Tour.Description;
                orderData.TourName = order.Tour.Title;
                orderData.TourImage = order.Tour.ImageName;
                orderData.Quantity = (int)order.Quantity;
                orderData.OrderDate = order.OrderDate;
                orderData.TourId = order.Tour.Id;
                orderData.GuideDate = order.GuideDate + " " + order.GuideTime;
                orderData.GuideStatus = order.GuideStatus;
                orderData.TotalPrice = (order.Quantity * double.Parse(order.Tour.Price.ToString())).ToString();
                orderData.ExploiterId = order.Exploiter.Id;
                orderData.UserName = order.Exploiter.Firstname + " " + order.Exploiter.Lastname;
                orderData.ExploiterPhone = order.Exploiter.Phone;

                if (order.GuidePersonId == 0)
                {
                    orderData.GuidePersonContact = Constants.GuideStatus.PENDING.ToString();
                    orderData.GuidePersonName = Constants.GuideStatus.PENDING.ToString();
                }
                else
                {
                    Exploiter guidePerson = context.Exploiters.FirstOrDefault(e => e.Id == order.GuidePersonId);
                    if (guidePerson != null)
                    {
                        orderData.GuidePersonContact = guidePerson.Phone;
                        orderData.GuidePersonName = guidePerson.Firstname;
                    }
                }
                orderDatas.Add(orderData);
            }
            string json = JsonConvert.SerializeObject(orderDatas);
            Console.WriteLine(json);
            Console.WriteLine("response sent");
            return Ok(orderDatas);
        }

        /// <summary>
        /// Показ определенного заказа
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("admin/showorder")]
        public IActionResult GetOrdersByOrderId([FromQuery(Name = "orderId")] string orderId)
        {
            Console.WriteLine("FETCH ALL ORDERS");
            List<Order> exploiterOrder = context.Orders.Where(o => o.OrderId == orderId).Include(o => o.Tour).Include(o => o.Exploiter).ToList();
            List<MyOrderResponse> orderDatas = new List<MyOrderResponse>();

            foreach (Order order in exploiterOrder)
            {
                MyOrderResponse orderData = new MyOrderResponse();
                orderData.OrderId = order.OrderId;
                orderData.TourDescription = order.Tour.Description;
                orderData.TourName = order.Tour.Title;
                orderData.TourImage = order.Tour.ImageName;
                orderData.Quantity = (int)order.Quantity;
                orderData.OrderDate = order.OrderDate;
                orderData.TourId = order.Tour.Id;
                orderData.GuideDate = order.GuideDate + " " + order.GuideTime;
                orderData.GuideStatus = order.GuideStatus;
                orderData.TotalPrice = (order.Quantity * double.Parse(order.Tour.Price.ToString())).ToString();
                orderData.ExploiterId = order.Exploiter.Id;
                orderData.UserName = order.Exploiter.Firstname + " " + order.Exploiter.Lastname;
                orderData.ExploiterPhone = order.Exploiter.Phone;

                if (order.GuidePersonId == 0)
                {
                    orderData.GuidePersonContact = Constants.GuideStatus.PENDING.ToString();
                    orderData.GuidePersonName = Constants.GuideStatus.PENDING.ToString();
                }
                else
                {
                    Exploiter guidePerson = context.Exploiters.FirstOrDefault(e => e.Id == order.GuidePersonId);
                    if (guidePerson != null)
                    {
                        orderData.GuidePersonContact = guidePerson.Phone;
                        orderData.GuidePersonName = guidePerson.Firstname;
                    }
                }

                orderDatas.Add(orderData);
            }
            string json = JsonConvert.SerializeObject(orderDatas);
            Console.WriteLine(json);
            Console.WriteLine("response sent");
            return Ok(orderDatas);
        }

        /// <summary>
        /// Обновление данных заказа
        /// </summary>
        /// <param name="guideRequest"></param>
        /// <returns></returns>
        [HttpPost("admin/order/guideStatus/update")]
        public IActionResult UpdateOrderGuideStatus([FromBody] UpdateGuideStatusRequest guideRequest)
        {
            Console.WriteLine("UPDATE GUIDE STATUS");
            Console.WriteLine(guideRequest);
            List<Order> orders = context.Orders.Where(o => o.OrderId == guideRequest.OrderId).ToList();
            
            foreach (Order order in orders)
            {
                order.GuideDate = guideRequest.GuideDate;
                order.GuideStatus = guideRequest.GuideStatus;
                order.GuideTime = guideRequest.GuideTime;
                context.Orders.Update(order);
            }
            context.SaveChanges();
            List<Order> exploiterOrder = context.Orders.Where(o => o.OrderId == guideRequest.OrderId).Include(o => o.Tour).Include(o => o.Exploiter).ToList();
            List<MyOrderResponse> orderDatas = new List<MyOrderResponse>();
            foreach (Order order in exploiterOrder)
            {
                MyOrderResponse orderData = new MyOrderResponse();
                orderData.OrderId = order.OrderId;
                orderData.TourDescription = order.Tour.Description;
                orderData.TourName = order.Tour.Title;
                orderData.TourImage = order.Tour.ImageName;
                orderData.Quantity = (int)order.Quantity;
                orderData.OrderDate = order.OrderDate;
                orderData.TourId = order.Tour.Id;
                orderData.GuideDate = order.GuideDate + " " + order.GuideTime;
                orderData.GuideStatus = order.GuideStatus;
                orderData.TotalPrice = (order.Quantity * double.Parse(order.Tour.Price.ToString())).ToString();
                orderData.ExploiterId = order.Exploiter.Id;
                orderData.UserName = order.Exploiter.Firstname + " " + order.Exploiter.Lastname;
                orderData.ExploiterPhone = order.Exploiter.Phone;

                if (order.GuidePersonId == 0)
                {
                    orderData.GuidePersonContact = Constants.GuideStatus.PENDING.ToString();
                    orderData.GuidePersonName = Constants.GuideStatus.PENDING.ToString();
                }
                else
                {
                    Exploiter guidePerson = context.Exploiters.FirstOrDefault(e => e.Id == order.GuidePersonId);
                    if (guidePerson != null)
                    {
                        orderData.GuidePersonContact = guidePerson.Phone;
                        orderData.GuidePersonName = guidePerson.Firstname;
                    }
                }

                orderDatas.Add(orderData);
            }
            string json = JsonConvert.SerializeObject(orderDatas);
            Console.WriteLine(json);
            Console.WriteLine("response sent");
            return Ok(orderDatas);
        }

        /// <summary>
        /// Назначение гида
        /// </summary>
        /// <param name="guideRequest"></param>
        /// <returns></returns>
        [HttpPost("admin/order/assignGuide")]
        public IActionResult AssignGuidePersonForOrder([FromBody] UpdateGuideStatusRequest guideRequest)
        {
            Console.WriteLine("ASSIGN GUIDE PERSON FROM ORDERS");
            Console.WriteLine(guideRequest);
            List<Order> orders = context.Orders.Where(o => o.OrderId == guideRequest.OrderId).ToList();
            Exploiter guidePerson = null;
            var optionalGuidePerson = context.Exploiters.Find(guideRequest.GuideId);
            if (optionalGuidePerson != null)
            {
                guidePerson = optionalGuidePerson;
            }
            
            if (guidePerson != null)
            {
                foreach (Order order in orders)
                {
                    order.GuideAssigned = Constants.IsGuideAssigned.YES.ToString();
                    order.GuidePersonId = guideRequest.GuideId;
                    context.Orders.Update(order);
                    
                }
                context.SaveChanges();
            }
            List<Order> exploiterOrder = context.Orders
                .Where(o => o.OrderId == guideRequest.OrderId)
                .Include(o => o.Tour)
                .Include(o => o.Exploiter)
                .ToList();
            List<MyOrderResponse> orderDatas = new List<MyOrderResponse>();
            foreach (Order order in exploiterOrder)
            {
                MyOrderResponse orderData = new MyOrderResponse();
                orderData.OrderId = order.OrderId;
                orderData.TourDescription = order.Tour.Description;
                orderData.TourName = order.Tour.Title;
                orderData.TourImage = order.Tour.ImageName;
                orderData.Quantity = (int)order.Quantity;
                orderData.OrderDate = order.OrderDate;
                orderData.TourId = order.Tour.Id;
                orderData.GuideDate = order.GuideDate + " " + order.GuideTime;
                orderData.GuideStatus = order.GuideStatus;
                orderData.TotalPrice = (order.Quantity * double.Parse(order.Tour.Price.ToString())).ToString();
                orderData.ExploiterId = order.Exploiter.Id;
                orderData.UserName = order.Exploiter.Firstname + " " + order.Exploiter.Lastname;
                orderData.ExploiterPhone = order.Exploiter.Phone;

                if (order.GuidePersonId == 0)
                {
                    orderData.GuidePersonContact = Constants.GuideStatus.PENDING.ToString();
                    orderData.GuidePersonName = Constants.GuideStatus.PENDING.ToString();
                }
                else
                {
                    Exploiter gPerson = context.Exploiters.FirstOrDefault(e => e.Id == order.GuidePersonId);
                    if (gPerson != null)
                    {
                        orderData.GuidePersonContact = gPerson.Phone;
                        orderData.GuidePersonName = gPerson.Firstname;
                    }
                }

                orderDatas.Add(orderData);
            }

            string json = JsonConvert.SerializeObject(orderDatas);
            Console.WriteLine(json);
            Console.WriteLine("response sent");
            return Ok(orderDatas);
        }

        /// <summary>
        /// Получение своих заказов(у гида)
        /// </summary>
        /// <param name="guidePersonId"></param>
        /// <returns></returns>
        [HttpGet("guide/myorder")]
        public IActionResult GetMyGuideOrders([FromQuery(Name = "guidePersonId")] int guidePersonId)
        {
            Console.WriteLine("MY ORDER FOR EXPLOITER ID: " + guidePersonId);
            Exploiter person = context.Exploiters.FirstOrDefault(e => e.Id == guidePersonId);
            List<Order> exploiterOrder = context.Orders
                .Where(o => o.GuidePersonId == guidePersonId)
                .Include(o => o.Tour)
                .Include(o => o.Exploiter)
                .ToList();

            List<MyOrderResponse> orderDatas = new List<MyOrderResponse>();
            foreach (Order order in exploiterOrder)
            {
                MyOrderResponse orderData = new MyOrderResponse();
                orderData.OrderId = order.OrderId;
                orderData.TourDescription = order.Tour.Description;
                orderData.TourName = order.Tour.Title;
                orderData.TourImage = order.Tour.ImageName;
                orderData.Quantity = (int)order.Quantity;
                orderData.OrderDate = order.OrderDate;
                orderData.TourId = order.Tour.Id;
                orderData.GuideDate = order.GuideDate + " " + order.GuideTime;
                orderData.GuideStatus = order.GuideStatus;
                orderData.TotalPrice = (order.Quantity * double.Parse(order.Tour.Price.ToString())).ToString();
                orderData.ExploiterId = order.Exploiter.Id;
                orderData.UserName = order.Exploiter.Firstname + " " + order.Exploiter.Lastname;
                orderData.ExploiterPhone = order.Exploiter.Phone;

                if (order.GuidePersonId == 0)
                {
                    orderData.GuidePersonContact = Constants.GuideStatus.PENDING.ToString();
                    orderData.GuidePersonName = Constants.GuideStatus.PENDING.ToString();
                }
                else
                {
                    orderData.GuidePersonContact = person?.Phone ?? Constants.GuideStatus.PENDING.ToString();
                    orderData.GuidePersonName = person?.Firstname ?? Constants.GuideStatus.PENDING.ToString();
                }

                orderDatas.Add(orderData);
            }

            string json = JsonConvert.SerializeObject(orderDatas);
            Console.WriteLine(json);
            Console.WriteLine("response sent");
            return Ok(orderDatas);
        }
    }
}
