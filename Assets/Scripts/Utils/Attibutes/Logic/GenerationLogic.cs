using System;
using System.Linq;
using System.Reflection;
using Unity.Collections;
using native = NALogic.NativeAttributesLogic;

namespace AGLogic
{
    public static class Generation
    {
        private static string folder = "Assets/TemporalFolder";
        private static string fileInfo = ".Generate.cs";

        private static string ModifyFieldName(string name)
        {
            return $"{name[0].ToString().ToUpper()}{name[1..]}";
        }

        private static string getStringCode(
            string className,
            string fieldCode,
            string fieldName,
            string imports
        )
        {
            return $@"
    // {imports}

    // public partial class {className}
    // {{      
    // public readonly {fieldCode} {fieldName};
    // }}
";
        }

        public static void Generate(Type type, FieldInfo fieldInfo)
        {
            string fieldCode = native.GetTypeName(fieldInfo.FieldType);
            native.CreateDirectory(folder);

            string className = type.Name;

            // NativeArray<string>;

            var fields = type.GetFields()
                .Where(field => Attribute.IsDefined(field, typeof(PublicReadonlyAttribute)));

            string fieldName = ModifyFieldName(fieldInfo.Name);
            string imports = native.GetImports(fieldInfo);

            //Debug.LogWarning(fieldInfo.FieldType);

            // if (native.isFileExist(folder, className, fileInfo))
            //     return;

            string code = getStringCode(className, fieldCode, fieldName, imports);

            native.Build(folder, className, fileInfo, code);
        }
    }
}
