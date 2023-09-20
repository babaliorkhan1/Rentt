using Microsoft.AspNetCore.Mvc;

namespace FinalBackend.Services
{
    public interface IMailService
    {
        public  Task Send(string from, string to, string link, string text, string subject);
        public  Task Send1(string from, string to, string text, string subject);
    }
}
