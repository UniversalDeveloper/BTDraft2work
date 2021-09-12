using System;
using System.Collections.Generic;
using System.Text;

namespace BTDraft2work
{
   
        class Node
        {
            public int size;
            public int[] data;
            public Node[] children;
            public int countDataInNode = 0;
            bool leaf = true;

            public Node(int size)
            {
                this.size = size;
                data = new int[size];
                children = new Node[size + 1];

            }
            // Search key
            public bool findInNode(int value)
            {
                for (int i = 0; i < countDataInNode; i++)
                {
                    if (value == data[i])
                        return true;
                    if (value < data[i])
                        return children[i].findInNode(value);
                }
                return children[countDataInNode].findInNode(value);
            }
            ///  Insert the node
            public Node add(Node x, int value)
            {
                if (x.leaf)
                {
                    if (x.countDataInNode == 0)
                    {
                        x.data[x.countDataInNode] = value;
                        x.countDataInNode++;
                        return this;
                    }
                    for (int pos = 0; pos < x.countDataInNode; pos++)
                    {
                        if (value < x.data[pos])
                        {
                            //выполнить сдвиг элемента массива в право со вставлением входного элемента в позтцию
                            for (int i = x.countDataInNode - 1; i != pos; i--)
                            {
                                x.data[i + 1] = x.data[i];

                            }
                            x.countDataInNode++;
                            x.data[pos] = value;
                            return this;
                        }
                    }

                }
                x.data[x.countDataInNode] = value;
                return this;
            }

        }
    }


