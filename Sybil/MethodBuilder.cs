using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sybil
{
    public sealed class MethodBuilder : IBuilder<MethodDeclarationSyntax>
    {
        private readonly List<AttributeBuilder> attributeBuilders = new List<AttributeBuilder>();

        private BlockSyntax BlockBody { get; set; }
        private ArrowExpressionClauseSyntax ArrowExpression { get; set; }
        private MethodDeclarationSyntax MethodDeclarationSyntax { get; set; }
        private readonly List<TypeParameterBuilder> TypeParameters = new List<TypeParameterBuilder>();
        private readonly List<TypeParameterConstraintBuilder> TypeParameterConstraints = new List<TypeParameterConstraintBuilder>();

        internal MethodBuilder(
            string returnType, string name)
        {
            _ = string.IsNullOrWhiteSpace(returnType) ? throw new ArgumentNullException(nameof(returnType)) : returnType;
            _ = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name;

            this.MethodDeclarationSyntax = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName(returnType), SyntaxFactory.ParseToken(name));
            this.ArrowExpression = SyntaxFactory.ArrowExpressionClause(
                SyntaxFactory.ParseExpression("throw new NotImplementedException();"));
        }

        public MethodBuilder WithModifier(string modifier)
        {
            _ = string.IsNullOrWhiteSpace(modifier) ? throw new ArgumentNullException(nameof(modifier)) : modifier;

            this.MethodDeclarationSyntax = this.MethodDeclarationSyntax.AddModifiers(SyntaxFactory.ParseToken(modifier));

            return this;
        }

        public MethodBuilder WithModifiers(string modifiers)
        {
            _ = string.IsNullOrWhiteSpace(modifiers) ? throw new ArgumentNullException(nameof(modifiers)) : modifiers;

            this.MethodDeclarationSyntax = this.MethodDeclarationSyntax.AddModifiers(SyntaxFactory.ParseTokens(modifiers).ToArray());

            return this;
        }

        public MethodBuilder WithParameter(string parameterType, string parameterName, string defaultValue = null)
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
                SyntaxFactory.List<AttributeListSyntax>(),
                SyntaxFactory.TokenList(),
                type: SyntaxFactory.ParseTypeName(parameterType),
                identifier: SyntaxFactory.ParseToken(parameterName),
                @default: equalsValueClauseSyntax);

            this.MethodDeclarationSyntax = this.MethodDeclarationSyntax.AddParameterListParameters(parameter);

            return this;
        }

        public MethodBuilder WithThisParameter(string parameterType, string parameterName)
        {
            _ = string.IsNullOrWhiteSpace(parameterName) ? throw new ArgumentNullException(nameof(parameterName)) : parameterName;
            _ = string.IsNullOrWhiteSpace(parameterType) ? throw new ArgumentNullException(nameof(parameterType)) : parameterType;

            var parameter = SyntaxFactory.Parameter(
                SyntaxFactory.List<AttributeListSyntax>(),
                SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.ThisKeyword)),
                type: SyntaxFactory.ParseTypeName(parameterType),
                identifier: SyntaxFactory.ParseToken(parameterName),
                @default: null);

            this.MethodDeclarationSyntax = this.MethodDeclarationSyntax.AddParameterListParameters(parameter);

            return this;
        }

        public MethodBuilder WithBody(string body)
        {
            if (string.IsNullOrWhiteSpace(body))
            {
                throw new ArgumentNullException(nameof(body));
            }

            this.BlockBody = SyntaxFactory.Block(
                body.Split('\n', '\r').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => SyntaxFactory.ParseStatement(s)));
            this.ArrowExpression = null;

            return this;
        }

        public MethodBuilder WithExpression(string expression)
        {
            this.ArrowExpression = SyntaxFactory.ArrowExpressionClause(
                SyntaxFactory.ParseExpression(expression));
            this.BlockBody = null;

            return this;
        }

        public MethodBuilder WithNoBody()
        {
            this.BlockBody = null;
            this.ArrowExpression = null;
            return this;
        }

        public MethodBuilder WithAttribute(AttributeBuilder attributeBuilder)
        {
            this.attributeBuilders.Add(attributeBuilder ?? throw new ArgumentNullException(nameof(attributeBuilder)));

            return this;
        }

        public MethodBuilder WithTypeParameter(TypeParameterBuilder typeParameterBuilder)
        {
            this.TypeParameters.Add(typeParameterBuilder ?? throw new ArgumentNullException(nameof(typeParameterBuilder)));

            return this;
        }

        public MethodBuilder WithTypeParameterConstraint(TypeParameterConstraintBuilder typeConstraintBuilder)
        {
            this.TypeParameterConstraints.Add(typeConstraintBuilder ?? throw new ArgumentNullException(nameof(typeConstraintBuilder)));

            return this;
        }

        public MethodDeclarationSyntax Build()
        {
            if (this.attributeBuilders.Count > 0)
            {
                this.MethodDeclarationSyntax = this.MethodDeclarationSyntax.AddAttributeLists(SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(this.attributeBuilders.Select(p => p.Build()).ToArray())));
            }

            if (this.TypeParameters.Count > 0)
            {
                this.MethodDeclarationSyntax = this.MethodDeclarationSyntax.AddTypeParameterListParameters(this.TypeParameters.Select(t => t.Build()).ToArray());
                if (this.TypeParameterConstraints.Count > 0)
                {
                    this.MethodDeclarationSyntax = this.MethodDeclarationSyntax.AddConstraintClauses(this.TypeParameterConstraints.Select(t => t.Build()).ToArray());
                }
            }

            if (this.BlockBody is null is false)
            {
                return this.MethodDeclarationSyntax
                    .WithBody(this.BlockBody)
                    .NormalizeWhitespace();
            }

            if (this.ArrowExpression is null is false)
            {
                return this.MethodDeclarationSyntax
                .WithExpressionBody(this.ArrowExpression)
                .NormalizeWhitespace();
            }

            return this.MethodDeclarationSyntax
                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                .NormalizeWhitespace();
        }
    }
}
