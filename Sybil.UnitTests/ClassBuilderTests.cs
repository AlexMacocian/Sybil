using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sybil.Tests;

[TestClass]
public class ClassBuilderTests
{
    private const string Name = "Test";
    private const string Base = "Base";
    private const string IBase = "IBase";
    private const string PublicClass =
@"public class Test
{
}";
    private const string PublicStaticClass =
@"public static class Test
{
}";
    private const string ClassWithBase =
@"class Test : Base
{
}";
    private const string ClassWithInterface =
@"class Test : IBase
{
}";

    private readonly ClassBuilder builder;

    public ClassBuilderTests()
    {
        this.builder = new ClassBuilder(Name);
    }

    [TestMethod]
    public void Constructor_NullName_ThrowArgumentNullException()
    {
        var action = () =>
        {
            _ = new ClassBuilder(null);
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
        var returnedBuilder = this.builder.WithModifiers("public static");

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
    public void WithBaseClass_NullBaseClass_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            this.builder.WithBaseClass(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithBaseClass_BaseClassValid_ReturnsBuilder()
    {
        var returnedBuilder = this.builder.WithBaseClass(Base);

        returnedBuilder.Should().NotBeNull().And.Subject.Should().BeOfType<ClassBuilder>();
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
        var returnedBuilder = this.builder.WithInterface(Base);

        returnedBuilder.Should().NotBeNull().And.Subject.Should().BeOfType<ClassBuilder>();
    }

    [TestMethod]
    public void Build_ReturnsClassDeclarationSyntax()
    {
        var syntax = this.builder.Build();

        syntax.Should().NotBeNull().And.Subject.Should().BeOfType<ClassDeclarationSyntax>();
    }

    [TestMethod]
    public void WithModifier_ReturnsExpectedString()
    {
        var result = this.builder.WithModifier("public").Build().ToFullString();

        result.Should().Be(PublicClass);
    }

    [TestMethod]
    public void WithModifiers_ReturnsExpectedString()
    {
        var result = this.builder.WithModifiers("public static").Build().ToFullString();

        result.Should().Be(PublicStaticClass);
    }

    [TestMethod]
    public void WithBaseClass_ReturnsExpectedString()
    {
        var result = this.builder
            .WithBaseClass(Base)
            .Build()
            .ToFullString();

        result.Should().Be(ClassWithBase);
    }

    [TestMethod]
    public void WithInterface_ReturnsExpectedString()
    {
        var result = this.builder
            .WithInterface(IBase)
            .Build()
            .ToFullString();

        result.Should().Be(ClassWithInterface);
    }
}