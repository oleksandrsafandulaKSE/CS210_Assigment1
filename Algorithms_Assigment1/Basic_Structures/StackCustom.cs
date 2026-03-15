namespace Algorithms_Assigment1;

/// <summary>
/// Stack 
/// </summary>

public class StackCustom<T>
{
    private T[] _items;
    private int _top;
    private int _capacity;

    public StackCustom(int capacity = 100)
    {
        _items = new T[capacity]; 
        _top = -1;
        _capacity = capacity;
    }

    public void Push(T item)
    {
        if (_top == _capacity - 1)
            throw new StackOverflowException();
        
        _items[++_top] = item;
    }
    
    public T Pop()
    {
        if (_top == -1)
             throw new Exception("Stack is empty");
        
        return _items[_top--];
    }

    public T Peek()
    {
        if (_top == -1)
            throw new Exception("Stack is empty");
        return _items[_top];
    }

    public bool IsEmpty()
    {
        return _top == -1;
    }

    public bool IsFull()
    {
        return _top == _capacity - 1;
    }
}