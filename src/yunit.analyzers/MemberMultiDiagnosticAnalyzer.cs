using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Yunit.Analyzers.Extentions;

namespace Yunit.Analyzers
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class MemberMultiDiagnosticAnalyzer : LocalDiagnosticAnalyzer
	{
		public MemberMultiDiagnosticAnalyzer() : base(Descriptors.CD1003)
		{
		}

		protected override void Analysis(AnalysisContext context)
		{
			context.RegisterSymbolAction(AnalyzeGeneralSymbol, SymbolKind.Method, SymbolKind.Parameter, SymbolKind.Property, SymbolKind.Field);
		}

		private void AnalyzeGeneralSymbol(SymbolAnalysisContext context)
		{
			var symbol = context.Symbol;

			var typeSymbol = symbol.Kind switch
			{
				SymbolKind.Field when symbol is IFieldSymbol fieldSymbol => fieldSymbol.Type,
				SymbolKind.Local when symbol is ILocalSymbol localSymbol => localSymbol.Type,
				SymbolKind.Property when symbol is IPropertySymbol propertySymbol => propertySymbol.Type,
				SymbolKind.Parameter when symbol is IParameterSymbol parameterSymbol => parameterSymbol.Type,
				SymbolKind.Method when symbol is IMethodSymbol methodSymbol => methodSymbol.ReturnType,
				_ => throw new NotSupportedException($"类型【{symbol.Kind}】不受支持！"),
			};

			if (typeSymbol.IsMulti())
			{
				if (!symbol.Name.EndsWith("s"))
				{
					context.ReportDiagnostic(Create(symbol.Locations.FirstOrDefault()));
				}
			}
		}

		protected override void AnalyzeLocalSymbol(LocalSymbolAnalysisContext context)
		{
            var symbol = context.Symbol;

			var typeSymbol = symbol.Type;

			if (typeSymbol.IsMulti())
			{
				if (!symbol.Name.EndsWith("s"))
				{
					context.ReportDiagnostic(Create(symbol.Locations.FirstOrDefault()));
				}
			}
		}
	}
}
