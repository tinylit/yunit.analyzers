using System.Threading.Tasks;
using Xunit;
using Verifier = CSharpVerifier<Yunit.Analyzers.MethodMultiAsyncDiagnosticAnalyzer>;

namespace Yunit.Analyzers.Analyzers
{
	public class MethodMultiAsyncDiagnosticAnalyzerTests
	{
		[Fact]
		public async Task Test()
		{
			string csharpTxt = @"
				using System;
				using System.Collections.Generic;
				using System.Threading.Tasks;

				namespace ClassLibrary1;

				public class Class1
				{
					private Task<List<int>> FooAsync()
					{						
						return Task.FromResult(new List<int>(0));
					}
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}
	}
}
