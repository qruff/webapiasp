namespace webapiasp.Models
{
    public partial class Tour
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public string? ImageName { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public string? Title { get; set; }

        public int? CategoryId { get; set; }

        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

        public virtual Category? Category { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public override string ToString()
        {
            return $"Tour [id={Id}, title={Title}, description={Description}, quantity={Quantity}, price={Price}, categoryId={CategoryId}], image={ImageName}";
        }
    }
}
