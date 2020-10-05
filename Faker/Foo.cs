using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Faker
{
    class Foo
    {
        public string Data { get; private set; } = "book";
        public int Number { get; private set; } = 10;
        public string Info { get; set; } = "info";
        public int Id { get; set; } = 5;

        public int field = 2;
        public Foo(int number, string data)
        {
            Data = data;
            Number = number;
        }
        public Foo()
        {

        }

        public Foo(int number)
        {
            Number = number;
        }

    }
}
