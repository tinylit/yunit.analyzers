using System.Threading.Tasks;
using Xunit;
using Verifier = CSharpVerifier<Yunit.Analyzers.EnumDiagnosticAnalyzer>;

namespace Yunit.Analyzers.Analyzers
{
	public class EnumDiagnosticAnalyzerTests
	{
		[Fact]
		public async Task Test()
		{
			string csharpTxt = @"
				using System;

				namespace ClassLibrary1;

				public enum Class1
				{
					Normal
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}
	}
}
