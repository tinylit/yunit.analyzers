using System.Threading.Tasks;
using Xunit;
using Verifier = CSharpVerifier<Yunit.Analyzers.MethodAsyncDiagnostAnalyzer>;

namespace Yunit.Analyzers.Analyzers
{
	public class MethodAsyncDiagnostAnalyzerTests
	{
		[Fact]
		public async Task Test()
		{
			string csharpTxt = @"
				using System;
				using System.Threading.Tasks;

				namespace ClassLibrary1;

				public class Class1
				{
					private async Task Foo()
					{						
						for (int i = 0;i < 10; i++)
						{
							await Task.Delay(i);
						}
					}
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}
	}
}
