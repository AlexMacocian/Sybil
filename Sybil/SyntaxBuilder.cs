using Microsoft.CodeAnalysis.CSharp;

namespace Sybil
{
    public static class SyntaxBuilder
    {
        public static NamespaceBuilder CreateNamespace(string @namespace) => new NamespaceBuilder(@namespace);
        public static ClassBuilder CreateClass(string className) => new ClassBuilder(className);
        public static ConstructorBuilder CreateConstructor(string className) => new ConstructorBuilder(className);
        public static BaseConstructorBuilder CreateBaseConstructor() => new BaseConstructorBuilder();
        public static PropertyBuilder CreateProperty(string typeName, string propertyName) => new PropertyBuilder(typeName, propertyName);
        public static AccessorBuilder CreateSetter() => new AccessorBuilder(SyntaxKind.SetAccessorDeclaration);
        public static AccessorBuilder CreateGetter() => new AccessorBuilder(SyntaxKind.GetAccessorDeclaration);
        public static FieldBuilder CreateField(string fieldType, string fieldName) => new FieldBuilder(fieldType, fieldName);
        public static MethodBuilder CreateMethod(string returnType, string methodName) => new MethodBuilder(returnType, methodName);
        public static AttributeBuilder CreateAttribute(string attributeName) => new AttributeBuilder(attributeName);
    }
}
