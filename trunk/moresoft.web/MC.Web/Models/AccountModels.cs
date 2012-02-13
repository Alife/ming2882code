using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace MC.Web.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "<=OldPassword>")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "<=NewPasswordErrorMessage>", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "<=NewPassword>")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "<=ConfirmPassword>")]
        [Compare("NewPassword", ErrorMessage = "<=ConfirmPasswordErrorMessage>")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "<=UserName>")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "<=Password>")]
        public string Password { get; set; }

        [Display(Name = "<=RememberMe>")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "<=UserName>")]
        [Remote("checkusernameexists", "account", ErrorMessage = "<=CheckUserNameExistsErrorMessage>")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "<=Email>")]
        [Remote("checkemailexists", "account", ErrorMessage = "<=CheckEmailExistsErrorMessage>")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "<=PasswordErrorMessage>", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "<=Password>")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "<=ConfirmPassword1>")]
        [Compare("Password", ErrorMessage = "<=ConfirmPassword1ErrorMessage>")]
        public string ConfirmPassword { get; set; }
    }
}
