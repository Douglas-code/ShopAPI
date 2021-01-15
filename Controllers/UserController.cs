using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Route("v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "superAdmin")]
        public async Task<ActionResult<User>> GetUsersAsync([FromServices] DataContext context)
        {
            var users = await context.Users.AsNoTracking().ToListAsync();
            return Ok(users);
        }   

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<User>> PostUserAsync ([FromServices] DataContext context, [FromBody] User model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                model.Role = "admin";

                context.Users.Add(model);
                await context.SaveChangesAsync();
                return Ok(new { message = "Conta criada com sucesso" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível criar usuário" });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromServices] DataContext context, [FromBody] User model)
        {
            User user = await context.Users.AsNoTracking().Where(x => x.UserName == model.UserName && x.Password == model.Password)
                .FirstOrDefaultAsync();

            if (user == null)
                return BadRequest(new { message = "login ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return Ok(new { user = user, token = token });
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> PutUserAsync([FromServices] DataContext context, int id, [FromBody] User model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Erro ao atualizar" });

            if (id != model.Id)
                return NotFound(new { message = "Usuario não encontrado" });

            try
            {
                context.Entry<User>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(new { message = "Atualizado com sucesso!"});
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Erro ao atualizar" });
            }
        }
    }
}
