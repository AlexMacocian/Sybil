using Microsoft.CodeAnalysis.CSharp;

namespace Sybil
{
    public static class SyntaxBuilder
    {
        public static NamespaceBuilder CreateNamespace(string @namespace)
        {
            return new NamespaceBuilder(@namespace);
        }
        public static ClassBuilder CreateClass(string className)
        {
            return new ClassBuilder(className);
        }
        public static ConstructorBuilder CreateConstructor(string className)
        {
            return new ConstructorBuilder(className);
        }
        public static BaseConstructorBuilder CreateBaseConstructor()
        {
            return new BaseConstructorBuilder();
        }
        public static PropertyBuilder CreateProperty(string typeName, string propertyName)
        {
            return new PropertyBuilder(typeName, propertyName);
        }
        public static AccessorBuilder CreateSetter()
        {
            return new AccessorBuilder(SyntaxKind.SetAccessorDeclaration);
        }
        public static AccessorBuilder CreateGetter()
        {
            return new AccessorBuilder(SyntaxKind.GetAccessorDeclaration);
        }
        public static FieldBuilder CreateField(string fieldType, string fieldName)
        {
            return new FieldBuilder(fieldType, fieldName);
        }
        public static MethodBuilder CreateMethod(string returnType, string methodName)
        {
            return new MethodBuilder(returnType, methodName);
        }
    }
}
