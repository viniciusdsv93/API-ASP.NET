using Microsoft.AspNetCore.Mvc;
using PIMVIII.Models;
using PIMVIII.Repositories;

namespace PIMVIII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaRepository _pessoaRepository;

        public PessoaController()
        {
            _pessoaRepository = new PessoaRepository();
        }
        
        [HttpGet]
        public ActionResult<List<Pessoa>> BuscarPessoas()
        {
            var pessoas = _pessoaRepository.BuscarPessoas;
            return Ok(pessoas);
        }

        [HttpGet("{cpf}")]
        public ActionResult<List<Pessoa>> BuscarPessoaPorCpf(Int64 cpf)
        {
            var pessoa = _pessoaRepository.BuscarPessoaPorCpf(cpf);
            if (pessoa.Id != null)
            {
                return Ok(pessoa);
            }
            return NotFound("Não foi encontrada nenhuma pessoa cadastrada com o CPF informado");   
        }

        [HttpPost]
        public ActionResult<Pessoa> Post([FromBody] PessoaEnderecoTelefone pessoa)
        {
            var pessoaId = _pessoaRepository.InserirPessoa(pessoa);
            if (pessoaId < 0)
            {
                return BadRequest("Já existe uma pessoa cadastrada com o CPF informado");
            }
            else
            {
                return Ok("Pessoa cadastrada com sucesso com o Id: " + pessoaId);
            }
        }

        [HttpDelete("{cpf}")]
        public ActionResult DeletarPessoa(Int64 cpf)
        {
            var isDeleted = _pessoaRepository.DeletarPessoa(cpf);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound("Não foi encontrada nenhuma pessoa cadastrada com o CPF informado");
        }
    }
}