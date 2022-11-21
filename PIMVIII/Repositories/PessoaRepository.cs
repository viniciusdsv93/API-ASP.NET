using PIMVIII.Dao;
using PIMVIII.Models;

namespace PIMVIII.Repositories
{
    public class PessoaRepository
    {
        private readonly PessoaDao _pessoaDao;

        public PessoaRepository()
        {
            _pessoaDao = new PessoaDao();
        }

        public List<Pessoa> BuscarPessoas
        {
            get
            {
                return _pessoaDao.BuscarPessoas();
            }
        }

        public Pessoa BuscarPessoaPorCpf(Int64 cpf)
        {
            {
                return _pessoaDao.BuscarPessoaPorCpf(cpf);
            }
        }

        public int InserirPessoa(PessoaEnderecoTelefone pessoa)
        {
            {
                var pessoaId = _pessoaDao.InserirPessoa(pessoa);
                return pessoaId;
            }
        }

        public bool DeletarPessoa(Int64 cpf)
        {
            {
                return _pessoaDao.DeletarPessoa(cpf);
            }
        }

        public bool AlterarPessoa(Int64 cpf, PessoaEnderecoTelefone pessoa)
        {
            {
                return _pessoaDao.AlterarPessoa(cpf, pessoa);
            }
        }
    }
}
