using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthExampleClient.DTOs.Login;
using AuthExampleClient.DTOs.Token;

namespace AuthExampleClient.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> AuthenticateAsync(string endpoint,Login login);
        void SetTokenInCookie(Token token);
        void RemoveTokenFromCookie();
    }
}
