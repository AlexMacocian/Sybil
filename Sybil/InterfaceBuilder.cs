using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Sybil
{
    public sealed class InterfaceBuilder : IBuilder<InterfaceDeclarationSyntax>
    {
        private InterfaceDeclarationSyntax InterfaceDeclaration { get; set; }

        private readonly List<PropertyBuilder> Properties = new List<PropertyBuilder>();
        private readonly List<MethodBuilder> Methods = new List<MethodBuilder>();

        internal InterfaceBuilder(
            string interfaceName)
        {
            _ = string.IsNullOrWhiteSpace(interfaceName) ? throw new ArgumentNullException(nameof(interfaceName)) : interfaceName;

            this.InterfaceDeclaration = SyntaxFactory.InterfaceDeclaration(interfaceName);
        }

        public InterfaceBuilder WithModifier(string modifier)
        {
            _ = string.IsNullOrWhiteSpace(modifier) ? throw new ArgumentNullException(nameof(modifier)) : modifier;

            this.InterfaceDeclaration = this.InterfaceDeclaration.AddModifiers(SyntaxFactory.ParseToken(modifier));

            return this;
        }

        public InterfaceBuilder WithModifiers(string modifiers)
        {
            _ = string.IsNullOrWhiteSpace(modifiers) ? throw new ArgumentNullException(nameof(modifiers)) : modifiers;

            this.InterfaceDeclaration = this.InterfaceDeclaration.AddModifiers(SyntaxFactory.ParseTokens(modifiers).ToArray());

            return this;
        }

        public InterfaceBuilder WithProperty(PropertyBuilder propertyBuilder)
        {
            this.Properties.Add(propertyBuilder ?? throw new ArgumentNullException(nameof(propertyBuilder)));

            return this;
        }

        public InterfaceBuilder WithMethod(MethodBuilder methodBuilder)
        {
            this.Methods.Add(methodBuilder ?? throw new ArgumentNullException(nameof(methodBuilder)));

            return this;
        }

        public InterfaceDeclarationSyntax Build()
        {
            return this.InterfaceDeclaration
                .AddMembers(this.Properties.Select(p => p.Build()).ToArray())
                .AddMembers(this.Methods.Select(p => p.Build()).ToArray())
                .NormalizeWhitespace();
        }
    }
}
