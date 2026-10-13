using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sybil.Tests;

[TestClass]
public class StructBuilderTests
{
    private const string Name = "Test";
    private const string IBase = "IBase";
    private const string PublicStruct =
@"public struct Test
{
}";
    private const string PublicReadonlyPartialStruct =
@"public readonly partial struct Test
{
}";
    private const string StructWithInterface =
@"struct Test : IBase
{
}";

    private readonly StructBuilder builder;

    public StructBuilderTests()
    {
        this.builder = new StructBuilder(Name);
    }

    [TestMethod]
    public void Constructor_NullName_ThrowArgumentNullException()
    {
        var action = () =>
        {
            _ = new StructBuilder(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithModifier_ModifierNull_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            this.builder.WithModifier(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithModifier_ModifierValid_ReturnsBuilder()
    {
        var returnedBuilder = this.builder.WithModifier("public");

        returnedBuilder.Should().NotBeNull().And.Subject.Should().Be(this.builder);
    }

    [TestMethod]
    public void WithModifiers_ModifiersNull_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            this.builder.WithModifiers(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithModifiers_ModifiersValid_ReturnsBuilder()
    {
        var returnedBuilder = this.builder.WithModifiers("public readonly partial");

        returnedBuilder.Should().NotBeNull().And.Subject.Should().Be(this.builder);
    }

    [TestMethod]
    public void WithField_NullFieldBuilder_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            this.builder.WithField(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithProperty_NullPropertyBuilder_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            this.builder.WithProperty(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithMethod_NullMethodBuilder_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            this.builder.WithMethod(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithConstructor_NullConstructorBuilder_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            this.builder.WithConstructor(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithInterface_NullInterface_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            this.builder.WithInterface(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithInterface_InterfaceValid_ReturnsBuilder()
    {
        var returnedBuilder = this.builder.WithInterface(IBase);

        returnedBuilder.Should().NotBeNull().And.Subject.Should().BeOfType<StructBuilder>();
    }

    [TestMethod]
    public void Build_ReturnsStructDeclarationSyntax()
    {
        var syntax = this.builder.Build();

        syntax.Should().NotBeNull().And.Subject.Should().BeOfType<StructDeclarationSyntax>();
    }

    [TestMethod]
    public void WithModifier_ReturnsExpectedString()
    {
        var result = this.builder.WithModifier("public").Build().ToFullString();

        result.Should().Be(PublicStruct);
    }

    [TestMethod]
    public void WithModifiers_ReturnsExpectedString()
    {
        var result = this.builder.WithModifiers("public readonly partial").Build().ToFullString();

        result.Should().Be(PublicReadonlyPartialStruct);
    }

    [TestMethod]
    public void WithInterface_ReturnsExpectedString()
    {
        var result = this.builder
            .WithInterface(IBase)
            .Build()
            .ToFullString();

        result.Should().Be(StructWithInterface);
    }

    [TestMethod]
    public void WithFieldsAndMethod_ReturnsExpectedString()
    {
        var result = this.builder
            .WithModifiers("public partial")
            .WithAttribute(SyntaxBuilder.CreateAttribute("Structure"))
            .WithField(SyntaxBuilder.CreateField("uint", "Value").WithModifier("public"))
            .WithField(SyntaxBuilder.CreateField("int", "Other").WithModifier("public").WithInitializer("1"))
            .WithMethod(SyntaxBuilder.CreateMethod("int", "Get").WithModifier("public").WithExpression("this.Other"))
            .Build()
            .ToFullString();

        result.Should().Be(
@"[Structure]
public partial struct Test
{
    public uint Value;
    public int Other = 1;
    public int Get() => this.Other;
}");
    }
}
