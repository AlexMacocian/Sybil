using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sybil
{
    public sealed class ClassBuilder : IBuilder<ClassDeclarationSyntax>
    {
        private ClassDeclarationSyntax ClassDeclaration { get; set; }

        private readonly List<AttributeBuilder> Attributes = new List<AttributeBuilder>();
        private readonly List<ConstructorBuilder> Constructors = new List<ConstructorBuilder>();
        private readonly List<FieldBuilder> Fields = new List<FieldBuilder>();
        private readonly List<PropertyBuilder> Properties = new List<PropertyBuilder>();
        private readonly List<MethodBuilder> Methods = new List<MethodBuilder>();
        private readonly List<TypeParameterBuilder> TypeParameters = new List<TypeParameterBuilder>();
        private readonly List<TypeParameterConstraintBuilder> TypeParameterConstraints = new List<TypeParameterConstraintBuilder>();

        internal ClassBuilder(
            string className)
        {
            _ = string.IsNullOrWhiteSpace(className) ? throw new ArgumentNullException(nameof(className)) : className;

            this.ClassDeclaration = SyntaxFactory.ClassDeclaration(className);
        }

        public ClassBuilder WithBaseClass(string baseClass)
        {
            _ = string.IsNullOrWhiteSpace(baseClass) ? throw new ArgumentNullException(nameof(baseClass)) : baseClass;

            this.ClassDeclaration = this.ClassDeclaration.AddBaseListTypes(
                SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(baseClass)));

            return this;
        }

        public ClassBuilder WithInterface(string interfaceName)
        {
            _ = string.IsNullOrWhiteSpace(interfaceName) ? throw new ArgumentNullException(nameof(interfaceName)) : interfaceName;

            this.ClassDeclaration = this.ClassDeclaration.AddBaseListTypes(
                SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(interfaceName)));

            return this;
        }

        public ClassBuilder WithModifier(string modifier)
        {
            _ = string.IsNullOrWhiteSpace(modifier) ? throw new ArgumentNullException(nameof(modifier)) : modifier;

            this.ClassDeclaration = this.ClassDeclaration.AddModifiers(SyntaxFactory.ParseToken(modifier));

            return this;
        }

        public ClassBuilder WithModifiers(string modifiers)
        {
            _ = string.IsNullOrWhiteSpace(modifiers) ? throw new ArgumentNullException(nameof(modifiers)) : modifiers;

            this.ClassDeclaration = this.ClassDeclaration.AddModifiers(SyntaxFactory.ParseTokens(modifiers).ToArray());

            return this;
        }

        public ClassBuilder WithField(FieldBuilder fieldBuilder)
        {
            this.Fields.Add(fieldBuilder ?? throw new ArgumentNullException(nameof(fieldBuilder)));

            return this;
        }

        public ClassBuilder WithProperty(PropertyBuilder propertyBuilder)
        {
            this.Properties.Add(propertyBuilder ?? throw new ArgumentNullException(nameof(propertyBuilder)));

            return this;
        }

        public ClassBuilder WithMethod(MethodBuilder methodBuilder)
        {
            this.Methods.Add(methodBuilder ?? throw new ArgumentNullException(nameof(methodBuilder)));

            return this;
        }

        public ClassBuilder WithConstructor(ConstructorBuilder constructorBuilder)
        {
            this.Constructors.Add(constructorBuilder ?? throw new ArgumentNullException(nameof(constructorBuilder)));

            return this;
        }

        public ClassBuilder WithAttribute(AttributeBuilder attributeBuilder)
        {
            this.Attributes.Add(attributeBuilder ?? throw new ArgumentNullException(nameof(attributeBuilder)));

            return this;
        }

        public ClassBuilder WithTypeParameter(TypeParameterBuilder typeParameterBuilder)
        {
            this.TypeParameters.Add(typeParameterBuilder ?? throw new ArgumentNullException(nameof(typeParameterBuilder)));

            return this;
        }

        public ClassBuilder WithTypeParameterConstraint(TypeParameterConstraintBuilder typeConstraintBuilder)
        {
            this.TypeParameterConstraints.Add(typeConstraintBuilder ?? throw new ArgumentNullException(nameof(typeConstraintBuilder)));

            return this;
        }

        public ClassDeclarationSyntax Build()
        {
            if (this.Attributes.Count > 0)
            {
                this.ClassDeclaration = this.ClassDeclaration.AddAttributeLists(SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(this.Attributes.Select(p => p.Build()).ToArray())));
            }

            if (this.TypeParameters.Count > 0)
            {
                this.ClassDeclaration = this.ClassDeclaration.AddTypeParameterListParameters(this.TypeParameters.Select(t => t.Build()).ToArray());
                if (this.TypeParameterConstraints.Count > 0)
                {
                    this.ClassDeclaration = this.ClassDeclaration.AddConstraintClauses(this.TypeParameterConstraints.Select(t => t.Build()).ToArray());
                }
            }

            return this.ClassDeclaration
                .AddMembers(this.Constructors.Select(p => p.Build()).ToArray())
                .AddMembers(this.Fields.Select(p => p.Build()).ToArray())
                .AddMembers(this.Properties.Select(p => p.Build()).ToArray())
                .AddMembers(this.Methods.Select(p => p.Build()).ToArray())
                .NormalizeWhitespace();
        }
    }
}
