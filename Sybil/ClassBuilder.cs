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
        private readonly string className;
        private ClassDeclarationSyntax ClassDeclaration { get; set; }

        private readonly List<ConstructorBuilder> Constructors = new List<ConstructorBuilder>();
        private readonly List<FieldBuilder> Fields = new List<FieldBuilder>();
        private readonly List<PropertyBuilder> Properties = new List<PropertyBuilder>();
        private readonly List<MethodBuilder> Methods = new List<MethodBuilder>();

        internal ClassBuilder(
            string className)
        {
            _ = string.IsNullOrWhiteSpace(className) ? throw new ArgumentNullException(nameof(className)) : className;

            this.className = className;
            this.ClassDeclaration = SyntaxFactory.ClassDeclaration(className);
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

        public ClassDeclarationSyntax Build()
        {
            return this.ClassDeclaration
                .AddMembers(this.Constructors.Select(p => p.Build()).ToArray())
                .AddMembers(this.Fields.Select(p => p.Build()).ToArray())
                .AddMembers(this.Properties.Select(p => p.Build()).ToArray())
                .AddMembers(this.Methods.Select(p => p.Build()).ToArray())
                .NormalizeWhitespace();
        }
    }
}
