using AstractTask.Domain.DTOs;
using AstractTask.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AstractTask.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<Response<string>> Register(RegisterDTO registerDto);

        Task<Response<LoginResponseDTO>> Login(LoginDTO loginDto);

        Task<Response<bool>> ValidateUser(LoginDTO loginDTO);

        string GetErrors(IdentityResult result);
    }
}