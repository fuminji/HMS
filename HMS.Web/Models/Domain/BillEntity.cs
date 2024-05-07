namespace HMS.Web.Models.Domain
{
    public class BillEntity
    {
        public int Id { get; set; }
        public string Billtype{ get; set; }

        public long Rate { get; set; }
        public long Quantity { get; set; }
        public string ImagePath { get; set; }
    }
}
