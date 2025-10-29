using AutoMapper;
using ReEV.Service.Auth.DTOs;
using ReEV.Service.Auth.Models;
using ReEV.Service.Auth.Repositories;

namespace ReEV.Service.Auth.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginationResult<UserDTO>> GetUsers(int page = 1, int pageSize = 10, string search = "")
        {
            var paginatedUsers = await _repository.GetAllAsync(page, pageSize, search);
            var userDtos = _mapper.Map<List<UserDTO>>(paginatedUsers.Items);
            return new PaginationResult<UserDTO>
            {
                Items = userDtos,
                TotalCount = paginatedUsers.TotalCount,
                TotalPages = paginatedUsers.TotalPages,
                Page = paginatedUsers.Page,
                Pagesize = paginatedUsers.Pagesize
            };
        }

        public async Task<UserDTO?> GetUserById(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO?> UpdateUser(Guid id, UserUpdateDTO userUpdateDto)
        {
            var userEntity = _mapper.Map<User>(userUpdateDto);

            var updatedUser = await _repository.UpdateAsync(id, userEntity);

            if (updatedUser == null)
            {
                return null;
            }
            return _mapper.Map<UserDTO>(updatedUser);
        }

        public async Task<UserDTO> CreateUser(UserCreateDTO userCreateDto)
        {
            var userEntity = _mapper.Map<User>(userCreateDto);

            var newUser = await _repository.CreateAsync(userEntity);

            return _mapper.Map<UserDTO>(newUser);
        }
    }
}
