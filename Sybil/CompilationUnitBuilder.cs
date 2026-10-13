using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sybil
{
    public sealed class CompilationUnitBuilder : IBuilder<CompilationUnitSyntax>
    {
        private readonly List<NamespaceBuilder> namespaceBuilders = new List<NamespaceBuilder>();
        private readonly List<string> headerComments = new List<string>();
        private bool? nullableContext;

        private CompilationUnitSyntax CompilationUnitSyntax { get; set; }

        internal CompilationUnitBuilder()
        {
            this.CompilationUnitSyntax = SyntaxFactory.CompilationUnit();
        }

        public CompilationUnitBuilder WithUsing(string usingName)
        {
            if (string.IsNullOrWhiteSpace(usingName))
            {
                throw new ArgumentNullException(nameof(usingName));
            }

            this.CompilationUnitSyntax = this.CompilationUnitSyntax.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(usingName)));
            return this;
        }

        public CompilationUnitBuilder WithNamespace(NamespaceBuilder namespaceBuilder)
        {
            this.namespaceBuilders.Add(namespaceBuilder ?? throw new ArgumentNullException(nameof(namespaceBuilder)));
            return this;
        }

        /// <summary>
        /// Adds a single line comment at the top of the compilation unit (e.g. "&lt;auto-generated/&gt;").
        /// </summary>
        public CompilationUnitBuilder WithHeaderComment(string comment)
        {
            _ = comment ?? throw new ArgumentNullException(nameof(comment));

            this.headerComments.Add(comment);
            return this;
        }

        /// <summary>
        /// Adds a #nullable enable/disable directive at the top of the compilation unit.
        /// </summary>
        public CompilationUnitBuilder WithNullableContext(bool enabled)
        {
            this.nullableContext = enabled;
            return this;
        }

        public CompilationUnitSyntax Build()
        {
            var compilationUnitSyntax = this.CompilationUnitSyntax
                .AddMembers(this.namespaceBuilders.Select(n => n.Build()).ToArray())
                .NormalizeWhitespace();
            compilationUnitSyntax = Formatting.SeparateFileScopedNamespaces(compilationUnitSyntax);

            var headerTrivia = new List<SyntaxTrivia>();
            foreach (var comment in this.headerComments)
            {
                headerTrivia.Add(SyntaxFactory.Comment($"// {comment}"));
                headerTrivia.Add(SyntaxFactory.CarriageReturnLineFeed);
            }

            if (this.nullableContext is bool enabled)
            {
                var directive = SyntaxFactory.NullableDirectiveTrivia(
                    SyntaxFactory.Token(enabled ? SyntaxKind.EnableKeyword : SyntaxKind.DisableKeyword),
                    true)
                    .NormalizeWhitespace()
                    .WithEndOfDirectiveToken(
                        SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken)
                            .WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed));
                headerTrivia.Add(SyntaxFactory.Trivia(directive));
            }

            if (headerTrivia.Count == 0)
            {
                return compilationUnitSyntax;
            }

            return compilationUnitSyntax.WithLeadingTrivia(headerTrivia.Concat(compilationUnitSyntax.GetLeadingTrivia()));
        }
    }
}
