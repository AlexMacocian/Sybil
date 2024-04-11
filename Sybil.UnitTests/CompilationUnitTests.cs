using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sybil.Tests
{
    [TestClass]
    public class CompilationUnitTests
    {
        private const string UsingSystemWindows = "using System.Windows;";

        private readonly CompilationUnitBuilder compilationUnitBuilder = new();

        [TestMethod]
        public void WithUsing_NullUsing_ThrowsArgumentNullException()
        {
            var action = () =>
            {
                this.compilationUnitBuilder.WithUsing(null);
            };

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WithNamespace_NullNamespaceBuilder_ThrowsArgumentNullException()
        {
            var action = () =>
            {
                this.compilationUnitBuilder.WithNamespace(null);
            };

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WithUsing_ReturnsExpected()
        {
            var result = this.compilationUnitBuilder.WithUsing("System.Windows").Build().ToFullString();

            result.Should().Be(UsingSystemWindows);
        }
    }
}
