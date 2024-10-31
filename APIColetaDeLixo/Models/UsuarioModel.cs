namespace APIColetaDeLixo.Models
{
    public class UsuarioModel
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }

        public ICollection<ColetaModel>? Coletas { get; set; } = new List<ColetaModel>();
    }
}
