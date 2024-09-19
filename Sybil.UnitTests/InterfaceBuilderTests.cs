using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sybil.Tests;

[TestClass]
public class InterfaceBuilderTests
{
    private const string Name = "ITest";
    private const string PublicInterface =
@"public interface ITest
{
}";
    private const string Interface =
@"interface ITest
{
}";

    private readonly InterfaceBuilder builder;

    public InterfaceBuilderTests()
    {
        this.builder = new InterfaceBuilder(Name);
    }

    [TestMethod]
    public void Constructor_NullName_ThrowArgumentNullException()
    {
        var action = () =>
        {
            _ = new InterfaceBuilder(null);
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
    public void Build_ReturnsInterfaceDeclarationSyntax()
    {
        var syntax = this.builder.Build();

        syntax.Should().NotBeNull().And.Subject.Should().BeOfType<InterfaceDeclarationSyntax>();
    }

    [TestMethod]
    public void WithModifier_ReturnsExpectedString()
    {
        var result = this.builder.WithModifier("public").Build().ToFullString();

        result.Should().Be(PublicInterface);
    }

    [TestMethod]
    public void Build_ReturnsExpectedString()
    {
        var result = this.builder
            .Build()
            .ToFullString();

        result.Should().Be(Interface);
    }
}