
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Faker
{
    class Faker : IFaker
    {
        public T Create<T>() // публичный метод для пользователя
        {
            return (T)Create(typeof(T));
        }

        private object Create(Type t) // метод для внутреннего использования
        {


            var properties = t.GetProperties();
            var constructors = t.GetConstructors();
            var fields = t.GetFields();
            constructors = constructors.OrderBy(x => x.GetParameters().Count()).ToArray();
            var paramList = new List<object>();

            foreach (var param in constructors[^1].GetParameters())
            {
                paramList.Add(GetDefaultValue(param.ParameterType));
            }

            var obj = Activator.CreateInstance(t, paramList.ToArray());

            foreach (var property in properties)
            {
                property.SetValue(obj, GetDefaultValue(property.PropertyType));
            }
            foreach (var field in fields)
            {
                field.SetValue(obj, GetDefaultValue(field.FieldType));
            }

            return obj;
            // Процедура создания и инициализации объекта.


        }
        private static object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                // Для типов-значений вызов конструктора по умолчанию даст default(T).
                return Activator.CreateInstance(t);
            else
                // Для ссылочных типов значение по умолчанию всегда null.
                return null;
        }

    }
}
