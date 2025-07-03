using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Server.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }       
    }    
}
