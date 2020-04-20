using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Test
{
    public class RefSerializator
    {
        /* public String Serialize<T>() where T : struct
         {
             DynamicMethod method = new DynamicMethod("Getter", typeof(string), new []{typeof(T)});
             ILGenerator generator = method.GetILGenerator();
             generator.Emit(OpCodes.Ldarg_0);
             generator.Emit(OpCodes.);
         }*/
        delegate void MYDel(CustomRefStruct str);
        public void BuildFieldsPrinter(Type type)
        {
            var method = new DynamicMethod(Guid.NewGuid().ToString(),
                                           typeof(void),
                                           new[] { type });
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

            var t = (MYDel)method.CreateDelegate(typeof(MYDel));
            t.Invoke(new CustomRefStruct(2));
        }
    }
}