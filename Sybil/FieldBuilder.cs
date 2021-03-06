using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace Sybil
{
    public sealed class FieldBuilder : IBuilder<FieldDeclarationSyntax>
    {
        private FieldDeclarationSyntax FieldDeclarationSyntax { get; set; }

        internal FieldBuilder(
            string fieldType,
            string fieldName)
        {
            _ = string.IsNullOrWhiteSpace(fieldType) ? throw new ArgumentNullException(nameof(fieldType)) : fieldType;
            _ = string.IsNullOrWhiteSpace(fieldName) ? throw new ArgumentNullException(nameof(fieldName)) : fieldName;

            var variableDeclaration = SyntaxFactory.VariableDeclaration(
                SyntaxFactory.ParseTypeName(fieldType))
                .AddVariables(SyntaxFactory.VariableDeclarator(fieldName));
            this.FieldDeclarationSyntax = SyntaxFactory.FieldDeclaration(variableDeclaration);
        }

        public FieldBuilder WithModifier(string modifier)
        {
            _ = string.IsNullOrWhiteSpace(modifier) ? throw new ArgumentNullException(nameof(modifier)) : modifier;

            this.FieldDeclarationSyntax = this.FieldDeclarationSyntax.AddModifiers(SyntaxFactory.ParseToken(modifier));

            return this;
        }
        public FieldBuilder WithModifiers(string modifiers)
        {
            _ = string.IsNullOrWhiteSpace(modifiers) ? throw new ArgumentNullException(nameof(modifiers)) : modifiers;

            this.FieldDeclarationSyntax = this.FieldDeclarationSyntax.AddModifiers(SyntaxFactory.ParseTokens(modifiers).ToArray());

            return this;
        }

        public FieldDeclarationSyntax Build()
        {
            return this.FieldDeclarationSyntax.NormalizeWhitespace();
        }
    }
}
