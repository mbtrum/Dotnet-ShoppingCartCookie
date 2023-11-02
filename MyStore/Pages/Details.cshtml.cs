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
    public class DetailsModel : PageModel
    {
        private readonly MyStore.Data.MyStoreContext _context;

        [BindProperty]
        public Product product { get; set; } = default!;

        public string Message { get; set; } = string.Empty;

        public DetailsModel(MyStore.Data.MyStoreContext context)
        {
            _context = context;
        }

      public Product Product { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            product = await _context.Product.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Product == null || product == null)
            {
                return Page();
            }

            // Add product to shopping cart (cookie)
            string cart = Request.Cookies["ShoppingCart"];
            
            if(string.IsNullOrEmpty(cart))
            {
                // cart is empty, initialize it
                Response.Cookies.Append("ShoppingCart", product.ProductId.ToString());
            }
            else
            {
                // add product to cart
                Response.Cookies.Append("ShoppingCart", cart + "," + product.ProductId);
            }   

            Message = "Product added to cart!";

            return Page();
        }


    }
}
