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

        public List<Pessoa> GetPessoas
        {
            get
            {
                return _pessoaDao.ObterPessoas();
            }
        }

        public void InserirPessoa(PessoaEnderecoTelefone pessoa)
        {
            {
                _pessoaDao.InserirPessoa(pessoa);
            }
        }
    }
}
