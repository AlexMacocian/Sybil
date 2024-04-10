using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sybil.Tests;

[TestClass]
public class AttributeBuilderTests
{
    private const string AttributeName = "SomeAttribute";
    private const string PropertyName = "SomeProperty";
    private const string StringValue = "SomeString";
    private const int IntValue = 1;
    private const uint UintValue = 1U;
    private const long LongValue = 1L;
    private const ulong UlongValue = 1UL;
    private const short ShortValue = 1;
    private const ushort UshortValue = 1;
    private const float FloatValue = 1.5f;
    private const double DoubleValue = 1.5;
    private const byte ByteValue = 1;
    private const char CharValue = 'c';
    private const bool BoolTrueValue = true;
    private const bool BoolFalseValue = false;

    private const string AttributeWithPropertyNameAndStringValue = $"{AttributeName}({PropertyName} = \"{StringValue}\")";
    private const string AttributeWithPropertyNameAndIntValue = $"{AttributeName}({PropertyName} = 1)";
    private const string AttributeWithPropertyNameAndUintValue = $"{AttributeName}({PropertyName} = 1U)";
    private const string AttributeWithPropertyNameAndLongValue = $"{AttributeName}({PropertyName} = 1L)";
    private const string AttributeWithPropertyNameAndUlongValue = $"{AttributeName}({PropertyName} = 1UL)";
    private const string AttributeWithPropertyNameAndShortValue = $"{AttributeName}({PropertyName} = 1)";
    private const string AttributeWithPropertyNameAndUshortValue = $"{AttributeName}({PropertyName} = 1)";
    private const string AttributeWithPropertyNameAndFloatValue = $"{AttributeName}({PropertyName} = 1.5F)";
    private const string AttributeWithPropertyNameAndDoubleValue = $"{AttributeName}({PropertyName} = 1.5)";
    private const string AttributeWithPropertyNameAndByteValue = $"{AttributeName}({PropertyName} = 1)";
    private const string AttributeWithPropertyNameAndCharValue = $"{AttributeName}({PropertyName} = 'c')";
    private const string AttributeWithPropertyNameAndBoolTrueValue = $"{AttributeName}({PropertyName} = true)";
    private const string AttributeWithPropertyNameAndBoolFalseValue = $"{AttributeName}({PropertyName} = false)";
    private const string AttributeWithPropertyNameAndNullValue = $"{AttributeName}({PropertyName} = null)";
    private const string AttributeWithStringValue = $"{AttributeName}(\"{StringValue}\")";
    private const string AttributeWithIntValue = $"{AttributeName}(1)";
    private const string AttributeWithUintValue = $"{AttributeName}(1U)";
    private const string AttributeWithLongValue = $"{AttributeName}(1L)";
    private const string AttributeWithUlongValue = $"{AttributeName}(1UL)";
    private const string AttributeWithShortValue = $"{AttributeName}(1)";
    private const string AttributeWithUshortValue = $"{AttributeName}(1)";
    private const string AttributeWithFloatValue = $"{AttributeName}(1.5F)";
    private const string AttributeWithDoubleValue = $"{AttributeName}(1.5)";
    private const string AttributeWithByteValue = $"{AttributeName}(1)";
    private const string AttributeWithCharValue = $"{AttributeName}('c')";
    private const string AttributeWithBoolTrueValue = $"{AttributeName}(true)";
    private const string AttributeWithBoolFalseValue = $"{AttributeName}(false)";
    private const string AttributeWithNullValue = $"{AttributeName}(null)";

    private readonly AttributeBuilder attributeBuilder = SyntaxBuilder.CreateAttribute(AttributeName);

    [TestMethod]
    public void WithArgumentBool_NullPropertyName_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            attributeBuilder.WithArgument(null, false);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithArgumentString_NullPropertyName_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            attributeBuilder.WithArgument(null, "Hello");
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithArgumentInt_NullPropertyName_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            attributeBuilder.WithArgument(null, 1);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithArgumentFloat_NullPropertyName_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            attributeBuilder.WithArgument(null, 1f);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithArgumentDouble_NullPropertyName_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            attributeBuilder.WithArgument(null, 1d);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithArgumentLong_NullPropertyName_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            attributeBuilder.WithArgument(null, 1L);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithArgumentUint_NullPropertyName_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            attributeBuilder.WithArgument(null, 1U);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithArgumentUlong_NullPropertyName_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            attributeBuilder.WithArgument(null, 1UL);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithArgumentShort_NullPropertyName_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            attributeBuilder.WithArgument(null, (short)1);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithArgumentUshort_NullPropertyName_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            attributeBuilder.WithArgument(null, (ushort)1);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithArgumentByte_NullPropertyName_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            attributeBuilder.WithArgument(null, (byte)1);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithArgumentChar_NullPropertyName_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            attributeBuilder.WithArgument(null, 'c');
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithNullArgument_NullPropertyName_ThrowsArgumentNullException()
    {
        var action = () =>
        {
            attributeBuilder.WithNullArgument(null);
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithArgumentString_NullValue_ThrowsArgumentNullException()
    {
        var action = () =>
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            attributeBuilder.WithArgument("SomeProperty", (string)null);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        };

        action.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void WithArgument_WithPropertyNameAndStringValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(PropertyName, StringValue).Build().ToFullString();

        result.Should().Be(AttributeWithPropertyNameAndStringValue);
    }

    [TestMethod]
    public void WithArgument_WithPropertyNameAndIntValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(PropertyName, IntValue).Build().ToFullString();

        result.Should().Be(AttributeWithPropertyNameAndIntValue);
    }

