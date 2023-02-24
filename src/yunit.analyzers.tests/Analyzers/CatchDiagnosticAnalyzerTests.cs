using System.Threading.Tasks;
using Xunit;
using Verifier = CSharpVerifier<Yunit.Analyzers.CatchDiagnosticAnalyzer>;

namespace Yunit.Analyzers.Analyzers
{
	public class CatchDiagnosticAnalyzerTests
	{
		[Fact]
		public async Task Test()
		{
			string csharpTxt = @"
					using System;

					namespace ClassLibrary1;

					public class Class1
					{
						public static void Foo()
						{
							try { } catch (Exception ex) { }
						}
					}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}
	}
}
