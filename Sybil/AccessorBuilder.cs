using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace Sybil
{
    public sealed class AccessorBuilder : IBuilder<AccessorDeclarationSyntax>
    {
        private readonly SyntaxKind accessor;

        private SyntaxTokenList Modifiers { get; set; }
        private BlockSyntax BlockSyntax { get; set; }
        private ArrowExpressionClauseSyntax ArrowExpressionClauseSyntax { get; set; }

        internal AccessorBuilder(
            SyntaxKind accessor)
        {
            this.accessor = accessor;
        }

        public AccessorBuilder WithModifier(string modifier)
        {
            _ = string.IsNullOrWhiteSpace(modifier) ? throw new ArgumentNullException(nameof(modifier)) : modifier;

            this.Modifiers = SyntaxFactory.TokenList(SyntaxFactory.ParseToken(modifier));

            return this;
        }
        public AccessorBuilder WithModifiers(string modifiers)
        {
            _ = string.IsNullOrWhiteSpace(modifiers) ? throw new ArgumentNullException(nameof(modifiers)) : modifiers;

            this.Modifiers = SyntaxFactory.TokenList(SyntaxFactory.ParseTokens(modifiers));

            return this;
        }
        public AccessorBuilder WithBody(string body)
        {
            _ = string.IsNullOrWhiteSpace(body) ? throw new ArgumentNullException(nameof(body)) : body;

            this.ArrowExpressionClauseSyntax = null;
            this.BlockSyntax = SyntaxFactory.Block(body.Split('\n', '\r').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => SyntaxFactory.ParseStatement(s)));

            return this;
        }
        public AccessorBuilder WithArrowExpression(string expression)
        {
            _ = string.IsNullOrWhiteSpace(expression) ? throw new ArgumentNullException(nameof(expression)) : expression;

            this.BlockSyntax = null;
            this.ArrowExpressionClauseSyntax = SyntaxFactory.ArrowExpressionClause(SyntaxFactory.ParseExpression(expression));

            return this;
        }

        public AccessorDeclarationSyntax Build()
        {
            return this.BuildAccessorDeclaration().NormalizeWhitespace();
        }

        private AccessorDeclarationSyntax BuildAccessorDeclaration()
        {
            var accessorDeclaration = SyntaxFactory.AccessorDeclaration(
                kind: this.accessor,
                attributeLists: SyntaxFactory.List<AttributeListSyntax>(),
                modifiers: this.Modifiers,
                body: this.BlockSyntax,
                expressionBody: this.ArrowExpressionClauseSyntax);

            if (this.BlockSyntax is null && this.ArrowExpressionClauseSyntax is null)
            {
                accessorDeclaration = accessorDeclaration.WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
            }

            return accessorDeclaration;
        }
    }
}
