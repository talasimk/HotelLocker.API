using HotelLocker.Common.Exceptions;
using HotelLocker.WEB.ErrorCodes;
using HotelLocker.WEB.ResponseHelpers;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HotelLocker.WEB.Middlewares
{
    public class ValidationExceptionHandliingMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionHandliingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                HandleException(context, ex);
            }
        }

        private void HandleException(HttpContext context, Exception ex)
        {
            string exceptionResponse;
            if (ex.GetType() == typeof(NotFoundException))
            {
                exceptionResponse = ResponseCreator.CreateBadResponse((int)ErrorCode.NotFoundError,
                                                                       ex.Message);
            }
            else if (ex.GetType() == typeof(DBException))
            {
                exceptionResponse = ResponseCreator.CreateBadResponse((int)ErrorCode.DBError,
                                                                       ex.Message);
            }
            else if (ex.GetType() == typeof(PermissionException))
            {
                exceptionResponse = ResponseCreator.CreateBadResponse((int)ErrorCode.PermissionError,
                                                                       ex.Message);
            }
            else if (ex.GetType() == typeof(ValidationException))
            {
                exceptionResponse = ResponseCreator.CreateBadResponse((int)ErrorCode.ValidationError,
                                                                       ex.Message);
            }
            else
            {
                exceptionResponse = ResponseCreator.CreateBadResponse((int)ErrorCode.ServerError,
                                                                        "server error");
            }
            ErrorResposeWriter.WriteExceptionResponse(context, exceptionResponse);
        }
    }
}
