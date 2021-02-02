using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSet
{
    public class NumSet
    {
        private HashSet<int>? numberSet;


        public NumSet(params int[] ? numSet)
        {
            if(numSet is null)
            {
                throw new ArgumentNullException(nameof(NumSet));
            }
            numberSet = new HashSet<int>(numSet);
        }

        public override string ToString()
        {

            if(numberSet is null)
            {
                throw new ArgumentNullException(nameof(NumSet));
            }

            return String.Join(",", numberSet);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is not NumSet numSet)
            {
                return false;
            }

            if(numberSet is null)
            {
                throw new ArgumentNullException(nameof(NumSet));
            }


            return numberSet.SetEquals(numSet.numberSet!);
        }

        public override int GetHashCode()
        {
            if(numberSet is null)
            {
                throw new ArgumentNullException(nameof(NumSet));
            }

            return numberSet.Sum().GetHashCode();
        }

        public static bool operator ==(NumSet first, NumSet second)
        {
            if (first is null && second is null) return true;
            if (first is null ^ second is null) return false;
            return first!.Equals(second);
        }

        public static bool operator !=(NumSet first, NumSet second) => !(first == second);

        public int[] returnArray()
        {
            if(numberSet is null)
            {
                throw new ArgumentNullException(nameof(numberSet));
            }

            int[] set = new int[numberSet.Count];
            numberSet.CopyTo(set);
            return set;
        }
        
    }
}
