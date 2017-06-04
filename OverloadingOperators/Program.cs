using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverloadingOperators
{
    class Time
    {
        private int _seconds;
        private int _minutes;

        /// <summary>
        /// Overloading operators, 
        /// we operate on minutes and seconds, not on a decimal system (50/100 goin to be 30/60 etc.)
        /// </summary>
        /// <param name="value">Used to convert int to minutes</param>
        public static implicit operator Time(int value)
        {
            return new Time(0, value);
        }

        /// <summary>
        /// We change seconds greater than 59 and less than -59 to the minutes, we also change the minutes after the fraction to the second
        /// </summary>
        /// <param name="min">Minutes</param>
        /// <param name="sec">Seconds</param>
        public Time(int min, int sec)
        {
            while (sec < 0 && min >= 1 || sec <= -60)
            {
                min--;
                sec = sec + 60;
            }

            while (sec > 0 && min < 0 || sec >= 60)
            {
                min++;
                sec = sec - 60;
            }

            _seconds = sec;
            _minutes = min;
        }

        public static Time operator +(Time a, Time b)
        {
            int seconds = (a._seconds + b._seconds) % 60;
            int minutes = a._minutes + b._minutes + (a._seconds + b._seconds) / 60;
            return new Time(minutes, seconds);
        }

        public static Time operator -(Time a, Time b)
        {
            int seconds = (a._seconds - b._seconds) % 60;
            int minutes = a._minutes - b._minutes + (a._seconds - b._seconds) / 60;
            return new Time(minutes, seconds);
        }

        public static bool operator >(Time a, Time b)
        {
            return TimeEqual(a._minutes, a._seconds, b._minutes, b._seconds, false, 0);
        }

        public static bool operator <(Time a, Time b)
        {
            return TimeEqual(b._minutes, b._seconds, a._minutes, a._seconds, false, 0);
        }

        public static bool operator >=(Time a, Time b)
        {
            return TimeEqual(a._minutes, a._seconds, b._minutes, b._seconds, true, 0);
        }

        public static bool operator <=(Time a, Time b)
        {
            return TimeEqual(b._minutes, b._seconds, a._minutes, a._seconds, true, 0);
        }

        public static bool operator ==(Time a, Time b)
        {
            return TimeEqual(b._minutes, b._seconds, a._minutes, a._seconds, true, 1);
        }

        public static bool operator !=(Time a, Time b)
        {
            return TimeEqual(b._minutes, b._seconds, a._minutes, a._seconds, true, 2);
        }

        public static bool TimeEqual(int min1, int sek1, int min2, int sek2, bool moreOrEqual, int equal)
        {
            bool equation = true;
            if (moreOrEqual == true)
                if (equal == 1)
                    if (min1 * 60 + sek1 == min2 * 60 + sek2)
                        equation = true;
                    else
                        equation = false;
                else if (equal == 2)
                    if (min1 * 60 + sek1 != min2 * 60 + sek2)
                        equation = true;
                    else
                        equation = false;
                else
                    if (min1 * 60 + sek1 >= min2 * 60 + sek2)
                    equation = true;
                else
                    equation = false;
            else
                if (min1 * 60 + sek1 > min2 * 60 + sek2)
                equation = true;
            else
                equation = false;
            return equation;
        }

        public static Time operator *(Time a, Time b)
        {
            int time = ((a._minutes * 60 + a._seconds) * (b._minutes * 60 + b._seconds));
            int seconds = time % 3600 / 60;
            int minutes = (time - time % 3600) / 3600;
            return new Time(minutes, seconds);
        }

        public static Time operator /(Time a, Time b)
        {
            int time = (((a._minutes * 60 + a._seconds) * 60) / ((b._minutes * 60 + b._seconds)) * 60);
            int seconds = (time - time % 60) / 60;
            int minutes = (time % 60) / 60;
            return new Time(minutes, seconds);
        }

        /// <summary>
        /// Displays times
        /// </summary>
        public void Show()
        {
            Console.WriteLine("Number of minutes: {1}{0}Number of seconds: {2}{0}", Environment.NewLine, _minutes, _seconds);
        }
    }

    class Program
    {
        /// <summary>
        /// Examples of use
        /// </summary>
        static void Main(string[] args)
        {
            Time t1 = new Time(2, 0);
            Time t2 = new Time(-1, 122);
            Time t3 = t1 + t2;
            Time t4 = t1 + 1;
            Time t5 = 100 + t1;
            Time t6 = t1 - t2;
            Time t7 = 1 - t2;
            Time t8 = t1 - 232;
            Time t9 = t1 * t2;
            Time t10 = t1 * 2;
            Time t11 = 4 * t2;
            Time t12 = t1 / t2;
            Time t13 = t1 / 2;
            Time t14 = 400 / t2;
            t1.Show();
            t2.Show();
            t3.Show();
            t4.Show();
            t5.Show();
            t6.Show();
            t7.Show();
            t8.Show();
            t9.Show();
            t10.Show();
            t11.Show();
            t12.Show();
            t13.Show();
            t14.Show();
            Console.WriteLine("t1 < t2  ->  " + (t1 < t2));
            Console.WriteLine("t1 < 12  ->  " + (t1 < 12));
            Console.WriteLine("-31 < t1  ->  " + (-31 < t12));
            Console.WriteLine("t1 > t2  ->  " + (t1 > t2));
            Console.WriteLine("1 > t1  ->  " + (1 > t1));
            Console.WriteLine("t1 > 2  ->  " + (t1 > 2));
            Console.WriteLine("t1 <= t2  ->  " + (t1 <= t2));
            Console.WriteLine("32 <= t1  ->  " + (32 <= t1));
            Console.WriteLine("t1 <= 321  ->  " + (t1 <= 321));
            Console.WriteLine("t1 >= t2  ->  " + (t1 >= t2));
            Console.WriteLine("21 >= t1  ->  " + (21 >= t1));
            Console.WriteLine("t1 >= -12  ->  " + (t1 >= -12));
            Console.WriteLine("t1 == t2  ->  " + (t1 == t2));
            Console.WriteLine("t1 == 121  ->  " + (t1 == 121));
            Console.WriteLine("12 == t1  ->  " + (1 == t1));
            Console.WriteLine("t1 != t2  ->  " + (t1 != t2));
            Console.WriteLine("t1 != 32  ->  " + (t1 != 32));
            Console.WriteLine("121 != t1  ->  " + (121 != t1));
            Console.ReadKey();
        }
    }
}
