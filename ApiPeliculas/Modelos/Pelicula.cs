using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPeliculas.Modelos
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string RutaImgen { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int Duracion { get; set; }
        [DefaultValue(Siete)]
        public enum TipoClasificacion { Siete, Trece, Dieciseis, Dieciocho }
        public DateTime FechaCreacion { get; set; }
        [ForeignKey("categoriaId")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set;}

    }
}
