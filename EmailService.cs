using MimeKit;
using MailKit.Net.Smtp;

namespace HW_ASP_11._2
{
    public class EmailService
    {
        public async Task SendReminderEmailAsync(string email, string title, DateTime dueDate)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Todo App", "todo@app.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Напоминание о задаче";

            message.Body = new TextPart("plain")
            {
                Text = $"Напоминаем, что задача '{title}' должна быть выполнена до {dueDate.ToShortDateString()}."
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.example.com", 587, false);
            await client.AuthenticateAsync("your-email@example.com", "your-password");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
