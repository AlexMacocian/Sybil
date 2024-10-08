﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sybil
{
    public sealed class FieldBuilder : IBuilder<FieldDeclarationSyntax>
    {
        private readonly List<AttributeBuilder> attributeBuilders = new List<AttributeBuilder>();
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

        public FieldBuilder WithAttribute(AttributeBuilder attributeBuilder)
        {
            this.attributeBuilders.Add(attributeBuilder ?? throw new ArgumentNullException(nameof(attributeBuilder)));

            return this;
        }

        public FieldBuilder WithInitializer(string initializer)
        {
            if (string.IsNullOrWhiteSpace(initializer))
            {
                throw new ArgumentNullException(nameof(initializer));
            }

            var variableDeclaration = this.FieldDeclarationSyntax.Declaration.Variables[0];
            variableDeclaration = variableDeclaration.WithInitializer(SyntaxFactory.EqualsValueClause(SyntaxFactory.ParseExpression(initializer)));
            this.FieldDeclarationSyntax = this.FieldDeclarationSyntax
                .WithDeclaration(
                    this.FieldDeclarationSyntax.Declaration
                        .WithVariables(SyntaxFactory.SeparatedList<VariableDeclaratorSyntax>(new SyntaxNodeOrToken[] { variableDeclaration })));

            return this;
        }

        public FieldDeclarationSyntax Build()
        {
            if (this.attributeBuilders.Count > 0)
            {
                this.FieldDeclarationSyntax = this.FieldDeclarationSyntax.AddAttributeLists(SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(this.attributeBuilders.Select(p => p.Build()).ToArray())));
            }

            return this.FieldDeclarationSyntax
                .NormalizeWhitespace();
        }
    }
}
