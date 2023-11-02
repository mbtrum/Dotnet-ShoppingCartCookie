using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Models;

namespace MyStore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MyStore.Data.MyStoreContext _context;

        public string Message { get; set; } = string.Empty;

        public IndexModel(MyStore.Data.MyStoreContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Product != null)
            {
                Message = "Your shopping cart items: " + Request.Cookies["ShoppingCart"];
                Product = await _context.Product.ToListAsync();
            }
        }
    }
}
