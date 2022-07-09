using System.Threading.Tasks;

namespace DRX.Services.EmailService
{
    public interface IEmailService
    {
        Task<dynamic> GetTokenForForgotPasswordAsync(string email, string key);
        Task SendRentFinishedEmailAsync(int userId);
        Task SendRentMadeEmailAsync(int userId);
        Task SendCreatedEmailAsync(string emailTo);
        Task SendForgotPasswordEmailAsync(string emailTo);
    }
}