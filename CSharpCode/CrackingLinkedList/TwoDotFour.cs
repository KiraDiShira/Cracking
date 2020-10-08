using AdtLinkedList;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace CrackingLinkedList
{
    public class TwoDotFour
    {
        public void Run()
        {
            int partitionKey = 5;

            SinglyLinkedList linkedList = new SinglyLinkedList();
            linkedList.PushFront(1);
            linkedList.PushFront(2);
            linkedList.PushFront(10);
            linkedList.PushFront(5);
            linkedList.PushFront(8);
            linkedList.PushFront(5);
            linkedList.PushFront(3);

            Node current = linkedList.Head;
            Node runner = null;

            while(current != null)
            {
                if(current.Key < partitionKey)
                {                  
                    current = current.Next;                                        
                }
                else
                {
                    if (runner == null)
                    {
                        runner = current.Next;
                    }
                    
                    while(runner != null)
                    {
                        if(runner.Key < partitionKey)
                        {                            
                            int runnerKey = runner.Key;
                            runner.Key = current.Key;
                            current.Key = runnerKey;
                            current = current.Next;
                            runner = runner.Next;
                            break;
                        }
                        runner = runner.Next;
                    }
                    
                    if(runner == null)
                    {
                        break;
                    }
                }
            }

            Console.WriteLine(linkedList);
            Console.Read();
        }


        public void Run2()
        {
            int partitionKey = 8;

            SinglyLinkedList linkedList = new SinglyLinkedList();
            linkedList.PushFront(1);
            linkedList.PushFront(2);
            linkedList.PushFront(10);
            linkedList.PushFront(5);
            linkedList.PushFront(8);
            linkedList.PushFront(5);
            linkedList.PushFront(3);

            SinglyLinkedList output = new SinglyLinkedList();

            Node current = linkedList.Head;
            while(current != null)
            {
                if(current.Key < partitionKey)
                {
                    output.PushFront(current.Key);
                }
                else
                {
                    output.PushBack(current.Key);
                }
                current = current.Next;
            }

            Console.WriteLine(output);
            Console.Read();
        }

    }
}
