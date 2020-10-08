using AdtLinkedList;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrackingLinkedList
{
    public class TwoDotSix
    {
        public void Run()
        {
            var list = new DoublyLinkedList();
            list.PushFront(1);
            list.PushFront(3);
            list.PushFront(2);
            list.PushFront(3);
            list.PushFront(1);

            bool result = Calculate(list);
            Console.WriteLine(result);
            Console.Read();
        }

        private static bool Calculate(DoublyLinkedList list)
        {
          
            DoublyNode current1 = list.Head;
            DoublyNode current2 = list.Tail;
            while (current1 != null)
            {
                if (current1.Key != current2.Key)
                {
                   return false;
                    
                }
                current1 = current1.Next;
                current2 = current2.Prev;
            }
           
            return true ;
        }
    }
}
