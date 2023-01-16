using System;

namespace Hash
{
    internal class HashTable
    {
        private static int tableSize = 50;
        private Train[] table = new Train[tableSize];

        private int GetHash(int number)
        {
            return DividingHash(number);
        }

        public string Add(Train train)
        {
            string res = "";
            int hash = GetHash(train.number);

            Train value = table[hash];
            if (value == null)
            {
                table[hash] = train;
                res = $"Добавлено на позицию: {hash}\nМесто в цепочке: 1";
            }
            else if (value.deleted)
            {
                table[hash].UpdateTrain(train);
                res = $"Добавлено на позицию: {hash}\nМесто в цепочке: 1";
            }
            else
            {
                int chainCount = 2;
                while (true)
                {
                    if (value.nextTrain == null)
                    {
                        value.nextTrain = train;
                        res = $"Добавлено на позицию: {hash}\nМесто в цепочке: {chainCount}";

                        break;
                    }
                    else if (value.nextTrain.deleted)
                    {
                        value.nextTrain.UpdateTrain(train);

                        res = $"Добавлено на позицию: {hash}\nМесто в цепочке: {chainCount}";

                        break;
                    }

                    chainCount++;
                    value = value.nextTrain;
                }
            }

            return res;
        }

        public string Find(int number)
        {
            int hash = GetHash(number);
            Train value = table[hash];

            int chainCount = 1;
            while (value != null)
            {
                if (value.number == number && !value.deleted)
                {
                    return $"Индекс: {hash}\nПозиция в цепочке: {chainCount}\nПоезд: {value}";
                }
                value = value.nextTrain;
                chainCount++;
            }
            return "Элемента нет";
        }

        public string Delete(int number)
        {
            int hash = GetHash(number);
            Train value = table[hash];

            int chainCount = 1;
            while (value != null)
            {
                if (value.number == number && !value.deleted)
                {
                    value.deleted = true;
                    return $"Поезд: {value}\nУдален с индекса: {hash}\nПозиция в цепочке: {chainCount}\n";
                }
                value = value.nextTrain;
                chainCount++;
            }
            return "Элемента нет";
        }

        public string Show()
        {
            string output = "";

            for (int i = 0; i < tableSize; i++)
            {
                Train value = table[i];
                if (value == null) continue;

                int chainCount = 1;
                output += i < 10 ? $"0{i}":i;
                int drawCount = 1;
                while (value != null)
                {
                    if (!value.deleted)
                    {
                        output += $"{(drawCount > 1 ? "    " : "")} ({chainCount}): {value}\n";
                        drawCount++;
                    }
                    chainCount++;
                    value = value.nextTrain;
                }
            }
            return output;
        }

        private int DividingHash(int number)
        {
            int m = 145;
            int hash = number % m + 1;
            return hash % tableSize;
        }

        private int SquareCenterHash(int number)
        {
            int square = number * number;
            string str = square.ToString();

            int count = tableSize.ToString().Length;
            double m = tableSize / Math.Pow(10, count);

            int offset = ( str.Length - 1 - count ) / 2 + 1;
            str = str.Substring(offset, count);
            
            double res = Convert.ToInt32(str) * m;
            return (int)res;
        }
    }
}
