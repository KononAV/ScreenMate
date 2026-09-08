using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace NALogic
{
    public static class NativeAttributesLogic
    {
        public static void CreateDirectory(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        public static string GetTypeName(ITypeSymbol type)
        {
            if (type is IArrayTypeSymbol array)
            {
                return $"{GetTypeName(array.ElementType)}[]";
            }

            if (type is INamedTypeSymbol named)
            {
                if (named.SpecialType != SpecialType.None)
                {
                    return HELPER.GetSpecialTypeName(named.SpecialType);
                }

                if (named.IsGenericType)
                {
                    string arguments = string.Join(", ", named.TypeArguments.Select(GetTypeName));

                    return $"{named.Name}<{arguments}>";
                }

                return named.Name;
            }

            return type.Name;
        }

        public static string GetImports(IReadOnlyList<IFieldSymbol> fields)
        {
            HashSet<string> usings = new HashSet<string>();

            foreach (var field in fields)
            {
                HELPER.AddNamespace(usings, field.Type);
            }
            return String.Join("\n", (usings.Select(use => "using " + use + ";")).ToArray());
        }

        public static void Build(string folder, string className, string fileInfo, string code)
        {
            CreateDirectory(folder);

            string path = Path.Combine(folder, $"{className}{fileInfo}");

            File.WriteAllText(path, code);

            Console.WriteLine($"Generated: {path}");
        }
    }

    static class HELPER
    {
        public static string GetSpecialTypeName(SpecialType type)
        {
            return type switch
            {
                SpecialType.System_Boolean => "bool",
                SpecialType.System_Byte => "byte",
                SpecialType.System_SByte => "sbyte",
                SpecialType.System_Char => "char",
                SpecialType.System_Decimal => "decimal",
                SpecialType.System_Double => "double",
                SpecialType.System_Single => "float",
                SpecialType.System_Int16 => "short",
                SpecialType.System_Int32 => "int",
                SpecialType.System_Int64 => "long",
                SpecialType.System_UInt16 => "ushort",
                SpecialType.System_UInt32 => "uint",
                SpecialType.System_UInt64 => "ulong",
                SpecialType.System_String => "string",
                SpecialType.System_Object => "object",
                SpecialType.System_Void => "void",

                _ => type.ToString(),
            };
        }

        public static void AddNamespace(HashSet<string> usings, ITypeSymbol type)
        {
            if (type is IArrayTypeSymbol array)
            {
                AddNamespace(usings, array.ElementType);

                return;
            }

            if (type is INamedTypeSymbol named)
            {
                AddTypeNamespace(usings, named);

                if (named.IsGenericType)
                {
                    foreach (var argument in named.TypeArguments)
                    {
                        AddNamespace(usings, argument);
                    }
                }

                return;
            }

            AddTypeNamespace(usings, type);
        }

        public static void AddTypeNamespace(HashSet<string> usings, ITypeSymbol type)
        {
            if (type.SpecialType != SpecialType.None)
            {
                return;
            }

            string? namespaceName = type.ContainingNamespace?.ToDisplayString();

            Console.WriteLine("NAMESPACE NAME: " + namespaceName);

            if (namespaceName == "<global namespace>")
                namespaceName = "UnityEngine";

            usings.Add(namespaceName);
        }
    }
}
