using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    [DisplayName("使用者登入")]
    public class LoginVM:IValidatableObject
    {
        [Required]
        [MinLength(3)]
        [DisplayName("使用者")]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        [DisplayName("密碼")]
        public string Password { get; set; }

        public bool LoginCheck()
        {
            return (this.Username == "chi" && this.Password == "123456");
        }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (!this.LoginCheck())
            {
                yield return new ValidationResult("登入失敗", new string[] { "Username" });
                yield break;
            }

            yield return ValidationResult.Success;
        }
    }
}