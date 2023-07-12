namespace webapiasp.DTO
{
    public class MyOrderResponse
    {
        public string OrderId { get; set; }
        public int TourId { get; set; }
        public int ExploiterId { get; set; }
        public string UserName { get; set; }
        public string ExploiterPhone { get; set; }
        public string TourName { get; set; }
        public string TourDescription { get; set; }
        public string TourImage { get; set; }
        public int Quantity { get; set; }
        public string TotalPrice { get; set; }
        public string OrderDate { get; set; }
        public string GuideDate { get; set; }
        public string GuideStatus { get; set; }
        public string GuidePersonName { get; set; }
        public string GuidePersonContact { get; set; }
    }
}
