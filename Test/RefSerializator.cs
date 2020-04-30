using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Test
{
    public unsafe ref struct RefSerializator<T> where T : unmanaged
    {
        public Delegate BuildFieldsPrinter(Type type)
        {
            var method = new DynamicMethod(Guid.NewGuid().ToString(),
                                           typeof(void),
                                           new[] { type }, type);
            var il = method.GetILGenerator();

            var fieldValue = il.DeclareLocal(typeof(object));

            var toStringMethod = typeof(object).GetMethod("ToString");

            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            
            foreach (var field in fields)
            {
                il.Emit(OpCodes.Ldstr, field.Name + ": {0}");
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, field);

                if (field.FieldType.IsValueType)
                    il.Emit(OpCodes.Box, field.FieldType);

                il.Emit(OpCodes.Dup);
                il.Emit(OpCodes.Stloc, fieldValue);

                var notNullLabel = il.DefineLabel();

                il.Emit(OpCodes.Brtrue, notNullLabel);
                il.Emit(OpCodes.Ldstr, "null");

                var printedLabel = il.DefineLabel();

                il.Emit(OpCodes.Br, printedLabel);

                il.MarkLabel(notNullLabel);

                il.Emit(OpCodes.Ldloc, fieldValue);
                il.EmitCall(OpCodes.Callvirt, toStringMethod, null);

                il.MarkLabel(printedLabel);

                var writeLineMethod = typeof(Console).GetMethod("WriteLine", new[] { typeof(string), typeof(object) });
                il.EmitCall(OpCodes.Call, writeLineMethod, null);
            }
            il.Emit(OpCodes.Ret);
            return method.CreateDelegate(type);
        }

        public void TestIL(Type type)
        {
            var method = new DynamicMethod(Guid.NewGuid().ToString(),
                typeof(void),
                new[] { type }, type);
            var il = method.GetILGenerator();

            var fieldValue = il.DeclareLocal(typeof(object));

            var toStringMethod = typeof(object).GetMethod("ToString");

            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                il.Emit(OpCodes.Ldstr, field.Name + ": {0}");
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, field);

                if (field.FieldType.IsValueType)
                    il.Emit(OpCodes.Box, field.FieldType);

                il.Emit(OpCodes.Dup);
                il.Emit(OpCodes.Stloc, fieldValue);

                var notNullLabel = il.DefineLabel();

                il.Emit(OpCodes.Brtrue, notNullLabel);
                il.Emit(OpCodes.Ldstr, "null");

                var printedLabel = il.DefineLabel();

                il.Emit(OpCodes.Br, printedLabel);

                il.MarkLabel(notNullLabel);

                il.Emit(OpCodes.Ldloc, fieldValue);
                il.EmitCall(OpCodes.Callvirt, toStringMethod, null);

                il.MarkLabel(printedLabel);

                var writeLineMethod = typeof(Console).GetMethod("WriteLine", new[] { typeof(string), typeof(object) });
                il.EmitCall(OpCodes.Call, writeLineMethod, null);
            }
            il.Emit(OpCodes.Ret);

        }
    }
}