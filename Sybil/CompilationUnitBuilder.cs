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

        public CompilationUnitSyntax Build()
        {
            this.CompilationUnitSyntax = this.CompilationUnitSyntax.AddMembers(this.namespaceBuilders.Select(n => n.Build()).ToArray());
            return this.CompilationUnitSyntax.NormalizeWhitespace();
        }
    }
}
