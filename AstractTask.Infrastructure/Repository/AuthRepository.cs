using AstractTask.Domain.DTOs;
using AstractTask.Domain.Entities;
using AstractTask.Domain.Enum;
using AstractTask.Domain.Interfaces;
using AstractTask.Infrastruture.Identity;
using AstractTask.Infrastruture.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace AstractTask.Infrastruture.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _generateToken;

        public AuthRepository(UserManager<ApplicationUser> userManager,
           IMapper mapper, ITokenService generateToken)
        {
            _userManager = userManager;
            _mapper = mapper;
            _generateToken = generateToken;
        }

        public string GetErrors(IdentityResult result)
        {
            return result.Errors.Aggregate(string.Empty, (current, err) => current + err.Description + "\n");
        }

        /// <summary>
        /// Logs Users Into Application
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>LoginResponseDTO</returns>
        public async Task<Response<LoginResponseDTO>> Login(LoginDTO loginDto)
        {
            var response = new Response<LoginResponseDTO>();
            var validatedResult = await ValidateUser(loginDto);

            if (!validatedResult.Succeeded)
            {
                response.Message = validatedResult.Message;
                response.StatusCode = validatedResult.StatusCode;
                response.Succeeded = false;
                return response;
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                var token = await _generateToken.GenerateToken(user);
                await _userManager.UpdateAsync(user);
                var result = new LoginResponseDTO()
                {
                    Id = user.Id,
                    Token = token
                };
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Login Successfully";
                response.Data = result;
                response.Succeeded = true;
                return response;
            }
        }

        public async Task<Response<string>> Register(RegisterDTO registerDto)
        {
            var user = _mapper.Map<ApplicationUser>(registerDto);
            user.UserName = registerDto.Email;
            var response = new Response<string>();

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.User.ToString());
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Succeeded = true;
                response.Data = user.Id;
                response.Message = "User created successfully!";
                return response;
            }
            else
            {
                response.Message = GetErrors(result);
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Succeeded = false;
                return response;
            }
        }

        public async Task<Response<bool>> ValidateUser(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            var response = new Response<bool>();
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                response.Message = "Invalid Credentials";
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;
            }
            else if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                response.Message = "Account not activated";
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                return response;
            }
            else
            {
                response.Succeeded = true;
                return response;
            }
        }
    }
}