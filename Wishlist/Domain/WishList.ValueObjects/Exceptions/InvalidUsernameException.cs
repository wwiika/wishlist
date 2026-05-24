using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WishList.ValueObjects.Exceptions
{
    public class InvalidUsernameException : ArgumentException
    {
        public InvalidUsernameException(string message) : base(message)
        {
        }
    }
}
