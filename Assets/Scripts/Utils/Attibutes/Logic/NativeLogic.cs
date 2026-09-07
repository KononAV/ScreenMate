using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace NALogic
{
    static class NativeAttributesLogic
    {
        public static IEnumerable<FieldInfo> GetFieldsWithSpecificAttribute<TAttribute>(Type type)
            where TAttribute : Attribute
        {
            return type.GetRuntimeFields()
                .Where(field => Attribute.IsDefined(field, typeof(TAttribute)));
        }

        public static string GetTypeName(Type type)
        {
            if (!type.IsGenericType)
            {
                return type.Name;
            }
            string name = type.Name;

            HELPER.RemoveNumericsFromString(ref name);

            Type[] arguments = type.GetGenericArguments();

            string genericArguments = string.Join(", ", arguments.Select(GetTypeName));

            return $"{name}<{genericArguments}>";
        }

        public static void CreateDirectory(string folder)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }

        public static string GetImports(FieldInfo fieldInfo)
        {
            HashSet<string> usings = new();
            string imports = "";

            ImportsLogic.setUsings(usings, fieldInfo.FieldType);

            foreach (string item in usings)
            {
                imports += $"using {item};";
            }
            return imports;
        }

        public static void Build(string folder, string className, string fileInfo, string code)
        {
            string path = Path.Combine(folder, $"{className}{fileInfo}");

            File.WriteAllText(path, code);

            AssetDatabase.Refresh();

            Debug.Log($"Generated: {path}");
        }

        public static bool isFileExist(string folder, string className, string fileInfo)
        {
            if (File.Exists(Path.Combine(folder, $"{className}{fileInfo}")))
            {
                Debug.Log("FILE EXIST");
                return true;
            }
            return false;
        }
    }

    static class ImportsLogic
    {
        public static void setUsings(HashSet<string> usings, Type type)
        {
            if (type.IsArray)
            {
                setUsings(usings, type.GetElementType());
                return;
            }

            Type nullableType = Nullable.GetUnderlyingType(type);

            if (nullableType != null)
            {
                setUsings(usings, nullableType);
                return;
            }

            if (type.IsGenericType)
            {
                GenericTypeSetting(usings, type);
                return;
            }

            setNamespace(usings, type);
        }

        private static void GenericTypeSetting(HashSet<string> usings, Type type)
        {
            setNamespace(usings, type);

            foreach (Type argument in type.GetGenericArguments())
            {
                setUsings(usings, argument);
            }
        }

        private static void setNamespace(HashSet<string> usings, Type type)
        {
            string namespaceName = type.Namespace;

            if (HELPER.IsNullOrEmpty(namespaceName))
                return;

            if (type.IsPrimitive)
                return;

            if (HELPER.IsTypeRequireImport(type))
                return;

            usings.Add(namespaceName);
        }
    }

    static class HELPER
    {
        public static bool IsNullOrEmpty(string name)
        {
            return string.IsNullOrEmpty(name);
        }

        public static bool IsTypeRequireImport(Type type)
        {
            return type == typeof(string) || type == typeof(object) || type == typeof(void);
        }

        public static void RemoveNumericsFromString(ref string name)
        {
            int genericIndex = name.IndexOf('`');
            if (genericIndex >= 0)
            {
                name = name.Substring(0, genericIndex);
            }
        }
    }
}
