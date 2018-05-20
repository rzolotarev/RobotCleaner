using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Map
{
    public class CoordinateComparer : IEqualityComparer<Coordinate>
    {        
        public bool Equals(Coordinate x, Coordinate y)
        {
            return x.X == y.X && x.Y == y.Y;
        }

        public int GetHashCode(Coordinate obj)
        {
            unchecked
            {

                int hash = 17;                
                hash = hash * 23 + obj.X.GetHashCode();
                hash = hash * 23 + obj.Y.GetHashCode();                
                return hash;
            }                
        }
    }
}
