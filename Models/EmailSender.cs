using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // در اینجا می‌توانید ایمیل‌ها را به سیستم لاگ ارسال کنید یا یک پیاده‌سازی واقعی ایجاد کنید.
        System.Diagnostics.Debug.WriteLine($"Sending email to {email} with subject {subject}");
        return Task.CompletedTask;
    }
}