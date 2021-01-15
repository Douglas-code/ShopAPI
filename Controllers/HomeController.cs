using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Route("v1")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> GetAsync([FromServices] DataContext context)
        {
            var admin = new User { Id = 1, UserName = "Robin", Password = "Robin", Role = "admin" };
            var category = new Category { Id = 1, Title = "Informática" };
            var product = new Product { Id = 1, Title = "Notebook i7", Description = "16GB ram", Price = 5578, Category = category };
            context.Users.Add(admin);
            context.Categories.Add(category);
            context.Products.Add(product);
            await context.SaveChangesAsync();

            return Ok(new
            {
                message = "Dados configurados"
            });
        }
    }
}
