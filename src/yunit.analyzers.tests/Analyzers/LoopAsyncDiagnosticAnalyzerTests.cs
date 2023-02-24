using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Verifier = CSharpVerifier<Yunit.Analyzers.LoopAsyncDiagnosticAnalyzer>;

namespace Yunit.Analyzers.Analyzers
{
	public class LoopAsyncDiagnosticAnalyzerTests
	{
		[Fact]
		public async Task TestFor()
		{
			string csharpTxt = @"
				using System;
				using System.Threading.Tasks;

				namespace ClassLibrary1;

				public class xClass1
				{
					private async Task FooAsync()
					{						
						for (int i = 0;i < 10; i++)
						{
							await Task.Delay(i);
						}
					}
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}

		[Fact]
		public async Task TestForeach()
		{
			string csharpTxt = @"
				using System;
				using System.Linq;
				using System.Threading.Tasks;

				namespace ClassLibrary1;

				public class xClass1
				{
					private async Task FooAsync()
					{						
						foreach (var i in Enumerable.Range(1, 10))
						{
							await Task.Delay(i);
						}
					}
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}

		[Fact]
		public async Task TestWhile()
		{
			string csharpTxt = @"
				using System;
				using System.Linq;
				using System.Threading.Tasks;

				namespace ClassLibrary1;

				public class xClass1
				{
					private async Task FooAsync()
					{			
						int i = 0;

						while (i < 10)
						{
							await Task.Delay(i);
							
							i++;
						}
					}
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}

		[Fact]
		public async Task TestDoWhile()
		{
			string csharpTxt = @"
				using System;
				using System.Linq;
				using System.Threading.Tasks;

				namespace ClassLibrary1;

				public class xClass1
				{
					private async Task FooAsync()
					{			
						int i = 0;

						do
						{
							await Task.Delay(i);
							
							i++;
						}
						while (i < 10);
					}
				}";

			await Verifier.VerifyAnalyzerAsync(csharpTxt);
		}
	}
}
