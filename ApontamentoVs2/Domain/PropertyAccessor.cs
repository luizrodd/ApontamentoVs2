using System.Linq.Expressions;

public static class PropertyAccessor
{
    public static Func<T, TProp> CreateGetter<T, TProp>(string propertyName)
    {
        var param = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(param, propertyName);
        var lambda = Expression.Lambda<Func<T, TProp>>(property, param);
        return lambda.Compile();
    }
}
