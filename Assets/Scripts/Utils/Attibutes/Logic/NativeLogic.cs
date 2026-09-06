using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace NALogic
{
    static class NativeAttributesLogic
    {
        public static string getFieldCode(FieldInfo fieldInfo)
        {
            return $"public {fieldInfo.FieldType.Name} {fieldInfo.Name};";
        }

        public static void checkDirectoryExist(string folder)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }

        public static void build(string folder, string className, string fileInfo, string code)
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
}
