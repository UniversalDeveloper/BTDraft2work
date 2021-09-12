using System;
using System.Collections.Generic;
using System.Text;

namespace BTDraft2work
{

    // Searching a key on a B-tree in Java 

 

    public class BTree
    {

        private static int T;

        // Node creation
        public class Node
        {
            public int n;// счетчик показывающий количество входных значений всавленных в узел
            public int[] key = new int[2 * T - 1];//
            public Node[] child = new Node[2 * T];
            public bool leaf = true;//вставить элемент в узел можно только тогда когда этот токазатель true показывает
                                    //что это не промежуточный узел, а последний



        }
        ///===============
        public BTree(int t)
        {
            T = t;
            root = new Node();
            root.n = 0;
            root.leaf = true;
        }

        private Node root;

        // Search key
        private Node Search(Node x, int key)////////////////////////////////////
        {
            int i = 0;
            if (x == null)
                return x;
            for (i = 0; i < x.n; i++)
            {
                if (key < x.key[i])
                {
                    break;
                }
                if (key == x.key[i])
                {
                    return x;
                }
            }
            if (x.leaf)
            {
                return null;
            }
            else
            {
                return Search(x.child[i], key);
            }
        }/// //////////////////////////////////////

        // Splitting the node
        private void Split(Node x, int pos, Node y)//x=root и он пустой. y содержит
                                                   //старые значения корня которые нужно разбить
                                                   //pos это позиция которя определяет средний элемент и индекс куда он станет в корневом узле

        {
            Node z = new Node();// создается новый пучтой узел куда будут разбиваться половина старых
                                // элементов из полного корня со вставлением новог входного значения
            z.leaf = y.leaf; //копируется указатель на лист из старого корня
            z.n = T - 1;//3-1=2
            for (int j = 0; j < T - 1; j++)//условие на то что не в корневой ноде количество хранимых данных от 2х элементов выполнено
            {
                z.key[j] = y.key[j + T];//11,15 значение данных
            }
            if (!y.leaf)// если это промежуточный узел??????
            {
                for (int j = 0; j < T; j++)
                {
                    z.child[j] = y.child[j + T];
                }
            }
            y.n = T - 1;//3-1=2 присваиваем теперь что в старом корне будет хронится оставшаяся половина водных данных
            for (int j = x.n; j >= pos + 1; j--)
            {
                x.child[j + 1] = x.child[j];
            }
            x.child[pos + 1] = z;// присваиваем теперь новому корю .первого ребенка со значениями переопределенными в строке 70

            for (int j = x.n - 1; j >= pos; j--)
            {
                x.key[j + 1] = x.key[j];
            }
            x.key[pos] = y.key[T - 1];//3-1=2 добавляем второй элемент рутового массива который является в нем
                                      //средним и добавляем его в нулевую позицию нового корня
            x.n = x.n + 1;
        }

        // Inserting a value
        public void Insert(int key)////
        {
            Node r = root;// буферный узел который содкржит значения первоночального корня
                          // в котором кончилось место для входных элементов
            if (r.n == 2 * T - 1)
            {
                Node s = new Node();//новый пустой узел который теперь станет корнем куда будут
                                    //добавленны средние значения после разбиения листа
                root = s;
                s.leaf = false;
                s.n = 0;
                s.child[0] = r;
                Split(s, 0, r);
                insertValue(s, key);// метод который принимает новый корень и входное значение произведет добавление этого значения
            }
            else
            {
                insertValue(r, key);
            }
        }

        ////////// Insert the node
        private void insertValue(Node x, int k)
        {

            if (x.leaf)
            {//находим позицию куда вставить входной элемент
                int i = 0;
                for (i = x.n - 1; i >= 0 && k < x.key[i]; i--)
                {
                    x.key[i + 1] = x.key[i];
                }
                x.key[i + 1] = k;
                // увеличить счетчик показывающи что пока он
                x.n = x.n + 1;
            }
            else
            {
                int i = 0;


                i++;
                Node tmp = x.child[i];//присваем во временный ребенка нового корня со значениями больше среднего элемента
                if (tmp.n == 2 * T - 1)//если в корне нет места
                {
                    Split(x, i, tmp); //разбиваем x это указатель на новый корень tmp это указатель на
                                      //ребенка со значениями больше среднего элемента в корне
                    if (k > x.key[i])
                    {
                        i++;
                    }
                }
                insertValue(x.child[i], k);
            }

        }

        public void Show()
        {
            Show(root);
        }

        // Display
        private void Show(Node x)
        {
            if (x == null) ;
            for (int i = 0; i < x.n; i++)
            {
                Console.WriteLine(x.key[i] + " ");
            }
            if (!x.leaf)
            {
                for (int i = 0; i < x.n + 1; i++)
                {
                    Show(x.child[i]);
                }
            }
        }

        // Check if present
        public bool Contain(int k)
        {
            if (this.Search(root, k) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
