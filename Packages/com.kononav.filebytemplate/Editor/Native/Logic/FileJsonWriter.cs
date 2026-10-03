using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using UnityEngine;

public static class FileJsonWriter
{
    [System.Serializable]
    private class FileList
    {
        public string[] files;
    }

    public static void Save(string folderPath, string jsonPath)
    {
        string[] files = Directory.GetFiles(folderPath, "*.cs");

        HashSet<string> names = new();

        foreach (var file in files)
        {
            string source = File.ReadAllText(file);

            var tree = CSharpSyntaxTree.ParseText(source);

            var classes = tree.GetRoot()
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .Where(c =>
                    c.BaseList != null
                    && c.BaseList.Types.Any(type => type.Type.ToString() == "PropertyAttribute")
                );

            foreach (var classDeclaration in classes)
            {
                string name = classDeclaration.Identifier.Text;

                names.Add(name);
            }
        }

        var data = new FileList { files = names.ToArray() };

        File.WriteAllText(jsonPath, JsonUtility.ToJson(data, true));
    }
}
