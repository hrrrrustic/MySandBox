using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Management;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;


namespace Test
{
    public ref struct Test
    {
        public Int32 Field1;
        public Int32 Field2;
        public Int32 Field3;

        public Test(int f1)
        {
            Field1 = 5;
            Field3 = 9;
            Field2 = 7;
        }

        public void M1() { }
        public void M2() { }
        public void M3() { }
        
    }

    public class R
    {
        public Int32 Field1 = 5;
        public Int32 Field2 = 7;
        public Int32 Field3 = 9;
    }
    public struct JustStruct { }

    public delegate object RefStructDel(Test test);
    public delegate object CommonStructDel(JustStruct test);
    public delegate object IntDel(int test);

    public static class ObjectExt
    {
        static ObjectExt()
        {
            throw new Exception();
        }
        public static object Ext(this object src)
        {
            return src;
        }
    }

    public class JustClass
    {
        public readonly int A = 5;

        public JustClass(int t)
        {
            A = t;
        }

    }
    // public class DerivedJustClass : JustClass
    //{
    // public DerivedJustClass()
    //  {
    //}
    //}
    public readonly ref struct Test<T> where T : struct
    {

    }
    public class Program
    {
        public int Value { get; set; }

        //ConditionalWeakTable<>
        public static void Main(String[] args)
        {
            new RefSerializator().BuildFieldsPrinter(typeof(CustomRefStruct));

            //BenchmarkRunner.Run<SpanVSStringBuilder>();
            //var res = Test<MyEnum>();
            //var res2 = Test<MyEnum2>();
            //Console.WriteLine(res + "     :     " + res2);
            //int a = 1;
            //RefMeth(ref a);
            //var jc = new JustClass();
            //Console.WriteLine(jc.A);
            //jc.GetType().GetFields().First().SetValue(jc, 1);
            //Console.WriteLine(jc.A);
            //int a = 5;
            //Nullable<Int32> value = 4;
            //TestNullable(value);
            //BenchmarkRunner.Run<Bench>();
            //object res = //BoxInt();
            //BoxCommonStruct();
            //BoxRefStruct();
            //RefMeth(Value);

        }

        public static string Switch(MyEnum val) =>
            val switch
            {
                MyEnum.First => "1",
                MyEnum.Second => "2",
                MyEnum.Third => "3",
                {} => "",
                //_ => "t"
            };
    
        
        public enum MyEnum
        {
            First = 1,
            Second = 2,
            Third = 3,
        }
        public enum MyEnum2
        {
            First = 1
        }
        public static string Test<T>() where T : struct, Enum
        {
            return nameof(T);
        }

        public static void RefMeth(ref int val)
        {
            dynamic t = new ExpandoObject();
            t.e = 4;
            t.q = ";;";
            foreach (var VARIABLE in t)
            {
                Console.WriteLine(VARIABLE);   
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TestNullable(object obj)
        {

        }
        public static object BoxRefStruct()
        {
            Type refStructType = typeof(Test);
            DynamicMethod method = new DynamicMethod("boxRefStruct", typeof(object), new[] { refStructType });

            var generator = method.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Box, refStructType);
            generator.Emit(OpCodes.Ret);

            var del = (RefStructDel)method.CreateDelegate(typeof(RefStructDel));
            return del.Invoke(new Test());
        }
        public static object BoxCommonStruct()
        {
            Type structType = typeof(JustStruct);
            DynamicMethod method = new DynamicMethod("boxStruct", typeof(object), new[] { structType });
            var generator = method.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Box, structType);
            generator.Emit(OpCodes.Ret);

            var del = (CommonStructDel)method.CreateDelegate(typeof(CommonStructDel));
            return del.Invoke(new JustStruct());
        }

        public static object BoxInt()
        {
            Type structType = typeof(int);
            DynamicMethod method = new DynamicMethod("boxStruct", typeof(object), new[] { structType });
            var generator = method.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Box, structType);
            generator.Emit(OpCodes.Ret);

            var del = (IntDel)method.CreateDelegate(typeof(IntDel));
            return del.Invoke(6);
        }

