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
        public void setItemsLeft(Node x ,int[] arr,int pos)//разделение данных полного узла 
        {
            for (int i = 0; i < pos; i++)
            {
                x.key[i] = arr[i];
                x.n++;
            }
        }
        public void setItemsRight(Node x, int[] arr, int pos)//разделение данных полного узла 
        {
            int count = 0;
            for (int i = pos+1; i <arr.Length; i++)
                x.key[count++] = arr[i];
            x.n = count;
            //затереть оставшийсямассив 0 оставш масив
            for (int i = count; i < arr.Length; i++)
            {
                x.key[i] = 0;
            }
        }
        // Splitting the node
        private void Split(Node x, int pos, Node y)//x=root и он пустой. y содержит
                                                   //старые значения корня которые нужно разбить
                                                   //pos это позиция которя определяет средний элемент и индекс куда он станет в корневом узле

        {// x теперь новый корень в него нужно поместить среднее значение из старого корня

            // нужно найти средний элемент из старого корня
            Node z = new Node();// создается новый пустой узел куда будут разбиваться половина старых
                                // элементов из полного корня со вставлением новог входного значения
            z.leaf = y.leaf; //копируется указатель на лист из старого корня
           // z.n = T - 1;//3-1=2
            //найти средний элемент
          int mid =y.key.Length / 2;
            int m = y.key[mid];
          
           //помещаем половину ключей из старог корня в левого ребенка то что меньше
            setItemsLeft(z, y.key,mid);
            //3-1=2 присваиваем теперь что в старом корне будет хронится оставшаяся половина водных данных то что больше среднего
            setItemsRight(y, y.key,mid);
           if (!y.leaf)// если это промежуточный узел??????
            {
                for (int j = 0; j < T; j++)
                {
                    z.child[j] = y.child[j + T];
                }
            }
            //y.n = T - 1;//3-1=2 присваиваем теперь что в старом корне будет хронится оставшаяся половина водных данных
           
            x.child[pos] = z;// присваиваем теперь новому корю .первого ребенка со значениями переопределенными в строке 70
           // ///добавить средний элемент в новый корень
            for (int j = 0; j < x.n; j++)
            {
                if (m < x.key[pos])
                {
                    //выполнить сдвиг элемента массива в право со вставлением входного элемента в позтцию
                    for (int i = x.n - 1; i > j; i--)
                        x.key[i + 1] = x.key[i];

                    x.key[j] = m;
                    x.n++;

                }
            }
            x.key[x.n] = m;
            x.n++;
/////////////////////////////
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
                s.child[1] = r;
                Split(s, 0, r);//s(значение присвоеное новому корею) присвоен старый кокень, r(присвоено значение старого корня) теперь первый ребенок 
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
            int c = 0;//позиция элемента в массиве узла
            Node currentNode = x;
            while (true)
            {
                if (currentNode.leaf)
                {
                    if (k < currentNode.key[c])
                    {
                        for (int pos = 0; pos < x.n; pos++)
                        {
                            if (k < x.key[pos])
                            {
                                //выполнить сдвиг элемента массива в право со вставлением входного элемента в позтцию
                                for (int i = currentNode.n - 1; i > pos; i--)
                                    currentNode.key[i + 1] = currentNode.key[i];

                                currentNode.key[pos] = k;
                                currentNode.n++;
                            }
                        }
                    }
                    currentNode.key[currentNode.n] = k;//////////000000000 на 20падает
                    currentNode.n++;
                    return;
                }///если не лист найти ребенка
                else if (k < currentNode.key[c] && !currentNode.leaf)
                {
                    currentNode = currentNode.child[c];
                    c++;
                }
                else if (k > currentNode.key[c] && !currentNode.leaf)
                {
                    currentNode = currentNode.child[c + 1];
                    c++;
                }
                /*{
                    if (k > currentNode.key[c])
                    {
                        for (int pos = 0; pos < x.n; pos++)
                        {
                            if (k < x.key[pos])
                            {
                                //выполнить сдвиг элемента массива в право со вставлением входного элемента в позтцию
                                for (int i = x.n - 1; i > pos; i--)
                                    x.key[i + 1] = x.key[i];

                                x.key[pos] = k;
                                x.n++;
                            }
                        }
                    }
                    x.key[x.n] = k;
                    x.n++;
                   
                    if (k > currentNode.key[c + 1])
                    {
                        currentNode = currentNode.child[c + 1];
                        c++;
                    }
                    return;
                }*/
            }

          /*  if (x.leaf) рабочее но не до конца
            {//находим позицию куда вставить входной элемент

                for (int pos = 0; pos < x.n; pos++)
                {
                    if (k < x.key[pos])
                    {
                        //выполнить сдвиг элемента массива в право со вставлением входного элемента в позтцию
                        for (int i = x.n - 1; i > pos; i--)
                            x.key[i + 1] = x.key[i];

                        x.key[pos] = k;
                        x.n++;
                    }
                }
                x.key[x.n] = k;
                x.n++;
            }
            else if(!x.leaf&& x.n== 2 * T - 1)// разбиение промежуточного узла
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
            }//если это не лист найти лист и вставить значение в нужную позицию
            else if (!x.leaf) {
                x = x.child[i];
            }*/

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
