namespace Algorithms_Assigment1;

public class Node
{
    public string Value { get; set; }

    public Node? LeftSon { get; set; }
    public Node? RightSon { get; set; }

    public Node(string value) => Value = value;

}