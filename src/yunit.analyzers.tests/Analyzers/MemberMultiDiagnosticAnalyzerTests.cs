using System.Threading.Tasks;
using Xunit;
using Verifier = CSharpVerifier<Yunit.Analyzers.MemberMultiDiagnosticAnalyzer>;
namespace Yunit.Analyzers.Analyzers
{
	public class MemberMultiDiagnosticAnalyzerTests
	{
		[Fact]
		public async Task TestPrivateField()
		{
			string csharpTxt = @"
				using System;
				using System.Collections.Generic;
				using System.Threading.Tasks;

				namespace ClassLibrary1;

				public class Class1
				{
					private List<int> total;
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}
	}
}
