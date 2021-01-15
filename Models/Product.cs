using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    [Table("tb_produto")]
    public class Product
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(60, ErrorMessage = "Campo deve conter no máximo 60 caracteres")]
        [MinLength(3, ErrorMessage = "Campo deve conter no mínimo 3 caracteres")]
        [Column("titulo")]
        public string Title { get; set; }

        [MaxLength(1024, ErrorMessage = "Campo deve conter no máximo 60 caracteres")]
        [Column("descricao")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        [Column("preco")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Categoria inválida")]
        [Column("categoria_id")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Product() { }

        public Product(int id, string title, string description, decimal price, int categoryId)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            CategoryId = categoryId;
        }
    }
}
