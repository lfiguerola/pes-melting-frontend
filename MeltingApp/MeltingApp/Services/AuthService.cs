using MeltingApp.Interfaces;
using MeltingApp.Models;
using Xamarin.Forms;

namespace MeltingApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDataBaseService _dataBaseService;
        public AuthService()
        {
            _dataBaseService = DependencyService.Get<IDataBaseService>();
        }
        public Token GetCurrentToken()
        {
            return _dataBaseService.Get<Token>(t => true);
        }

        public void UpdateCurrentToken(Token token)
        {
            _dataBaseService.Clear<Token>();
            _dataBaseService.Insert(token);
        }
    }
}
