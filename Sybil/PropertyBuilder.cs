using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sybil
{
    public sealed class PropertyBuilder : IBuilder<PropertyDeclarationSyntax>
    {
        private readonly List<AccessorBuilder> accessors = new List<AccessorBuilder>();

        private PropertyDeclarationSyntax PropertyDeclarationSyntax { get; set; }

        internal PropertyBuilder(
            string typeName,
            string propertyName)
        {
            _ = string.IsNullOrWhiteSpace(typeName) ? throw new ArgumentNullException(nameof(typeName)) : typeName;
            _ = string.IsNullOrWhiteSpace(propertyName) ? throw new ArgumentNullException(nameof(propertyName)) : propertyName;

            this.PropertyDeclarationSyntax = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(typeName), propertyName);
        }

        public PropertyBuilder WithModifier(string modifier)
        {
            _ = string.IsNullOrWhiteSpace(modifier) ? throw new ArgumentNullException(nameof(modifier)) : modifier;

            this.PropertyDeclarationSyntax = this.PropertyDeclarationSyntax.AddModifiers(SyntaxFactory.ParseToken(modifier));

            return this;
        }
        public PropertyBuilder WithModifiers(string modifiers)
        {
            _ = string.IsNullOrWhiteSpace(modifiers) ? throw new ArgumentNullException(nameof(modifiers)) : modifiers;

            this.PropertyDeclarationSyntax = this.PropertyDeclarationSyntax.AddModifiers(SyntaxFactory.ParseTokens(modifiers).ToArray());

            return this;
        }
        public PropertyBuilder WithAccessor(AccessorBuilder accessorBuilder)
        {
            this.accessors.Add(accessorBuilder);

            return this;
        }

        public PropertyDeclarationSyntax Build()
        {
            return this.PropertyDeclarationSyntax.WithAccessorList(this.BuildAccessorList()).NormalizeWhitespace();
        }

        private AccessorListSyntax BuildAccessorList()
        {
            return SyntaxFactory.AccessorList(
                SyntaxFactory.Token(SyntaxKind.OpenBraceToken),
                accessors: SyntaxFactory.List(this.accessors.Select(a => a.Build())),
                SyntaxFactory.Token(SyntaxKind.CloseBraceToken));
        }
    }
}
