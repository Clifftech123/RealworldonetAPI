﻿using Microsoft.AspNetCore.Diagnostics;

namespace RealworldonetAPI.Application.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler

    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
