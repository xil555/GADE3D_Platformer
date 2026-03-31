using UnityEngine;

public class CustomQueue
{
    private const int MAX_SIZE = 100;

    private NPCDialogueSO[] queue = new NPCDialogueSO[MAX_SIZE];
    private int front;
    private int rear;

    public CustomQueue()
    {
        front = -1;
        rear = -1;
    }

    public bool IsEmpty()
    {
        return (front == -1);
    }

    public bool IsFull()
    {
        return (rear == MAX_SIZE - 1);
    }

    public void Enqueue(NPCDialogueSO data)
    {
        if (IsFull())
        {
            Debug.Log("Queue is full");
            return;
        }

        if (IsEmpty())
        {
            front = 0;
        }

        rear++;
        queue[rear] = data;
    }

    public NPCDialogueSO Dequeue()
    {
        if (IsEmpty())
        {
            Debug.Log("Queue is empty");
            return null;
        }

        NPCDialogueSO data = queue[front];

        if (front == rear)
        {
            front = -1;
            rear = -1;
        }
        else
        {
            front++;
        }

        return data;
    }

    public void Clear()
    {
        front = -1;
        rear = -1;
    }
}