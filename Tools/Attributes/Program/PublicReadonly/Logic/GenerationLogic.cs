using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using attributeGenerator = AttributeGenerateTypes.Types;

namespace AGLogic
{
    public static class Generation
    {
        private static string folder = "D:/Unity/proj/ScreenMate/Assets/TemporalFolder";
        private static string fileInfo = ".Generate.cs";
        private static string modificator = "public";

        private static string ModifyFieldName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return name;

            return $"{name[0].ToString().ToUpper()}{name[1..]}";
        }

        private static string BuildFields(IReadOnlyList<IFieldSymbol> fields)
        {
            return string.Join(
                "\n",
                fields.Select(field =>
                    $"        {modificator} {field.Type} "
                    + $"{ModifyFieldName(field.Name)} => {field.Name};"
                )
            );
        }

        private static string GetStringCode(
            string className,
            IReadOnlyList<IFieldSymbol> fields,
            string imports
        )
        {
            string fieldCode = BuildFields(fields);

            return $@"using System;
using UnityEngine;
            
{imports}

public partial class {className}
{{
{fieldCode}
}}
";
        }

        public static void Generate(INamedTypeSymbol type, IReadOnlyList<IFieldSymbol> fields)
        {
            string imports = "";

            foreach (var field in fields)
            {
                imports += NALogic.NativeAttributesLogic.GetImports(field);

                imports += "\n";
            }

            string code = GetStringCode(type.Name, fields, imports);

            NALogic.NativeAttributesLogic.Build(folder, type.Name, fileInfo, code);
        }
    }
}
