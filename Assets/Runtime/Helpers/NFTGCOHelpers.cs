using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace NFTGCO.Helpers
{
    public static class NFTGCOHelpers
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
        {
            return source.Select((item, index) => (item, index));
        }

        /// <summary>
        /// Get the attribute of an Enum
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }

        public static int FindIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindIndex(array, match);
        }

        /// <summary>
        /// Join two list into one
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">the first list to concat</param>
        /// <param name="second">the second list to concat</param>
        /// <returns></returns>
        public static List<T> Join<T>(this List<T> first, List<T> second)
        {
            if (first == null)
                return second;

            if (second == null)
                return first;

            return first.Concat(second).ToList();
        }

        #region Awaits

        /// <summary>
        /// Executes an action after the given condition is met
        /// </summary>
        /// <param name="me">the current class where this Waiter gonna happend</param>
        /// <param name="inCompletionCheck">function that returns true or false depending on if the "condition" is met</param>
        /// <param name="DoAction">Action to be invoked once the condition is met</param>
        /// <returns></returns>
        public static WW.Waiters.Waiter DoAfter(this MonoBehaviour me, Func<bool> inCompletionCheck, Action DoAction)
        {
            return WW.Waiters.WaitController.DoAfter(inCompletionCheck, DoAction).SetID(me);
        }
        /// <summary>
        /// Executes an action after x seconds
        /// </summary>
        /// <param name="me"></param>
        /// <param name="inTimeToWait"></param>
        /// <param name="inOnCompleteAction"></param>
        /// <returns></returns>
        public static WW.Waiters.Waiter DoAfterWait(this MonoBehaviour me, float inTimeToWait,
            Action inOnCompleteAction)
        {
            return WW.Waiters.WaitController.DoAfterWait(inTimeToWait, inOnCompleteAction).SetID(me);
        }

        /// <summary>
        /// Deep clone an object https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepClone<T>(this T obj)
        {
            using (var memoryStream = new System.IO.MemoryStream())
            {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(memoryStream, obj);
                memoryStream.Position = 0;

                return (T)formatter.Deserialize(memoryStream);
            }
        }

        #endregion

        #region UI

        /// <summary>
        /// Set the state of a CanvasGroup
        /// </summary>
        /// <param name="canvasGroup"></param>
        /// <param name="state"></param>
        public static void CanvasGroupBehaviour(CanvasGroup canvasGroup, bool state)
        {
            canvasGroup.alpha = state ? 1f : 0f;
            canvasGroup.interactable = state;
            canvasGroup.blocksRaycasts = state;
        }

        #endregion

        #region DateTime

        /// <summary>
        /// Get the week of the month
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetWeekOfMonth(DateTime date)
        {
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);
            while (date.Date.AddDays(1).DayOfWeek !=
                   System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(1);
            return (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
        }

        /// <summary>
        /// Get the month of the year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetMonthOfYear(DateTime date)
        {
            return date.Month;
        }

        /// <summary>
        /// Check if two dates are in the same week
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static bool IsSameWeek(DateTime date1, DateTime date2)
        {
            return GetWeekOfMonth(date1) == GetWeekOfMonth(date2);
        }

        /// <summary>
        /// Check if two dates are in the same month
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static bool IsSameMonth(DateTime date1, DateTime date2)
        {
            return GetMonthOfYear(date1) == GetMonthOfYear(date2);
        }

        /// <summary>
        /// Check if two dates are in the same week, month and year
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static bool IsSameWeekMonthYear(DateTime date1, DateTime date2)
        {
            return IsSameWeek(date1, date2) && IsSameMonth(date1, date2) && date1.Year == date2.Year;
        }

        public static DateTime ParseStringToDateTime(string date)
        {
            //since the id are composed by the name of the item and the date of the reset, we need to split the string to get the date
            string[] splitString = date.Split('-');
            //return the last element of the array, that is the date
            return DateTime.Parse(splitString.Last());
        }

        #endregion
    }
}