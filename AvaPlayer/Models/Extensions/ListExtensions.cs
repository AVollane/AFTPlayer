using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTPlayer.Models.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Перемешивает список
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToMix"></param>
        public static void Mix<T>(this List<T> listToMix)
        {
            Random random = new Random();
            int upperBoundIndex = listToMix.Count - 1;
            for(int i = 0; i < listToMix.Count; i++)
            {
                int randomElementIndex = random.Next(0, upperBoundIndex + 1); // Выбираем случайный элемент массива
                T lastMedia = listToMix[upperBoundIndex];
                listToMix[upperBoundIndex] = listToMix[randomElementIndex]; // Меняем его местами с последним элементом
                listToMix[randomElementIndex] = lastMedia;
                upperBoundIndex--;
            }
        }
    }
}
