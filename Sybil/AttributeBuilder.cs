using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace Sybil
{
    public sealed class AttributeBuilder : IBuilder<AttributeSyntax>
    {
        private AttributeSyntax AttributeSyntax { get; set; }

        internal AttributeBuilder(string attributeName)
        {
            this.AttributeSyntax = SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(attributeName));
        }

        public AttributeBuilder WithArgument(string propertyName, string value)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.NameEquals(propertyName),
                    null,
                    SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(string propertyName, int value)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.NameEquals(propertyName),
                    null,
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(string propertyName, float value)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.NameEquals(propertyName),
                    null,
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(string propertyName, double value)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.NameEquals(propertyName),
                    null,
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(string propertyName, long value)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.NameEquals(propertyName),
                    null,
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(string propertyName, uint value)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.NameEquals(propertyName),
                    null,
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(string propertyName, ulong value)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.NameEquals(propertyName),
                    null,
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(string propertyName, short value)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.NameEquals(propertyName),
                    null,
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(string propertyName, ushort value)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.NameEquals(propertyName),
                    null,
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(string propertyName, byte value)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.NameEquals(propertyName),
                    null,
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(string propertyName, bool value)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.NameEquals(propertyName),
                    null,
                    SyntaxFactory.LiteralExpression(value ? SyntaxKind.TrueLiteralExpression : SyntaxKind.FalseLiteralExpression)));

            return this;
        }

        public AttributeBuilder WithArgument(string propertyName, char value)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.NameEquals(propertyName),
                    null,
                    SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithNullArgument(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.NameEquals(propertyName),
                    null,
                    SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression)));

            return this;
        }

        public AttributeBuilder WithArgument(string propertyName, Type type)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.NameEquals(propertyName),
                    null,
                    SyntaxFactory.TypeOfExpression(SyntaxFactory.ParseTypeName(type.Name))));

            return this;
        }

        public AttributeBuilder WithArgument<TEnum>(string propertyName, TEnum value)
            where TEnum : Enum
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.NameEquals(propertyName),
                    null,
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName(typeof(TEnum).Name),
                        SyntaxFactory.IdentifierName(Enum.GetName(typeof(TEnum), value)))));

            return this;
        }

        public AttributeBuilder WithArgument(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(int value)
        {
            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(float value)
        {
            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(double value)
        {
            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(long value)
        {
            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(uint value)
        {
            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(ulong value)
        {
            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(short value)
        {
            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(ushort value)
        {
            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(byte value)
        {
            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithArgument(bool value)
        {
            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.LiteralExpression(value ? SyntaxKind.TrueLiteralExpression : SyntaxKind.FalseLiteralExpression)));

            return this;
        }

        public AttributeBuilder WithArgument(char value)
        {
            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(value))));

            return this;
        }

        public AttributeBuilder WithNullArgument()
        {
            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression)));

            return this;
        }

        public AttributeBuilder WithArgument(Type type)
        {
            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.TypeOfExpression(
                        SyntaxFactory.ParseTypeName(type.Name))));

            return this;
        }

        public AttributeBuilder WithArgument<TEnum>(TEnum value)
            where TEnum : Enum
        {
            this.AttributeSyntax = this.AttributeSyntax.AddArgumentListArguments(
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName(typeof(TEnum).Name),
                        SyntaxFactory.IdentifierName(Enum.GetName(typeof(TEnum), value)))));

            return this;
        }

        public AttributeSyntax Build()
        {
            return this.AttributeSyntax
                .NormalizeWhitespace();
        }
    }
}
