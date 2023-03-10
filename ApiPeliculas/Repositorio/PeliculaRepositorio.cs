using ApiPeliculas.Data;
using ApiPeliculas.Modelos;
using ApiPeliculas.Repositorio.IRepositorio;

namespace ApiPeliculas.Repositorio
{
    public class PeliculaRepositorio : IPeliculaRepositorio
    {
        private readonly ApplicationDbContext __bd;
        public bool ActualizarPelicula(Pelicula pelicula)
        {
            pelicula.FechaCreacion = DateTime.Now;
            __bd.Pelicula.Update(pelicula);
            return Guardar();
        }

        public bool BorrarPelicula(Pelicula pelicula)
        {
            __bd.Pelicula.Remove(pelicula);
            return Guardar();
        }

        public bool CrearPelicula(Pelicula pelicula)
        {
            pelicula.FechaCreacion = DateTime.Now;
            __bd.Pelicula.Add(pelicula);
            return Guardar();
        }

        public bool ExistePelicula(string nombre)
        {
            return __bd.Pelicula.Any(p => p.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
        }

        public bool ExistePelicula(int peliculaId)
        {
            return __bd.Pelicula.Any(p => p.Id == peliculaId);
        }

        public Pelicula GetPelicula(int peliculaId)
        {
           return __bd.Pelicula.FirstOrDefault(p => p.Id == peliculaId);
        }

        public ICollection<Pelicula> GetPeliculas()
        {
            return __bd.Pelicula.OrderBy(p => p.Nombre).ToList();
        }

        public bool Guardar()
        {
            return __bd.SaveChanges() >= 0 ? true: false;
        }
    }
}
