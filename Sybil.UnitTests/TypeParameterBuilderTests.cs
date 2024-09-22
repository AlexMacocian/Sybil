using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sybil.Tests;

[TestClass]
public class TypeParameterBuilderTests
{
    private const string TypeName = "T";

    private readonly TypeParameterBuilder builder;

    public TypeParameterBuilderTests()
    {
        this.builder = new TypeParameterBuilder(TypeName);
    }

    [TestMethod]
    public void Constructor_NullType_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            _ = new TypeParameterBuilder(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Build_ReturnsPropertyDeclarationSyntax()
    {
        var syntax = this.builder.Build();

        syntax.Should().NotBeNull().And.Subject.Should().BeOfType<TypeParameterSyntax>();
    }
}