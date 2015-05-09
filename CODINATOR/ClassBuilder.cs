using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using Microsoft.CSharp;
using System.IO;
using System.CodeDom.Compiler;

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

        public CodeMemberField addField<T>(string name)
            {
            CodeMemberField Field = new CodeMemberField(typeof(T), name);
            Class.Members.Add(Field);
            return Field;
            }

        private CodeBinaryOperatorExpression getEqualityOperation(string var1, string var2)
            {
            return new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression(var1), CodeBinaryOperatorType.ValueEquality, new CodeVariableReferenceExpression(var2));
            }

        public void CreateValuesEqualsMethod()
            {
            CodeMemberMethod EqualsMethod = new CodeMemberMethod();
            EqualsMethod.Name = "EqualsByValues";
            EqualsMethod.ReturnType = new CodeTypeReference(typeof(bool));

            CodeBinaryOperatorExpression AND=null;

            foreach (var member in Class.Members)
                {
                var field = member as CodeMemberField;
                if (field == null) continue;
                var parameter = new CodeParameterDeclarationExpression(field.Type, "Expected" + field.Name);
                EqualsMethod.Parameters.Add(parameter);
                if (AND == null)
                    {
                    AND = getEqualityOperation(field.Name, parameter.Name);
                    }
                else
                    {
                    AND = new CodeBinaryOperatorExpression(AND, CodeBinaryOperatorType.BooleanAnd, getEqualityOperation(field.Name, parameter.Name));
                    }
                }

            CodeMethodReturnStatement returnStatement = new CodeMethodReturnStatement(AND);
            EqualsMethod.Statements.Add(returnStatement);
            Class.Members.Add(EqualsMethod);
            }

        public void SerializeCs()
            {
            provider = new CSharpCodeProvider();
            resultingFile = new FileInfo(Class.Name + ".cs");
            var writer = resultingFile.CreateText();
            provider.GenerateCodeFromCompileUnit(ResultingSc, writer, new CodeGeneratorOptions() { BracingStyle = "C", BlankLinesBetweenMembers = false });
            writer.Close();
            }
        }
    }

