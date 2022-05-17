namespace SC.SenseTower.Common.Validators
{
    /// <summary>
    /// Валидатор модели.
    /// </summary>
    /// <typeparam name="T">Тип валидируемой модели</typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Валидация модели.
        /// </summary>
        /// <param name="model">Валидируемая модель.</param>
        /// <param name="data">Дополнительные необязательные параметры</param>
        /// <returns>True, если модель валидна</returns>
        Task<bool> ValidateAsync(T model, params object[] data);

        /// <summary>
        /// Валидация модели.
        /// </summary>
        /// <param name="model">Валидируемая модель.</param>
        /// <param name="data">Дополнительные необязательные параметры</param>
        /// <returns>True, если модель валидна</returns>
        bool Validate(T model, params object[] data);

        /// <summary>
        /// Чтение списка ошибок при последней валидации.
        /// </summary>
        /// <returns>Список сообщений об ошибках.</returns>
        IEnumerable<string> GetErrors();
    }
}
