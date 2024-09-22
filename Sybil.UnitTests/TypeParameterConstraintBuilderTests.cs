using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sybil.Tests;

[TestClass]
public class TypeParameterConstraintBuilderTests
{
    private const string TypeName = "T";
    private const string ConstraintType = "TType";
    private const string Expected = "where T : class, struct, new(), TType";

    private readonly TypeParameterConstraintBuilder builder;

    public TypeParameterConstraintBuilderTests()
    {
        this.builder = new TypeParameterConstraintBuilder(TypeName);
    }

    [TestMethod]
    public void Constructor_NullType_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            _ = new TypeParameterConstraintBuilder(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithClass_ReturnsBuilder()
    {
        this.builder.WithClass().Should().BeOfType<TypeParameterConstraintBuilder>();
    }

    [TestMethod]
    public void WithStruct_ReturnsBuilder()
    {
        this.builder.WithStruct().Should().BeOfType<TypeParameterConstraintBuilder>();
    }

    [TestMethod]
    public void WithParameterlessConstructor_ReturnsBuilder()
    {
        this.builder.WithParameterlessConstructor().Should().BeOfType<TypeParameterConstraintBuilder>();
    }

    [TestMethod]
    public void WithType_ReturnsBuilder()
    {
        this.builder.WithType(ConstraintType).Should().BeOfType<TypeParameterConstraintBuilder>();
    }

    [TestMethod]
    public void WithType_NullType_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            _ = this.builder.WithType(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Build_ReturnsPropertyDeclarationSyntax()
    {
        var syntax = this.builder.Build();

        syntax.Should().NotBeNull().And.Subject.Should().BeOfType<TypeParameterConstraintClauseSyntax>();
    }

    [TestMethod]
    public void Build_ReturnsExpectedString()
    {
        var syntax = this.builder
            .WithClass()
            .WithStruct()
            .WithParameterlessConstructor()
            .WithType(ConstraintType)
            .Build();

        var result = syntax.NormalizeWhitespace().ToFullString();

        result.Should().Be(Expected);
    }
}