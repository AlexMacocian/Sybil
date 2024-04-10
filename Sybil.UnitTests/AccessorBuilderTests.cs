using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sybil.Tests;

[TestClass]
public class AccessorBuilderTests
{
    private const string PublicGet = "public get;";
    private const string PublicStaticGet = "public static get;";
    private const string Set = "set;";
    private const string GetExpressionField = "get => this.field ;";
    private const string GetBodyField =
@"get
{
    return this.field;
}";

    private readonly AccessorBuilder builder;

    public AccessorBuilderTests()
    {
        this.builder = new AccessorBuilder(Microsoft.CodeAnalysis.CSharp.SyntaxKind.GetAccessorDeclaration);
    }

    [TestMethod]
    public void WithModifier_ModifierNull_ThrowsNullArgumentException()
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
    public void WithBody_BodyNull_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            this.builder.WithBody(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithBody_BodyValid_ReturnsBuilder()
    {
        var returnedBuilder = this.builder.WithBody("return this.field;");

        returnedBuilder.Should().NotBeNull().And.Subject.Should().Be(this.builder);
    }

    [TestMethod]
    public void WithArrowExpression_ExpressionNull_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            this.builder.WithArrowExpression(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithArrowExpression_ExpressionValid_ReturnsBuilder()
    {
        var returnedBuilder = this.builder.WithArrowExpression("this.field;");

        returnedBuilder.Should().NotBeNull().And.Subject.Should().Be(this.builder);
    }

    [TestMethod]
    public void Build_ReturnsAccessorDeclarationSyntax()
    {
        var syntax = this.builder.Build();

        syntax.Should().NotBeNull().And.Subject.Should().BeOfType<AccessorDeclarationSyntax>();
    }

    [TestMethod]
    public void WithModifier_ReturnsExpectedString()
    {
        var result = this.builder.WithModifier("public").Build().ToFullString();

        result.Should().Be(PublicGet);
    }

    [TestMethod]
    public void WithModifiers_ReturnsExpectedString()
    {
        var result = this.builder.WithModifiers("public static").Build().ToFullString();

        result.Should().Be(PublicStaticGet);
    }

    [TestMethod]
    public void SetAccessor_ReturnsExpectedString()
    {
        var result = new AccessorBuilder(Microsoft.CodeAnalysis.CSharp.SyntaxKind.SetAccessorDeclaration).Build().ToFullString();

        result.Should().Be(Set);
    }

    [TestMethod]
    public void WithBody_ReturnsExpectedString()
    {
        var result = this.builder.WithBody("return this.field;").Build().ToFullString();

        result.Should().Be(GetBodyField);
    }

    [TestMethod]
    public void WithArrowExpression_ReturnsExpectedString()
    {
        var result = this.builder.WithArrowExpression("this.field;").Build().ToFullString();

        result.Should().Be(GetExpressionField);
    }

    [TestMethod]
    public void WithBody_OverridesWithArrowExpression_ReturnsExpectedString()
    {
        var result = this.builder
            .WithArrowExpression("this field;")
            .WithBody("return this.field;")
            .Build()
            .ToFullString();

        result.Should().Be(GetBodyField);
    }

    [TestMethod]
    public void WithArrowExpression_OverridesWithBody_ReturnsExpectedString()
    {
        var result = this.builder
            .WithBody("return this.field;")
            .WithArrowExpression("this.field;")
            .Build()
            .ToFullString();

        result.Should().Be(GetExpressionField);
    }
}