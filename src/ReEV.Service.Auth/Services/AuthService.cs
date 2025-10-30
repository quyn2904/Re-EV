using AutoMapper;
using ReEV.Service.Auth.DTOs;
using ReEV.Service.Auth.Repositories.Interfaces;

namespace ReEV.Service.Auth.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly UserService _userService;
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthService(
            IConfiguration configuration,
            IUserRepository userRepository,
            UserService userService,
            TokenService tokenService,
            IMapper mapper)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<UserDTO> Register(UserCreateDTO registerDto)
        {
            var existingUser = await _userRepository.GetByEmailOrPhoneAsync(registerDto.PhoneNumber);
            if (existingUser != null)
            {
                throw new BadHttpRequestException("Email or Phone number already exists.");
            }

            var newUserDto = await _userService.CreateUser(registerDto);

            return newUserDto;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginDto)
        {
            var user = await _userRepository.GetByEmailOrPhoneAsync(loginDto.Identifier);

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

            var tokens = await _tokenService.GenerateTokensAsync(user);

            return new LoginResponseDTO
            {
                TokenResponse = tokens,
                User = _mapper.Map<UserDTO>(user)
            };
        }
    }
}
