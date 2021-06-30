using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Dtos.UserDtos;
using Entity.Enum;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IUserDal _userDal;
        private IUserOperationClaimDal _userOperationClaimDal;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUserDal userDal, IUserOperationClaimDal userOperationClaimDal)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userDal = userDal;
            _userOperationClaimDal = userOperationClaimDal;
        }
        public IDataResult<User> Login(UserLoginDto userLoginDto)
        {
            var userToCheck = _userService.GetByEmail(userLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>("Hesap bulunamadı, lütfen kayıt olunuz.!");
            }

            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>("Email yada şifreniz yanlıştır.");
            }

            return new SuccessDataResult<User>(userToCheck);

        }

        public IDataResult<User> Register(UserRegisterDto registerDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);
            var existEmail = _userDal.Get(u => u.Email == registerDto.Email);
            if (existEmail != null)
            {
                return new ErrorDataResult<User>("Bu mail adresi kullanılmaktadır.");
            }
            var user = new User
            {
                FirstName = registerDto.FirstName.ToUpper(),
                LastName = registerDto.LastName.ToUpper(),
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CityId = 1,
                DistrictId = 1
            };

            _userDal.Add(user);

            var savedUser = _userService.GetByEmail(registerDto.Email);

            var userClaim = new UserOperationClaim
            {
                UserId = savedUser.Id,
                OperationClaimId = (int)EnmRol.User
            };

            _userOperationClaimDal.Add(userClaim);
            return new SuccessDataResult<User>(user);

        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByEmail(email) != null)
            {
                return new ErrorResult("User already exists");
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaim(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken);
        }

        public User UserExistsId(int id)
        {
            var user = _userService.GetById(id);
            return user ?? null;
        }



        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult("UserDeleted");
        }

        public IDataResult<User> UserUpdate(User userToCheck, UserUpdateDto userUpdateDto, string password = null)
        {
            if (userUpdateDto.Email != userToCheck.Email)
            {
                userToCheck.Email = userUpdateDto.Email;
            }
            else
            {
                return new ErrorDataResult<User>("UserAlreadyExists");
            }
            userToCheck.FirstName = userUpdateDto.FirstName;
            userToCheck.LastName = userUpdateDto.LastName;

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            userToCheck.PasswordHash = passwordHash;
            userToCheck.PasswordSalt = passwordSalt;


            _userDal.Update(userToCheck);
            return new SuccessDataResult<User>("UserUpdated");

        }
    }
}
