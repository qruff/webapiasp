namespace webapiasp.DTO
{
    public class CartDataResponse
    {
        public int CartId { get; set; }
        public int TourId { get; set; }
        public string TourName { get; set; }
        public string TourDescription { get; set; }
        public string TourImage { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"CartDataResponse [cartId={CartId}, tourId={TourId}, tourName={TourName}, tourDescription={TourDescription}, tourImage={TourImage}, quantity={Quantity}]";
        }
    }
}
