using BDP.Domain.Repositories.Exceptions;
using BDP.Domain.Repositories.Extensions.Exceptions;
using BDP.Domain.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace BDP.Web.Api.Filters;

/// <summary>
/// An http filter to handle application-sepcific exceptions
/// </summary>
public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    /// <inheritdoc/>
    public int Order => int.MaxValue - 10;

    /// <inheritdoc/>
    public void OnActionExecuting(ActionExecutingContext context)
    { }

    /// <inheritdoc/>
    public void OnActionExecuted(ActionExecutedContext context)
    {
        var message = context.Exception?.Message ?? "unknown error occured";
        var statusCode = context.Exception switch
        {
            // 400 - BadRequest
            InvalidDepositAmountException => StatusCodes.Status400BadRequest,
            AlreadyUsedUsernameException => StatusCodes.Status400BadRequest,
            AlreadyUsedEmailException => StatusCodes.Status400BadRequest,
            InvalidPaginationParametersException => StatusCodes.Status400BadRequest,
            InvalidRatingValueException => StatusCodes.Status400BadRequest,
            InvalidRangeException => StatusCodes.Status400BadRequest,
            ValidationAggregateException => StatusCodes.Status400BadRequest,

            // 401 - Unauthorized
            InvalidUsernameOrPasswordException => StatusCodes.Status401Unauthorized,
            InvalidConfirmationCredentials => StatusCodes.Status401Unauthorized,
            SecurityTokenException => StatusCodes.Status401Unauthorized,
            InsufficientBalanceException => StatusCodes.Status401Unauthorized,

            // 404 - NotFound
            NotFoundException => StatusCodes.Status404NotFound,
            NotEnoughStockException => StatusCodes.Status404NotFound,

            // 409 - Conflict
            FinancialRecordAlreadyVerifiedException => StatusCodes.Status409Conflict,
            TransactionAlreadyConfirmedException => StatusCodes.Status409Conflict,
            InvalidQuantityException => StatusCodes.Status409Conflict,
            PendingRequestExistsException => StatusCodes.Status409Conflict,
            PendingOrdersLeftException => StatusCodes.Status409Conflict,
            PendingPurchasesLeftException => StatusCodes.Status409Conflict,
            ItemAlreadyReviewedException => StatusCodes.Status409Conflict,

            // 500 - Internal error
            _ => -1
        };

        if (statusCode == -1)
            return;

        context.Result = new ObjectResult(new { message }) { StatusCode = statusCode };
        context.ExceptionHandled = true;
    }
}