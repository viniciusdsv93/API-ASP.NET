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
        
        [HttpGet(Name = "pessoa")]
        public ActionResult<IEnumerable<Pessoa>> Get()
        {
            return _pessoaRepository.GetPessoas;
        }

        [HttpPost]
        public void Post([FromBody] PessoaEnderecoTelefone pessoa)
        {
            _pessoaRepository.InserirPessoa(pessoa);
        }
    }
}