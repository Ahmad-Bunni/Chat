using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Domain.Interface;
using ChatApp.Domain.Model;
using ChatApp.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ChatApp.Domain.Serivce
{
    public class UserService : IUserService
    {
        private readonly ICosmosRepository<User> _cosmosRepository;
        private readonly AppSettings _appSettings;

        public UserService(ICosmosRepository<User> cosmosRepository, IOptions<AppSettings> appSettings)
        {
            _cosmosRepository = cosmosRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<Authentication> Authenticate(string username)
        {
            var user = await FindUser(username);

            // return null if user with the same username exists
            if (user != null)
                return null;

            await JoinGroup(null, username, null);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);



            return new Authentication
            {
                ExpirationDate = tokenDescriptor.Expires,
                Token = tokenHandler.WriteToken(token)
            };
        }

        public async Task<User> FindUser(string username)
        {
            try
            {
                var user = await _cosmosRepository.FindItemAsync(x => x.Username.ToLower() == username.ToLower());
                if (user != null)
                {
                    return user;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllGroupUsers(string groupName)
        {
            try
            {
                return await _cosmosRepository.GetItemsAsync(x => x.GroupName == groupName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task JoinGroup(string groupName, string username, string connectionId)
        {
            try
            {
                var user = await _cosmosRepository.FindItemAsync(x => x.Username == username);
                if (user != null)
                {
                    user.GroupName = groupName;
                    user.ConnectionId = connectionId;

                    await _cosmosRepository.UpdateItemAsync(user);
                }
                else
                {
                    await _cosmosRepository.InsertItemAsync(new User { Username = username, GroupName = groupName, ConnectionId = connectionId });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task LeaveGroup(string userId, string username)
        {
            try
            {
                await _cosmosRepository.RemoveItemAsync(userId, username);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
