using APIMaisEventos.Models;
using APIMaisEventos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIMaisEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioGruposController : ControllerBase
    {
        private UsuarioGrupoRepository userGroupRepository = new UsuarioGrupoRepository();

        // POST - Cadastrar
        /// <summary>
        /// Registra um novo UsuarioGrupo no DB
        /// </summary>
        /// <param name="userGroup">Dados do UsuarioGrupo</param>
        /// <returns>Dados do UsuarioGrupo</returns>
        [HttpPost]
        public IActionResult Register([FromForm] UsuarioGrupo userGroup)
        {
            try
            {
                var IUserGroup = userGroupRepository.Insert(userGroup);
                // Retorna o código 200 e os dado do UsuarioGrupo cadastrado
                return Ok(IUserGroup);
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
        /// Retorna os dados de todos os UsuarioGrupos
        /// </summary>
        /// <returns>Dados de todos os UsuarioGrupos</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var usersGroups = userGroupRepository.GetAll();

                // Retorna o código 200 e os dados de todos os UsuarioGrupos
                return Ok(usersGroups);
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
        /// Modifica os dados de um UsuarioGrupo
        /// </summary>
        /// <param name="id">Id do UsuarioGrupo</param>
        /// <param name="userGroup">Dados do UsuarioGrupo</param>
        /// <returns>Dados do UsuarioGrupo atualizados</returns>
        [HttpPut("{id}")]
        public IActionResult Change(int id, [FromForm] UsuarioGrupo userGroup)
        {
            try
            {
                // Verificando se o usuário existe no DB
                var search_userGroup = userGroupRepository.GetById(id);
                if (search_userGroup == null)
                {
                    return NotFound();
                }

                var updated_userGroup = userGroupRepository.Update(id, userGroup);
                // Retorna o código 200 e os dados do UsuarioGrupo alterado
                return Ok(updated_userGroup);
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
        /// Exclui um UsuarioGrupo da aplicação
        /// </summary>
        /// <param name="id">Id do UsuarioGrupo</param>
        /// <returns>Mensagem de exclusão</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Verificando se o usuário existe no DB
                var search_userGroup = userGroupRepository.GetById(id);
                if (search_userGroup == null)
                {
                    return NotFound();
                }

                userGroupRepository.Delete(id);

                // Retorna o código 200 e os dados do UsuarioGrupo alterado
                return Ok(new
                {
                    msg = "UsuarioGrupo excluído com sucesso."
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
