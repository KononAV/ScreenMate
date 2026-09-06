using System;
using System.Reflection;
using UnityEngine;
using native = NALogic.NativeAttributesLogic;

namespace AGLogic
{
    static class Generation
    {
        private static string folder = "Assets/TemporalFolder";
        private static string fileInfo = ".Generate.cs";

        private static string getStringCode(string className, string fieldCode)
        {
            return $@"
    // public partial class {className}
    // {{      
    // {fieldCode};
    // }}
";
        }

        public static void Generate(Type type, FieldInfo fieldInfo)
        {
            string fieldCode = native.getFieldCode(fieldInfo);
            native.checkDirectoryExist(folder);

            string className = type.Name;
            Debug.LogWarning(fieldInfo.FieldType);

            // if (native.isFileExist(folder, className, fileInfo))
            //     return;

            string code = getStringCode(className, fieldCode);

            native.build(folder, className, fileInfo, code);
        }
    }
}
