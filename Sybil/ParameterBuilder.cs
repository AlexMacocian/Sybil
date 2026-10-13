using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sybil
{
    public sealed class ParameterBuilder : IBuilder<ParameterSyntax>
    {
        private readonly List<AttributeBuilder> attributes = new List<AttributeBuilder>();

        private ParameterSyntax ParameterSyntax { get; set; }

        internal ParameterBuilder(
            string parameterType,
            string parameterName)
        {
            _ = string.IsNullOrWhiteSpace(parameterType) ? throw new ArgumentNullException(nameof(parameterType)) : parameterType;
            _ = string.IsNullOrWhiteSpace(parameterName) ? throw new ArgumentNullException(nameof(parameterName)) : parameterName;

            this.ParameterSyntax = SyntaxFactory.Parameter(SyntaxFactory.ParseToken(parameterName))
                .WithType(SyntaxFactory.ParseTypeName(parameterType));
        }

        public ParameterBuilder WithModifier(string modifier)
        {
            _ = string.IsNullOrWhiteSpace(modifier) ? throw new ArgumentNullException(nameof(modifier)) : modifier;

            this.ParameterSyntax = this.ParameterSyntax.AddModifiers(SyntaxFactory.ParseToken(modifier));

            return this;
        }

        public ParameterBuilder WithModifiers(string modifiers)
        {
            _ = string.IsNullOrWhiteSpace(modifiers) ? throw new ArgumentNullException(nameof(modifiers)) : modifiers;

            this.ParameterSyntax = this.ParameterSyntax.AddModifiers(SyntaxFactory.ParseTokens(modifiers).ToArray());

            return this;
        }

        public ParameterBuilder WithDefaultValue(string defaultValue)
        {
            _ = string.IsNullOrWhiteSpace(defaultValue) ? throw new ArgumentNullException(nameof(defaultValue)) : defaultValue;

            this.ParameterSyntax = this.ParameterSyntax.WithDefault(
                SyntaxFactory.EqualsValueClause(SyntaxFactory.ParseExpression(defaultValue)));

            return this;
        }

        public ParameterBuilder WithAttribute(AttributeBuilder attributeBuilder)
        {
            this.attributes.Add(attributeBuilder ?? throw new ArgumentNullException(nameof(attributeBuilder)));

            return this;
        }

        public ParameterSyntax Build()
        {
            var parameterSyntax = this.ParameterSyntax;
            if (this.attributes.Count > 0)
            {
                parameterSyntax = parameterSyntax.AddAttributeLists(SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(this.attributes.Select(p => p.Build()).ToArray())));
            }

            return parameterSyntax.NormalizeWhitespace();
        }
    }
}
