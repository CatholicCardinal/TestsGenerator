﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGeneratorLib.FileInformation;




namespace TestGeneratorLib
{
    class TestMethodsGenerator
    {
        internal MethodDeclarationSyntax GenerateMethodDeclaration(MethodDescription methodDescription, AttributeSyntax TestMethodAttribute, string ClassVariableName)
        {
            List<StatementSyntax> failTest = new List<StatementSyntax>();

            var failExpression = SyntaxFactory.ExpressionStatement(SyntaxFactory.InvocationExpression(SyntaxFactory.MemberAccessExpression(
                                  SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName("Assert"), SyntaxFactory.IdentifierName("Fail"))).WithArgumentList(
                        SyntaxFactory.ArgumentList(SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
                                SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal("Autogenerated")))))));

            failTest.Add(failExpression);
            var method = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("void"), methodDescription.Name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAttributeLists(SyntaxFactory.AttributeList(SyntaxFactory.AttributeList().Attributes.Add(TestMethodAttribute)))
                .WithBody(SyntaxFactory.Block(failTest));
            return method;
        }
    }
}
