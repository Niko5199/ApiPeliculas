using ApiPeliculas.Modelos;

namespace ApiPeliculas.Repositorio.IRepositorio
{
    /*
        Las interfaces nos ayudan a describir los metodos
        que despues se crea la funcionalidad de cada metodo,
        es basicamente una plantilla del menu de opciones que 
        tenemos disponibles para interactuar con el modelo
     */
    public interface ICategoriaRepositorio
    {
        ICollection<Categoria> GetCategorias();
        Categoria GetCategoria(int categorId);
        bool ExisteCategoria(string Nombre);
        bool ExisteCategoria(int id);
        bool CrearCategoria(Categoria categoria);
        bool ActualizarCategoria(Categoria categoria);
        bool BorrarCategoria(Categoria categoria);
        bool Guardar();
    }
}
