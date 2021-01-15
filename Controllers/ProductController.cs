using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Route("v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> GetProductsAsync([FromServices] DataContext context)
        {
            var products = await context.Products.Include(x => x.Category).AsNoTracking().ToListAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetProductByIdAsync([FromServices] DataContext context, int id)
        {
            var product = await context.Products.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(product);
        }

        [HttpGet]
        [Route("categories/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> GetProductsByCategoryAsync([FromServices] DataContext context, int id)
        {
            var products = await context.Products.Include(x => x.Category).AsNoTracking().Where(x => x.CategoryId == id).ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> PostProduct([FromServices] DataContext context, Product model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return Ok(new { message = "Salvo com sucesso" });
            }
            catch
            {
                return BadRequest(new { message = "Ocorreu um erro ao adicionar a categoria" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> PutProductAsync([FromServices] DataContext context, int id, Product model)
        {
            if (model.Id != id)
                return NotFound(new { message = "Id não encontrado" });


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Product>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(new { message = "Atuazizado com sucesso" });
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Erro ao atualizar" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Erro ao atualizar" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteProductAsync([FromServices] DataContext context, int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return NotFound(new { message = "produto não encontrado" });

            context.Remove(product);
            await context.SaveChangesAsync();
            return Ok(new { message = "Excluido com sucesso" });
        }
    }
}
