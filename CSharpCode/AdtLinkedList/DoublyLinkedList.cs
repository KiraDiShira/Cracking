using System;
using System.Collections.Generic;
using System.Text;

namespace AdtLinkedList
{
    public class DoublyNode
    {
        public int Key { get; set; }
        public DoublyNode Next { get; set; }
        public DoublyNode Prev { get; set; }
    }

    public class DoublyLinkedList
    {
        public DoublyNode Head { get; set; }
        public DoublyNode Tail { get; set; }

        public void PushFront(int key)
        {
            var node = new DoublyNode()
            {
                Key = key,
                Next = Head,
                Prev = null
            };

            if (Head != null)
            {
                Head.Prev = node;
            }
            Head = node;

            //Se la coda non punta ad alcuna parte vuol dire che prima la lista era vuota e 
            //il nuovo nodo è il primo nodo, quindi faccio puntare la tail all'unico elemento inserito
            if (Tail == null)
            {
                Tail = Head;
            }
        }


        public void PopBack()
        {
            if (Head == null)
            {
                throw new Exception("ERROR: empty list");
            }

            if (Head == Tail)
            {
                Head = null;
                Tail = null;
                return;
            }

            Tail = Tail.Prev;
            Tail.Next = null;
        }

        public void PushBack(int key)
        {
            var node = new DoublyNode()
            {
                Key = key,
                Next = null
            };

            if (Tail == null)
            {
                Head = node;
                Tail = node;
                node.Prev = null;
            }
            else
            {
                node.Prev = Tail;
                Tail.Next = node;
                Tail = node;
            }
        }

        public void AddAfter(DoublyNode node, int key)
        {
            var newNode = new DoublyNode()
            {
                Key = key,
                Next = node.Next,
                Prev = node
            };

            node.Next = newNode;

            //devo aggiornare il nodo precedente a quello successivo a newnode
            if (newNode.Next != null)
            {
                newNode.Next.Prev = newNode;
            }

            if (Tail == node)
            {
                Tail = newNode;
            }
        }

        public void AddBefore(DoublyNode node, int key)
        {
            if (Head == null)
            {
                return;
            }

            var newNode = new DoublyNode()
            {
                Key = key,
                Next = node,
                Prev = node.Prev
            };

            node.Prev = newNode;

            if (newNode.Prev != null)
            {
                newNode.Prev.Next = newNode;
            }

            if (Head == node)
            {
                Head = newNode;
            }
        }

        public override string ToString()
        {
            if (Head == null)
            {
                return "NULL";
            }

            string toString = Head.Key + "->";

            DoublyNode curr = Head;
            while (curr.Next != null)
            {
                curr = curr.Next;
                toString += curr.Key;
                toString += "->";
            }

            return toString;
        }

    }
}
