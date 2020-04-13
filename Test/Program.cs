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

    public class Program
    {
        public int Value { get; set; }

        //ConditionalWeakTable<>
        public static void Main(String[] args)
        {
            int a = 1;
            RefMeth(ref a);
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

        public void Test<T>() where T : Exception
        {
            try
            {
            }
            catch (T)
            {
            }
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
        public class Report
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
        [Benchmark]
        public void Test1()
        {
            for (int i = 0; i < 1000; i++)
            {
                JsonConvert.DeserializeObject<Report>(data);
            }
        }

        [Benchmark]
        public void Test2()
        {
            for (int i = 0; i < 1000; i++)
            {
                JsonConvert.DeserializeObject<CReport>(data);
            }
        }
    }

}
