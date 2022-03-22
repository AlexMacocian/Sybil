using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace Sybil
{
    public sealed class BaseConstructorBuilder : IBuilder<ConstructorInitializerSyntax>
    {
        private ConstructorInitializerSyntax ConstructorInitializerSyntax { get; set; }

        internal BaseConstructorBuilder()
        {
            this.ConstructorInitializerSyntax = SyntaxFactory.ConstructorInitializer(SyntaxKind.BaseConstructorInitializer);
        }

        public BaseConstructorBuilder WithArgument(string argumentName)
        {
            _ = string.IsNullOrWhiteSpace(argumentName) ? throw new ArgumentNullException(nameof(argumentName)) : argumentName;

            this.ConstructorInitializerSyntax = this.ConstructorInitializerSyntax.AddArgumentListArguments(SyntaxFactory.Argument(SyntaxFactory.IdentifierName(argumentName)));

            return this;
        }

        public ConstructorInitializerSyntax Build()
        {
            return ConstructorInitializerSyntax.NormalizeWhitespace();
        }
    }
}
