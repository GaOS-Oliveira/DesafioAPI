using APIMaisEventos.Interfaces;
using DesafioAPI.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APIMaisEventos.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
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
                string script = "DELETE FROM Usuarios WHERE Id=@Id";

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

        public ICollection<Usuario> GetAll()
        {
            var users = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para visualização de dados
                string query = "SELECT * FROM Usuarios";

                // Comando de Execução da Query (Comando de busca)
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Lendo a lista
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Lê os dados na tabela do DB e adiciona nas classes dentro da lista da variável users
                        while (reader.Read())
                        {
                            users.Add(new Usuario
                            {
                                Id = (int)reader[0],
                                UserName = (string)reader[1],
                                UserEmail = (string)reader[2],
                                UserPassword = (string)reader[3],
                                ProfilePic = (string)reader[4].ToString(),
                            });
                        }
                    }
                }
            }

            return users;
        }

        public Usuario GetById(int id)
        {
            var user = new Usuario();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para visualização de dados
                string query = "SELECT * FROM Usuarios WHERE Id = @Id";

                // Comando de Execução da Query (Comando de busca)
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    // Lendo a lista
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Lê os dados na tabela do DB e adiciona na classe da variável user
                        while (reader.Read())
                        {
                            user.Id = (int)reader[0];
                            user.UserName = (string)reader[1];
                            user.UserEmail = (string)reader[2];
                            user.UserPassword = (string)reader[3];
                            user.ProfilePic = (string)reader[4].ToString();
                        }
                    }
                }
            }

            return user;
        }

        public Usuario Insert(Usuario user)
        {
            // Abrir conexão com banco de dados
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para inserção de dados
                string script = "INSERT INTO Usuarios (UserName, UserEmail, UserPassword, ProfilePic) " +
                    "VALUES (@UserName, @UserEmail, @UserPassword, @ProfilePic)";

                // Comando de Execução da Query
                using (SqlCommand cmd = new SqlCommand(script, connection))
                {
                    // Declaração das variáveis
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
                    cmd.Parameters.Add("@UserEmail", SqlDbType.NVarChar).Value = user.UserEmail;
                    cmd.Parameters.Add("@UserPassword", SqlDbType.NVarChar).Value = user.UserPassword;
                    cmd.Parameters.Add("@ProfilePic", SqlDbType.NVarChar).Value = user.ProfilePic;

                    // Executando comando
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return user;
        }

        public Usuario Update(int id, Usuario user)
        {
            // Abrir conexão com banco de dados
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para alteração de dados
                string script = "UPDATE Usuarios " +
                    "SET UserName = @UserName, UserEmail = @UserEmail, " +
                    "UserPassword = @UserPassword, ProfilePic = @ProfilePic " +
                    "WHERE Id = @Id";

                // Comando de Execução da Query
                using (SqlCommand cmd = new SqlCommand(script, connection))
                {
                    // Declaração das variáveis
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
                    cmd.Parameters.Add("@UserEmail", SqlDbType.NVarChar).Value = user.UserEmail;
                    cmd.Parameters.Add("@UserPassword", SqlDbType.NVarChar).Value = user.UserPassword;
                    cmd.Parameters.Add("@ProfilePic", SqlDbType.NVarChar).Value = user.ProfilePic;

                    // Executando comando
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    user.Id = id;
                }
            }
            return user;
        }
    }
}
