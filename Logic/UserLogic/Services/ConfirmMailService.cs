using MailKit.Net.Smtp;
using MimeKit;
using UserLogic.Models.JSON;
using UserLogic.Services.Interfaces;

namespace UserLogic.Services;

/// <inheritdoc />
public class ConfirmMailService : IConfirmMailService
{
    private readonly EmailOptions _options;

    public ConfirmMailService(EmailOptions options)
    {
        _options = options;
    }

    /// <inheritdoc />
    public async Task<bool> SendMessage(string uri, string email)
    {
        try
        {
            var confirmEmail = _options.ConfirmEmail;
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(confirmEmail.By, _options.Email));
            emailMessage.To.Add(new MailboxAddress(confirmEmail.To, email));
            emailMessage.Subject = confirmEmail.Subject;

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"Подтвердите регистрацию, перейдя по ссылке: <a href='{uri}'>Подтвердить</a>"
            };

            var smtpOptions = _options.Host.Smtp;

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpOptions.Address, smtpOptions.Port, true);
                await client.AuthenticateAsync(_options.Email, _options.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }

            return true;
        }
        catch(Exception e)
        {
            return false;
        }
    }
}