using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Yunit.Analyzers.Extentions;

namespace Yunit.Analyzers
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class MethodAsyncDiagnostAnalyzer : GeneralDiagnosticAnalyzer
	{
		public MethodAsyncDiagnostAnalyzer() : base(Descriptors.CD1110)
		{
		}

		protected override void Analysis(AnalysisContext context)
		{
			context.RegisterSymbolAction(AnalyzeGeneralSymbol, SymbolKind.Method);
		}

		private void AnalyzeGeneralSymbol(SymbolAnalysisContext context)
		{
			var symbol = (IMethodSymbol)context.Symbol;

			if (symbol.IsAsync || symbol.ReturnType.IsAsync())
			{
				if (!symbol.Name.EndsWith("Async"))
				{
					context.ReportDiagnostic(Create(symbol.Locations.FirstOrDefault()));
				}
			}
		}
	}
}
