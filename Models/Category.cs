using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    [Table("tb_categoria")]
    public class Category
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(60, ErrorMessage = "Campo deve conter no máximo 60 caracteres")]
        [MinLength(3, ErrorMessage = "Campo deve conter no mínimo 3 caracteres")]
        [Column("titulo")]
        public string Title { get; set; }

        public Category() { }

        public Category(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
