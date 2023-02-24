using System.Threading.Tasks;
using Xunit;
using Verifier = CSharpVerifier<Yunit.Analyzers.SwitchDiagnosticAnalyzer>;
namespace Yunit.Analyzers.Analyzers
{
	public class SwitchDiagnosticAnalyzerTests
	{
		[Fact]
		public async Task Test()
		{
			var csharpTxt = @"
				using System;

				namespace ClassLibrary1;

				public class Class1
				{
					private void Foo()
					{						
						var kind = DateTimeKind.Utc;

						switch (kind)
						{
							case DateTimeKind.Utc:
								break;
							case DateTimeKind.Local:
								break;
						}
					}
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}
	}
}
