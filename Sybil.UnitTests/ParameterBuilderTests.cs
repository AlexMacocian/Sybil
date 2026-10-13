using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sybil.Tests;

[TestClass]
public class ParameterBuilderTests
{
    [TestMethod]
    public void Constructor_NullType_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            _ = new ParameterBuilder(null, "name");
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Constructor_NullName_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            _ = new ParameterBuilder("string", null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Build_NullableGenericType_ReturnsExpectedString()
    {
        var result = new ParameterBuilder("IEnumerable<string?>", "values").Build().ToFullString();

        result.Should().Be("IEnumerable<string?> values");
    }

    [TestMethod]
    public void WithAttribute_WithDefaultValue_ReturnsExpectedString()
    {
        var result = new ParameterBuilder("CancellationToken", "cancellationToken")
            .WithAttribute(SyntaxBuilder.CreateAttribute("EnumeratorCancellation"))
            .WithDefaultValue("default")
            .Build()
            .ToFullString();

        result.Should().Be("[EnumeratorCancellation] CancellationToken cancellationToken = default");
    }

    [TestMethod]
    public void WithModifiers_ReturnsExpectedString()
    {
        var result = new ParameterBuilder("string[]", "values")
            .WithModifier("params")
            .Build()
            .ToFullString();

        result.Should().Be("params string[] values");
    }

    [TestMethod]
    public void VerbatimName_ReturnsExpectedString()
    {
        var result = new ParameterBuilder("int", "@class").Build().ToFullString();

        result.Should().Be("int @class");
    }
}
