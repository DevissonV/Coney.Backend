using Coney.Backend.Models.Entities;
using Coney.Backend.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace Coney.Backend.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(UserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _userRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de usuarios");
                throw new ApplicationException("Se produjo un error al obtener los usuarios, inténtelo de nuevo más tarde.");
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    throw new KeyNotFoundException("Usuario no encontrado.");
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el usuario con ID {id}");
                throw new ApplicationException($"Se produjo un error al obtener el usuario con ID {id}, inténtelo de nuevo más tarde.");
            }
        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                var existingUser = await _userRepository.GetByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    throw new InvalidOperationException("El correo ya está en uso.");
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                await _userRepository.AddAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un nuevo usuario");
                throw new ApplicationException("Se produjo un error al crear el usuario, inténtelo de nuevo más tarde.");
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                await _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el usuario con ID {user.Id}");
                throw new ApplicationException($"Se produjo un error al actualizar el usuario con ID {user.Id}, inténtelo de nuevo más tarde.");
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            try
            {
                await _userRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el usuario con ID {id}");
                throw new ApplicationException($"Se produjo un error al eliminar el usuario con ID {id}, inténtelo de nuevo más tarde.");
            }
        }

    }
}
