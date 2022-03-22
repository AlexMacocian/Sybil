using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sybil.Tests
{
    [TestClass]
    public class MethodBuilderTests
    {
        private const string Public = "public";
        private const string PublicSealed = "public sealed";
        private const string Null = "null";
        private const string ParameterType = "string";
        private const string ParameterName = "someString";
        private const string Name = "Test";
        private const string ReturnType = "string";
        private const string Expression = "this.someString;";
        private const string Body = "this.someString = someString;";
        private const string PublicEmptyMethod = "public string Test() => throw new NotImplementedException() ;";
        private const string PublicSealedEmptyMethod = "public sealed string Test() => throw new NotImplementedException() ;";
        private const string EmptyMethodWithParameter = "string Test(string someString) => throw new NotImplementedException() ;";
        private const string EmptyMethodWithParameterWithDefault = "string Test(string someString = null) => throw new NotImplementedException() ;";
        private const string MethodWithArrowExpression = "string Test() => this.someString ;";
        private const string PublicSealedMethodWithParameterAndExpression = "public sealed string Test(string someString = null) => this.someString ;";
        private const string MethodWithBody = 
@"string Test()
{
    this.someString = someString;
}";
        private const string PublicSealedMethodWithBodyAndParameter =
@"public sealed string Test(string someString = null)
{
    this.someString = someString;
}";

        private readonly MethodBuilder builder;

        public MethodBuilderTests()
        {
            this.builder = new MethodBuilder(ReturnType, Name);
        }

        [TestMethod]
        public void Constructor_NullName_ThrowArgumentNullException()
        {
            var action = () =>
            {
                _ = new MethodBuilder(ReturnType, null);
            };

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Constructor_NullReturnType_ThrowArgumentNullException()
        {
            var action = () =>
            {
                _ = new MethodBuilder(null, Name);
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
            var returnedBuilder = this.builder.WithModifiers(PublicSealed);

            returnedBuilder.Should().NotBeNull().And.Subject.Should().Be(this.builder);
        }

        [TestMethod]
        public void WithParameter_ParameterNameNull_ThrowsArgumentNullException()
        {
            var action = () =>
            {
                this.builder.WithParameter(ParameterType, null);
            };

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WithParameter_ParameterTypeNull_ThrowsArgumentNullException()
        {
            var action = () =>
            {
                this.builder.WithParameter(null, ParameterName);
            };

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WithParameter_ParameterValid_ReturnsBuilder()
        {
            var returnedBuilder = this.builder.WithParameter(ParameterType, ParameterName);

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
            var returnedBuilder = this.builder.WithBody(Body);

            returnedBuilder.Should().NotBeNull().And.Subject.Should().Be(this.builder);
        }

        [TestMethod]
        public void WithExpression_ExpressionNull_ThrowsArgumentNullException()
        {
            var action = () =>
            {
                this.builder.WithExpression(null);
            };

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WithExpression_ExpressionValid_ReturnsBuilder()
        {
            var returnedBuilder = this.builder.WithExpression(Expression);

            returnedBuilder.Should().NotBeNull().And.Subject.Should().Be(this.builder);
        }

        [TestMethod]
        public void Build_ReturnsMethodDeclarationSyntax()
        {
            var syntax = this.builder.Build();

            syntax.Should().NotBeNull().And.Subject.Should().BeOfType<MethodDeclarationSyntax>();
        }

        [TestMethod]
        public void WithModifier_ReturnsExpectedString()
        {
            var result = this.builder.WithModifier(Public).Build().ToFullString();

            result.Should().Be(PublicEmptyMethod);
        }

        [TestMethod]
        public void WithModifiers_ReturnsExpectedString()
        {
            var result = this.builder.WithModifiers(PublicSealed).Build().ToFullString();

            result.Should().Be(PublicSealedEmptyMethod);
        }

        [TestMethod]
        public void WithParameter_ReturnsExpectedString()
        {
            var result = this.builder.WithParameter(ParameterType, ParameterName).Build().ToFullString();

            result.Should().Be(EmptyMethodWithParameter);
        }

        [TestMethod]
        public void WithParameter_WithDefault_ReturnsExpectedString()
        {
            var result = this.builder.WithParameter(ParameterType, ParameterName, Null).Build().ToFullString();

            result.Should().Be(EmptyMethodWithParameterWithDefault);
        }

        [TestMethod]
        public void WithBody_ReturnsExpectedString()
        {
            var result = this.builder.WithBody(Body).Build().ToFullString();

            result.Should().Be(MethodWithBody);
        }

        [TestMethod]
        public void WithArrowExpression_ReturnsExpectedString()
        {
            var result = this.builder.WithExpression(Expression).Build().ToFullString();

            result.Should().Be(MethodWithArrowExpression);
        }

        [TestMethod]
        public void WithBody_OverwritesWithExpression_ReturnsExpectedString()
        {
            var result = this.builder
                .WithExpression(Expression)
                .WithBody(Body)
                .Build()
                .ToFullString();

            result.Should().Be(MethodWithBody);
        }

        [TestMethod]
        public void WithExpression_OverwritesWithBody_ReturnsExpectedString()
        {
            var result = this.builder
                .WithBody(Body)
                .WithExpression(Expression)
                .Build()
                .ToFullString();

            result.Should().Be(MethodWithArrowExpression);
        }

        [TestMethod]
        public void WithBody_WithModifiers_WithParameter_ReturnsExpectedString()
        {
            var result = this.builder
                .WithBody(Body)
                .WithModifiers(PublicSealed)
                .WithParameter(ParameterType, ParameterName, Null)
                .Build()
                .ToFullString();

            result.Should().Be(PublicSealedMethodWithBodyAndParameter);
        }

        [TestMethod]
        public void WithExpression_WithModifiers_WithParameter_ReturnsExpectedString()
        {
            var result = this.builder
                .WithExpression(Expression)
                .WithModifiers(PublicSealed)
                .WithParameter(ParameterType, ParameterName, Null)
                .Build()
                .ToFullString();

            result.Should().Be(PublicSealedMethodWithParameterAndExpression);
        }
    }
}