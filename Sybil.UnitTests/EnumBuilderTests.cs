using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sybil.Tests;

[TestClass]
public class EnumBuilderTests
{
    private const string EnumWithMembers =
@"public enum Test : byte
{
    First,
    Second = 5
}";
    private const string EnumWithAttribute =
@"[Flags]
enum Test
{
    A = 1
}";

    private readonly EnumBuilder builder = new("Test");

    [TestMethod]
    public void Constructor_NullName_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            _ = new EnumBuilder(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithMember_NullName_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            this.builder.WithMember(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithBaseType_NullType_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            this.builder.WithBaseType(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithMembers_WithBaseType_ReturnsExpectedString()
    {
        var result = this.builder
            .WithModifier("public")
            .WithBaseType("byte")
            .WithMember("First")
            .WithMember("Second", "5")
            .Build()
            .ToFullString();

        result.Should().Be(EnumWithMembers);
    }

    [TestMethod]
    public void WithAttribute_ReturnsExpectedString()
    {
        var result = this.builder
            .WithAttribute(SyntaxBuilder.CreateAttribute("Flags"))
            .WithMember("A", "1")
            .Build()
            .ToFullString();

        result.Should().Be(EnumWithAttribute);
    }

    [TestMethod]
    public void Build_CalledTwice_ReturnsSameString()
    {
        this.builder
            .WithAttribute(SyntaxBuilder.CreateAttribute("Flags"))
            .WithMember("A", "1");

        var first = this.builder.Build().ToFullString();
        var second = this.builder.Build().ToFullString();

        second.Should().Be(first);
    }
}
