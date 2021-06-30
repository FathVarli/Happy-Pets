﻿using Core.Utilities.Results;
using Entity.Dtos.UserDtos;

namespace Business.Abstract
{
    public interface IForgotPasswordService
    {
        
        IResult PasswordResetMail(ForgotPasswordDto forgotPasswordDto);
        IResult ValidatePasswordResetToken(int userId,string token);
        IResult CreateNewPassword(PasswordResetDto model,int id);
    }
}
