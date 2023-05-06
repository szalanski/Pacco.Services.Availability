﻿using Convey.WebApi.Exceptions;
using Pacco.Services.Availability.Application.Exceptions;
using Pacco.Services.Availability.Core.Exceptions;
using System;
using System.Net;

namespace Pacco.Services.Availability.Infrastructure.Exceptions
{
    internal sealed class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        public ExceptionResponse Map(Exception exception)
        => exception switch
        {
            DomainException ex => new ExceptionResponse(new { code = ex.Code, reson = ex.Message }, HttpStatusCode.BadRequest),
            AppException ex => new ExceptionResponse(new { code = ex.Code, reson = ex.Message }, HttpStatusCode.BadRequest),
            _ => new ExceptionResponse(new { code = "error", reson = "There was an error." }, HttpStatusCode.InternalServerError)
        };
    }
}
