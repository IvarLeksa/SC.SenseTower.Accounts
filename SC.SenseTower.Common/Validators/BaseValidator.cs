namespace SC.SenseTower.Common.Validators
{
    /// <summary>
    /// Базовый класс валидаторов.
    /// </summary>
    public abstract class BaseValidator<T> : IValidator<T>
    {
        private readonly IList<string> messages = new List<string>();

        /// <summary>
        /// Очистка списка сообщений об ошибках валидации.
        /// </summary>
        protected void ClearErrors()
        {
            messages.Clear();
        }

        /// <summary>
        /// Добавление сообщения об ошибке валидации.
        /// </summary>
        /// <param name="errorMessage"></param>
        protected void AddError(string errorMessage)
        {
            messages.Add(errorMessage);
        }

        private bool HasErrors()
        {
            return GetErrors().Count() > 0;
        }

        public async Task<bool> ValidateAsync(T model, params object[] data)
        {
            ClearErrors();
            try
            {
                await InternalValidateAsync(model, data);
            }
            catch(Exception ex)
            {
                AddError(ex.Message);
            }
            return !HasErrors();
        }

        public bool Validate(T model, params object[] data)
        {
            ClearErrors();
            try
            { 
                InternalValidate(model, data);
            }
            catch (Exception ex)
            {
                AddError(ex.Message);
            }
            return !HasErrors();
        }

        public virtual IEnumerable<string> GetErrors()
        {
            return messages;
        }

        /// <summary>
        /// Асинхронная валидация модели.
        /// </summary>
        /// <typeparam name="T">Тип валидируемой модели.</typeparam>
        /// <param name="model">Валидируемая модель.</param>
        /// <param name="data">Доплнительные параметры.</param>
        /// <returns>True, если модель валидна.</returns>
        protected abstract Task InternalValidateAsync(T model, params object[] data);

        /// <summary>
        /// Синхронная валидация модели.
        /// </summary>
        /// <typeparam name="T">Тип валидируемой модели.</typeparam>
        /// <param name="model">Валидируемая модель.</param>
        /// <param name="data">Доплнительные параметры.</param>
        /// <returns>True, если модель валидна.</returns>
        protected abstract void InternalValidate(T model, params object[] data);
    }
}
