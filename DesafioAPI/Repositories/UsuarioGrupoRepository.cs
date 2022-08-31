using APIMaisEventos.Interfaces;
using APIMaisEventos.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APIMaisEventos.Repositories
{
    public class UsuarioGrupoRepository : IUsuarioGrupoRepository
    {
        // String de Conexão com o DB
        readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=DesafioAPI";

        public bool Delete(int id)
        {
            // Abrir conexão com banco de dados
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para exclusão de dados
                string script = "DELETE FROM UsuarioGrupo WHERE Id=@Id";

                // Comando de Execução da Query
                using (SqlCommand cmd = new SqlCommand(script, connection))
                {
                    // Declaração das variáveis
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    // Executando comando
                    cmd.CommandType = CommandType.Text;
                    int afectedLines = cmd.ExecuteNonQuery();

                    // Verifica se nenhuma linha da tabela do DB foi afetada
                    if (afectedLines == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public ICollection<UsuarioGrupo> GetAll()
        {
            var usersGroup = new List<UsuarioGrupo>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para visualização de dados
                string query = "SELECT * FROM UsuarioGrupo";

                // Comando de Execução da Query (Comando de busca)
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Lendo a lista
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Lê os dados na tabela do DB e adiciona nas classes dentro da lista da variável userGroup
                        while (reader.Read())
                        {
                            usersGroup.Add(new UsuarioGrupo
                            {
                                Id = (int)reader[0],
                                UsuarioId = (int)reader[1],
                                GrupoId = (int)reader[2],
                            });
                        }
                    }
                }
            }

            return usersGroup;
        }

        public UsuarioGrupo GetById(int id)
        {
            var userGroup = new UsuarioGrupo();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para visualização de dados
                string query = "SELECT * FROM UsuarioGrupo WHERE Id = @Id";

                // Comando de Execução da Query (Comando de busca)
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    // Lendo a lista
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Lê os dados na tabela do DB e adiciona na classe da variável userGroup
                        while (reader.Read())
                        {
                            userGroup.Id = (int)reader[0];
                            userGroup.UsuarioId = (int)reader[1];
                            userGroup.GrupoId = (int)reader[2];
                        }
                    }
                }
            }

            return userGroup;
        }

        public UsuarioGrupo Insert(UsuarioGrupo userGroup)
        {
            // Abrir conexão com banco de dados
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para inserção de dados
                string script = "INSERT INTO UsuarioGrupo (UsuarioID, GrupoId) " +
                    "VALUES (@UsuarioId, @GrupoId)";

                // Comando de Execução da Query
                using (SqlCommand cmd = new SqlCommand(script, connection))
                {
                    // Declaração das variáveis
                    cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = userGroup.UsuarioId;
                    cmd.Parameters.Add("@GrupoId", SqlDbType.Int).Value = userGroup.GrupoId;

                    // Executando comando
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return userGroup;
        }

        public UsuarioGrupo Update(int id, UsuarioGrupo userGroup)
        {
            // Abrir conexão com banco de dados
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para alteração de dados
                string script = "UPDATE UsuarioGrupo " +
                    "SET UsuarioId = @UsuarioId, GrupoId = @GrupoId " +
                    "WHERE Id = @Id";

                // Comando de Execução da Query
                using (SqlCommand cmd = new SqlCommand(script, connection))
                {
                    // Declaração das variáveis
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = userGroup.UsuarioId;
                    cmd.Parameters.Add("@GrupoId", SqlDbType.Int).Value = userGroup.GrupoId;

                    // Executando comando
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    userGroup.Id = id;
                }
            }
            return userGroup;
        }
    }
}
