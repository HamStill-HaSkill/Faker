using System;
using Newtonsoft.Json;

namespace Faker
{
    class Program
    {
        static void Main(string[] args)
        {
            var faker = new Faker();
            Foo foo = faker.Create<Foo>();
            Console.WriteLine(JsonConvert.SerializeObject(foo));
            //Bar bar = faker.Create<Bar>();
        }
    }
}
