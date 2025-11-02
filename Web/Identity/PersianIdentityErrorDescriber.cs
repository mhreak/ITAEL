using Microsoft.AspNetCore.Identity;

namespace Web.Identity
{
    public class PersianIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
            => new IdentityError()
            {
                Code = nameof(DuplicateEmail),
                Description = "این ایمیل قبلا توسط شخص دیگری انتخاب شده است"
            };

        public override IdentityError DuplicateUserName(string userName)
            => new IdentityError()
            {
                Code = nameof(DuplicateUserName),
                Description = "این نام کاربری قبلا توسط شخص دیگری انتخاب شده است"
            };

        public override IdentityError InvalidEmail(string email)
            => new IdentityError()
            {
                Code = nameof(InvalidEmail),
                Description = "ایمیل واردشده نامعتبر است"
            };

        public override IdentityError DuplicateRoleName(string role)
            => new IdentityError()
            {
                Code = nameof(DuplicateRoleName),
                Description = "این نقش قبلا تعریف شده است"
            };
    }
}
