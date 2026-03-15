using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Threading.Channels;

namespace Algorithms_Assigment1;

public class ShuntingYard
{
    private string[] _operations = new[] { "+", "-", "*", "/", "^", "cos", "sin", "max" };
    
    private StackCustom<string> _stackOperations = new StackCustom<string>();
    
    private QueueCustom<string> _outputPolishTokens = new QueueCustom<string>();

    private QueueCustom<string> _inputTokens = new QueueCustom<string>();

    public ShuntingYard(string input_str)
    {
        TokenizeStr(input_str);
        var de = "";
    }

    private int OperatorEval(string oper)
    {
        switch (oper)
        {
            case "+":
            case "-": return 2; 
            
            case "*":
            case "/": return 4;
            
            case "^": return 7;
            
            case "sin":
            case "cos":
            case "max": return 10;
        }

        return -1;  
    }

    private double? evalOperation(string num1Str, string num2Str, string operation)
    {
        var num1 = double.Parse(num1Str);
        var num2 = double.TryParse(num2Str, out var num)? num : int.MinValue;
        
        switch (operation)
        {
            case "+": return num1 + num2;
            case "-": return num2 - num1;
            case "*": return num1 * num2;
            case "/": return num2 / num1;
            case "^": return (float)Math.Pow(num2, num1);
            case "max": return Math.Max(num1, num2);
            case "sin": return (float)Math.Sin(num1);
            case "cos": return (float)Math.Cos(num1);
            
            default: return null;
        }
    }

    public void TokenizeStr(string input_str)
    {
        var currentToken = "";
        var funcDetection = "";

        for (int i = 0; i < input_str.Length; i++)
        {
             funcDetection = i < input_str.Length - 2 ? string.Join("", [input_str[i].ToString() + input_str[i + 1].ToString() + input_str[i + 2].ToString()]) : "";
            
            if (char.IsWhiteSpace(input_str[i]))
            {
                continue;
            }
            else if (funcDetection == "sin"
                     || funcDetection == "cos"
                     || funcDetection == "max")
            {
                _inputTokens.Enqueue(funcDetection);
                i += 3;
            }
            else if ((input_str[i] >= '0' && input_str[i] <= '9') || input_str[i] == '.')
            {
                currentToken += input_str[i];
            }
            else if (input_str[i] == '(' || input_str[i] == ')')
            {
                if (currentToken != "")
                {
                    _inputTokens.Enqueue(currentToken);
                    currentToken = "";
                }
                _inputTokens.Enqueue(input_str[i].ToString());

            }
            else if ((_operations.Contains(input_str[i].ToString()) || input_str[i] == ','))
            {
                if (currentToken != "")
                    _inputTokens.Enqueue(currentToken);
                _inputTokens.Enqueue(input_str[i].ToString());
                
                currentToken = "";
            }
            else if (_operations.Contains(input_str[i].ToString()) && input_str[i] == '-')
            { 
                currentToken += input_str[i];
            }
            else
            { 
                throw new InvalidExpressionException("Invalid input");
            }
        }
        if (currentToken != "")
        {
            _inputTokens.Enqueue(currentToken);

        }
    }


    private void PushingToStack(string item)
    {
        if (!_stackOperations.IsEmpty())
        {
            if (item == ")")
            {
                while (_stackOperations.Peek() != "(")
                {
                    _outputPolishTokens.Enqueue(_stackOperations.Pop());
                }

                _stackOperations.Pop();
                if (OperatorEval(_stackOperations.Peek()) == 10)
                    _outputPolishTokens.Enqueue(_stackOperations.Pop());

            }
            else if (item == ",")
            {
                while (!_stackOperations.IsEmpty() && _stackOperations.Peek() != "(")
                {
                    _outputPolishTokens.Enqueue(_stackOperations.Pop());
                }
            }
            else
            {
                while (!_stackOperations.IsEmpty()
                       && item != "("
                       && (OperatorEval(_stackOperations.Peek()) > OperatorEval(item)
                           || OperatorEval(_stackOperations.Peek()) == OperatorEval(item)
                           && OperatorEval(item) % 2 == 0))
                {
                    if (item == "^")
                    {
                        break;
                    }

                    _outputPolishTokens.Enqueue(_stackOperations.Pop());
                }
            }

            if (item != ")" && item != ",")
                _stackOperations.Push(item);
        }
        else 
            _stackOperations.Push(item);
    }


    public QueueCustom<string> GetPolishNotation()
    {
        string item; 

        while (!_inputTokens.IsEmpty())
        {
            item = _inputTokens.Dequeue();
            if (double.TryParse(item, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                _outputPolishTokens.Enqueue(result.ToString());
            else
                PushingToStack(item);
        }

        while (!_stackOperations.IsEmpty())
        {
            _outputPolishTokens.Enqueue(_stackOperations.Pop());
        }

        return _outputPolishTokens;
    }
    
    
    //3 + 4 * 2 / ( 1 - 5 ) ^ 2
    //3 4 2 * 1 5 - 2 ^ / +
    public double CalcExp()
    {
        var evalStack = new StackCustom<string>();

        while (!_outputPolishTokens.IsEmpty())
        {
            var item = _outputPolishTokens.Dequeue();
            
            if (double.TryParse(item, out var result))
                evalStack.Push(result.ToString());
            else if (item == "sin" || item == "cos")
            {
                evalStack.Push(evalOperation(evalStack.Pop(), "null", item).ToString());
            }
            else
            {
                evalStack.Push(evalOperation(evalStack.Pop(), evalStack.Pop(), item).ToString());
            }

            
        }


        return double.Parse(evalStack.Pop());
    }
}