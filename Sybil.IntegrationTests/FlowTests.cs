using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sybil.IntegrationTests
{
    [TestClass]
    public sealed class FlowTests
    {
        private const string ExpectedNamespace = 
@"namespace TestNamespace
{
    using System;

    public sealed class TestClass : BaseTestClass
    {
        TestClass(string fieldString) : base(fieldString)
        {
            this.fieldString = fieldString ?? throw new ArgumentNullException();
        }

        private string fieldString;
        public string PropertyString { private set; get; }

        public string GetFieldString()
        {
            return this.fieldString;
        }
    }
}";

        [TestMethod]
        public void NewNamespace_GeneratesExpected()
        {
            var namespaceSyntax = SyntaxBuilder.CreateNamespace("TestNamespace")
                .WithUsing("System")
                .WithClass(
                    SyntaxBuilder.CreateClass("TestClass")
                    .WithBaseClass("BaseTestClass")
                    .WithModifiers("public sealed")
                    .WithConstructor(
                        SyntaxBuilder.CreateConstructor("TestClass")
                        .WithBase(
                            SyntaxBuilder.CreateBaseConstructor()
                            .WithArgument("fieldString"))
                        .WithParameter("string", "fieldString")
                        .WithBody("this.fieldString = fieldString ?? throw new ArgumentNullException();"))
                    .WithField(
                        SyntaxBuilder.CreateField("string", "fieldString")
                        .WithModifier("private"))
                    .WithProperty(
                        SyntaxBuilder.CreateProperty("string", "PropertyString")
                        .WithModifier("public")
                        .WithAccessor(
                            SyntaxBuilder.CreateSetter()
                            .WithModifier("private"))
                        .WithAccessor(
                            SyntaxBuilder.CreateGetter()))
                    .WithMethod(
                        SyntaxBuilder.CreateMethod("string", "GetFieldString")
                        .WithModifier("public")
                        .WithBody("return this.fieldString;")))
                .Build();

            var namespaceString = namespaceSyntax.ToFullString();
            namespaceString.Should().Be(ExpectedNamespace);
        }
    }
}
