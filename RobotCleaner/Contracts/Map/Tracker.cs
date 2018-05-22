using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Map
{
    public class Tracker
    {
        private Dictionary<Coordinate, int> visited { get; set; }
        private Dictionary<Coordinate, int> cleaned { get; set; }
        
        public Tracker(PositionState positionState)
        {
            visited = new Dictionary<Coordinate, int>(new CoordinateComparer()) { { new Coordinate(positionState.X, positionState.Y), 0 } };
            cleaned = new Dictionary<Coordinate, int>(new CoordinateComparer());
        }

        public Tracker(int x, int y)
        {
            visited = new Dictionary<Coordinate, int>(new CoordinateComparer()) { { new Coordinate(x, y), 0 } };
            cleaned = new Dictionary<Coordinate, int>(new CoordinateComparer());
        }

        public void TryAddingToVisited(int x, int y)
        {
            try
            {
                var length = visited.Count;
                visited.Add(new Coordinate(x, y), length);
            }
            catch (Exception ex) { }
        }

        public void TryAddingToCleaned(int x, int y)
        {
            try
            {
                var length = cleaned.Count;
                cleaned.Add(new Coordinate(x, y), length);
            }
            catch (Exception ex) { }
        }

        public List<Coordinate> Visited => visited.OrderBy(v => v.Value).Select(v => v.Key).ToList();
        public List<Coordinate> Cleaned => cleaned.OrderBy(c => c.Value).Select(c => c.Key).ToList();
    }
}
