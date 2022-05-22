using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SC.SenseTower.Common.Validators
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> logger;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            this.validators = validators;
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(validators.Select(r => r.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(r => r != null).ToArray();

            if (failures.Length != 0)
            {
                logger.LogError(string.Join("; ", failures.Select(r => $"{r.ErrorCode}: ({r.PropertyName}) {r.ErrorMessage}")));
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
