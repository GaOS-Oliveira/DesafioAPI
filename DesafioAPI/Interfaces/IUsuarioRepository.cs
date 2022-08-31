using DesafioAPI.Models;
using System.Collections.Generic;

namespace APIMaisEventos.Interfaces
{
    public interface IUsuarioRepository
    {
        // Read
        ICollection<Usuario> GetAll();
        Usuario GetById(int id);
        // Create
        Usuario Insert(Usuario user);
        // Update
        Usuario Update(int id, Usuario user);
        // Delete
        bool Delete(int id);
    }
}
