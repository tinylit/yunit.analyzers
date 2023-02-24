using System.Threading.Tasks;
using Xunit;
using Verifier = CSharpVerifier<Yunit.Analyzers.PublicDiagnosticAnalyzer>;

namespace Yunit.Analyzers.Analyzers
{
	public class PublicDiagnosticAnalyzerTests
	{
		[Fact]
		public async Task TestNamedType()
		{
			string csharpTxt = @"
				using System;

				namespace ClassLibrary1;

				public class xClass1
				{
					private static void Foo()
					{
						try { } catch (Exception ex) { }
					}
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}

		[Fact]
		public async Task TestMethod()
		{
			string csharpTxt = @"
				using System;

				namespace ClassLibrary1;

				public class Class1
				{
					private static void foo()
					{
						try { } catch (Exception ex) { }
					}
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}

		[Fact]
		public async Task TestProperty()
		{
			string csharpTxt = @"
				using System;

				namespace ClassLibrary1;

				public class Class1
				{
					public int total{ get;set;}
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}

		[Fact]
		public async Task TestPublicField()
		{
			string csharpTxt = @"
				using System;

				namespace ClassLibrary1;

				public class Class1
				{
					public int total = 0;
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}
	}
}
