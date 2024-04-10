# Sybil

C# Syntax builders following builder pattern

## Usage

Sybil supports building of namespaces with classes, constructors, parameters, properties, fields and methods.
Example of creating a namespace with one class with one constructor and base class, multiple fields, properties and methods:
```C#
var namespaceSyntax = SyntaxBuilder.CreateNamespace("TestNamespace")
        .WithAttribute(SyntaxBuilder.CreateAttribute("InternalsVisibleTo")
            .WithArgument("FlowTests"))
        .WithUsing("System")
        .WithClass(
            SyntaxBuilder.CreateClass("TestClass")
            .WithBaseClass("BaseTestClass")
            .WithAttribute(SyntaxBuilder.CreateAttribute("SomeAttribute")
                .WithArgument("SomeIntProperty", 1)
                .WithArgument("SomeStringProperty", "Hello")
                .WithArgument("SomeEnumProperty", MyEnum.Value)
                .WithArgument("SomeTypeProperty", typeof(string))
                .WithNullArgument("SomeNullProperty"))
            .WithModifiers("public sealed")
            .WithConstructor(
                SyntaxBuilder.CreateConstructor("TestClass")
                .WithBase(
                    SyntaxBuilder.CreateBaseConstructor()
                    .WithArgument("fieldString"))
                .WithAttribute(
                    SyntaxBuilder.CreateAttribute("SomeAttribute5")
                    .WithNullArgument())
                .WithParameter("string", "fieldString")
                .WithBody("this.fieldString = fieldString ?? throw new ArgumentNullException();"))
            .WithField(
                SyntaxBuilder.CreateField("string", "fieldString")
                .WithAttribute(SyntaxBuilder.CreateAttribute("SomeAttribute2"))
                .WithModifier("private"))
            .WithProperty(
                SyntaxBuilder.CreateProperty("string", "PropertyString")
                .WithModifier("public")
                .WithAccessor(
                    SyntaxBuilder.CreateSetter()
                    .WithModifier("private"))
                .WithAccessor(
                    SyntaxBuilder.CreateGetter())
                .WithAttribute(SyntaxBuilder.CreateAttribute("SomeAttribute3")
                    .WithArgument(typeof(string))))
            .WithMethod(
                SyntaxBuilder.CreateMethod("string", "GetFieldString")
                .WithModifier("public")
                .WithAttribute(SyntaxBuilder.CreateAttribute("SomeAttribute4")
                    .WithArgument(0.5f))
                .WithBody("return this.fieldString;")))
        .Build();

        var namespaceString = namespaceSyntax.ToFullString();
```

This generates the following syntax:
```C#
[InternalsVisibleTo(""FlowTests"")]
namespace TestNamespace
{
    using System;

    [SomeAttribute(SomeIntProperty = 1, SomeStringProperty = ""Hello"", SomeEnumProperty = MyEnum.Value, SomeTypeProperty = typeof(String), SomeNullProperty = null)]
    public sealed class TestClass : BaseTestClass
    {
        [SomeAttribute5(null)]
        TestClass(string fieldString) : base(fieldString)
        {
            this.fieldString = fieldString ?? throw new ArgumentNullException();
        }

        [SomeAttribute2]
        private string fieldString;
        [SomeAttribute3(typeof(String))]
        public string PropertyString { private set; get; }

        [SomeAttribute4(0.5F)]
        public string GetFieldString()
        {
            return this.fieldString;
        }
    }
}
```