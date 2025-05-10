using System.Collections.Generic;
using System;
namespace Exercise4.Models
{
    public class OrderCreateViewModel
    {
        public int AgentID { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now; // default to today
        public List<OrderItemDetail> Items { get; set; } = new List<OrderItemDetail>();
    }

    public class OrderItemDetail
    {
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitAmount { get; set; }
    }
}
