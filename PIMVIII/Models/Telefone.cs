namespace PIMVIII.Models
{
    public class Telefone
    {
        public string Id { get; set; }
        public int Numero { get; set; }
        public int Ddd { get; set; }
        public TipoTelefone Tipo { get; set; }

        public Telefone(string id, int numero, int ddd, TipoTelefone tipo)
        {
            Id = id;
            Numero = numero;
            Ddd = ddd;
            Tipo = tipo;
        }
    }
}
