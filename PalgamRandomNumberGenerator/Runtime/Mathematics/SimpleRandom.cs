using System;
using FixMath;

namespace Shared.Mathematics
{
    public class SimpleRandom
    {
        private long _seed;
        private const long a = 1664525;
        private const long c = 1013904223;
        private const long m = 4294967296; // 2^32

        public SimpleRandom(long seed)
        {
            _seed = seed;
        }

        // Returns a random integer in the range [0, int.MaxValue)
        public int Next()
        {
            _seed = (a * _seed + c) % m;
            return (int)(_seed >> 16); // Return the higher bits for better randomness
        }

        // Returns a random integer in the range [minValue, maxValue)
        public int Next(int minValue, int maxValue)
        {
            if (minValue >= maxValue)
                throw new ArgumentOutOfRangeException("minValue must be less than maxValue");

            return minValue + Next() % (maxValue - minValue);
        }

        public F32 NextF32Int(int min, int max)
        {
            return F32.FromInt(Next(min, max));
        }

        public F64 NextF64Int(int min, int max)
        {
            return F64.FromInt(Next(min, max));
        }

        public F32 NextF32Fraction()
        {
            return F32.Ratio1000(Next(0, 1000));
        }

        // Returns a random double in the range [0.0, 1.0)
        public double NextDouble()
        {
            return (double)Next() / int.MaxValue;
        }
    }
}