using Microsoft.AspNetCore.Mvc.ModelBinding;
using SC.SenseTower.Common.Extensions;

namespace SC.SenseTower.Common.Validators
{
    /// <summary>
    /// Базовый класс валидаторов моделей представления.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseViewModelValidator<T> : BaseValidator<T>
    {
        protected override void InternalValidate(T model, params object[] data)
        {
            var modelState = data[0] as ModelStateDictionary;
            modelState.Values
                .Where(r => r.ValidationState == ModelValidationState.Invalid)
                .SelectMany(r => r.Errors)
                .ForEach(r =>
                {
                    AddError(r.ErrorMessage);
                });
        }

        protected override async Task InternalValidateAsync(T model, params object[] data)
        {
            var modelState = data[0] as ModelStateDictionary;
            modelState.Values
                .Where(r => r.ValidationState == ModelValidationState.Invalid)
                .SelectMany(r => r.Errors)
                .ForEach(r =>
                {
                    AddError(r.ErrorMessage);
                });
            await Task.CompletedTask;
        }
    }
}