        public static void TestBound(int[] source, int pos)
        {
            if ((uint)pos < (uint)source.Length)
            {
                source[pos] = 0;
            }
        }
    }

    public class Bench
    {
        /*public class Report
        {
            public int Count { get; set; }
            public List<Changesets> value { get; set; }
        }

        public class Changesets
        {
            public int changesetId { get; set; }
            public DateTime createdDate { get; set; }

        }
        public struct CReport
        {
            public int Count { get; set; }
            public List<Changesets> value { get; set; }
        }

        public struct CChangesets
        {
            public int changesetId { get; set; }
            public DateTime createdDate { get; set; }

        }

        private string data = File.ReadAllText("C:\\Users\\hrrrrustic\\Downloads\\data.json");
        //[Benchmark]
        public void Test1()
        {
            for (int i = 0; i < 1000; i++)
            {
                JsonConvert.DeserializeObject<Report>(data);
            }
        }

        //[Benchmark]
        public void Test2()
        {
            for (int i = 0; i < 1000; i++)
            {
                JsonConvert.DeserializeObject<CReport>(data);
            }
        }*/

        public class TestClass
        {
            public string S1 { get; set; } = "TEST";
            public string S2 { get; set; } = "TEST";
            public string S3 { get; set; } = "TEST";
            public string S4 { get; set; } = "TEST";
            public string S5 { get; set; } = "TEST";
            public string S6 { get; set; } = "TEST";
            public int I1 { get; set; } = 1;
            public int I2 { get; set; } = 1;
            public int I3 { get; set; } = 1;
        }
        public ref struct TestRefStruct
        {
            public string S1 { get; set; }
            public string S2 { get; set; }
            public string S3 { get; set; }
            public string S4 { get; set; }
            public string S5 { get; set; }
            public string S6 { get; set; }
            public int I1 { get; set; }
            public int I2 { get; set; }
            public int I3 { get; set; }

            public TestRefStruct(string str)
            {
                S1 = "TEST";
                S2 = "TEST";
                S3 = "TEST";
                S4 = "TEST";
                S5 = "TEST";
                S6 = "TEST";
                I1 = 1;
                I2 = 1;
                I3 = 1;
            }
        }

        [Benchmark]
        public void TestClassWork()
        {
            var instance = new TestRefStruct("ff");
            for (int i = 0; i < 1000; i++)
            {
                MethodRefStruct(instance);
            }
        }

        public void MethodRefStruct(TestRefStruct instance)
        {
            string str;
            int i;
            for (int j = 0; j < 10000; j++)
            {
                i = instance.I1;
                i = instance.I2;
                i = instance.I3;
                str = instance.S1;
                str = instance.S2;
                str = instance.S3;
                str = instance.S4;
                str = instance.S5;
                str = instance.S6;
            }
            for (int j = 0; j < 10000; j++)
            {
                instance.I1 = j;
                instance.I2 = j;
                instance.I3 = j;
                instance.S1 = "TEST" + j;
                instance.S2 = "TEST" + j;
                instance.S3 = "TEST" + j;
                instance.S4 = "TEST" + j;
                instance.S5 = "TEST" + j;
                instance.S6 = "TEST" + j;
            }
        }

        [Benchmark]
        public void TestRefStructWork()
        {
            var instance = new TestClass();
            for (int i = 0; i < 1000; i++)
            {
                MethodClass(instance);
            }
        }

        public void MethodClass(TestClass instance)
        {
            string str;
            int i;
            for (int j = 0; j < 10000; j++)
            {
                i = instance.I1;
                i = instance.I2;
                i = instance.I3;
                str = instance.S1;
                str = instance.S2;
                str = instance.S3;
                str = instance.S4;
                str = instance.S5;
                str = instance.S6;
            }

            for (int j = 0; j < 10000; j++)
            {
                instance.I1 = j;
                instance.I2 = j;
                instance.I3 = j;
                instance.S1 = "TEST" + j;
                instance.S2 = "TEST" + j;
                instance.S3 = "TEST" + j;
                instance.S4 = "TEST" + j;
                instance.S5 = "TEST" + j;
                instance.S6 = "TEST" + j;
            }
        }
    }

}
