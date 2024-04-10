using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sybil.Tests;

[TestClass]
public class PropertyBuilderTests
{
    private const string Public = "public";
    private const string PublicStatic = "public static";
    private const string TypeName = "string";
    private const string PropertyName = "SomeString";
    private const string PublicProperty = "public string SomeString { }";
    private const string PublicStaticProperty = "public static string SomeString { }";

    private readonly PropertyBuilder builder;

    public PropertyBuilderTests()
    {
        this.builder = new PropertyBuilder(TypeName, PropertyName);
    }

    [TestMethod]
    public void Constructor_NullPropertyType_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            _ = new PropertyBuilder(null, PropertyName);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Constructor_NullPropertyName_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            _ = new PropertyBuilder(TypeName, null);
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
        var returnedBuilder = this.builder.WithModifier(Public);

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
        var returnedBuilder = this.builder.WithModifiers(PublicStatic);

        returnedBuilder.Should().NotBeNull().And.Subject.Should().Be(this.builder);
    }

    [TestMethod]
    public void WithAccessor_NullAccessor_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            this.builder.WithAccessor(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Build_ReturnsPropertyDeclarationSyntax()
    {
        var syntax = this.builder.Build();

        syntax.Should().NotBeNull().And.Subject.Should().BeOfType<PropertyDeclarationSyntax>();
    }

    [TestMethod]
    public void WithModifier_ReturnsExpectedString()
    {
        var result = this.builder.WithModifier(Public).Build().ToFullString();

        result.Should().Be(PublicProperty);
    }

    [TestMethod]
    public void WithModifiers_ReturnsExpectedString()
    {
        var result = this.builder.WithModifiers(PublicStatic).Build().ToFullString();

        result.Should().Be(PublicStaticProperty);
    }
}