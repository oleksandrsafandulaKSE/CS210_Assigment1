using System.Threading.Channels;

namespace Algorithms_Assigment1;

class Program
{
    static void Main(string[] args)
    {

        var ob = new ShuntingYard("2 + (34 + 5) ^ 2 + 2 / 3");

        var polish = ob.GetPolishNotation();

        // while (!polish.IsEmpty())
        // {
        //     Console.Write(polish.Dequeue() + " ");
        // }
        
        
        Console.Write(ob.CalcExp());
    }
}