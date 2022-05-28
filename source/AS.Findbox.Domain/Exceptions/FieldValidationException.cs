using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Findbox.Domain.Exceptions
{
    public class FieldValidationException: DomainException
    {
        public FieldValidationException(string field) : base(string.Format(Messages.FieldValidationException, field.ToLower()))
        {
        }
    }
}
