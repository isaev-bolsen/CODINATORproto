using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using Microsoft.CSharp;
using System.IO;

namespace CODINATOR
    {
    class ClassBuilder
        {
        private CodeCompileUnit ResultingSc;
        private CodeNamespace Namespace;
        private CodeTypeDeclaration Class;

        private CSharpCodeProvider provider;
        private FileInfo resultingFile;

        public ClassBuilder(string NamespaceName, string className)
            {
            ResultingSc = new CodeCompileUnit();
            Namespace = new CodeNamespace(NamespaceName);
            Namespace.Imports.Add(new CodeNamespaceImport("System"));
            ResultingSc.Namespaces.Add(Namespace);
            Class = new CodeTypeDeclaration(className);
            Namespace.Types.Add(Class);
            }

        public void addField(Type fieldType, string name)
            {
            CodeMemberField Field = new CodeMemberField(fieldType, name);
            Class.Members.Add(Field);
            }

        public void SerializeCs()
            {
            if (provider == null) provider = new CSharpCodeProvider();
            if (resultingFile == null) resultingFile = new FileInfo(Class.Name + ".cs");
            var writer = resultingFile.CreateText();
            provider.GenerateCodeFromCompileUnit(ResultingSc, writer, new System.CodeDom.Compiler.CodeGeneratorOptions());
            writer.Close();
            }
        }
    }

