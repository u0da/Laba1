using System;
namespace Laba1
{
    struct Grid
    {
        public float Start { get; }
        public float Step { get; }
        public int Number { get; }

        public Grid(float start, float step, int number)
        {
            Start = start;
            Step = step;
            Number = number;
        }

        public float GetTime(int n)
        {
            return Start + n * Step; 
        }

        public override string ToString()
        {
            return GetType().Name + $"Start {Start}, Step {Step}, Number {Number}";
        }
    }
}
