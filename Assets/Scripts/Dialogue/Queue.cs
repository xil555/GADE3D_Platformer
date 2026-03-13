using UnityEngine;

public class Queue<T>
{
    private T[] items;
    private int first;
    private int end;
    private int count;

    public Queue(int size)
    {
        items = new T[size];
        first = 0;
        end = -1;
        count = 0;
    }

    public void Enqueue(T item)
    {
        end = (end + 1) % items.Length;
        items[end] = item;
        count++;
    }

    public T Dequeue()
    {
        T item = items[first];
        first = (first + 1) % items.Length;
        count--;
        return item;
    }

    public bool IsEmpty()
    {
        return count == 0;
    }

    public int Count
    {
        get { return count; }
    }

    public void Clear()
    {
        first = 0;
        end = -1;
        count = 0;
    }
}