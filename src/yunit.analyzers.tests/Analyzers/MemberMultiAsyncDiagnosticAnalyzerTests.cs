using System.Threading.Tasks;
using Xunit;
using Verifier = CSharpVerifier<Yunit.Analyzers.MemberMultiAsyncDiagnosticAnalyzer>;

namespace Yunit.Analyzers.Analyzers
{
	public class MemberMultiAsyncDiagnosticAnalyzerTests
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
					private Task<List<int>> total;
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}

		[Fact]
		public async Task TestVariable()
		{
			string csharpTxt = @"
				using System;
				using System.Collections.Generic;
				using System.Threading.Tasks;

				namespace ClassLibrary1;

				public class Class1
				{
					private static void Foo()
					{
						Task<List<int>> total;
					}
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}
	}
}
