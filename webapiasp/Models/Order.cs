namespace webapiasp.Models
{
    public partial class Order
    {
        public int Id { get; set; }

        public string? GuideAssigned { get; set; }

        public string? GuideDate { get; set; }

        public int? GuidePersonId { get; set; }

        public string? GuideStatus { get; set; }

        public string? GuideTime { get; set; }

        public string? OrderDate { get; set; }

        public string? OrderId { get; set; }

        public int? Quantity { get; set; }

        public int? ExploiterId { get; set; }

        public int? TourId { get; set; }

        public virtual Exploiter? Exploiter { get; set; }

        public virtual Tour? Tour { get; set; }
    }
}
