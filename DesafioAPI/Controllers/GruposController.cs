using APIMaisEventos.Models;
using APIMaisEventos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIMaisEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GruposController : ControllerBase
    {
        private GrupoRepository groupRepository = new GrupoRepository();

        // POST - Cadastrar
        /// <summary>
        /// Cadastra um novo grupo na aplicação
        /// </summary>
        /// <param name="group">Dados do Grupo</param>
        /// <returns>Dados do grupo cadastrado</returns>
        [HttpPost]
        public IActionResult Register([FromForm] Grupo group)
        {
            try
            {
                var IGroup = groupRepository.Insert(group);
                // Retorna o código 200 e os dado do usuário cadastrado
                return Ok(IGroup);
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
        /// Lista todos os grupos da aplicação
        /// </summary>
        /// <returns>Dados de todos os grupos</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var groups = groupRepository.GetAll();

                // Retorna o código 200 e os dados de todos os usuários
                return Ok(groups);
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
        /// Modifica os dados de um grupo
        /// </summary>
        /// <param name="id">Id do grupo</param>
        /// <param name="group">Dados do grupo</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Change(int id, [FromForm] Grupo group)
        {
            try
            {
                // Verificando se o grupo existe no DB
                var search_group = groupRepository.GetById(id);
                if (search_group == null)
                {
                    return NotFound();
                }

                var updated_group = groupRepository.Update(id, group);
                // Retorna o código 200 e os dados do grupo alterado
                return Ok(updated_group);
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
        /// Exclui um grupo da aplicação
        /// </summary>
        /// <param name="id">Id do grupo</param>
        /// <returns>Mensagem de exclusão</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Verificando se o grupo existe no DB
                var search_group = groupRepository.GetById(id);
                if (search_group == null)
                {
                    return NotFound();
                }

                groupRepository.Delete(id);

                // Retorna o código 200 e os dados do grupo alterado
                return Ok(new
                {
                    msg = "Grupo excluído com sucesso."
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
