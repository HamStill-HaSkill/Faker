using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public interface IFaker
    {
        public T Create<T>();

    }
}
