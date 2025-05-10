using Exercise4.Data;
using Exercise4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise4.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ReportForm()
        {
            var topItems = await _context.OrderDetail
                .Include(od => od.Item)
                .GroupBy(od => od.Item.ItemName)
                .Select(g => new ItemReport
                {
                    ItemName = g.Key,
                    TotalQuantity = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(5)
                .ToListAsync();

            var customerPurchases = await _context.Order
                .Include(o => o.Agent)
                .Include(o => o.OrderDetails).ThenInclude(od => od.Item)
                .Select(o => new CustomerPurchase
                {
                    OrderID = o.OrderID,
                    AgentName = o.Agent.AgentName,
                    Items = o.OrderDetails.Select(d => d.Item.ItemName).ToList(),
                    Total = o.OrderDetails.Sum(d => d.Quantity * d.UnitAmount)
                })
                .ToListAsync();

            var viewModel = new ReportFormViewModel
            {
                TopItems = topItems,
                CustomerPurchases = customerPurchases
            };

            return View(viewModel);
        }
    }

    // These are ViewModels to pass to the View
    public class ReportFormViewModel
    {
        public List<ItemReport> TopItems { get; set; }
        public List<CustomerPurchase> CustomerPurchases { get; set; }
    }

    public class ItemReport
    {
        public string ItemName { get; set; }
        public int TotalQuantity { get; set; }
    }

    public class CustomerPurchase
    {
        public int OrderID { get; set; }
        public string AgentName { get; set; }
        public List<string> Items { get; set; }
        public decimal Total { get; set; }
    }
}
