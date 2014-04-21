using System;
using System.Linq.Expressions;
using System.Reflection;
using SalesforceMagic.Extensions;

namespace SalesforceMagic.LinqProvider
{
    internal static class SOQLVisitor
    {
        internal static string ConvertToSOQL(Expression expression)
        {
            return VisitExpression(expression);
        }

        private static string VisitExpression(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Not:
                    return VisitExpression(Expression.NotEqual(((UnaryExpression)expression).Operand, Expression.Constant(true)));
                case ExpressionType.IsTrue:
                    return VisitBinary(expression as BinaryExpression, "=");
                case ExpressionType.IsFalse:
                    return VisitBinary(expression as BinaryExpression, "!=");
                case ExpressionType.GreaterThanOrEqual:
                    return VisitBinary(expression as BinaryExpression, ">=");
                case ExpressionType.LessThanOrEqual:
                    return VisitBinary(expression as BinaryExpression, "<=");
                case ExpressionType.LessThan:
                    return VisitBinary(expression as BinaryExpression, "<");
                case ExpressionType.GreaterThan:
                    return VisitBinary(expression as BinaryExpression, ">");
                case ExpressionType.AndAlso:
                    return VisitBinary(expression as BinaryExpression, "AND");
                case ExpressionType.Equal:
                    return VisitBinary(expression as BinaryExpression, "=");
                case ExpressionType.NotEqual:
                    return VisitBinary(expression as BinaryExpression, "!=");
                case ExpressionType.Lambda:
                    return VisitLambda(expression as LambdaExpression);
                case ExpressionType.MemberAccess:
                    // TODO: I don't like this
                    if (expression.Type == typeof(bool))
                    {
                        return ((MemberExpression) expression).Member.Name + " = True";
                    }
                    return VisitMember(expression as MemberExpression);
                case ExpressionType.Constant:
                    return VisitConstant(expression as ConstantExpression);
                default:
                    return null;
            }
        }

        private static string VisitBinary(BinaryExpression node, string opr)
        {
            return VisitExpression(node.Left) + " " + opr + " " + VisitExpression(node.Right);
        }

        private static string VisitConstant(ConstantExpression node)
        {
            if (node.Value is string)
                return "'" + node.Value + "'";
            if (node.Value == null)
                return "null";

            return node.Value.ToString();
        }

        private static string VisitLambda(LambdaExpression node)
        {
            return VisitExpression(node.Body);
        }

        private static string VisitMember(MemberExpression node)
        {
            return ((PropertyInfo)node.Member).GetName();
        }

        #region Non-Implemented Methods

        private static string VisitBlock(BlockExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitConditional(ConditionalExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitDefault(DefaultExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitDynamic(DynamicExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitExtension(Expression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitGoto(GotoExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitIndex(IndexExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitInvocation(InvocationExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitLabel(LabelExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitListInit(ListInitExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitLoop(LoopExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitMemberInit(MemberInitExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitMethodCall(MethodCallExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitNew(NewExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitNewArray(NewArrayExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitParameter(ParameterExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitSwitch(SwitchExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitTry(TryExpression node)
        {
            throw new NotImplementedException();
        }

        private static string VisitTypeBinary(TypeBinaryExpression node)
        {
            throw new NotImplementedException();
        }

        private static CatchBlock VisitCatchBlock(CatchBlock node)
        {
            throw new NotImplementedException();
        }

        private static ElementInit VisitElementInit(ElementInit node)
        {
            throw new NotImplementedException();
        }

        private static LabelTarget VisitLabelTarget(LabelTarget node)
        {
            throw new NotImplementedException();
        }

        private static MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            throw new NotImplementedException();
        }

        private static MemberBinding VisitMemberBinding(MemberBinding node)
        {
            throw new NotImplementedException();
        }

        private static MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            throw new NotImplementedException();
        }

        private static MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            throw new NotImplementedException();
        }

        private static SwitchCase VisitSwitchCase(SwitchCase node)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}