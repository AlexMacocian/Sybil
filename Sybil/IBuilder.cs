using Microsoft.CodeAnalysis.CSharp;

namespace Sybil
{
    public interface IBuilder<T> where T : CSharpSyntaxNode
    {
        T Build();
    }
}
