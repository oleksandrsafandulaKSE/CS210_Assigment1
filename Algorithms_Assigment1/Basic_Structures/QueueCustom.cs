namespace Algorithms_Assigment1;

/// <summary>
/// Firs In First Out
/// </summary>
/// <typeparam name="T"></typeparam>

public class QueueCustom<T>
{
    private T[] _items;
    private int _capacity;
    private int _size;

    public QueueCustom(int capacity = 100)
    {
        _capacity = capacity;
        _items = new T[capacity];
        _size = 0;
    }

    public void Enqueue(T item)
    {
        if (_size == _capacity)
            throw new Exception("Queue overflowed");
        
        _items[_size++] = item;
        var test = "";
    }

    public T Dequeue()
    {
        if (_size == 0)
            throw new Exception("Queue is empty");


        var item = _items.First();
        for (int i = 1; i < _size; i++)
        {
            _items[i - 1] = _items[i];
        }
        
        _size--;

        return item;
    }

    public T GetFront()
    {
        if (_size == 0)
            throw new Exception("Empty Queue");
        
        return _items[0];
    }

    public T GetRear()
    {
        if (_size == 0)
            throw new Exception("Empty Queue");
        return _items[_size - 1];
    }

    public bool IsEmpty()
    {
        return _size == 0;
    }

    public bool IsFull()
    {
        return _size == _capacity;
    }
}