using System.Threading.Tasks;

namespace EmailService
{
    public interface IEmailEmitter
    {
        Task SendMailAsync(Email message);
    }
}
