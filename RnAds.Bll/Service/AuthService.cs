using Microsoft.IdentityModel.Tokens;
using RnAds.Bll.Options;
using RnAds.Core.Interfaces;
using RnAds.InterFaces.Interfaces;
using RnAds.Model.Dto;
using RnAds.Model.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RnAds.Bll.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _iAccountRepository;

        public AuthService(IAccountRepository iAccountRepository)
        {
            _iAccountRepository = iAccountRepository;
        }
        /// <summary>
        /// регистрация пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<User> CreateAccountAsync(RegisterDTO dto)
        {
            var check = await _iAccountRepository.ExistByNameAsync(dto);

            if (check)
                return null;
            else
            {
                User user = new User();
                user.Name = dto.Name;
                user.Email = dto.Email;
                user.Password = dto.Password;
                user.Phone = dto.Phone;
                user.Role = "user";

                await _iAccountRepository.CreateAsyncUser(user);

                return user;

            }
        }

        public string EncodedJwtToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
        public async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            var user = await _iAccountRepository.GetUserByLoginAndPasswordAsync(username, password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        /// <summary>
        /// получение id текущего пользователя
        /// </summary>
        /// <returns></returns>
        public async Task<Guid> GetCurrentUserIdAsync(string email)
        {
            var user = await _iAccountRepository.GetUserByLoginAsync(email);
            return user.Id;
        }

        /// <summary>
        /// получение имени  пользователя по id
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetSelectUserNameAsync(Guid id)
        {
            var user = await _iAccountRepository.GetByIdAsync(id);
            return user.Name;
        }

        /// <summary>
        /// получение текущего пользователя по email
        /// </summary>
        /// <returns></returns>
        public async Task<User> GetCurrentUser(string email)
        {
            var user = await _iAccountRepository.GetUserByLoginAsync(email);
            return user;
        }

        /// <summary>
        /// редактирование текущего пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task EditCurrentUser(DtoUser user)
        {
            var currentUser = await _iAccountRepository.GetByIdAsync(user.Id);
            if (currentUser != null)
            {
                currentUser.Name = user.Name;
                currentUser.Email = user.Email;
                currentUser.Password = user.Password;
                currentUser.Phone = user.Phone;

                await _iAccountRepository.UpdateAsync(currentUser);
            }
        }


    }
}
