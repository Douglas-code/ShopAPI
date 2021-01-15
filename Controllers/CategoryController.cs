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
    [Route("v1/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Category>>> GetCategoriesAsync([FromServices] DataContext context)
        {
            var categories = await context.Categories.AsNoTracking().ToListAsync();
            return Ok(categories);
        }

        [Route("{id:int}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<Category>> GetCategoryByIdAsync([FromServices] DataContext context, int id)
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(category);
        }

        [Route("")]
        [HttpPost]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> PostCategoryAsync([FromBody] Category model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Categories.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possivel inserir" });
            }
        }

        [Route("{id:int}")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> PutCategoryAsync(int id, [FromBody] Category model, [FromServices] DataContext context)
        {
            if (model.Id != id)
                return NotFound(new { message = "Categoria não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Category>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
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
        public async Task<ActionResult> DeleteAsync([FromServices] DataContext context, int id)
        {
            var model = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
                return NotFound(new { message = "Categoria não encontrada" });

            context.Categories.Remove(model);
            await context.SaveChangesAsync();
            return Ok(new { message = "Excluído com sucesso" });
        }
    }
}
