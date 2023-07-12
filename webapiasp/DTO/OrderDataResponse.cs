namespace webapiasp.DTO
{
    public class OrderDataResponse
    {
        public List<MyOrderResponse> OrderResponse { get; set; }
        public string TotalCartPrice { get; set; }

        public override string ToString()
        {
            return $"OrderDataResponse [orderResponse={OrderResponse}, totalCartPrice={TotalCartPrice}]";
        }
    }
}
