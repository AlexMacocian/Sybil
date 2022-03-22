using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sybil.Tests
{
    [TestClass]
    public class BaseConstructorBuilderTests
    {
        private const string EmptyBase = ": base()";
        private const string TestBase = ": base(test)";

        private readonly BaseConstructorBuilder builder;

        public BaseConstructorBuilderTests()
        {
            this.builder = new BaseConstructorBuilder();
        }

        [TestMethod]
        public void WithArgument_NullArgument_ThrowsArgumentNullException()
        {
            var action = () =>
            {
                this.builder.WithArgument(null);
            };

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WithArgument_ValidArgument_ReturnsBuilder()
        {
            var returnedBuilder = this.builder.WithArgument("test");

            returnedBuilder.Should().NotBeNull().And.Subject.Should().Be(this.builder);
        }

        [TestMethod]
        public void Build_ReturnsConstructorInitializerSyntax()
        {
            var syntax = this.builder.Build();

            syntax.Should().NotBeNull().And.Subject.Should().BeOfType<ConstructorInitializerSyntax>();
        }

        [TestMethod]
        public void EmptyBaseConstructor_ReturnsExpectedString()
        {
            var result = this.builder.Build().ToFullString();

            result.Should().Be(EmptyBase);
        }

        [TestMethod]
        public void WithArgument_ShouldReturnExpectedString()
        {
            var result = this.builder
                .WithArgument("test")
                .Build()
                .ToFullString();

            result.Should().Be(TestBase);
        }
    }
}