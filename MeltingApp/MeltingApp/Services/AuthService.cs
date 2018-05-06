using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        public User GetCurrentLoggedUser()
        {
            var loggedUser = _dataBaseService.GetWithChildren<User>(user => user.dbId == App.LoginRequest.LoggedUserIdDb);
            return loggedUser;
        }

        public void SetCurrentLoggedUser(User user)
        {
            App.LoginRequest.IsLogged = user.Token != null;
            var dbUser = _dataBaseService.GetWithChildren<User>(u => u.email == user.email);
            if (dbUser != null)
            {
                dbUser.Token = user.Token;
            }
            else
            {
                dbUser = user;
            }
            App.LoginRequest.LoggedUserIdDb = _dataBaseService.UpdateWithChildren(dbUser);
            App.LoginRequest.LoggedUserIdBackend = dbUser.id;
            var allusers = _dataBaseService.GetCollectionWithChildren<User>(u => true);
        }

        public void RefreshToken(Token token)
        {
            if (!App.LoginRequest.IsLogged)
            {
                throw new UnauthorizedAccessException("User not logged in");
            }

            var tokenDecoded = new JwtSecurityToken(token.jwt);
            var loggedUser = GetCurrentLoggedUser();
            loggedUser.id = Int32.Parse(tokenDecoded.Claims.First(c => c.Type == "sub").Value);
            //loggedUser.Token = token;
            App.LoginRequest.LoggedUserIdBackend = loggedUser.id;
            _dataBaseService.UpdateWithChildren(loggedUser);

            var allusers = _dataBaseService.GetCollectionWithChildren<User>(u => true);
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
