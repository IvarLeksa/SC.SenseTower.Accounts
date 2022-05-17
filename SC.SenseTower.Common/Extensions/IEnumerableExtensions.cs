namespace SC.SenseTower.Common.Extensions
{
    /// <summary>
    /// Дополнительные методы для классов, реализующих IEnumerable.
    /// </summary>
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> array, Action<T> action)
        {
            foreach (var item in array)
                action(item);
            return array;
        }
    }
}
