using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Sybil
{
    internal static class ParameterFactory
    {
        private static readonly HashSet<SyntaxKind> ParameterModifiers = new HashSet<SyntaxKind>
        {
            SyntaxKind.ParamsKeyword,
            SyntaxKind.ThisKeyword,
            SyntaxKind.RefKeyword,
            SyntaxKind.OutKeyword,
            SyntaxKind.InKeyword,
            SyntaxKind.ReadOnlyKeyword
        };

        /// <summary>
        /// Creates a parameter from a type string. Leading parameter modifiers in the type string (e.g. "params string[]") become parameter modifiers.
        /// </summary>
        public static ParameterSyntax Create(string parameterType, string parameterName, EqualsValueClauseSyntax defaultValue)
        {
            var modifiers = new List<SyntaxToken>();
            var typeText = parameterType.Trim();
            while (true)
            {
                var separatorIndex = typeText.IndexOf(' ');
                if (separatorIndex <= 0)
                {
                    break;
                }

                var candidate = typeText.Substring(0, separatorIndex);
                var kind = SyntaxFacts.GetKeywordKind(candidate);
                if (kind == SyntaxKind.None)
                {
                    kind = SyntaxFacts.GetContextualKeywordKind(candidate);
                }

                if (!ParameterModifiers.Contains(kind))
                {
                    break;
                }

                modifiers.Add(SyntaxFactory.Token(kind));
                typeText = typeText.Substring(separatorIndex + 1).TrimStart();
            }

            return SyntaxFactory.Parameter(
                SyntaxFactory.List<AttributeListSyntax>(),
                SyntaxFactory.TokenList(modifiers),
                SyntaxFactory.ParseTypeName(typeText),
                SyntaxFactory.ParseToken(parameterName),
                defaultValue);
        }
    }
}
