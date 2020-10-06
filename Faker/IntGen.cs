using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    class IntGen : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(int);
        }

        public object Generate(GeneratorContext context)
        {
            return context.Random.Next(0, 100);
        }
    }
}
