using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sybil.Tests
{
    [TestClass]
    public class FieldBuilderTests
    {
        private const string Public = "public";
        private const string PublicSealed = "public sealed";
        private const string FieldName = "someString";
        private const string FieldType = "string";
        private const string PublicField = "public string someString;";
        private const string PublicSealedField = "public sealed string someString;";

        private readonly FieldBuilder builder;

        public FieldBuilderTests()
        {
            this.builder = new FieldBuilder(FieldType, FieldName);
        }

        [TestMethod]
        public void Constructor_NullFieldType_ThrowsArgumentNullException()
        {
            var action = () =>
            {
                _ = new FieldBuilder(null, FieldName);
            };

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Constructor_NullFieldName_ThrowsArgumentNullException()
        {
            var action = () =>
            {
                _ = new FieldBuilder(FieldType, null);
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
        public void Build_ReturnsFieldDeclarationSyntax()
        {
            var syntax = this.builder.Build();

            syntax.Should().NotBeNull().And.Subject.Should().BeOfType<FieldDeclarationSyntax>();
        }

        [TestMethod]
        public void WithModifier_ReturnsExpectedString()
        {
            var result = this.builder.WithModifier(Public).Build().ToFullString();

            result.Should().Be(PublicField);
        }

        [TestMethod]
        public void WithModifiers_ReturnsExpectedString()
        {
            var result = this.builder.WithModifiers(PublicSealed).Build().ToFullString();

            result.Should().Be(PublicSealedField);
        }
    }
}