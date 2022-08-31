using APIMaisEventos.Interfaces;
using APIMaisEventos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APIMaisEventos.Repositories
{
    public class PostagemRepository : IPostagemRepository
    {
        // String de Conexão com o DB
        readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=DesafioAPI";

        /// <summary>
        /// Exclui uma postagem do DB
        /// </summary>
        /// <param name="id">Id da postagem</param>
        /// <returns>bool</returns>
        public bool Delete(int id)
        {
            // Abrir conexão com banco de dados
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para exclusão de dados
                string script = "DELETE FROM Postagens WHERE Id=@Id";

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
        /// Retorna os dados de todas as postagens
        /// </summary>
        /// <returns>Dados de todas as postagens</returns>
        public ICollection<Postagem> GetAll()
        {
            var posts = new List<Postagem>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para visualização de dados
                string query = "SELECT * FROM Postagens";

                // Comando de Execução da Query (Comando de busca)
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Lendo a lista
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Lê os dados na tabela do DB e adiciona nas classes dentro da lista da variável posts
                        while (reader.Read())
                        {
                            posts.Add(new Postagem
                            {
                                Id = (int)reader[0],
                                PostText = (string)reader[1],
                                PostImage = (string)reader[2],
                                PostDate = (DateTime)reader[3],
                                UsuarioId = (int)reader[4],
                                GrupoId = (int)reader[5],
                            });
                        }
                    }
                }
            }

            return posts;
        }

        /// <summary>
        /// Retorna os dados de uma postagem
        /// </summary>
        /// <param name="id">Id da postagem</param>
        /// <returns>Dados da postagem</returns>
        public Postagem GetById(int id)
        {
            var post = new Postagem();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para visualização de dados
                string query = "SELECT * FROM Postagens WHERE Id = @Id";

                // Comando de Execução da Query (Comando de busca)
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    // Lendo a lista
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Lê os dados na tabela do DB e adiciona na classe da variável post
                        while (reader.Read())
                        {
                            post.Id = (int)reader[0];
                            post.PostText = (string)reader[1];
                            post.PostImage = (string)reader[2];
                            post.PostDate = (DateTime)reader[3];
                            post.UsuarioId = (int)reader[4];
                            post.GrupoId = (int)reader[5];
                        }
                    }
                }
            }

            return post;
        }

        /// <summary>
        /// Adiciona uma postagem ao DB
        /// </summary>
        /// <param name="post">Dados da postagem</param>
        /// <param name="sqlFormattedDate">Data e hora atuais</param>
        /// <returns>Dados da postagem</returns>
        public Postagem Insert(Postagem post, DateTime sqlFormattedDate)
        {
            // Abrir conexão com banco de dados
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para inserção de dados
                string script = "INSERT INTO Postagens (PostText, PostImage, PostDate, UsuarioId, GrupoId) " +
                    "VALUES (@PostText, @PostImage, @PostDate, @UsuarioId, @GrupoId)";

                // Comando de Execução da Query
                using (SqlCommand cmd = new SqlCommand(script, connection))
                {
                    // Adiciona a data à Classe Postagem
                    post.PostDate = sqlFormattedDate;

                    // Declaração das variáveis
                    cmd.Parameters.Add("@PostText", SqlDbType.NVarChar).Value = post.PostText;
                    cmd.Parameters.Add("@PostImage", SqlDbType.NVarChar).Value = post.PostImage;
                    cmd.Parameters.Add("@PostDate", SqlDbType.DateTime).Value = post.PostDate;
                    cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = post.UsuarioId;
                    cmd.Parameters.Add("@GrupoId", SqlDbType.Int).Value = post.GrupoId;

                    // Executando comando
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return post;
        }
        
        /// <summary>
        /// Modifica os dados de uma postagem
        /// </summary>
        /// <param name="id">Id da postagem</param>
        /// <param name="post">Dados da postagem</param>
        /// <param name="sqlFormattedDate">Data e hora atuais</param>
        /// <returns>Dados da postagem atualizados</returns>
        public Postagem Update(int id, Postagem post, DateTime sqlFormattedDate)
        {

            // Abrir conexão com banco de dados
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Comando SQL para alteração de dados
                string script = "UPDATE Postagens " +
                    "SET PostText = @PostText, PostImage = @PostImage, " +
                    "PostDate = @PostDate, UsuarioId = @UsuarioId, GrupoId = @GrupoId " +
                    "WHERE Id = @Id";

                // Comando de Execução da Query
                using (SqlCommand cmd = new SqlCommand(script, connection))
                {
                    // Adiciona a data à Classe Postagem
                    post.PostDate = sqlFormattedDate;

                    // Declaração das variáveis
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@PostText", SqlDbType.NVarChar).Value = post.PostText;
                    cmd.Parameters.Add("@PostImage", SqlDbType.NVarChar).Value = post.PostImage;
                    cmd.Parameters.Add("@PostDate", SqlDbType.DateTime).Value = post.PostDate;
                    cmd.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = post.UsuarioId;
                    cmd.Parameters.Add("@GrupoId", SqlDbType.Int).Value = post.GrupoId;

                    // Executando comando
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    post.Id = id;
                }
            }
            return post;
        }
    }
}
