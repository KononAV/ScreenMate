using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace AttributeGenerateTypes
{
    public static partial class Types
    {
        public static void BaseTemplateByAttributeBuild(
            INamedTypeSymbol type,
            IReadOnlyList<IFieldSymbol> fields,
            string folder,
            string fileInfo,
            Func<string, IReadOnlyList<IFieldSymbol>, string, string> getStringCode
        )
        {
            string imports = NALogic.NativeAttributesLogic.GetImports(fields);

            Console.WriteLine("FIELDS: " + fields);

            string code = getStringCode(type.Name, fields, imports);

            NALogic.NativeAttributesLogic.Build(folder, type.Name, fileInfo, code);
        }
    }
}
