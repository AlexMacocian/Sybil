using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sybil.Tests;

[TestClass]
public class NamespaceBuilderTests
{
    private const string Using = "System";
    private const string Namespace = "Test";
    private const string NamespaceWithUsing =
@"namespace Test
{
    using System;
}";
    private const string FileScopedNamespaceWithUsing =
@"namespace Test;
using System;";

    private readonly NamespaceBuilder builder;

    public NamespaceBuilderTests()
    {
        this.builder = new NamespaceBuilder(Namespace);
    }

    [TestMethod]
    public void Constructor_NullNamespace_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            _ = new NamespaceBuilder(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithUsing_NullUsing_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            this.builder.WithUsing(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithUsing_ValidUsing_ReturnsBuilder()
    {
        var returnedBuilder = this.builder.WithUsing(Using);

        returnedBuilder.Should().NotBeNull().And.Subject.Should().Be(this.builder);
    }

    [TestMethod]
    public void WithClass_NullClassBuilder_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            this.builder.WithClass(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Build_ReturnsFileScopedNamespaceDeclarationSyntax()
    {
        var syntax = this.builder.Build();

        syntax.Should().NotBeNull().And.Subject.Should().BeOfType<FileScopedNamespaceDeclarationSyntax>();
    }

    [TestMethod]
    public void Build_ReturnsNamespaceDeclarationSyntax()
    {
        var syntax = new NamespaceBuilder(Namespace, false).Build();

        syntax.Should().NotBeNull().And.Subject.Should().BeOfType<NamespaceDeclarationSyntax>();
    }

    [TestMethod]
    public void FileScoped_WithUsing_ReturnsExpectedString()
    {
        var result = this.builder
            .WithUsing(Using)
            .Build()
            .ToFullString();

        result.Should().Be(FileScopedNamespaceWithUsing);
    }

    [TestMethod]
    public void Enclosed_WithUsing_ReturnsExpectedString()
    {
        var result = new NamespaceBuilder(Namespace, false)
            .WithUsing(Using)
            .Build()
            .ToFullString();

        result.Should().Be(NamespaceWithUsing);
    }
}