
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FakerLib
{
    public class Faker : IFaker
    {
        private Stack<Type> types = new Stack<Type>();
        public T Create<T>() // публичный метод для пользователя
        {
            return (T)Create(typeof(T));
        }

        private object Create(Type t) // метод для внутреннего использования
        {
            types.Push(t);
            var rand = new Random();
            GeneratorContext context;

            var properties = t.GetProperties();
            var constructors = t.GetConstructors();
            var fields = t.GetFields();
            constructors = constructors.OrderBy(x => x.GetParameters().Count()).ToArray();
            var paramList = new List<object>();
            object obj;

            if (constructors.Length != 0)
            {
                foreach (var param in constructors[^1].GetParameters())
                {
                    context = new GeneratorContext(rand, param.ParameterType);
                    if (Generator.Generate(context) == null)
                    {
                        if (types.Contains(param.ParameterType))
                        {
                            continue;
                        }
                        paramList.Add(Create(param.ParameterType));
                        continue;
                    }
                    paramList.Add(Generator.Generate(context));
                }
                try
                {
                    obj = Activator.CreateInstance(t, paramList.ToArray());
                }
                catch
                {
                    return null;
                }
               
            }
            else
            {
                try
                {
                    obj = Activator.CreateInstance(t);
                }
                catch
                {
                    return null;
                }
                
            }
            foreach (var property in properties)
            {
                context = new GeneratorContext(rand, property.PropertyType);
                if (Generator.Generate(context) == null)
                {
                    if (types.Contains(property.PropertyType))
                    {
                        continue;
                    }

                    try
                    {
                        property.SetValue(obj, Create(property.PropertyType));
                    }
                    catch
                    {
                        return null;
                    }
                    continue;
                }

                try
                {
                    property.SetValue(obj, Generator.Generate(context));
                }
                catch
                {
                    return null;
                }
            }
            foreach (var field in fields)
            {
                context = new GeneratorContext(rand, field.FieldType);
                if (Generator.Generate(context) == null)
                {
                    if (types.Contains(field.FieldType))
                    {
                        continue;
                    }
                    try
                    {
                        field.SetValue(obj, Create(field.FieldType));
                    }
                    catch
                    {
                        return null;
                    }
                    continue;
                }
                try
                {
                    field.SetValue(obj, Generator.Generate(context));
                }
                catch
                {
                    return null;
                }
            }
            types.Pop();
            return obj;
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
