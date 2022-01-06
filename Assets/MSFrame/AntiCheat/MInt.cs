using System;
using System.Collections;
using System.Collections.Generic;


namespace MSFrame.AntiCheat
{
    public class SimpleRandom
    {
        private static readonly System.Random random = new System.Random();
        private SimpleRandom()
        {

        }
        public static int Random(int min, int max)
        {
            if (min > max) return random.Next(max, min);
            return random.Next(min, max);
        }
        public static int Random(int max)
        {
            return Random(0, max);
        }
        public static int Random()
        {
            return Random(0, 100);
        }
    }

    /// <summary>
    /// Using : <see cref="MInt.Value"/>
    /// </summary>
    public struct MInt
    {
        internal int[] iList;
        internal int publicValue;
        public int Value
        {
            get
            {
                int a = 0;
                for (int i = 0; i < iList.Length; i++)
                {
                    a += iList[i];
                }
                return a;
            }

            set
            {
                if (Value != publicValue)
                {
                    //Cheat Detected
                }

                int offset = value - Value;
                iList[SimpleRandom.Random(0, iList.Length - 1)] += offset;

                publicValue = value;
            }
        }

        public MInt(int value) : this(value, SimpleRandom.Random(2, 10))
        {

        }

        public MInt(int value, int arrayLength)
        {
            publicValue = value;
            iList = new int[arrayLength];
            int t = value / arrayLength;
            for (int i = 0; i < arrayLength - 1; i++) iList[i] = SimpleRandom.Random(-t, t);
            iList[iList.Length - 1] = 0;
            iList[iList.Length - 1] = value - Value;
        }
    }

}

