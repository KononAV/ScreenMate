using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using attributeGenerator = AttributeGenerateTypes.Types;

public static class PublicReadonly
{
    public static string folder = "D:/Unity/proj/ScreenMate/Assets/TemporalFolder";
    public static string fileInfo = ".Generate.cs";
    public static string modificator = "public";

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
            {
                string type = NALogic.NativeAttributesLogic.GetTypeName(field.Type);
                string name = ModifyFieldName(field.Name);
                return $"        {modificator} {type} " + $"{name} => {field.Name};";
            })
        );
    }

    public static string GetStringCode(
        string className,
        IReadOnlyList<IFieldSymbol> fields,
        string imports
    )
    {
        string fieldCode = BuildFields(fields);

        return $@"using UnityEditor;     

{imports}

public partial class {className}
{{
{fieldCode}
}}
";
    }
}
