using System.Threading.Tasks;
using Xunit;
using Verifier = CSharpVerifier<Yunit.Analyzers.NonPublicDiagnosticAnalyzer>;

namespace Yunit.Analyzers.Analyzers
{
	public class NonPublicDiagnosticAnalyzerTests
	{
		[Fact]
		public async Task TestPrivateField()
		{
			string csharpTxt = @"
				using System;

				namespace ClassLibrary1;

				public class Class1
				{
					private int Total;
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}

		[Fact]
		public async Task TestParameter()
		{
			string csharpTxt = @"
				using System;

				namespace ClassLibrary1;

				public class Class1
				{
					private static void Foo(int Total)
					{
						try { } catch (Exception ex) { }
					}
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}

		[Fact]
		public async Task TestCtorParameter()
		{
			string csharpTxt = @"
				using System;

				namespace ClassLibrary1;

				public class Class1
				{
					Class1(int Total){

					}

					private static void Foo()
					{
						try { } catch (Exception ex) { }
					}
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}

		[Fact]
		public async Task TestVariable()
		{
			string csharpTxt = @"
				using System;

				namespace ClassLibrary1;

				public class Class1
				{
					private static void Foo(int total)
					{
						try { int Count = total + 1;} catch (Exception Ex) { }
					}
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}
	}
}
