using APIMaisEventos.Models;
using APIMaisEventos.Repositories;
using APIMaisEventos.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace APIMaisEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostagensController : ControllerBase
    {
        private PostagemRepository postRepository = new PostagemRepository();

        // POST - Cadastrar
        /// <summary>
        /// Registra uma nova postagem
        /// </summary>
        /// <param name="post">Dados da postagem</param>
        /// <param name="file">Imagem da postagem</param>
        /// <returns>Dados da postagem</returns>
        [HttpPost]
        public IActionResult Register([FromForm] Postagem post, IFormFile file)
        {
            try
            {
                #region Upload de Imagem
                string[] allowedExtension = { "jpeg", "jpg", "png", "svg" };

                // Fazendo Upload do arquivo
                string uploadResult = Upload.UploadFile(file, allowedExtension, "Images");

                // Retornando erro para arquivos inválidos
                if (uploadResult == "")
                {
                    return BadRequest("Arquivo não encontrado");
                }

                // Adicionando a imagem para a postagem
                post.PostImage = uploadResult;
                #endregion

                // Pega a data e horário atuais
                DateTime dateNow = DateTime.Now;
                string sqlFormattedDate = dateNow.ToString("yyyy-MM-dd HH:mm:ss.fff");

                var IPost = postRepository.Insert(post, dateNow);
                // Retorna o código 200 e os dados da postagem cadastrada
                return Ok(IPost);
            }
            catch (System.Exception ex)
            {
                // Código 500 -> Sem Conexão com o servidor ou aplicação
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    error = ex.Message,
                });
            }
        }

        // GET - Visualizar/Listar
        /// <summary>
        /// Retorna os dados das postgens registradas
        /// </summary>
        /// <returns>Dados de todas as postagens</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var posts = postRepository.GetAll();

                // Retorna o código 200 e os dados de todos as postagens
                return Ok(posts);
            }
            catch (System.Exception ex)
            {
                // Código 500 -> Sem Conexão com o servidor ou aplicação
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    error = ex.Message,
                });
            }
        }

        // PUT - Alterar
        /// <summary>
        /// Modifica os dados de uma postagem
        /// </summary>
        /// <param name="id">Id da postagem</param>
        /// <param name="post">Dados da postagem</param>
        /// <param name="file">Imagem da postagem</param>
        /// <returns>Dados da postagem atualizados</returns>
        [HttpPut("{id}")]
        public IActionResult Change(int id, [FromForm] Postagem post, IFormFile file)
        {
            try
            {
                // Verificando se o usuário existe no DB
                var search_post = postRepository.GetById(id);
                if (search_post == null)
                {
                    return NotFound();
                }

                #region Upload de Imagem
                string[] allowedExtension = { "jpeg", "jpg", "png", "svg" };

                // Fazendo Upload do arquivo
                string uploadResult = Upload.UploadFile(file, allowedExtension, "Images");

                // Retornando erro para arquivos inválidos
                if (uploadResult == "")
                {
                    return BadRequest("Arquivo não encontrado");
                }

                // Adicionando a imagem para a postagem
                post.PostImage = uploadResult;
                #endregion

                // Pega a data e horário atuais
                DateTime dateNow = DateTime.Now;
                string sqlFormattedDate = dateNow.ToString("yyyy-MM-dd HH:mm:ss.fff");

                var updated_post = postRepository.Update(id, post, dateNow);
                // Retorna o código 200 e os dados da postagem alterada
                return Ok(updated_post);
            }
            catch (System.Exception ex)
            {
                // Código 500 -> Sem Conexão com o servidor ou aplicação
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    error = ex.Message,
                });
            }
        }

        // DELETE
        /// <summary>
        /// Exclui uma postagem do DB
        /// </summary>
        /// <param name="id">Id da postagem</param>
        /// <returns>Mensagem de exclusão</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Verificando se a postagem existe no DB
                var search_post = postRepository.GetById(id);
                if (search_post == null)
                {
                    return NotFound();
                }

                postRepository.Delete(id);

                // Retorna o código 200 e os dados da postagem alterada
                return Ok(new
                {
                    msg = "Postagem excluída com sucesso."
                });
            }
            catch (System.Exception ex)
            {
                // Código 500 -> Sem Conexão com o servidor ou aplicação
                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    error = ex.Message,
                });
            }
        }
    }
}
