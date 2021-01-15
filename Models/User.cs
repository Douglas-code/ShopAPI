using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    [Table("tb_usuario")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve ter no máximo 20 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo dever ter no minimo 3 caracteres")]
        [Column("login")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve ter no máximo 20 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo dever ter no minimo 3 caracteres")]
        [Column("senha")]
        public string Password { get; set; }

        [Column("funcao")]
        public string Role { get; set; }

        public User() { }

        public User(int id, string userName, string password, string role)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Role = role;
        }
    }
}
