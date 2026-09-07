using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using attributeGenerator = AttributeGenerateTypes.Types;

namespace AGLogic
{
    public static class Generation
    {
        private static string folder = "Assets/TemporalFolder";
        private static string fileInfo = ".Generate.cs";
        private static string modificator = "public";

        private static string ModifyFieldName(string name)
        {
            return $"{name[0].ToString().ToUpper()}{name[1..]}";
        }

        private static string BuildFields(string[] fieldCodes, string[] fieldNames)
        {
            return string.Join(
                "\n",
                fieldNames.Select(
                    (name, i) =>
                        $"        {modificator} {fieldCodes[i]} {ModifyFieldName(name)} => {name};"
                )
            );
        }

        private static string GetStringCode(
            string className,
            string[] fieldCodes,
            string[] fieldNames,
            string imports
        )
        {
            string fields = BuildFields(fieldCodes, fieldNames);

            return $@"
using System;
{imports}
public partial class {className}
{{
{fields}
}}
";
        }

        public static void Generate(Type type)
        {
            attributeGenerator.BaseGenerationByAttribute<PublicReadonlyAttribute>(
                type,
                folder,
                fileInfo,
                GetStringCode
            );
        }
    }
}
