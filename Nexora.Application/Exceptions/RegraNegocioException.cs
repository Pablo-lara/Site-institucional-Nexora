using System;
using System.Collections.Generic;
using System.Text;

namespace Nexora.Application.Exceptions;

public class RegraNegocioException : Exception
{
    public RegraNegocioException(string message)
        : base(message)
    {
        
    }
}
