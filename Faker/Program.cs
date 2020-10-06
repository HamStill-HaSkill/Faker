using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Faker
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterGen.Register(new IntGen());
            RegisterGen.Register(new StrGen());

            //Assembly asm = Assembly.LoadFrom("C:\\Users\\Xiaomi\\source\\repos\\Faker\\plugins\\LongGen.dll");
            //Type obj_type = asm.GetType($"{asm.GetTypes()[0]}", true, true);

            //IGenerator obj = (IGenerator)Activator.CreateInstance(obj_type);

            //RegisterGen.Register((IGenerator)obj);

            var faker = new Faker();
            Foo foo = faker.Create<Foo>();
            Moo moo = faker.Create<Moo>();
            Console.WriteLine(JsonConvert.SerializeObject(foo));
            Console.WriteLine(JsonConvert.SerializeObject(moo));
            //Bar bar = faker.Create<Bar>();
        }
    }
}
