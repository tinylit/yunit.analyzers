using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Yunit.Analyzers
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class EnumDiagnosticAnalyzer : GeneralDiagnosticAnalyzer
	{
		public EnumDiagnosticAnalyzer() : base(Descriptors.CD1200)
		{
		}

		protected override void Analysis(AnalysisContext context)
		{
			context.RegisterSymbolAction(AnalyzeEnumSymbol, SymbolKind.NamedType);
		}
		private void AnalyzeEnumSymbol(SymbolAnalysisContext context)
		{
			var symbol = context.Symbol;

			ITypeSymbol typeSymbol = (ITypeSymbol)symbol;

			if (typeSymbol.TypeKind == TypeKind.Enum)
			{
				if (!symbol.Name.StartsWith("Enum"))
				{
					context.ReportDiagnostic(Create(symbol.Locations.FirstOrDefault()));
				}
			}
		}
	}
}
