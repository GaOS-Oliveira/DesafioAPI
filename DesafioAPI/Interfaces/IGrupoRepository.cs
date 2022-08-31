using APIMaisEventos.Models;
using System.Collections.Generic;

namespace APIMaisEventos.Interfaces
{
    public interface IGrupoRepository
    {
        // Read
        ICollection<Grupo> GetAll();
        Grupo GetById(int id);
        // Create
        Grupo Insert(Grupo group);
        // Update
        Grupo Update(int id, Grupo group);
        // Delete
        bool Delete(int id);
    }
}
