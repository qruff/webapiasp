namespace webapiasp.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public int? ExploiterId { get; set; }
        public int? TourId { get; set; }
        public virtual Exploiter? Exploiter { get; set; }
        public virtual Tour? Tour { get; set; }
    }
}
