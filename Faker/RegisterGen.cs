using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public static class RegisterGen
    {
        public static void Register(IGenerator gen)
        {
            Generator.generators.Add(gen);
        }
    }
}
