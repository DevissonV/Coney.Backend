using Coney.Backend.DTOs.Users;
using Coney.Backend.Models.Entities;
using Coney.Backend.Data.Repositories.Users;

namespace Coney.Backend.Services.Users
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

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return users.Select(user => new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de usuarios");
                throw new ApplicationException("Se produjo un error al obtener los usuarios, inténtelo de nuevo más tarde.");
            }
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    throw new KeyNotFoundException("Usuario no encontrado.");
                }
                return new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el usuario con ID {id}");
                throw new ApplicationException($"Se produjo un error al obtener el usuario con ID {id}, inténtelo de nuevo más tarde.");
            }
        }

        public async Task AddUserAsync(CreateUserDto userDto)
        {
            try
            {
                var existingUser = await _userRepository.GetByEmailAsync(userDto.Email);
                if (existingUser != null)
                {
                    throw new InvalidOperationException("El correo ya está en uso.");
                }

                var user = new User
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _userRepository.AddAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un nuevo usuario");
                throw new ApplicationException("Se produjo un error al crear el usuario, inténtelo de nuevo más tarde.");
            }
        }

        public async Task UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    throw new KeyNotFoundException("Usuario no encontrado.");
                }

                user.FirstName = updateUserDto.FirstName;
                user.LastName = updateUserDto.LastName;
                user.Password = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password);
                user.UpdatedAt = DateTime.UtcNow;

                await _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el usuario con ID {id}");
                throw new ApplicationException($"Se produjo un error al actualizar el usuario con ID {id}, inténtelo de nuevo más tarde.");
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
