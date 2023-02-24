using System.Threading.Tasks;
using Xunit;
using Verifier = CSharpVerifier<Yunit.Analyzers.MemberAsyncDiagnosticAnalyzer>;

namespace Yunit.Analyzers.Analyzers
{
	public class MemberAsyncDiagnosticAnalyzerTests
	{
		[Fact]
		public async Task TestProperty()
		{

			string csharpTxt = @"
				using System;
				using System.Linq;
				using System.Threading.Tasks;

				namespace ClassLibrary1;

				public class xClass1
				{
					public ValueTask<int> Total{ get; set; }
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}

		[Fact]
		public async Task TestParameter()
		{
			string csharpTxt = @"
				using System;
				using System.Linq;
				using System.Threading.Tasks;

				namespace ClassLibrary1;

				public class xClass1
				{
					public static void Foo(Task<string> txt)
					{
					}
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}

		[Fact]
		public async Task TestVariable()
		{
			string csharpTxt = @"
				using System;
				using System.Linq;
				using System.Threading.Tasks;

				namespace ClassLibrary1;

				public class xClass1
				{
					public static void Foo()
					{
						var x = Task.FromResult(0);
					}
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}
	}
}
