using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sybil
{
    public sealed class EnumBuilder : IBuilder<EnumDeclarationSyntax>
    {
        private readonly List<AttributeBuilder> attributes = new List<AttributeBuilder>();
        private readonly List<EnumMemberDeclarationSyntax> members = new List<EnumMemberDeclarationSyntax>();

        private EnumDeclarationSyntax EnumDeclarationSyntax { get; set; }

        internal EnumBuilder(
            string enumName)
        {
            _ = string.IsNullOrWhiteSpace(enumName) ? throw new ArgumentNullException(nameof(enumName)) : enumName;

            this.EnumDeclarationSyntax = SyntaxFactory.EnumDeclaration(enumName);
        }

        public EnumBuilder WithModifier(string modifier)
        {
            _ = string.IsNullOrWhiteSpace(modifier) ? throw new ArgumentNullException(nameof(modifier)) : modifier;

            this.EnumDeclarationSyntax = this.EnumDeclarationSyntax.AddModifiers(SyntaxFactory.ParseToken(modifier));

            return this;
        }

        public EnumBuilder WithModifiers(string modifiers)
        {
            _ = string.IsNullOrWhiteSpace(modifiers) ? throw new ArgumentNullException(nameof(modifiers)) : modifiers;

            this.EnumDeclarationSyntax = this.EnumDeclarationSyntax.AddModifiers(SyntaxFactory.ParseTokens(modifiers).ToArray());

            return this;
        }

        public EnumBuilder WithBaseType(string baseType)
        {
            _ = string.IsNullOrWhiteSpace(baseType) ? throw new ArgumentNullException(nameof(baseType)) : baseType;

            this.EnumDeclarationSyntax = this.EnumDeclarationSyntax.AddBaseListTypes(
                SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(baseType)));

            return this;
        }

        public EnumBuilder WithMember(string memberName, string value = null)
        {
            _ = string.IsNullOrWhiteSpace(memberName) ? throw new ArgumentNullException(nameof(memberName)) : memberName;

            var member = SyntaxFactory.EnumMemberDeclaration(SyntaxFactory.ParseToken(memberName));
            if (string.IsNullOrWhiteSpace(value) is false)
            {
                member = member.WithEqualsValue(SyntaxFactory.EqualsValueClause(SyntaxFactory.ParseExpression(value)));
            }

            this.members.Add(member);

            return this;
        }

        public EnumBuilder WithAttribute(AttributeBuilder attributeBuilder)
        {
            this.attributes.Add(attributeBuilder ?? throw new ArgumentNullException(nameof(attributeBuilder)));

            return this;
        }

        public EnumDeclarationSyntax Build()
        {
            var enumDeclarationSyntax = this.EnumDeclarationSyntax;
            if (this.attributes.Count > 0)
            {
                enumDeclarationSyntax = enumDeclarationSyntax.AddAttributeLists(SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(this.attributes.Select(p => p.Build()).ToArray())));
            }

            return enumDeclarationSyntax
                .AddMembers(this.members.ToArray())
                .NormalizeWhitespace();
        }
    }
}
