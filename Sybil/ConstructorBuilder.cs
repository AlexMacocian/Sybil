using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sybil
{
    public sealed class ConstructorBuilder : IBuilder<ConstructorDeclarationSyntax>
    {
        private readonly List<AttributeBuilder> attributes = new List<AttributeBuilder>();

        private BaseConstructorBuilder baseConstructorBuilder;

        private ConstructorDeclarationSyntax ConstructorDeclarationSyntax { get; set; }

        internal ConstructorBuilder(
            string className)
        {
            _ = string.IsNullOrWhiteSpace(className) ? throw new ArgumentNullException(nameof(className)) : className;

            this.ConstructorDeclarationSyntax = SyntaxFactory.ConstructorDeclaration(className);
            this.ConstructorDeclarationSyntax = this.ConstructorDeclarationSyntax.WithBody(SyntaxFactory.Block());
        }

        public ConstructorBuilder WithBase(BaseConstructorBuilder baseConstructorBuilder)
        {
            _ = baseConstructorBuilder ?? throw new ArgumentNullException(nameof(baseConstructorBuilder));

            this.baseConstructorBuilder = baseConstructorBuilder;
            return this;
        }

        public ConstructorBuilder WithModifiers(string modifiers)
        {
            _ = string.IsNullOrWhiteSpace(modifiers) ? throw new ArgumentNullException(nameof(modifiers)) : modifiers;

            this.ConstructorDeclarationSyntax = this.ConstructorDeclarationSyntax.AddModifiers(SyntaxFactory.ParseTokens(modifiers).ToArray());

            return this;
        }

        public ConstructorBuilder WithModifier(string modifier)
        {
            _ = string.IsNullOrWhiteSpace(modifier) ? throw new ArgumentNullException(nameof(modifier)) : modifier;

            this.ConstructorDeclarationSyntax = this.ConstructorDeclarationSyntax.AddModifiers(SyntaxFactory.ParseToken(modifier));

            return this;
        }

        public ConstructorBuilder WithParameter(string parameterType, string parameterName, string defaultValue = null)
        {
            _ = string.IsNullOrWhiteSpace(parameterName) ? throw new ArgumentNullException(nameof(parameterName)) : parameterName;
            _ = string.IsNullOrWhiteSpace(parameterType) ? throw new ArgumentNullException(nameof(parameterType)) : parameterType;

            EqualsValueClauseSyntax equalsValueClauseSyntax = null;
            if (string.IsNullOrWhiteSpace(defaultValue) is false)
            {
                equalsValueClauseSyntax = SyntaxFactory.EqualsValueClause(
                    SyntaxFactory.ParseExpression(defaultValue));
            }

            var parameter = SyntaxFactory.Parameter(
                new SyntaxList<AttributeListSyntax>(),
                new SyntaxTokenList(),
                SyntaxFactory.IdentifierName(SyntaxFactory.Identifier(parameterType)),
                SyntaxFactory.Identifier(parameterName),
                equalsValueClauseSyntax);
            this.ConstructorDeclarationSyntax = this.ConstructorDeclarationSyntax.AddParameterListParameters(parameter);

            return this;
        }

        public ConstructorBuilder WithBody(string body)
        {
            _ = string.IsNullOrWhiteSpace(body) ? throw new ArgumentNullException(nameof(body)) : body;

            this.ConstructorDeclarationSyntax = this.ConstructorDeclarationSyntax.WithBody(SyntaxFactory.Block(SyntaxFactory.ParseStatement(body)));

            return this;
        }

        public ConstructorBuilder WithAttribute(AttributeBuilder attributeBuilder)
        {
            _ = attributeBuilder ?? throw new ArgumentNullException(nameof(attributeBuilder));

            this.attributes.Add(attributeBuilder);
            return this;
        }

        public ConstructorDeclarationSyntax Build()
        {
            if (this.baseConstructorBuilder is null is false)
            {
                this.ConstructorDeclarationSyntax = this.ConstructorDeclarationSyntax.WithInitializer(this.baseConstructorBuilder.Build());
            }

            if (this.attributes.Count > 0)
            {
                this.ConstructorDeclarationSyntax = this.ConstructorDeclarationSyntax.AddAttributeLists(SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(this.attributes.Select(p => p.Build()).ToArray())));
            }

            return this.ConstructorDeclarationSyntax
                .NormalizeWhitespace();
        }
    }
}
