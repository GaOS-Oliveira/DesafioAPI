using APIMaisEventos.Models;
using System;
using System.Collections.Generic;

namespace APIMaisEventos.Interfaces
{
    public interface IPostagemRepository
    {
        // Read
        ICollection<Postagem> GetAll();
        Postagem GetById(int id);
        // Create
        Postagem Insert(Postagem post, DateTime sqlFormattedDate);
        // Update
        Postagem Update(int id, Postagem post, DateTime sqlFormattedDate);
        // Delete
        bool Delete(int id);
    }
}
