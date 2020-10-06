using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public abstract class TypedValueGenerator<T> : IGenerator
    {
        object IGenerator.Generate(GeneratorContext context)
        {
            return Generate(context);
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(T);
        }

        public abstract T Generate(GeneratorContext context);
    }
}
