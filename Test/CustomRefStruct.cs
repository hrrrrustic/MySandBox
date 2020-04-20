using System;

namespace Test
{
    public readonly ref struct CustomRefStruct
    {
        private readonly Int32 TestSer;

        public CustomRefStruct(Int32 val)
        {
            TestSer = val;
        }
    }
}