using static System.Console;
class Program
{
    static void Main()
    {
        IStack<int> stack = new MyStack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        WriteLine(stack.Pop());
        WriteLine(stack.Pop());
        WriteLine(stack.Pop());
    }
}
public interface IStack<T>
{
    int Count { get; }


    T Pop();
    void Push(T item);
    T Peek();
}

public class MyStack<T> : IStack<T>
{
    private T[] _items;
    private int _size;
    private int _capacity;

    public MyStack()
    {
        _items = new T[4];
        _size = 0;
        _capacity = 4;
    }

    public int Count
    {
        get { return _size; }
    }

    public void Push(T item)
    {
        if (_size == _capacity)
        {
            T[] newItems = new T[_capacity * 2];

            for (int i = 0; i < _size; i++)
            {
                newItems[i] = _items[i];
            }

            _items = newItems;
            _capacity *= 2;
        }

        _items[_size] = item;
        _size++;
    }

    public T Pop()
    {
        if (_size == 0)
        {
            throw new System.InvalidOperationException("Stack is empty");
        }

        T item = _items[_size - 1];
        _items[_size - 1] = default(T);
        _size--;

        return item;
    }

    public T Peek()
    {
        if (_size == 0)
        {
            throw new System.InvalidOperationException("Stack is empty");
        }

        return _items[_size - 1];
    }

    public bool IsEmpty()
    {
        return _size == 0;
    }

    public void Clear()
    {
        _size = 0;
    }
}