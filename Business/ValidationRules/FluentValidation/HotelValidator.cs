using Core.Utilities.Constants;
using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class HotelValidator : AbstractValidator<Hotel>
    {
        public HotelValidator()
        {
            RuleFor(p => p.Name).Must(BeValidUtf8).WithMessage(Messages.InvalidNameFormat);
            RuleFor(p => p.Url).Must(BeValidURL).WithMessage(Messages.InvalidUrlFormat);
            RuleFor(p => p.Star).GreaterThanOrEqualTo(0).WithMessage(Messages.InvalidRatingNumber);
            RuleFor(p => p.Star).LessThanOrEqualTo(5).WithMessage(Messages.InvalidRatingNumber);
        }

        private bool BeValidUtf8(string arg)
        {
            for (int i = 0; i < arg.Length; i++)
            {
                var unicode = char.GetUnicodeCategory(arg, i);
                if (unicode == UnicodeCategory.OtherNotAssigned) return false;
            }
            return true;
        }

        private bool BeValidURL(string arg)
        {
            return Uri.IsWellFormedUriString(arg, UriKind.Absolute);
        }
    }
}
