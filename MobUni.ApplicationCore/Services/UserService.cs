using System;
using System.Security.Cryptography;
using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MobUni.ApplicationCore.Authorization;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities.UserAggregate;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Interfaces.Repositories;

namespace MobUni.ApplicationCore.Services
{
	public class UserService:IUserService
	{
        private readonly IUserRepository _userRepository;
       // private readonly IUniversityRepository _universityRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IMapper _mapper;
        private readonly IJwtUtils _jwtUtils;
		public UserService(IUserRepository userRepository,IMapper mapper,IDepartmentRepository departmentRepository, IUniversityRepository universityRepository,
            IJwtUtils jwtUtils)
		{
            _userRepository = userRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _universityRepository = universityRepository;
            _jwtUtils = jwtUtils;
        }

        public async Task<UserDTO> Add(CreateUserDTO dto)
        {
            var user = _mapper.Map<CreateUserDTO, User>(dto);
            var hashFunction=CreatePasswordHash(dto.Password);
            user.PasswordHash = hashFunction.Item1;
            user.PasswordSalt = hashFunction.Item2;
            user.CreateUserTime();
            user.Department = _departmentRepository.GetById(dto.DepartmentId);
            user.University = _universityRepository.GetById(dto.UniversityId);
            await _userRepository.Add(user);
            return _mapper.Map<User, UserDTO>(user);
        }
        public string Login(CreateUserDTO userDto)
        {
            var databaseUser=_userRepository.GetByUserName(userDto.UserName);
            if (databaseUser is null)
                throw new ArgumentNullException();
            var dtoPasswordBool = VerifyPasswordHash(userDto.Password,databaseUser.PasswordHash,databaseUser.PasswordSalt);
            if (dtoPasswordBool)
                return _jwtUtils.GenerateJwtToken(databaseUser);
            else return null;
        }


        public Task<bool> Delete(UserDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task< List<UserDTO>> GetAll()
        {
            return _mapper.Map<List<User>,List<UserDTO>>(await _userRepository.GetAll());
        }

        public  UserDTO GetById(string userId)
        {
            return _mapper.Map<User, UserDTO>( _userRepository.GetById(userId));
        }

        public async Task<UserDTO> Update(UserDTO dto)
        {
            //University veya Department'ı boş gönderirse değerler boş olur ve bu şekilde updatelenir veya veri tabanından verilen Id'ye göre yerleştirme yapılabilir.
            var dtoUser = _mapper.Map<User>(dto);
            var user = _userRepository.GetByIdAsNoTracking(dto.Id);
            dtoUser.UpdatedTime = DateTime.Now;
            dtoUser.CreatedTime = user?.CreatedTime;

            await _userRepository.UpdateAsync(dtoUser);
            return _mapper.Map<User, UserDTO>(dtoUser);
        }

        private (byte[],byte[]) CreatePasswordHash(string password)
        {
            byte[] passwordHash,passwordSalt; 
            
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            return (passwordHash, passwordSalt);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        UserDTO IService<UserDTO, CreateUserDTO>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Register(CreateUserDTO userDto)
        {
            var user = await Add(userDto);
            return _jwtUtils.GenerateJwtToken(_mapper.Map<UserDTO,User>(user));
        }
    }
}

