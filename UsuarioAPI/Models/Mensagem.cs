using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace UsuarioAPI.Models
{
    public class Mensagem
    {
        public List<MailboxAddress> Destinataio { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Mensagem(IEnumerable<string> destinatario, string assunto, int usuarioId, string codigo)
        {
            Destinataio = new List<MailboxAddress>();
            Destinataio.AddRange(destinatario.Select(d => new MailboxAddress(d)));
            Assunto = assunto;
            Conteudo = $"http://localhost:6000/ativa?UsarioId={usuarioId}&CodigoDeAtivacao={codigo}";
        }
    }
}
