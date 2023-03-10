using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Modelos.Dtos
{
    public class PeliculaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La ruta de la imagen es obligatoria")]
        public string RutaImagen { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El duracion es obligatorio")]
        public string Duracion { get; set; }

        public enum TipoClasificacion { Siete, Trece, Dieciseis, Dieciocho }

        public DateTime FechaCreacion { get; set; }

        public int CategoriaId { get; set; }


    }
}
