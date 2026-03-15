namespace Algorithms_Assigment1;

public class ListCustom<T>
{
    private T[] _array = new T[5];
    private int _pointer = 0;


    public void Add(T element)
    {
        _array[_pointer] = element;
        _pointer++;
        
        if (_pointer == _array.Length)
        {
            var newArray = new T[_array.Length * 2];

            for (int i = 0; i < _array.Length; i++)
            {
                newArray[i] = _array[i];
            }

            _array = newArray;
        }
            
    }

    public void Remove(T element)
    {
        
    }
}