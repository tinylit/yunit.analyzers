using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Yunit.Analyzers
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class NonPublicDiagnosticAnalyzer : LocalDiagnosticAnalyzer
	{
		public NonPublicDiagnosticAnalyzer() : base(Descriptors.CD1002)
		{
		}

		protected override void Analysis(AnalysisContext context)
		{
			context.RegisterSymbolAction(AnalyzeGeneralSymbol, SymbolKind.Parameter, SymbolKind.Event, SymbolKind.Field);
		}

		protected override void AnalyzeLocalSymbol(LocalSymbolAnalysisContext context)
		{
			var symbol = context.Symbol;

			var name = symbol.Name;

			var c = name[0];

			if (char.IsUpper(c) || name.Contains('_'))
			{
				context.ReportDiagnostic(Create(symbol.Locations.FirstOrDefault()));
			}
		}

		private void AnalyzeGeneralSymbol(SymbolAnalysisContext context)
		{
			var symbol = context.Symbol;

			if (symbol.Kind == SymbolKind.Parameter || symbol.DeclaredAccessibility == Accessibility.Private)
			{
				var name = symbol.Name;

				var c = name[0];

				if (char.IsUpper(c) || name.Contains('_'))
				{
					context.ReportDiagnostic(Create(symbol.Locations.FirstOrDefault()));
				}
			}
		}
	}
}
