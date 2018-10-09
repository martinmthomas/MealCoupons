using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealCoupons
{
    class Program
    {

        static int PairsCount(List<int> coupons, int mealCost)
        {
            coupons.Sort();

            int count = 0;
            int startIndex = 0;
            int endIndex = coupons.Count - 1;
            var couponPairsConsidered = new List<KeyValuePair<int, int>>();

            //since the coupons are sorted, we can create pair using one coupon from the start and other from the end.
            while (startIndex < endIndex)
            {
                var couponPair = new KeyValuePair<int, int>(coupons[startIndex], coupons[endIndex]);

                //if the coupon was already considered before, then ignore it.
                if (couponPairsConsidered.Any(c => AreCouponPairsSame(c, couponPair)))
                {
                    startIndex++;
                    continue;
                }

                var sumOfBoundaryItems = couponPair.Key + couponPair.Value;
                if (sumOfBoundaryItems < mealCost)
                    startIndex++;
                else if (sumOfBoundaryItems > mealCost)
                    endIndex--;
                else
                {
                    count++;
                    couponPairsConsidered.Add(couponPair);
                    startIndex++;
                    endIndex--;
                }
            }

            return count;
        }


        static bool AreCouponPairsSame(KeyValuePair<int, int> c1, KeyValuePair<int, int> c2) =>
            (c1.Key == c2.Key && c1.Value == c2.Value)
            || (c1.Value == c2.Key && c1.Key == c2.Value);


        static void Main(string[] args)
        {
            var couponValues = new List<int> { 3, 5, 7, 1, 3, 2 };
            var mealValue = 8;
            var count = PairsCount(couponValues, mealValue);
            Console.WriteLine($"Total number of coupon pairs that could be used to by the meal worth {mealValue} is {count}");

            Console.ReadKey();
        }
    }
}
