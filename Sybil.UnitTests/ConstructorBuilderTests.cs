using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sybil.Tests
{
    [TestClass]
    public class ConstructorBuilderTests
    {
        private const string Public = "public";
        private const string PublicSealed = "public sealed";
        private const string Null = "null";
        private const string ParameterType = "string";
        private const string ParameterName = "someString";
        private const string Name = "Test";
        private const string Body = "this.someString = someString;";
        private const string PublicEmptyConstructor =
@"public Test()
{
}";
        private const string PublicSealedEmptyConstructor =
@"public sealed Test()
{
}";
        private const string ConstructorWithParameter =
@"Test(string someString)
{
}";
        private const string ConstructorWithParameterWithDefault =
@"Test(string someString = null)
{
}";
        private const string ConstructorWithBody =
@"Test()
{
    this.someString = someString;
}";
        private const string ConstructorWithModifiersBodyAndParameter =
@"public public sealed Test(string someString = null)
{
    this.someString = someString;
}";

        private readonly ConstructorBuilder builder;

        public ConstructorBuilderTests()
        {
            this.builder = new ConstructorBuilder(Name);
        }

        [TestMethod]
        public void Constructor_NullName_ThrowArgumentNullException()
        {
            var action = () =>
            {
                _ = new ConstructorBuilder(null);
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
        public void WithBase_NullBase_ThrowsArgumentNullException()
        {
            var action = () =>
            {
                this.builder.WithBase(null);
            };

            action.Should().Throw<ArgumentNullException>();
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
        public void Build_ReturnsConstructorDeclarationSyntax()
        {
            var syntax = this.builder.Build();

            syntax.Should().NotBeNull().And.Subject.Should().BeOfType<ConstructorDeclarationSyntax>();
        }

        [TestMethod]
        public void WithModifier_ReturnsExpectedString()
        {
            var result = this.builder.WithModifier(Public).Build().ToFullString();

            result.Should().Be(PublicEmptyConstructor);
        }

        [TestMethod]
        public void WithModifiers_ReturnsExpectedString()
        {
            var result = this.builder.WithModifiers(PublicSealed).Build().ToFullString();

            result.Should().Be(PublicSealedEmptyConstructor);
        }

        [TestMethod]
        public void WithParameter_ReturnsExpectedString()
        {
            var result = this.builder.WithParameter(ParameterType, ParameterName).Build().ToFullString();

            result.Should().Be(ConstructorWithParameter);
        }

        [TestMethod]
        public void WithParameter_WithDefault_ReturnsExpectedString()
        {
            var result = this.builder.WithParameter(ParameterType, ParameterName, Null).Build().ToFullString();

            result.Should().Be(ConstructorWithParameterWithDefault);
        }

        [TestMethod]
        public void WithBody_ReturnsExpectedString()
        {
            var result = this.builder.WithBody(Body).Build().ToFullString();

            result.Should().Be(ConstructorWithBody);
        }

        [TestMethod]
        public void WithBody_WithParameter_WithModifier_WithModifiers_ReturnsExpectedString()
        {
            var result = this.builder
                .WithBody(Body)
                .WithParameter(ParameterType, ParameterName, Null)
                .WithModifier(Public)
                .WithModifiers(PublicSealed)
                .Build()
                .ToFullString();

            result.Should().Be(ConstructorWithModifiersBodyAndParameter);
        }
    }
}