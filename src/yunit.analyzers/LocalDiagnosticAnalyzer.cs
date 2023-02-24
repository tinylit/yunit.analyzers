using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Yunit.Analyzers
{
	public abstract class LocalDiagnosticAnalyzer : GeneralDiagnosticAnalyzer
	{
		public LocalDiagnosticAnalyzer(DiagnosticDescriptor descriptor) : base(descriptor)
		{
		}

		public sealed override void Initialize(AnalysisContext context)
		{
			context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
			context.EnableConcurrentExecution();

			Analysis(context);

			context.RegisterOperationAction(AnalyzeLocalSymbol, OperationKind.VariableDeclarator);
		}

		/// <summary>
		/// variable.
		/// </summary>
		private void AnalyzeLocalSymbol(OperationAnalysisContext context)
		{
			IVariableDeclaratorOperation variableDeclaratorOperation = (IVariableDeclaratorOperation)context.Operation;

			var localSymbol = variableDeclaratorOperation.Symbol;

			var symbolContext = new LocalSymbolAnalysisContext(context, localSymbol);

			AnalyzeLocalSymbol(symbolContext);
		}

		protected abstract void AnalyzeLocalSymbol(LocalSymbolAnalysisContext context);
	}
}
