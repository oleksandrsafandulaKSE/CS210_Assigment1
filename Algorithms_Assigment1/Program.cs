using System.Threading.Channels;

namespace Algorithms_Assigment1;

class Program
{
    static void Main(string[] args)
    {

        var ob = new ShuntingYard("3 * (5 + 2) - 2 ");
        
        

       ob.GetASTstring(ob.AST()); 
       Console.WriteLine(ob.ASTstring);
        
       Console.Write(ob.CalcExp());
    }
}