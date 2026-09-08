using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using native = NALogic.NativeAttributesLogic;

namespace AttributeGenerateTypes
{
    public static partial class Types
    {
        public static void BaseGenerationByAttribute(
            INamedTypeSymbol type,
            string attributeName,
            string folder,
            string fileInfo,
            Func<string, string[], string[], string, string> getStringCode
        )
        {
            native.CreateDirectory(folder);

            var fields = native.GetFieldsWithSpecificAttribute(type, attributeName).ToArray();

            string className = type.Name;
            string imports = "";

            string[] fieldNames = new string[fields.Length];
            string[] fieldCodes = new string[fields.Length];

            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];

                imports += native.GetImports(field);
                imports += "\n";

                fieldNames[i] = field.Name;
                fieldCodes[i] = native.GetTypeName(field.Type);
            }

            string code = getStringCode(className, fieldCodes, fieldNames, imports);

            native.Build(folder, className, fileInfo, code);
        }
    }
}
