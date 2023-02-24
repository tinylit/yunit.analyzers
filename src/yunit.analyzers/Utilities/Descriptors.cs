using Microsoft.CodeAnalysis;
using static Microsoft.CodeAnalysis.DiagnosticSeverity;
using static Yunit.Analyzers.Category;

namespace Yunit.Analyzers
{
	public enum Category
	{
		/// <summary>
		/// 规范。
		/// </summary>
		Standard,

		/// <summary>
		/// 性能。
		/// </summary>
		Performance,

		/// <summary>
		/// 安全。
		/// </summary>
		Safety
	}

	public static class Descriptors
	{
		static DiagnosticDescriptor Rule(
			string id,
			string title,
			Category category,
			DiagnosticSeverity defaultSeverity,
			string messageFormat)
		{
			return new DiagnosticDescriptor(id, title, messageFormat, category.ToString(), defaultSeverity, true);
		}

		/// <summary>
		/// 类型、属性、方法、非私有成员名称，请遵循帕斯卡命名法！
		/// </summary>
		public static DiagnosticDescriptor CD1001 { get; } =
			Rule(
				nameof(CD1001),
				"名称",
				Standard,
				Error,
				"类型、属性、方法、非私有成员名称，请遵循帕斯卡命名法！"
			);

		/// <summary>
		/// 除类型、属性和方法之外的私有成员名称，请遵循驼峰命名法！
		/// </summary>
		public static DiagnosticDescriptor CD1002 { get; } =
			Rule(
				nameof(CD1002),
				"名称",
				Standard,
				Error,
				"除类型、属性和方法之外的私有成员名称，请遵循驼峰命名法！"
			);

		/// <summary>
		/// 复数类型，以“s”结尾！
		/// </summary>
		public static DiagnosticDescriptor CD1003 { get; } =
			Rule(
				nameof(CD1003),
				"名称",
				Standard,
				Error,
				"数组或迭代类型，请以“s”结尾！"
			);

		/// <summary>
		/// 异步类型的字段、属性或参数名称，请以“Task”结尾！
		/// </summary>
		public static DiagnosticDescriptor CD1100 { get; } =
			Rule(
				nameof(CD1100),
				"名称",
				Standard,
				Error,
				"异步类型的属性、字段、参数、变量等名称，请以“Task”结尾！"
			);

		/// <summary>
		/// 异步复数类型的字段、属性或参数名称，以“sTask”结尾！
		/// </summary>
		public static DiagnosticDescriptor CD1101 { get; } =
			Rule(
				nameof(CD1101),
				"名称",
				Standard,
				Error,
				"异步数组或迭代类型的字段、属性、参数、变量等名称，以“sTask”结尾！"
			);

		/// <summary>
		/// 异步方法，名称需以“Async”结尾！
		/// </summary>
		public static DiagnosticDescriptor CD1110 { get; } =
			Rule(
				nameof(CD1110),
				"方法名称",
				Standard,
				Error,
				"异步方法，名称需以“Async”结尾！"
			);

		/// <summary>
		/// 结果为复数的方法名称，异步以“sAsync”结尾！
		/// </summary>
		public static DiagnosticDescriptor CD1111 { get; } =
			Rule(
				nameof(CD1111),
				"方法名称",
				Standard,
				Error,
				"异步方法，结果为数组或迭代类型时，以“sAsync”结尾！"
			);

		/// <summary>
		/// 枚举名称请以“Enum”开头！
		/// </summary>
		public static DiagnosticDescriptor CD1200 { get; } =
			Rule(
				nameof(CD1200),
				"枚举名称",
				Standard,
				Error,
				"枚举名称请以“Enum”开头！"
			);

		/// <summary>
		/// 循环执行异步方法！
		/// </summary>
		public static DiagnosticDescriptor CD2000 { get; } =
			Rule(
				nameof(CD2000),
				"性能",
				Performance,
				Warning,
				"循环执行异步方法！"
			);

		/// <summary>
		/// 使用“switch”代码块，请指定“default://TODO:..”代码块！
		/// </summary>
		public static DiagnosticDescriptor CD3000 { get; } =
			Rule(
				nameof(CD3000),
				"安全",
				Safety,
				Warning,
				"使用“switch”代码块，请指定“default://TODO:..”代码块！"
			);

		/// <summary>
		/// 请写捕获到异常的逻辑，不要吞掉异常！
		/// </summary>
		public static DiagnosticDescriptor CD3001 { get; } =
			Rule(
				nameof(CD3001),
				"安全",
				Safety,
				Warning,
				"请写捕获到异常的逻辑，不要吞掉异常！"
			);
	}
}
