
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
        public T Create<T>() 
        {
            return (T)Create(typeof(T));
        }

        private object Create(Type t) 
        {
            types.Push(t);
            var rand = new Random();
            GeneratorContext context;

            context = new GeneratorContext(rand, t);
            if (Generator.Generate(context) != null)
            {
                types.Pop();
                return Generator.Generate(context);
            }

            var properties = t.GetProperties();
            var constructors = t.GetConstructors();
            var fields = t.GetFields();
            constructors = constructors.OrderByDescending(x => x.GetParameters().Count()).ToArray();
            var paramList = new List<object>();
            object obj = new object();

            if (constructors.Length != 0)
            {
                foreach (var constructor in constructors)
                {
                    foreach (var param in constructor.GetParameters())
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
                        break;
                    }
                    catch
                    {
                        continue;
                    }
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
                    types.Pop();
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
                        types.Pop();
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
                    types.Pop();
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
                        types.Pop();
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
                    types.Pop();
                    return null;
                }
            }
            types.Pop();
            return obj;
        }
    }
}
