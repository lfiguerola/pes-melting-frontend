using System;
using System.Collections.Generic;
using System.Text;
using MeltingApp.Models;

namespace MeltingApp.Interfaces
{
    public interface IAuthService
    {
        User GetCurrentLoggedUser();
        void SetCurrentLoggedUser(User user);
        void RefreshToken(Token token);

        Token GetCurrentToken();
        void UpdateCurrentToken(Token token);
    }
}
