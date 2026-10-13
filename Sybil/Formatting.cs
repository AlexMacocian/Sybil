using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace Sybil
{
    internal static class Formatting
    {
        /// <summary>
        /// NormalizeWhitespace puts the first member right below a file scoped namespace; separate them with a blank line.
        /// </summary>
        public static TNode SeparateFileScopedNamespaces<TNode>(TNode node)
            where TNode : SyntaxNode
        {
            return node.ReplaceNodes(
                node.DescendantNodesAndSelf().OfType<FileScopedNamespaceDeclarationSyntax>().Where(n => n.Members.Count > 0).Select(n => n.Members[0]),
                (original, rewritten) => rewritten.WithLeadingTrivia(rewritten.GetLeadingTrivia().Insert(0, SyntaxFactory.CarriageReturnLineFeed)));
        }
    }
}
