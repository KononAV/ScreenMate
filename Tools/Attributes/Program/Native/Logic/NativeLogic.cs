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

        public static IEnumerable<IFieldSymbol> GetFieldsWithSpecificAttribute(
            INamedTypeSymbol type,
            string attributeName
        )
        {
            return type.GetMembers()
                .OfType<IFieldSymbol>()
                .Where(field =>
                    field
                        .GetAttributes()
                        .Any(attribute =>
                        {
                            string? name = attribute.AttributeClass?.Name;

                            return name == attributeName || name == attributeName + "Attribute";
                        })
                );
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
                    return GetSpecialTypeName(named.SpecialType);
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

        private static string GetSpecialTypeName(SpecialType type)
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

        public static string GetImports(IFieldSymbol field)
        {
            var usings = new HashSet<string>();

            AddNamespace(usings, field.Type);

            return string.Join(
                "\n",
                usings
                    .Where(x => !string.IsNullOrWhiteSpace(x) && x != "global namespace")
                    .Select(x => $"using {x};")
            );
        }

        private static void AddNamespace(HashSet<string> usings, ITypeSymbol type)
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

        private static void AddTypeNamespace(HashSet<string> usings, ITypeSymbol type)
        {
            if (type.SpecialType != SpecialType.None)
            {
                return;
            }

            string? namespaceName = type.ContainingNamespace?.ToDisplayString();

            if (
                string.IsNullOrEmpty(namespaceName)
                || namespaceName == "global namespace"
                || namespaceName == "System"
            )
            {
                return;
            }

            usings.Add(namespaceName);
        }

        public static void Build(string folder, string className, string fileInfo, string code)
        {
            CreateDirectory(folder);

            string path = Path.Combine(folder, $"{className}{fileInfo}");

            File.WriteAllText(path, code);

            Console.WriteLine($"Generated: {path}");
        }
    }
}
