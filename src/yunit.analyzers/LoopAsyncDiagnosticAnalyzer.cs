using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Yunit.Analyzers
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class LoopAsyncDiagnosticAnalyzer : GeneralDiagnosticAnalyzer
	{
		public LoopAsyncDiagnosticAnalyzer() : base(Descriptors.CD2000)
		{
		}

		protected override void Analysis(AnalysisContext context)
		{
			context.RegisterOperationAction(AnalyzeLoopExecuteAsync, OperationKind.Loop);
		}

		private void AnalyzeLoopExecuteAsync(OperationAnalysisContext context)
		{
			foreach (var operation in context.Operation.Children)
			{
				if (operation.Kind == OperationKind.Block)
				{
					AnalyzeLoopOperationAsync(context, operation);
				}
			}
		}

		private void AnalyzeLoopOperationAsync(OperationAnalysisContext context, IOperation operation)
		{
			if (operation.Kind == OperationKind.Await)
			{
				context.ReportDiagnostic(Create(operation.Syntax.GetLocation()));
			}
			else if (operation.Kind == OperationKind.Block
				|| operation.Kind == OperationKind.ExpressionStatement
				|| operation.Kind == OperationKind.Conditional
				|| operation.Kind == OperationKind.Try
				|| operation.Kind == OperationKind.CatchClause
				|| operation.Kind == OperationKind.Switch
				|| operation.Kind == OperationKind.SwitchCase)
			{
				foreach (var subOperation in operation.Children)
				{
					AnalyzeLoopOperationAsync(context, subOperation);
				}
			}
		}
	}
}
