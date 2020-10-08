using AdtLinkedList;
using System;

namespace CrackingLinkedList
{
    public class TwoDotTwo
    {
        public void Run()
        {
            SinglyLinkedList linkedList = new SinglyLinkedList();
            linkedList.PushFront(3);
            linkedList.PushFront(4);
            linkedList.PushFront(1);
            linkedList.PushFront(2);

            int k = 3;
            int listLength = 0;
            Node current = linkedList.Head;
            while(current != linkedList.Tail)
            {
                current = current.Next;
                listLength++;
            }

            int currentPosition = 0;
            current = linkedList.Head;
            while (currentPosition != listLength - k)
            {
                current = current.Next;
                currentPosition++;
            }



            Console.WriteLine(current.Key);
            Console.ReadLine();
        }
    }
}
