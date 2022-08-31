using APIMaisEventos.Models;
using System.Collections.Generic;

namespace APIMaisEventos.Interfaces
{
    public interface IUsuarioGrupoRepository
    {
        // Read
        ICollection<UsuarioGrupo> GetAll();
        UsuarioGrupo GetById(int id);
        // Create
        UsuarioGrupo Insert(UsuarioGrupo userGroup);
        // Update
        UsuarioGrupo Update(int id, UsuarioGrupo userGroup);
        // Delete
        bool Delete(int id);
    }
}
