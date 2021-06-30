using Entity.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Models
{
    public class LoginViewModel
    {
        public UserLoginDto userLoginDto { get; set; }
        public UserRegisterDto registerDto { get; set; }
        public ForgotPasswordDto forgotPasswordDto { get; set; }
    }
}
