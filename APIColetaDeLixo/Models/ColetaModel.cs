using Microsoft.AspNetCore.Mvc;

namespace APIColetaDeLixo.Models
{
    public class ColetaModel 
    {
        public int? Id { get; set; }
        public DateTime? DataColeta { get; set; }
        public string? Local { get; set; }
        public string? TipoResiduo { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel? Usuario { get; set; }
    }
}
