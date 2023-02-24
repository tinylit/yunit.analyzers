using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Yunit.Analyzers.Extentions;

namespace Yunit.Analyzers
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class MethodMultiAsyncDiagnosticAnalyzer : GeneralDiagnosticAnalyzer
	{
		public MethodMultiAsyncDiagnosticAnalyzer() : base(Descriptors.CD1111)
		{
		}

		protected override void Analysis(AnalysisContext context)
		{
			context.RegisterSymbolAction(AnalyzeGeneralSymbol, SymbolKind.Method);
		}

		private void AnalyzeGeneralSymbol(SymbolAnalysisContext context)
		{
			var symbol = (IMethodSymbol)context.Symbol;

			if ((symbol.IsAsync || symbol.ReturnType.IsAsync()) && symbol.ReturnType is INamedTypeSymbol namedTypeSymbol && namedTypeSymbol.TypeArguments.Length == 1 && namedTypeSymbol.TypeArguments[0].IsMulti())
			{
				if (!symbol.Name.EndsWith("sAsync"))
				{
					context.ReportDiagnostic(Create(symbol.Locations.FirstOrDefault()));
				}
			}
		}
	}
}
