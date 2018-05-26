using System;
using System.Collections.Generic;
using System.Text;
using MeltingApp.Models;

namespace MeltingApp.Interfaces
{
    public interface IAuthService
    {
        Token GetCurrentToken();
        void UpdateCurrentToken(Token token);
    }
}
