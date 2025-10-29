using AutoMapper;
using ReEV.Service.Auth.DTOs;
using ReEV.Service.Auth.Repositories;

namespace ReEV.Service.Auth.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserService _userService; // Inject UserService như bạn yêu cầu
        private readonly IMapper _mapper;

        public AuthService(
            IUserRepository userRepository,
            UserService userService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UserDTO> Register(UserCreateDTO registerDto)
        {
            var existingUser = await _userRepository.GetByEmailOrPhoneAsync(registerDto.Email, registerDto.PhoneNumber);
            if (existingUser != null)
            {
                throw new BadHttpRequestException("Email or Phone number already exists.");
            }

            var newUserDto = await _userService.CreateUser(registerDto);

            return newUserDto;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginDto)
        {
            var user = await _userRepository.GetByEmailOrPhoneAsync(loginDto.Identifier, loginDto.Identifier);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            if (loginDto.Password != user.Password)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            if (user.Status == UserStatus.BANNED)
            {
                throw new UnauthorizedAccessException("Account is banned.");
            }

            return new LoginResponseDTO
            {
                JwtToken = "ahihi",
                ExpiresAt = DateTime.Now,
                User = _mapper.Map<UserDTO>(user)
            };
        }
    }
}
