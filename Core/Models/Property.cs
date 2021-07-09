using System;

namespace Core.Models
{
    /// <summary>
    ///  я тут понял что вместо всего этого можно было создавать объекты анонимного типа
    ///  но поздно! придется использовать это
    /// </summary>

    public class Property<T>
    {
        public T Value { get; set; }


        public static Type MakeGenericType(Type type)
            => typeof(Property<>).MakeGenericType(type);

        /// <summary>
        /// на женерик не обращайте внимания
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object CreateInstance(object value)
        {
            if (value is null)
                return null;
            var genericType = MakeGenericType(value.GetType());
            var ret = Activator.CreateInstance(genericType);
            genericType.GetProperty(nameof(Value)).SetValue(ret, value);
            return ret;
        }

        public static Type MakeGenericTypeFromPropertyName(string propertyName)
            => MakeGenericType(typeof(T).GetProperty(propertyName).PropertyType);

        public static Type MakeGenericTypeFromPropertyName(Type ownerType, string propertyName)
            => MakeGenericType(ownerType.GetProperty(propertyName).PropertyType);


        public static Type MakeGenericTypeFromValue(object value)
           => MakeGenericType(value.GetType());

        public static object GetValue(object property)
            => property.GetType().GetProperty("Value").GetValue(property);
    }
}