    [TestMethod]
    public void WithArgument_WithPropertyNameAndFloatValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(PropertyName, FloatValue).Build().ToFullString();

        result.Should().Be(AttributeWithPropertyNameAndFloatValue);
    }

    [TestMethod]
    public void WithArgument_WithPropertyNameAndDoubleValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(PropertyName, DoubleValue).Build().ToFullString();

        result.Should().Be(AttributeWithPropertyNameAndDoubleValue);
    }

    [TestMethod]
    public void WithArgument_WithPropertyNameAndLongValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(PropertyName, LongValue).Build().ToFullString();

        result.Should().Be(AttributeWithPropertyNameAndLongValue);
    }

    [TestMethod]
    public void WithArgument_WithPropertyNameAndUintValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(PropertyName, UintValue).Build().ToFullString();

        result.Should().Be(AttributeWithPropertyNameAndUintValue);
    }

    [TestMethod]
    public void WithArgument_WithPropertyNameAndUlongValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(PropertyName, UlongValue).Build().ToFullString();

        result.Should().Be(AttributeWithPropertyNameAndUlongValue);
    }

    [TestMethod]
    public void WithArgument_WithPropertyNameAndShortValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(PropertyName, ShortValue).Build().ToFullString();

        result.Should().Be(AttributeWithPropertyNameAndShortValue);
    }

    [TestMethod]
    public void WithArgument_WithPropertyNameAndUshortValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(PropertyName, UshortValue).Build().ToFullString();

        result.Should().Be(AttributeWithPropertyNameAndUshortValue);
    }

    [TestMethod]
    public void WithArgument_WithPropertyNameAndByteValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(PropertyName, ByteValue).Build().ToFullString();

        result.Should().Be(AttributeWithPropertyNameAndByteValue);
    }

    [TestMethod]
    public void WithArgument_WithPropertyNameAndBoolTrueValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(PropertyName, BoolTrueValue).Build().ToFullString();

        result.Should().Be(AttributeWithPropertyNameAndBoolTrueValue);
    }

    [TestMethod]
    public void WithArgument_WithPropertyNameAndBoolFalseValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(PropertyName, BoolFalseValue).Build().ToFullString();

        result.Should().Be(AttributeWithPropertyNameAndBoolFalseValue);
    }

    [TestMethod]
    public void WithArgument_WithPropertyNameAndCharValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(PropertyName, CharValue).Build().ToFullString();

        result.Should().Be(AttributeWithPropertyNameAndCharValue);
    }

    [TestMethod]
    public void WithArgument_WithPropertyNameAndNullValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithNullArgument(PropertyName).Build().ToFullString();

        result.Should().Be(AttributeWithPropertyNameAndNullValue);
    }

    [TestMethod]
    public void WithArgument_WithStringValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(StringValue).Build().ToFullString();

        result.Should().Be(AttributeWithStringValue);
    }

    [TestMethod]
    public void WithArgument_WithIntValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(IntValue).Build().ToFullString();

        result.Should().Be(AttributeWithIntValue);
    }

    [TestMethod]
    public void WithArgument_WithFloatValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(FloatValue).Build().ToFullString();

        result.Should().Be(AttributeWithFloatValue);
    }

    [TestMethod]
    public void WithArgument_WithDoubleValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(DoubleValue).Build().ToFullString();

        result.Should().Be(AttributeWithDoubleValue);
    }

    [TestMethod]
    public void WithArgument_WithLongValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(LongValue).Build().ToFullString();

        result.Should().Be(AttributeWithLongValue);
    }

    [TestMethod]
    public void WithArgument_WithUintValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(UintValue).Build().ToFullString();

        result.Should().Be(AttributeWithUintValue);
    }

    [TestMethod]
    public void WithArgument_WithUlongValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(UlongValue).Build().ToFullString();

        result.Should().Be(AttributeWithUlongValue);
    }

    [TestMethod]
    public void WithArgument_WithShortValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(ShortValue).Build().ToFullString();

        result.Should().Be(AttributeWithShortValue);
    }

    [TestMethod]
    public void WithArgument_WithUshortValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(UshortValue).Build().ToFullString();

        result.Should().Be(AttributeWithUshortValue);
    }

    [TestMethod]
    public void WithArgument_WithByteValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(ByteValue).Build().ToFullString();

        result.Should().Be(AttributeWithByteValue);
    }

    [TestMethod]
    public void WithArgument_WithBoolTrueValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(BoolTrueValue).Build().ToFullString();

        result.Should().Be(AttributeWithBoolTrueValue);
    }

    [TestMethod]
    public void WithArgument_WithBoolFalseValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(BoolFalseValue).Build().ToFullString();

        result.Should().Be(AttributeWithBoolFalseValue);
    }

    [TestMethod]
    public void WithArgument_WithCharValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithArgument(CharValue).Build().ToFullString();

        result.Should().Be(AttributeWithCharValue);
    }

    [TestMethod]
    public void WithArgument_WithNullValue_ReturnsExpected()
    {
        var result = attributeBuilder.WithNullArgument().Build().ToFullString();

        result.Should().Be(AttributeWithNullValue);
    }
}
