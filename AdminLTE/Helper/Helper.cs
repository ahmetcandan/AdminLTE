using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;
using WebHelpers.Mvc5;

namespace AdminLTE
{
    public static class Helper
    {

    }

    public class UserException : Exception
    {
        public UserException()
            : base()
        {

        }

        public UserException(string message) 
            : base(message)
        {

        }

        public UserException(string message, Exception innerException)
            :base(message, innerException)
        {

        }
    }
}