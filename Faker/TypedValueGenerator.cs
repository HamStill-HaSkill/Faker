using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public abstract class TypedValueGenerator<T> : IValueGenerator
    {
        object IValueGenerator.Generate(GeneratorContext context)
        {
            return Generate(context);
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(T);
        }

        protected abstract T Generate(GeneratorContext context);
    }
}
