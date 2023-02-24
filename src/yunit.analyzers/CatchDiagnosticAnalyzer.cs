using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Yunit.Analyzers
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class CatchDiagnosticAnalyzer : GeneralDiagnosticAnalyzer
	{
		public CatchDiagnosticAnalyzer() : base(Descriptors.CD3001)
		{
		}

		protected override void Analysis(AnalysisContext context)
		{
			context.RegisterOperationAction(AnalyzeCatchOfSwallow, OperationKind.CatchClause);
		}

		private void AnalyzeCatchOfSwallow(OperationAnalysisContext context)
		{
			ICatchClauseOperation catchClauseOperation = (ICatchClauseOperation)context.Operation;

			if (!catchClauseOperation.Handler.Children.Any())
			{
				context.ReportDiagnostic(Create(catchClauseOperation.Syntax.GetLocation()));
			}
		}
	}
}
