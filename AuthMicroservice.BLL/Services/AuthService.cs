using AuthMicroservice.Domain.Abstract.Repository;
using AuthMicroservice.Domain.Abstract.Services;
using AuthMicroservice.Domain.Enums;
using AuthMicroservice.Domain.Models;
using AuthMicroservice.Domain.Models.Settings;
using Microsoft.Extensions.Options;
using System.Runtime;

namespace AuthMicroservice.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IHasher _hasher;
        private readonly IUserRepository _userRepository;
        private readonly IOptions<JWTOptions> _settings;

        public AuthService(ITokenService tokenService,
                           IHasher hasher, 
                           IUserRepository userRepository, 
                           IOptions<JWTOptions> settings)
        {
            _tokenService = tokenService;
            _hasher = hasher;
            _userRepository = userRepository;
            _settings = settings;
        }

        public string AnonUser()
        {
            var token = _tokenService.GenerateAnonymousAccessToken();
            return token;
        }

        public async Task<User> Authenticate(User model, bool isPassword = true)
        {
            var user = await _userRepository.GetByLogin(model.Login);

            if (user == null)
            {
                return null;
            }

            if (isPassword && !_hasher.Сompare(user.Password, model.Password))
            {
                return null;
            }

            user.Token = _tokenService.GenerateAccessToken(user);
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(_settings.Value.TokenLongLifeTime);
            user.RefreshToken = _tokenService.GenerateRefreshToken();

            await _userRepository.Update(user);
            return user;
        }

        public Task<bool> IsUserLoginExist(string login)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Registration(User model)
        {
            User newUser = new User
            {
                Login = model.Login,
                Password = _hasher.GetHash(model.Password),
                IsEmailСonfirm = model.IsEmailСonfirm,
                UserRoles = new List<UserRoles> { new UserRoles { User = model, Role = Role.User } }
            };

            var user = await _userRepository.Create(newUser);

            return user;
        }
    }
}
