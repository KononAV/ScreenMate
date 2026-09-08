using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AnalizerLogic
{
    public static class RoslynAnalyzer
    {
        public sealed class AnalysisResult
        {
            public INamedTypeSymbol Type { get; }
            public IReadOnlyList<IFieldSymbol> Fields { get; }

            public AnalysisResult(INamedTypeSymbol type, IReadOnlyList<IFieldSymbol> fields)
            {
                Type = type;
                Fields = fields;
            }
        }

        public static IReadOnlyList<AnalysisResult> Analyze(string filePath)
        {
            string source = File.ReadAllText(filePath);

            SyntaxTree tree = CSharpSyntaxTree.ParseText(source);

            var root = tree.GetRoot();

            var compilation = CSharpCompilation.Create(
                "AGGenerator",
                new[] { tree },
                GetReferences()
            );

            SemanticModel model = compilation.GetSemanticModel(tree);

            var results = new List<AnalysisResult>();

            foreach (var classSyntax in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
            {
                var type = model.GetDeclaredSymbol(classSyntax) as INamedTypeSymbol;

                if (type == null)
                    continue;

                var fields = type.GetMembers()
                    .OfType<IFieldSymbol>()
                    .Where(HasPublicReadonlyAttribute)
                    .ToList();

                if (fields.Count == 0)
                    continue;

                results.Add(new AnalysisResult(type, fields));
            }

            return results;
        }

        private static bool HasPublicReadonlyAttribute(IFieldSymbol field)
        {
            return field
                .GetAttributes()
                .Any(attribute =>
                {
                    string? name = attribute.AttributeClass?.Name;

                    return name == "PublicReadonlyAttribute" || name == "PublicReadonly";
                });
        }

        private static IEnumerable<MetadataReference> GetReferences()
        {
            return new[]
            {
                typeof(object).Assembly,
                typeof(Console).Assembly,
                typeof(Enumerable).Assembly,
            }
                .Select(assembly => MetadataReference.CreateFromFile(assembly.Location))
                .GroupBy(reference => reference.Display)
                .Select(group => group.First());
        }
    }
}
