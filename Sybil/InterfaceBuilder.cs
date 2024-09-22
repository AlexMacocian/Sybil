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
        private readonly List<TypeParameterBuilder> TypeParameters = new List<TypeParameterBuilder>();
        private readonly List<TypeParameterConstraintBuilder> TypeParameterConstraints = new List<TypeParameterConstraintBuilder>();

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

        public InterfaceBuilder WithTypeParameter(TypeParameterBuilder typeParameterBuilder)
        {
            this.TypeParameters.Add(typeParameterBuilder ?? throw new ArgumentNullException(nameof(typeParameterBuilder)));

            return this;
        }

        public InterfaceBuilder WithTypeParameterConstraint(TypeParameterConstraintBuilder typeConstraintBuilder)
        {
            this.TypeParameterConstraints.Add(typeConstraintBuilder ?? throw new ArgumentNullException(nameof(typeConstraintBuilder)));

            return this;
        }

        public InterfaceDeclarationSyntax Build()
        {
            if (this.TypeParameters.Count > 0)
            {
                this.InterfaceDeclaration = this.InterfaceDeclaration.AddTypeParameterListParameters(this.TypeParameters.Select(t => t.Build()).ToArray());
                if (this.TypeParameterConstraints.Count > 0)
                {
                    this.InterfaceDeclaration = this.InterfaceDeclaration.AddConstraintClauses(this.TypeParameterConstraints.Select(t => t.Build()).ToArray());
                }
            }

            return this.InterfaceDeclaration
                .AddMembers(this.Properties.Select(p => p.Build()).ToArray())
                .AddMembers(this.Methods.Select(p => p.Build()).ToArray())
                .NormalizeWhitespace();
        }
    }
}
