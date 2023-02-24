using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Yunit.Analyzers
{
	public sealed class LocalSymbolAnalysisContext
	{
		private readonly OperationAnalysisContext context;

		public LocalSymbolAnalysisContext(OperationAnalysisContext context, ILocalSymbol symbol)
		{
			Symbol = symbol;
			this.context = context;
		}

		public ILocalSymbol Symbol { get; }

		/// <summary>
		/// <see cref="IOperation"/> that is the subject of the analysis.
		/// </summary>
		public IVariableDeclaratorOperation Operation => (IVariableDeclaratorOperation)context.Operation;

		/// <summary>
		/// <see cref="ISymbol"/> for the declaration containing the operation.
		/// </summary>
		public ISymbol ContainingSymbol => context.ContainingSymbol;

		/// <summary>
		/// <see cref="CodeAnalysis.Compilation"/> containing the <see cref="IOperation"/>.
		/// </summary>
		public Compilation Compilation => context.Compilation;

		/// <summary>
		/// Options specified for the analysis.
		/// </summary>
		public AnalyzerOptions Options => context.Options;

		/// <summary>
		/// Token to check for requested cancellation of the analysis.
		/// </summary>
		public CancellationToken CancellationToken => context.CancellationToken;

		public void ReportDiagnostic(Diagnostic diagnostic)
		{
			context.ReportDiagnostic(Diagnostic.Create(diagnostic.Descriptor, context.Operation.Syntax.GetLocation(), diagnostic.DefaultSeverity, diagnostic.AdditionalLocations, diagnostic.Properties));
		}
	}
}
