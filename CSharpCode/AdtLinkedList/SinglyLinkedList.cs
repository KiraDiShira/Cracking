using System;
using System.Collections.Generic;
using System.Text;

namespace AdtLinkedList
{
    public class SinglyLinkedList
    {
        public Node Head { get; set; }
        public Node Tail { get; set; }

        public void PushFront(int key)
        {
            var node = new Node()
            {
                Key = key,
                Next = Head
            };

            Head = node;

            //Se la coda non punta ad alcuna parte vuol dire che prima la lista era vuota e 
            //il nuovo nodo è il primo nodo, quindi faccio puntare la tail all'unico elemento inserito
            if (Tail == null)
            {
                Tail = Head;
            }
        }

        public void PopFront()
        {
            if (Head == null)
            {
                throw new Exception("ERROR: empty list");
            }

            Head = Head.Next;
            if (Head == null) //significa che la lista è vuota, quindi setto a null anche la tail
            {
                Tail = null;
            }
        }

        public void PushBack(int key)
        {
            var node = new Node()
            {
                Key = key,
                Next = null
            };

            if (Tail == null)
            {
                Head = node;
                Tail = node;
            }
            else
            {
                Tail.Next = node;
                Tail = node;
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
            }
            else
            {
                Node currentNode = Head;

                //mi trova il penultimo elemento
                while (currentNode.Next.Next != null)
                {
                    currentNode = currentNode.Next;
                }

                currentNode.Next = null;
                Tail = currentNode;
            }
        }

        public void AddAfter(Node node, int key)
        {
            var newNode = new Node()
            {
                Key = key,
                Next = node.Next
            };

            node.Next = newNode;

            if (Tail == node)
            {
                Tail = newNode;
            }
        }

        public int TopFront()
        {
            if (Head == null)
            {
                throw new Exception("ERROR: empty list");
            }

            return Head.Key;
        }

        public int TopBack()
        {
            if (Head == null)
            {
                throw new Exception("ERROR: empty list");
            }

            return Tail.Key;
        }

        public bool Find(object key)
        {
            if (Head == null)
            {
                return false;
            }

            Node currentNode = Head;
            if (currentNode.Key.Equals(key))
            {
                return true;
            }

            //mi trova il ultimo elemento
            while (currentNode.Next != null)
            {
                currentNode = currentNode.Next;

                if (currentNode.Key.Equals(key))
                {
                    return true;
                }
            }

            return false;
        }

        public void Erase(object key)
        {
            if (Head == null)
            {
                throw new Exception("cannot delete");
            }

            if (Head.Key.Equals(key))
            {
                Head = Head.Next;
                if (Head == null)
                {
                    Tail = null;
                }
                return;
            }

            Node currentNode = Head;
            Node previousNode = null;

            while (currentNode != null && !currentNode.Key.Equals(key))
            {
                previousNode = currentNode;
                currentNode = currentNode.Next;
            }

            if (currentNode == null) throw new Exception("cannot delete");

            //delete cur node
            previousNode.Next = currentNode.Next;

        }

        public bool Empty()
        {
            if (Tail == null)
            {
                return true;
            }

            return false;
        }

        public void AddBefore(Node node, int key)
        {
            if (Head == null)
            {
                return;
            }

            if (Head.Key.Equals(key))
            {
                PushFront(key);
                return;
            }

            Node currentNode = Head;
            Node previousNode = null;

            while (currentNode != null && !currentNode.Key.Equals(key))
            {
                previousNode = currentNode;
                currentNode = currentNode.Next;
            }

            if (currentNode == null)
            {
                return;
            }

            var newNode2 = new Node()
            {
                Key = key,
                Next = currentNode
            };

            previousNode.Next = newNode2;
        }

        public override string ToString()
        {
            if (Head == null)
            {
                return "NULL";
            }

            string toString = Head.Key + "->";

            Node curr = Head;
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
