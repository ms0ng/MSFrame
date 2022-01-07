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
        internal int[] ints;
        internal int pValue;
        public int Value
        {
            get
            {
                return ints.Sum();
            }

            set
            {
                if (Value != pValue)
                {
                    //Cheat Detected
                }

                int offset = value - Value;
                ints[SimpleRandom.Random(0, ints.Length - 1)] += offset;

                pValue = value;
            }
        }

        public MInt(int value) : this(value, SimpleRandom.Random(2, 10))
        {

        }

        public MInt(int value, int arrayLength)
        {
            pValue = value;
            ints = new int[arrayLength];
            int t = value / arrayLength;
            for (int i = 0; i < arrayLength - 1; i++) ints[i] = SimpleRandom.Random(-t, t);
            ints[ints.Length - 1] = 0;
            ints[ints.Length - 1] = value - Value;
        }
    }

}

