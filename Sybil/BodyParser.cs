using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Sybil
{
    internal static class BodyParser
    {
        /// <summary>
        /// Parses the whole body as a single block, so that statements spanning multiple lines are preserved.
        /// </summary>
        public static BlockSyntax ParseBlock(string body)
        {
            return (BlockSyntax)SyntaxFactory.ParseStatement("{\n" + body + "\n}");
        }

        /// <summary>
        /// Parses an arrow expression. A trailing semicolon is accepted and ignored, the declaration emits its own.
        /// </summary>
        public static ArrowExpressionClauseSyntax ParseArrowExpression(string expression)
        {
            return SyntaxFactory.ArrowExpressionClause(SyntaxFactory.ParseExpression(expression.Trim().TrimEnd(';')));
        }
    }
}
