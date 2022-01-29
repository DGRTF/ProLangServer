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
    public async Task SendMessage(string link, string email)
    {
        var text = $"Подтвердите регистрацию, перейдя по ссылке: <a href='{link}'>Подтвердить</a>";
        await SendHtml(email, _options.ConfirmEmail.Subject, text);
    }

    /// <inheritdoc />
    public async Task SendChangePasswordLink(string link, string email)
    {
        var text = $"Поменяйте пароль, перейдя по ссылке: <a href='{link}'>Подтвердить</a>";
        await SendHtml(email, _options.NewPassword.Subject, text);
    }

    /// <inheritdoc />
    public async Task SendNewPassword(string password, string email)
    {
        var text = $"Ваш новый пароль: {password}";
        await SendHtml(email, _options.NewPassword.Subject, text);
    }

    private async Task SendHtml(string email, string subject, string text)
    {
        try
        {
            var confirmEmail = _options.ConfirmEmail;
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(confirmEmail.By, _options.Email));
            emailMessage.To.Add(new MailboxAddress(confirmEmail.To, email));
            emailMessage.Subject = subject;

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = text,
            };

            var smtpOptions = _options.Host.Smtp;

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpOptions.Address, smtpOptions.Port, true);
                await client.AuthenticateAsync(_options.Email, _options.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
        catch
        {
            throw new Exception("Внутренняя ошибка сервера");
        }
    }
}