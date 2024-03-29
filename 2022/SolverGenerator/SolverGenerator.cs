using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SolverGenerator
{
	[Generator]
	public class SolverGenerator : ISourceGenerator
	{
		public void Execute(GeneratorExecutionContext context)
		{
			var mainMethod = context.Compilation.GetTypeByMetadataName("AdventOfCode.Solver");

			string source = $@"// <auto-generated/>
using System;

namespace {mainMethod.ContainingNamespace.ToDisplayString()};

public static partial class {mainMethod.Name} {{
    private static partial IAnswer CreateSolutionInstance(int day) => day switch {{
";

			//
			var classDecls = context.Compilation.SyntaxTrees
				.SelectMany(t => t.GetRoot(context.CancellationToken).DescendantNodes())
				.OfType<ClassDeclarationSyntax>();

			foreach (var classDecl in classDecls)
			{
				var attribute = classDecl.AttributeLists
					.SelectMany(x => x.Attributes)
					.FirstOrDefault(attr => attr.Name.ToString() == "Answer");

				if (attribute == null)
				{
					continue;
				}

				var day = attribute.ArgumentList.Arguments[0].ToString();
				var className = classDecl.Identifier.ToString();

				source += $"        {day} => new {className}(),\n";
			}

			source += "        _ => throw new Exception(\"Bad day\")\n";
			source += "    };\n";
			source += "}\n";

			var typeName = mainMethod.Name;

			// Add the source code to the compilation
			context.AddSource($"{typeName}.g.cs", source);


			//var insuranceTypes = GetInsuranceTypes(context.Compilation, context.CancellationToken);
			//var factoryClass = GenerateFactoryClass(context.Compilation, insuranceTypes, context.CancellationToken);
			//var factoryContent = NamespaceDeclaration(ParseName(FactoryNamespaceName)).WithMembers(SingletonList<MemberDeclarationSyntax>(factoryClass));
			//context.AddSource("InsuranceFactory", factoryContent.NormalizeWhitespace().ToFullString());
		}

		/*
		private IEnumerable<ITypeSymbol> GetInsuranceTypes(Compilation compilation, CancellationToken cancellationToken)
		{
			var type = compilation.GetTypeByMetadataName(QualifiedInterfaceName);
			var classDecls = compilation.SyntaxTrees
				.SelectMany(t => t.GetRoot(cancellationToken).DescendantNodes())
				.OfType<ClassDeclarationSyntax>();
			foreach (var classDecl in classDecls)
			{
				var classSymbol = GetInsuranceClassSymbol(compilation, type, classDecl, cancellationToken);
				if (classSymbol != null)
					yield return classSymbol;
			}
		}

		private ITypeSymbol GetInsuranceClassSymbol(Compilation compilation, ITypeSymbol insuranceSymbol, ClassDeclarationSyntax classDeclaration, CancellationToken cancellationToken)
		{
			if (classDeclaration.BaseList == null) return null;
			var semanticModel = compilation.GetSemanticModel(classDeclaration.SyntaxTree);
			foreach (var baseType in classDeclaration.BaseList.Types)
			{
				var typeSymbol = compilation.GetTypeByMetadataName(baseType.Type.ToString());
				var conversion = compilation.ClassifyConversion(typeSymbol, insuranceSymbol);
				if (conversion.Exists && conversion.IsImplicit)
					return semanticModel.GetDeclaredSymbol(classDeclaration, cancellationToken);
			}
			return null;
		}

		
	private ClassDeclarationSyntax GenerateFactoryClass(Compilation compilation, IEnumerable<ITypeSymbol> insuranceTypes, CancellationToken cancellationToken)
	{
		var paramName = "insuranceName";
		return ClassDeclaration("InsuranceFactory")
			.WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.StaticKeyword)))
			.WithMembers(
				SingletonList<MemberDeclarationSyntax>(
					MethodDeclaration(ParseTypeName(QualifiedInterfaceName), "Get")
						.WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.StaticKeyword)))
						.WithParameterList(
							ParameterList(
								SingletonSeparatedList<ParameterSyntax>(
									Parameter(Identifier(paramName))
										.WithType(PredefinedType(Token(SyntaxKind.StringKeyword)))
								)
							)
						)
						.WithBody(
							Block(
								SwitchStatement(IdentifierName("insuranceName"), List(
									GenerateCases(compilation, insuranceTypes).Append(
										SwitchSection(
											SingletonList<SwitchLabelSyntax>(DefaultSwitchLabel()),
											SingletonList<StatementSyntax>(
												ParseStatement(@$"throw new ArgumentException(nameof({paramName}), $""Insurance not found for name '{{{paramName}}}'."");")
											)
										)
									)
								))
							)
						)
				)
			);
	}

	private IEnumerable<SwitchSectionSyntax> GenerateCases(Compilation compilation, IEnumerable<ITypeSymbol> insuranceTypes)
	{
		foreach (var insuranceType in insuranceTypes)
		{
			var label = insuranceType.Name!;
			var switchLabel = CaseSwitchLabel(LiteralExpression(SyntaxKind.StringLiteralExpression).WithToken(Literal(label)));
			var typeName = compilation.GetTypeByMetadataName(insuranceType.ToString()!)!;
			var instanceExpression = ReturnStatement(
				ObjectCreationExpression(ParseTypeName(typeName.ToString()!))
					.WithArgumentList(ArgumentList())
			);
			yield return SwitchSection(
				SingletonList<SwitchLabelSyntax>(switchLabel),
				SingletonList<StatementSyntax>(instanceExpression)
			);
		}
	}
		*/

		public void Initialize(GeneratorInitializationContext context)
		{
		}
	}
}
