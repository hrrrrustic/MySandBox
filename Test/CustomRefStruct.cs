using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    class MyClass
    {
        public unsafe ref partial struct CustomRefStruct
        {
            private readonly Int32 TestSer;
            private readonly Int32 TestSer2;
            private String Test { get; }
            private readonly List<Int32> List;

            public CustomRefStruct(Int32 val, Int32 val2)
            {
                TestSer = val;
                TestSer2 = val2;
                Test = "rand";
                List = Enumerable.Range(1, 15).ToList();
            }
        }
    }
    
}