namespace PIMVIII.Models
{
    public class PessoaEnderecoTelefone
    {
        public string? Nome { get; set; }
        public Int64? Cpf { get; set; }
        public string? Logradouro { get; set; }
        public int? Numero { get; set; }
        public int? Cep { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public int? NumeroTelefone { get; set; }
        public int? Ddd { get; set; }
        public string? TipoTelefone { get; set; }

        public PessoaEnderecoTelefone() { }
    }
}
