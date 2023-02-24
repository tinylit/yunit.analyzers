using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Yunit.Analyzers
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class PublicDiagnosticAnalyzer : GeneralDiagnosticAnalyzer
	{
		public PublicDiagnosticAnalyzer() : base(Descriptors.CD1001)
		{
		}

		protected override void Analysis(AnalysisContext context)
		{
			context.RegisterSymbolAction(AnalyzeGeneralSymbol, SymbolKind.NamedType, SymbolKind.Method, SymbolKind.Event, SymbolKind.Property, SymbolKind.Parameter, SymbolKind.Field);
		}

		/// <summary>
		/// 通用。
		/// </summary>
		/// <param name="context">上下文。</param>
		private void AnalyzeGeneralSymbol(SymbolAnalysisContext context)
		{
			var symbol = context.Symbol;

			if (symbol.Kind == SymbolKind.NamedType || symbol.Kind == SymbolKind.Property || symbol.Kind == SymbolKind.Parameter || (symbol.Kind == SymbolKind.Method
					? (symbol is IMethodSymbol methodSymbol) && methodSymbol.MethodKind != MethodKind.PropertyGet && methodSymbol.MethodKind != MethodKind.PropertySet
					: symbol.DeclaredAccessibility > Accessibility.Private))
			{
				var name = symbol.Name;

				var c = name[0];

				if (char.IsLower(c) || name.Contains('_'))
				{
					context.ReportDiagnostic(Create(symbol.Locations.FirstOrDefault()));
				}
			}
		}
	}
}
