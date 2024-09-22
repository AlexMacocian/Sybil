using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace Sybil
{
    public sealed class TypeParameterConstraintBuilder : IBuilder<TypeParameterConstraintClauseSyntax>
    {
        private TypeParameterConstraintClauseSyntax TypeParameterConstraintClauseSyntax { get; set; }

        internal TypeParameterConstraintBuilder(string typeIdentifier)
        {
            _ = typeIdentifier ?? throw new ArgumentNullException(nameof(typeIdentifier));
            this.TypeParameterConstraintClauseSyntax = SyntaxFactory.TypeParameterConstraintClause(typeIdentifier);
        }

        public TypeParameterConstraintBuilder WithClass()
        {
            this.TypeParameterConstraintClauseSyntax = this.TypeParameterConstraintClauseSyntax.AddConstraints(SyntaxFactory.ClassOrStructConstraint(SyntaxKind.ClassConstraint));
            return this;
        }

        public TypeParameterConstraintBuilder WithStruct()
        {
            this.TypeParameterConstraintClauseSyntax = this.TypeParameterConstraintClauseSyntax.AddConstraints(SyntaxFactory.ClassOrStructConstraint(SyntaxKind.StructConstraint));
            return this;
        }

        public TypeParameterConstraintBuilder WithParameterlessConstructor()
        {
            this.TypeParameterConstraintClauseSyntax = this.TypeParameterConstraintClauseSyntax.AddConstraints(SyntaxFactory.ConstructorConstraint());
            return this;
        }

        public TypeParameterConstraintBuilder WithType(string typeIdentifier)
        {
            this.TypeParameterConstraintClauseSyntax = this.TypeParameterConstraintClauseSyntax.AddConstraints(SyntaxFactory.TypeConstraint(SyntaxFactory.ParseTypeName(typeIdentifier ?? throw new ArgumentNullException(nameof(typeIdentifier)))));
            return this;
        }

        public TypeParameterConstraintClauseSyntax Build()
        {
            return this.TypeParameterConstraintClauseSyntax;
        }
    }
}
