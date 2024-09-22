using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace Sybil
{
    public sealed class TypeParameterBuilder : IBuilder<TypeParameterSyntax>
    {
        private readonly string identifier;

        internal TypeParameterBuilder(string identifier)
        {
            _ = identifier ?? throw new ArgumentNullException(nameof(identifier));

            this.identifier = identifier;
        }

        public TypeParameterSyntax Build()
        {
            return SyntaxFactory.TypeParameter(this.identifier);
        }
    }
}
