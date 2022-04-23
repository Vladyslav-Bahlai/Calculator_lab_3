using System;

namespace CalcClass
{
    public class CalcClass
    {
        static public int Add(long a, long b) {
            return (int)(a + b);
        }
        static public int Sub(long a, long b) {
            return (int)(a - b);
        }
        static public int Mult(long a, long b) {
            return (int)(a * b);
        }
        static public int Div(long a, long b) {
            return (int)(a / b);
        }
        static public int Mod(long a, long b) {
            return (int)(a % b);
        }
        static public int ABS(long a) {
            return (int)(+a);
        }
        static public int IABS(long a) {
            return (int)(-a);
        }
    }
}
