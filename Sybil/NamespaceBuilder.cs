using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sybil
{
    public sealed class NamespaceBuilder : IBuilder<BaseNamespaceDeclarationSyntax>
    {
        private readonly List<AttributeBuilder> attributeBuilders = new List<AttributeBuilder>();
        private readonly List<ClassBuilder> classBuilders = new List<ClassBuilder>();
        private BaseNamespaceDeclarationSyntax NamespaceDeclaration { get; set; }

        internal NamespaceBuilder(
            string @namespace, bool fileScoped = true)
        {
            _ = string.IsNullOrWhiteSpace(@namespace) ? throw new ArgumentNullException(nameof(@namespace)) : @namespace;
            
            this.NamespaceDeclaration = fileScoped ? 
                (BaseNamespaceDeclarationSyntax)SyntaxFactory.FileScopedNamespaceDeclaration(SyntaxFactory.ParseName(@namespace)) :
                (BaseNamespaceDeclarationSyntax)SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(@namespace));
        }

        public NamespaceBuilder WithUsing(string usingName)
        {
            _ = string.IsNullOrWhiteSpace(usingName) ? throw new ArgumentNullException(nameof(usingName)) : usingName;

            this.NamespaceDeclaration = this.NamespaceDeclaration.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(usingName)));

            return this;
        }

        public NamespaceBuilder WithClass(ClassBuilder classBuilder)
        {
            _ = classBuilder ?? throw new ArgumentNullException(nameof(classBuilder));

            this.classBuilders.Add(classBuilder);

            return this;
        }

        public NamespaceBuilder WithAttribute(AttributeBuilder attributeBuilder)
        {
            _ = attributeBuilder ?? throw new ArgumentNullException(nameof(attributeBuilder));

            this.attributeBuilders.Add(attributeBuilder);

            return this;
        }

        public BaseNamespaceDeclarationSyntax Build()
        {
            if (this.attributeBuilders.Count > 0)
            {
                this.NamespaceDeclaration = this.NamespaceDeclaration.AddAttributeLists(SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(this.attributeBuilders.Select(p => p.Build()).ToArray())));
            }

            return this.NamespaceDeclaration
                .AddMembers(this.classBuilders.Select(c => c.Build()).ToArray())
                .NormalizeWhitespace();
        }
    }
}
