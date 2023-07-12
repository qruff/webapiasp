namespace webapiasp.DTO
{
    public class AddToCartRequest
    {
        public int TourId { get; set; }
        public int Quantity { get; set; }
        public int ExploiterId { get; set; }

        public override string ToString()
        {
            return $"AddToCartRequest [tourId={TourId}, quantity={Quantity}, exploiterId={ExploiterId}]";
        }
    }
}
