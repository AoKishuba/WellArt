using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellArt
{
    static class MyExtensions
    {
        /// <summary>
        /// Shuffle objects in given list
        /// </summary>
        /// <typeparam name="T">Type of objects in list</typeparam>
        /// <param name="list">List to be shuffled</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }
    }
}
