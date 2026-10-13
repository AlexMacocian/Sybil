using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sybil
{
    public sealed class StructBuilder : IBuilder<StructDeclarationSyntax>
    {
        private StructDeclarationSyntax StructDeclaration { get; set; }

        private readonly List<AttributeBuilder> Attributes = new List<AttributeBuilder>();
        private readonly List<ConstructorBuilder> Constructors = new List<ConstructorBuilder>();
        private readonly List<FieldBuilder> Fields = new List<FieldBuilder>();
        private readonly List<PropertyBuilder> Properties = new List<PropertyBuilder>();
        private readonly List<MethodBuilder> Methods = new List<MethodBuilder>();
        private readonly List<TypeParameterBuilder> TypeParameters = new List<TypeParameterBuilder>();
        private readonly List<TypeParameterConstraintBuilder> TypeParameterConstraints = new List<TypeParameterConstraintBuilder>();

        internal StructBuilder(
            string structName)
        {
            _ = string.IsNullOrWhiteSpace(structName) ? throw new ArgumentNullException(nameof(structName)) : structName;

            this.StructDeclaration = SyntaxFactory.StructDeclaration(structName);
        }

        public StructBuilder WithInterface(string interfaceName)
        {
            _ = string.IsNullOrWhiteSpace(interfaceName) ? throw new ArgumentNullException(nameof(interfaceName)) : interfaceName;

            this.StructDeclaration = this.StructDeclaration.AddBaseListTypes(
                SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(interfaceName)));

            return this;
        }

        public StructBuilder WithModifier(string modifier)
        {
            _ = string.IsNullOrWhiteSpace(modifier) ? throw new ArgumentNullException(nameof(modifier)) : modifier;

            this.StructDeclaration = this.StructDeclaration.AddModifiers(SyntaxFactory.ParseToken(modifier));

            return this;
        }

        public StructBuilder WithModifiers(string modifiers)
        {
            _ = string.IsNullOrWhiteSpace(modifiers) ? throw new ArgumentNullException(nameof(modifiers)) : modifiers;

            this.StructDeclaration = this.StructDeclaration.AddModifiers(SyntaxFactory.ParseTokens(modifiers).ToArray());

            return this;
        }

        public StructBuilder WithField(FieldBuilder fieldBuilder)
        {
            this.Fields.Add(fieldBuilder ?? throw new ArgumentNullException(nameof(fieldBuilder)));

            return this;
        }

        public StructBuilder WithProperty(PropertyBuilder propertyBuilder)
        {
            this.Properties.Add(propertyBuilder ?? throw new ArgumentNullException(nameof(propertyBuilder)));

            return this;
        }

        public StructBuilder WithMethod(MethodBuilder methodBuilder)
        {
            this.Methods.Add(methodBuilder ?? throw new ArgumentNullException(nameof(methodBuilder)));

            return this;
        }

        public StructBuilder WithConstructor(ConstructorBuilder constructorBuilder)
        {
            this.Constructors.Add(constructorBuilder ?? throw new ArgumentNullException(nameof(constructorBuilder)));

            return this;
        }

        public StructBuilder WithAttribute(AttributeBuilder attributeBuilder)
        {
            this.Attributes.Add(attributeBuilder ?? throw new ArgumentNullException(nameof(attributeBuilder)));

            return this;
        }

        public StructBuilder WithTypeParameter(TypeParameterBuilder typeParameterBuilder)
        {
            this.TypeParameters.Add(typeParameterBuilder ?? throw new ArgumentNullException(nameof(typeParameterBuilder)));

            return this;
        }

        public StructBuilder WithTypeParameterConstraint(TypeParameterConstraintBuilder typeConstraintBuilder)
        {
            this.TypeParameterConstraints.Add(typeConstraintBuilder ?? throw new ArgumentNullException(nameof(typeConstraintBuilder)));

            return this;
        }

        public StructDeclarationSyntax Build()
        {
            if (this.Attributes.Count > 0)
            {
                this.StructDeclaration = this.StructDeclaration.AddAttributeLists(SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(this.Attributes.Select(p => p.Build()).ToArray())));
            }

            if (this.TypeParameters.Count > 0)
            {
                this.StructDeclaration = this.StructDeclaration.AddTypeParameterListParameters(this.TypeParameters.Select(t => t.Build()).ToArray());
                if (this.TypeParameterConstraints.Count > 0)
                {
                    this.StructDeclaration = this.StructDeclaration.AddConstraintClauses(this.TypeParameterConstraints.Select(t => t.Build()).ToArray());
                }
            }

            return this.StructDeclaration
                .AddMembers(this.Constructors.Select(p => p.Build()).ToArray())
                .AddMembers(this.Fields.Select(p => p.Build()).ToArray())
                .AddMembers(this.Properties.Select(p => p.Build()).ToArray())
                .AddMembers(this.Methods.Select(p => p.Build()).ToArray())
                .NormalizeWhitespace();
        }
    }
}
