namespace Algorithms
{
    internal static class DaySix
    {
        internal static void Test() => Notes();

        internal static void Notes()
        {
            /* First we have to find his starting location
             * 
             * Then we "watch" him move....While
             * 
             * MOVE TO NEXT LOCATION
             * 
             * If he can move in same direction (up/down/left/right) 
             *      (keep track of direction)
             *      (keep track of current and visited "locations")
             *  ==> MOVE
             *      
             *  If "Can't move", turn 90% Clockwise (to the right) => new direction
             *              * 
             *  ==> MOVE
             *              * 
             * DEFINITION OF: "Can't move"
             *   - Obstacle in front OR at beginning or end of row or beginning of end of column
             *   - If the "move" would put him off the grid, stop/return results
             * 
             */
            WriteLine(new string('*', 80));
            WriteLine(nameof(Notes));
            WriteLine(new string('*', 80));

        }

        internal static List<string> TestData()
        {
            return [
"....#....."
,".........#"
,".........."
,"..#......."
,".......#.."
,".........."
,".#..^....."
,"........#."
,"#........."
,"......#..."
                ];

        }
    }
}