using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sybil.IntegrationTests.Models;

namespace Sybil.IntegrationTests;

[TestClass]
public sealed class FlowTests
{
    private const string ExpectedSyntax =
@"using System;

[InternalsVisibleTo(""FlowTests"")]
namespace TestNamespace;
[SomeAttribute(SomeIntProperty = 1, SomeStringProperty = ""Hello"", SomeEnumProperty = MyEnum.Value, SomeTypeProperty = typeof(String), SomeNullProperty = null)]
public sealed class TestClass<T> : BaseTestClass, IInterface where T : new(), class
{
    [SomeAttribute5(null)]
    TestClass(string fieldString) : base(fieldString)
    {
        this.fieldString = fieldString ?? throw new ArgumentNullException();
        this.PropertyString = string.Empty;
    }

    private string someTypeString = $""{typeof(string)}"";
    [SomeAttribute2]
    private string fieldString;
    [SomeAttribute3(typeof(String))]
    public string PropertyString { private set; get; }

    [SomeAttribute4(0.5F)]
    public string GetFieldString<T>()
    {
        this.fieldString = 0;
        return this.fieldString;
    }
}";

    [TestMethod]
    public void NewNamespace_GeneratesExpected()
    {
        var compilationUnitSyntax = SyntaxBuilder.CreateCompilationUnit()
            .WithUsing("System")
            .WithNamespace(
            SyntaxBuilder.CreateFileScopedNamespace("TestNamespace")
            .WithAttribute(SyntaxBuilder.CreateAttribute("InternalsVisibleTo")
                .WithArgument("FlowTests"))
            .WithClass(
                SyntaxBuilder.CreateClass("TestClass")
                .WithBaseClass("BaseTestClass")
                .WithInterface("IInterface")
                .WithTypeParameter(SyntaxBuilder.CreateTypeParameter("T"))
                .WithTypeParameterConstraint(
                    SyntaxBuilder.CreateTypeParameterConstraint("T")
                        .WithParameterlessConstructor()
                        .WithClass())
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
                    .WithBody("this.fieldString = fieldString ?? throw new ArgumentNullException();\rthis.PropertyString = string.Empty;"))
                .WithField(
                    SyntaxBuilder.CreateField("string", "someTypeString")
                    .WithModifier("private")
                    .WithInitializer("$\"{typeof(string)}\""))
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
                    .WithTypeParameter(SyntaxBuilder.CreateTypeParameter("T"))
                    .WithAttribute(SyntaxBuilder.CreateAttribute("SomeAttribute4")
                        .WithArgument(0.5f))
                    .WithBody("this.fieldString = 0;\r\nreturn this.fieldString;"))))
            .Build();

        var compilationUnit = compilationUnitSyntax.ToFullString();
        compilationUnit.Should().Be(ExpectedSyntax);
    }
}
