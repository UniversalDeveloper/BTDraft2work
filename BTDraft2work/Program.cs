using System;

namespace BTDraft2work
{
  

        class Program
        {
            static void Main(string[] args)
            {

                BTree b = new BTree(2);
                b.Insert(8);
                b.Insert(9);
                b.Insert(10);
                b.Insert(11);
                b.Insert(15);
                b.Insert(20);
                b.Insert(17);

                b.Show();

                if (b.Contain(12))
                {
                    Console.WriteLine("\nfound");
                }
                else
                {
                    Console.WriteLine("\nnot found");
                }


            }
        }
 }


