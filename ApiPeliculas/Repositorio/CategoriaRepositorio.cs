using ApiPeliculas.Data;
using ApiPeliculas.Modelos;
using ApiPeliculas.Repositorio.IRepositorio;

namespace ApiPeliculas.Repositorio
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly ApplicationDbContext __bd;

        public CategoriaRepositorio(ApplicationDbContext bd)
        {
            __bd = bd;
        }

        public bool ActualizarCategoria(Categoria categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            __bd.Categoria.Update(categoria);
            return Guardar();
        }

        public bool BorrarCategoria(Categoria categoria)
        {
            __bd.Categoria.Remove(categoria);
            return Guardar();
        }

        public bool CrearCategoria(Categoria categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            __bd.Categoria.Add(categoria);
            return Guardar();
        }

        public bool ExisteCategoria(string Nombre)
        {
            bool valor = __bd.Categoria.Any(
                c => c.Nombre.ToLower().Trim() == Nombre.ToLower().Trim()
            );
            return valor;
        }

        public bool ExisteCategoria(int id)
        {
            return __bd.Categoria.Any(c => c.Id == id);
        }

        public Categoria GetCategoria(int categorId)
        {
            return __bd.Categoria.First(c => c.Id == categorId);
        }

        public ICollection<Categoria> GetCategorias()
        {
            return __bd.Categoria.OrderBy(c => c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return __bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
