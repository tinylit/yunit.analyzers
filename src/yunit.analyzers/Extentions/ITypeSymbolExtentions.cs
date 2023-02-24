using System.Linq;
using Microsoft.CodeAnalysis;

namespace Yunit.Analyzers.Extentions
{
	public static class ITypeSymbolExtentions
	{
		/// <summary>
		/// 异步结果。
		/// </summary>
		public static bool IsAsync(this ITypeSymbol typeSymbol)
		{
			if (typeSymbol.IsValueType)
			{
				return typeSymbol.Name == "ValueTask" && IsEqualTasks(typeSymbol.ContainingNamespace);
			}

			return typeSymbol.AllInterfaces.Any(x => x.SpecialType == SpecialType.System_IAsyncResult);
		}

		private static bool IsEqualTasks(INamespaceSymbol symbol)
		{
			return symbol.Name == "Tasks" && IsEqualThreading(symbol.ContainingNamespace);
		}

		private static bool IsEqualThreading(INamespaceSymbol symbol)
		{
			return symbol.Name == "Threading" && IsEqualSystem(symbol.ContainingNamespace);
		}

		private static bool IsEqualSystem(INamespaceSymbol symbol)
		{
			return symbol.Name == "System";
		}

		/// <summary>
		/// 复数。
		/// </summary>
		public static bool IsMulti(this ITypeSymbol typeSymbol)
		{
			return typeSymbol.TypeKind == TypeKind.Array || typeSymbol.AllInterfaces.Any(x => x.SpecialType == SpecialType.System_Collections_IEnumerable);
		}
	}
}
