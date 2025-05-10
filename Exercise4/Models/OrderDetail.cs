using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise4.Models
{
    public class OrderDetail
    {
        public int ID { get; set; }

        public int OrderID { get; set; }
        public Order Order { get; set; }

        public int ItemID { get; set; }
        public Item Item { get; set; }

        public int Quantity { get; set; }
        public decimal UnitAmount { get; set; }
    }
}
