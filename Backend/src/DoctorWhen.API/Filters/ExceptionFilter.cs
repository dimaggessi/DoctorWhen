using DoctorWhen.Communication.Responses;
using DoctorWhen.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace DoctorWhen.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is DoctorWhenException)
        {
            DoctorWhenCustomExceptions(context);
        }
        else
        {
            ThrowUnknownError(context);
        }
    }

    // Lançar exceção customizada
    private static void DoctorWhenCustomExceptions(ExceptionContext context)
    {
        if (context.Exception is ValidatorErrorsException)
        {
            ValidatorExceptionHandler(context);
        }
    }

    // Tratamento de exceção quando for algum erro de validação
    private static void ValidatorExceptionHandler(ExceptionContext context)
    {
        var validatorError = context.Exception as ValidatorErrorsException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ResponseErrorJson(validatorError.ErrorMessages));
    }

    // Lançar "erro desconhecido" quando for algum erro do sistema (evita vazar informações para o front-end)
    private static void ThrowUnknownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR));
    }
}
