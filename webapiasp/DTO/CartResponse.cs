namespace webapiasp.DTO
{
    public class CartResponse
    {
        public string TotalCartPrice { get; set; }
        public List<CartDataResponse> CartData { get; set; }

        public override string ToString()
        {
            return $"CartResponse [totalCartPrice={TotalCartPrice}, cartData={CartData}]";
        }
    }
}
