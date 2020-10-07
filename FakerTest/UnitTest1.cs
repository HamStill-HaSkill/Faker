using NUnit.Framework;
using FakerLib;
using System;

namespace FakerTest
{
    public class Tests
    {
        private Faker faker;
        [SetUp]
        public void Setup()
        {
            RegisterGen.Register(new IntGen());
            RegisterGen.Register(new StrGen());
            RegisterGen.Register(new DateGen());
            RegisterGen.Register(new ListGen());

            RegisterGen.RegisterPlugins();

            faker = new Faker();
        }
        class Bar
        {
            public string DataBar { get; set; }
        }
        class Foo
        {
            public Bar bar;
        }

        class Foo2
        {
            private int Data;
            private bool flag = false;

            public Foo2(int data)
            {
                Data = data;
                flag = true;
            }
            public bool IsSetData()
            {
                return flag;
            }
        }
        class FooTwoConstuctors
        {
            private int Data;
            private bool constructor1 = false;
            private bool constructor2 = false;

            public FooTwoConstuctors()
            {
                constructor1 = true;
            }
            public FooTwoConstuctors(int data)
            {
                Data = data;
                constructor2 = true;
            }
            public bool IsSetData()
            {
                return constructor2;
            }
        }
        class Rec1
        {
            public Rec2 A;
        }

        class Rec2
        {
            public Rec1 B;
        }

        class PrivateConstructor
        {
            private PrivateConstructor()
            {

            }
        }

        [Test]
        public void StringPropertyTest()
        {
            Bar bar = faker.Create<Bar>();
            Assert.AreEqual(true, bar.DataBar.Length > 0);
        }
        [Test]
        public void ObjectFieldTest()
        {
            Foo foo = faker.Create<Foo>();
            Assert.AreNotEqual(null, foo.bar);
        }
        [Test]
        public void PrivateFieldTest()
        {
            Foo2 foo = faker.Create<Foo2>();
            Assert.AreEqual(true, foo.IsSetData());
        }
        [Test]
        public void TwoConstructorsTest()
        {
            FooTwoConstuctors foo = faker.Create<FooTwoConstuctors>();
            Assert.AreEqual(true, foo.IsSetData());
        }
        [Test]
        public void ReccursionTest()
        {
            Rec1 rec = faker.Create<Rec1>();
            Assert.AreEqual(null, rec.A.B);
        }
        [Test]
        public void PrivateConstructorTest()
        {
            PrivateConstructor obj = faker.Create<PrivateConstructor>();
            Assert.AreEqual(null, obj);
        }
    }
}