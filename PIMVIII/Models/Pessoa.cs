namespace PIMVIII.Models
{
    public class Pessoa
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public Int64? Cpf { get; set; }

        public int? EnderecoId { get; set; }

        public Pessoa() { }
    }
}
