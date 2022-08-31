using APIMaisEventos.Interfaces;
using APIMaisEventos.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APIMaisEventos.Repositories
{
    public class GrupoRepository : IGrupoRepository
    {
        // String de Conexão com o DB
        readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=DesafioAPI";

        /// <summary>
        /// Exclui um grupo do DB
        /// </summary>
        /// <param name="id">Id do grupo</param>
        /// <returns>Mensagem de exclusão</returns>
        public bool Delete(int id)
        {
            // Abrir conexão com banco de dados
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para exclusão de dados
                string script = "DELETE FROM Grupos WHERE Id=@Id";

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

        /// <summary>
        /// Retorna os dados de todos os grupos do DB
        /// </summary>
        /// <returns>Dados de todos os grupos</returns>
        public ICollection<Grupo> GetAll()
        {
            var groups = new List<Grupo>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para visualização de dados
                string query = "SELECT * FROM Grupos";

                // Comando de Execução da Query (Comando de busca)
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Lendo a lista
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Lê os dados na tabela do DB e adiciona nas classes dentro da lista da variável groups
                        while (reader.Read())
                        {
                            groups.Add(new Grupo
                            {
                                Id = (int)reader[0],
                                GroupName = (string)reader[1],
                            });
                        }
                    }
                }
            }

            return groups;
        }

        /// <summary>
        /// Retorna os dados de um grupo
        /// </summary>
        /// <param name="id">Id do grupo</param>
        /// <returns>Dados do grupo</returns>
        public Grupo GetById(int id)
        {
            var group = new Grupo();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para visualização de dados
                string query = "SELECT * FROM Grupos WHERE Id = @Id";

                // Comando de Execução da Query (Comando de busca)
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    // Lendo a lista
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Lê os dados na tabela do DB e adiciona na classe da variável group
                        while (reader.Read())
                        {
                            group.Id = (int)reader[0];
                            group.GroupName = (string)reader[1];
                        }
                    }
                }
            }

            return group;
        }

        /// <summary>
        /// Registra um grupo no DB
        /// </summary>
        /// <param name="group">Dados do grupo</param>
        /// <returns>Dados do grupo registrado</returns>
        public Grupo Insert(Grupo group)
        {
            // Abrir conexão com banco de dados
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para inserção de dados
                string script = "INSERT INTO Grupos (GroupName) VALUES (@GroupName)";

                // Comando de Execução da Query
                using (SqlCommand cmd = new SqlCommand(script, connection))
                {
                    // Declaração das variáveis
                    cmd.Parameters.Add("@GroupName", SqlDbType.NVarChar).Value = group.GroupName;

                    // Executando comando
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return group;
        }

        /// <summary>
        /// Modifica os dados de um grupo no DB
        /// </summary>
        /// <param name="id">Id do grupo</param>
        /// <param name="group">Dados do grupo</param>
        /// <returns>Dados do grupo atualizados</returns>
        public Grupo Update(int id, Grupo group)
        {
            // Abrir conexão com banco de dados
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para alteração de dados
                string script = "UPDATE Grupos SET GroupName = @GroupName WHERE Id = @Id";

                // Comando de Execução da Query
                using (SqlCommand cmd = new SqlCommand(script, connection))
                {
                    // Declaração das variáveis
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@GroupName", SqlDbType.NVarChar).Value = group.GroupName;

                    // Executando comando
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    group.Id = id;
                }
            }
            return group;
        }
    }
}
