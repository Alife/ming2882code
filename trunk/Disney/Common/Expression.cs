// Expression.cs - 动态生成数学表达式并计算其值 
// 表达式使用 C# 语法，可带一个的自变量(x)。 
// 表达式的自变量和值均为(double)类型。 
// 使用举例: 
//   Expression expression = new Expression("Math.Sin(x)"); 
//   Console.WriteLine(expression.Compute(Math.PI / 2)); 
//   expression = new Expression("double u = Math.PI - x;" + 
//     "double pi2 = Math.PI * Math.PI;" + 
//     "return 3 * x * x + Math.Log(u * u) / pi2 / pi2 + 1;"); 
//   Console.WriteLine(expression.Compute(0)); 
using System;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
using System.Text; 

///<summary> 
///计算表达式的类 
///</summary> 
public class Expression
{
    object instance;
    MethodInfo method;

    public Expression(string expression)
    {
        if (expression.IndexOf("return") < 0) expression = "return " + expression + ";";
        string className = "Expression";
        string methodName = "Compute";
        CompilerParameters p = new CompilerParameters();
        p.GenerateInMemory = true;
        CompilerResults cr = new CSharpCodeProvider().CompileAssemblyFromSource(p, string.
          Format("using System;sealed class {0}{{public double {1}(double x){{{2}}}}}",
          className, methodName, expression));
        if (cr.Errors.Count > 0)
        {
            string msg = "Expression(\"" + expression + "\"): \n";
            foreach (CompilerError err in cr.Errors) msg += err.ToString() + "\n";
            throw new Exception(msg);
        }
        instance = cr.CompiledAssembly.CreateInstance(className);
        method = instance.GetType().GetMethod(methodName);
    }

    public double Compute(double x)
    {
        return (double)method.Invoke(instance, new object[] { x });
    } 
}