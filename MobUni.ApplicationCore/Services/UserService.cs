using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Cryptography;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MobUni.ApplicationCore.Authorization;
using MobUni.ApplicationCore.DTOs;
using MobUni.ApplicationCore.DTOs.Requests;
using MobUni.ApplicationCore.Entities;
using MobUni.ApplicationCore.Entities.UserAggregate;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.ApplicationCore.Result.Abstract;
using MobUni.ApplicationCore.Result.Concrete;


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

        public async Task<IDataResult<UserDTO>> Add(CreateUserDTO dto)
        {

            try
            {
                var user = _mapper.Map<CreateUserDTO, User>(dto);
                var hashFunction = CreatePasswordHash(dto.Password);
                user.PasswordHash = hashFunction.Item1;
                user.PasswordSalt = hashFunction.Item2;
                user.CreateUserTime();
                await _userRepository.Add(user);
                return new SuccessDataResult<UserDTO>(_mapper.Map<User, UserDTO>(user));
            }
            catch (SqlException ex)
            {
                var c = ex.Errors;
                var b = ex.ErrorCode;
                var a = ex.Number;
                throw;
            }
            catch(DbUpdateException ex)
            {
                var a = ex.Data;
                var b = ex.Source;
                throw;
            }
        }
        public IDataResult<Token> Login(LoginUserDTO userDto)
        {
            var databaseUser=_userRepository.GetByUserName(userDto.UserName);
           if(databaseUser is null)
            {
                databaseUser = _userRepository.GetByEmail(userDto.Email);
                if (databaseUser is null)
                    return new ErrorDataResult<Token>("Kullanıcı adı/E-posta veya şifre yanlış",422);
            }
            var dtoPasswordBool = VerifyPasswordHash(userDto.Password,databaseUser.PasswordHash,databaseUser.PasswordSalt);
            if (dtoPasswordBool)
                return new SuccessDataResult<Token>(_jwtUtils.GenerateJwtToken(databaseUser));
            else return new ErrorDataResult<Token>("Kullanıcı adı/E-posta veya şifre yanlış",422);
        }


        public Task<bool> Delete(UserDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task< IDataResult<List<UserDTO>>> GetAll()
        {
            return new SuccessDataResult<List<UserDTO>>(_mapper.Map<List<User>, List<UserDTO>>(await _userRepository.GetAll()));
        }

        public  IDataResult<UserDTO> GetById(string userId)
        {
            return new SuccessDataResult<UserDTO>(_mapper.Map<User, UserDTO>(_userRepository.GetById(userId)));
        }

        public async Task<IDataResult<UserDTO>> Update(UserDTO dto)
        {
            //University veya Department'ı boş gönderirse değerler boş olur ve bu şekilde updatelenir veya veri tabanından verilen Id'ye göre yerleştirme yapılabilir.
            var dtoUser = _mapper.Map<User>(dto);
            var user = _userRepository.GetByIdAsNoTracking(dto.Id);
            dtoUser.UpdatedTime = DateTime.Now;
            dtoUser.CreatedTime = user?.CreatedTime;

            await _userRepository.UpdateAsync(dtoUser);
            return new SuccessDataResult<UserDTO>(_mapper.Map<User, UserDTO>(dtoUser));
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

        IDataResult<UserDTO> IService<UserDTO, CreateUserDTO>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<Token>> Register(CreateUserDTO userDto)
        {

            try
            {
                if (userDto.Password.Length < 6)
                    return new ErrorDataResult<Token>("Şifre uzunluğunun 6 karakterden uzun olması gerekmektedir.", 422);
                var user = await Add(userDto);
                if (user.Data == null)
                    return new ErrorDataResult<Token>(message:"Kullanıcı belirlenemeyen bir nedenden dolayı eklenemedi",statusCode:500);
                return new SuccessDataResult<Token>(_jwtUtils.GenerateJwtToken(_mapper.Map<UserDTO, User>(user.Data)));
            }
            catch(DbUpdateException ex)
            {
                var exMessage = ex.InnerException.Message;
                if(exMessage.Contains("IXU_Users_Email"))
                    return new ErrorDataResult<Token>(message: "Bu email adresi daha önceden alınmış", statusCode: 422);
                else if(exMessage.Contains("IXU_Users_UserName"))
                    return new ErrorDataResult<Token>(message: "Bu kullanıcı adı daha önceden alınmış", statusCode: 422);
                return new ErrorDataResult<Token>(message: ex.InnerException.Message, statusCode: 500);
                
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Token>(message: ex.InnerException.Message, statusCode: 500);
            }
        }
    }
}

