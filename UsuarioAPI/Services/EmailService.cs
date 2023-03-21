using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using UsuarioAPI.Models;

namespace UsuarioAPI.Services
{
    public class EmailService
    {

        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void EnviarEmailDeConfirmacao(string[] emails, string assunto, int UsuarioId, string code)
        {
            Mensagem mensagem = new Mensagem(emails, assunto, UsuarioId, code);
            MimeMessage mensagemEmail = CriaCorpoEmail(mensagem);
            Enviar(mensagemEmail);
        }

        public MimeMessage CriaCorpoEmail(Mensagem mensagem)
        {
            var mensagemEmail = new MimeMessage();
            mensagemEmail.From.Add(new MailboxAddress(_configuration.GetValue<string>("EmailSettings:From")));
            mensagemEmail.To.AddRange(mensagem.Destinataio);
            mensagemEmail.Subject = mensagem.Assunto;
            mensagemEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = mensagem.Conteudo
            };
            return mensagemEmail;
        }

        private void Enviar(MimeMessage mensagemEmail)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"),
                                   _configuration.GetValue<int>("EmailSettings:Port"),
                                   true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"), _configuration.GetValue<string>("EmailSettings:Password"));

                    client.Send(mensagemEmail);
                }
                catch (Exception ex) { }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
                
            }
        }
    }
}
