namespace webapiasp.Models
{
    public partial class Category
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
    }
}
