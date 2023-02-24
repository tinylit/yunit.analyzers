using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Yunit.Analyzers
{
	public abstract class GeneralDiagnosticAnalyzer : DiagnosticAnalyzer
	{
		private readonly DiagnosticDescriptor descriptor;
		private readonly ImmutableArray<DiagnosticDescriptor> diagnosticDescriptors;

		protected GeneralDiagnosticAnalyzer(DiagnosticDescriptor descriptor)
		{
			if (descriptor is null) throw new ArgumentNullException(nameof(descriptor));

			this.descriptor = descriptor;
			diagnosticDescriptors = ImmutableArray.Create(descriptor);
		}
		public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => diagnosticDescriptors;

		public override void Initialize(AnalysisContext context)
		{
			context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
			context.EnableConcurrentExecution();

			Analysis(context);
		}

		protected abstract void Analysis(AnalysisContext context);

		protected Diagnostic Create(Location? location, params object?[]? messageArgs) => Diagnostic.Create(descriptor, location, messageArgs);
	}
}
