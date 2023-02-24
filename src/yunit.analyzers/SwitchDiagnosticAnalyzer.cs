using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Yunit.Analyzers
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class SwitchDiagnosticAnalyzer : GeneralDiagnosticAnalyzer
	{
		public SwitchDiagnosticAnalyzer() : base(Descriptors.CD3000)
		{
		}

		protected override void Analysis(AnalysisContext context)
		{
			context.RegisterOperationAction(AnalyzeSwitchCaseOfDefault, OperationKind.Switch);
		}

		private void AnalyzeSwitchCaseOfDefault(OperationAnalysisContext context)
		{
			bool flag = true;

			ISwitchOperation switchOperation = (ISwitchOperation)context.Operation;

			for (int i = switchOperation.Cases.Length - 1; i >= 0; i--)
			{
				var caseOperation = switchOperation.Cases[i];

				if (caseOperation.Clauses.Any(x => x.CaseKind == CaseKind.Default))
				{
					flag = false;

					break;
				}
			}

			if (flag)
			{
				context.ReportDiagnostic(Create(switchOperation.Syntax.GetLocation()));
			}
		}
	}
}
