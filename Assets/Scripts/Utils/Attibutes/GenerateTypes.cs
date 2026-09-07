namespace AttributeGenerateTypes
{
    using System;
    using System.Linq;
    using native = NALogic.NativeAttributesLogic;

    public static partial class Types
    {
        public static void BaseGenerationByAttribute<TAttribute>(
            Type type,
            string folder,
            string fileInfo,
            Func<string, string[], string[], string, string> GetStringCode
        )
            where TAttribute : Attribute
        {
            native.CreateDirectory(folder);

            var fields = native.GetFieldsWithSpecificAttribute<TAttribute>(type);

            string className = type.Name;
            string imports = "";

            string[] fieldNames = new string[fields.Count()];
            string[] fieldCodes = new string[fields.Count()];

            int i = 0;

            foreach (var field in fields)
            {
                imports += native.GetImports(field);
                imports += "\n";

                fieldNames[i] = field.Name;
                fieldCodes[i] = native.GetTypeName(field.FieldType);

                i++;
            }

            string code = GetStringCode(className, fieldCodes, fieldNames, imports);

            native.Build(folder, className, fileInfo, code);
        }
    }
}
