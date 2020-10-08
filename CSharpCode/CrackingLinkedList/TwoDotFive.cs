using AdtLinkedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrackingLinkedList
{
    public class TwoDotFive
    {
        public void Run()
        {
            SinglyLinkedList list1 = new SinglyLinkedList();
            list1.PushFront(6);
            list1.PushFront(1);
            list1.PushFront(7);

            SinglyLinkedList list2 = new SinglyLinkedList();
            list2.PushFront(2);
            list2.PushFront(9);
            list2.PushFront(5);

            SinglyLinkedList result = new SinglyLinkedList();

            int addendum3 = 0;

            while (!list1.Empty() || !list2.Empty())
            {
                int addendum1 = 0;
                if (!list1.Empty())
                {
                    addendum1 = list1.TopFront();
                    list1.PopFront();
                }

                int addendum2 = 0;
                if (!list2.Empty())
                {
                    addendum2 = list2.TopFront();
                    list2.PopFront();
                }

                int sum = addendum1 + addendum2 + addendum3;
                int nodeKey = sum % 10;
                addendum3 = sum >= 10 ? 1 : 0;

                result.PushBack(nodeKey);
            }

            Console.WriteLine(result);
            Console.ReadLine();
        }


        public void Run2()
        {
            SinglyLinkedList list1 = new SinglyLinkedList();
            list1.PushFront(7);
            list1.PushFront(1);
            list1.PushFront(6);

            SinglyLinkedList list2 = new SinglyLinkedList();
            list2.PushFront(5);
            list2.PushFront(9);
            list2.PushFront(2);

            SinglyLinkedList result = new SinglyLinkedList();

            int addendum3 = 0;

            while (!list1.Empty() || !list2.Empty())
            {
                int addendum1 = 0;
                if (!list1.Empty())
                {
                    addendum1 = list1.TopBack();
                    list1.PopBack();
                }

                int addendum2 = 0;
                if (!list2.Empty())
                {
                    addendum2 = list2.TopBack();
                    list2.PopBack();
                }

                int sum = addendum1 + addendum2 + addendum3;

                int nodeKey = sum % 10;
                addendum3 = sum >= 10 ? 1 : 0;

                result.PushFront(nodeKey);
            }

            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
