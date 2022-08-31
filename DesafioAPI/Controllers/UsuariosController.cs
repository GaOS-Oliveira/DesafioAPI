using APIMaisEventos.Repositories;
using APIMaisEventos.Utils;
using DesafioAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private UsuarioRepository userRepository = new UsuarioRepository();

        // POST - Cadastrar
        /// <summary>
        /// Cadastra usuários na aplicação.
        /// </summary>
        /// <param name="user">Dados do usuário</param>
        /// <param name="file">Foto de perfil do usuário</param>
        /// <returns>Dados do usuário cadastrado</returns>
        [HttpPost]
        public IActionResult Register([FromForm] Usuario user, IFormFile file)
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

                // Adicionando a foto de perfil para o usuário
                user.ProfilePic = uploadResult;
                #endregion

                var IUser = userRepository.Insert(user);
                // Retorna o código 200 e os dado do usuário cadastrado
                return Ok(IUser);
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
        /// Lista todos os usuários da aplicação
        /// </summary>
        /// <returns>Dados de todos os usuários</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var users = userRepository.GetAll();

                // Retorna o código 200 e os dados de todos os usuários
                return Ok(users);
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
        /// Altera os dados de um usuário
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <param name="user">Dados do usuário</param>
        /// <param name="file">Foto de perfil do usuário</param>
        /// <returns>Usuário alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Change(int id, [FromForm] Usuario user, IFormFile file)
        {
            try
            {
                // Verificando se o usuário existe no DB
                var search_user = userRepository.GetById(id);
                if (search_user == null)
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

                // Adicionando a foto de perfil para o usuário
                user.ProfilePic = uploadResult;
                #endregion

                var updated_user = userRepository.Update(id, user);
                // Retorna o código 200 e os dados do usuário alterado
                return Ok(updated_user);
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
        /// Exclui um usuário da aplicação
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Mensagem de exclusão</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Verificando se o usuário existe no DB
                var search_user = userRepository.GetById(id);
                if (search_user == null)
                {
                    return NotFound();
                }

                userRepository.Delete(id);

                // Retorna o código 200 e os dados do usuário alterado
                return Ok(new
                {
                    msg = "Usuário excluído com sucesso."
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
