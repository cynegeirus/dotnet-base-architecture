using Entities.Dtos.Requests;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class LoginValidator : AbstractValidator<LoginRequestDto>
{
    public LoginValidator()
    {
        RuleFor(p => p.Username).Length(2, 50).WithMessage("Kullanıcı adınız en az 2 karakter, en fazla 50 karakter olabilir.").NotEmpty().WithMessage("Username cannot be empty.");
        RuleFor(p => p.Password).Length(2, 50).WithMessage("Şifre bölümü en az 2, en fazla 50 karakter olabilir.").NotEmpty().WithMessage("Password cannot be empty.");
    }
}