using System.Diagnostics;

// ============================================================
// W03 — Problem 4 : Maze Navigation
//
// The maze is a Dictionary where:
//   Key   = (x, y)  of every open (non-wall) cell
//   Value = (left, right, up, down)  — true = that direction is passable
//
// Coordinate convention:
//   x  increases to the right  (1 … 6)
//   y  increases downward       (1 … 6)
//
// Graphical layout of the 6×6 sample maze (# = blocked cell):
//
//        x=1  x=2  x=3  x=4  x=5  x=6
//   y=1   .    .    #    .    .    .
//   y=2   .    .    #    #    .    #
//   y=3   #    .    #    .    .    .
//   y=4   .    .    .    .    .    #
//   y=5   .    #    .    #    .    #
//   y=6   .    #    .    #    .    .
//
// A wall surrounds the entire grid; no cell outside 1-6 exists.
// ============================================================

public class Maze
{
    private readonly Dictionary<(int X, int Y), (bool Left, bool Right, bool Up, bool Down)> _map;

    /// <summary>Current x-coordinate of the player (1-based).</summary>
    public int CurrX { get; private set; }

    /// <summary>Current y-coordinate of the player (1-based).</summary>
    public int CurrY { get; private set; }

    /// <param name="map">Pre-built direction map for the maze.</param>
    /// <param name="startX">Starting column (default 1).</param>
    /// <param name="startY">Starting row    (default 1).</param>
    public Maze(
        Dictionary<(int X, int Y), (bool Left, bool Right, bool Up, bool Down)> map,
        int startX = 1,
        int startY = 1)
    {
        _map = map;
        CurrX = startX;
        CurrY = startY;
    }

    // -------- Movement methods ----------------------------------------

    /// <summary>Move one step left (x - 1) if the current cell allows it.</summary>
    public void MoveLeft()
    {
        if (_map.TryGetValue((CurrX, CurrY), out var d) && d.Left)
        {
            CurrX--;
            Console.WriteLine($"Moved left  -> ({CurrX}, {CurrY})");
        }
        else
        {
            Console.WriteLine("Error: Can't go left from the current position.");
        }
    }

    /// <summary>Move one step right (x + 1) if the current cell allows it.</summary>
    public void MoveRight()
    {
        if (_map.TryGetValue((CurrX, CurrY), out var d) && d.Right)
        {
            CurrX++;
            Console.WriteLine($"Moved right -> ({CurrX}, {CurrY})");
        }
        else
        {
            Console.WriteLine("Error: Can't go right from the current position.");
        }
    }

    /// <summary>Move one step up (y - 1) if the current cell allows it.</summary>
    public void MoveUp()
    {
        if (_map.TryGetValue((CurrX, CurrY), out var d) && d.Up)
        {
            CurrY--;
            Console.WriteLine($"Moved up    -> ({CurrX}, {CurrY})");
        }
        else
        {
            Console.WriteLine("Error: Can't go up from the current position.");
        }
    }

    /// <summary>Move one step down (y + 1) if the current cell allows it.</summary>
    public void MoveDown()
    {
        if (_map.TryGetValue((CurrX, CurrY), out var d) && d.Down)
        {
            CurrY++;
            Console.WriteLine($"Moved down  -> ({CurrX}, {CurrY})");
        }
        else
        {
            Console.WriteLine("Error: Can't go down from the current position.");
        }
    }

    // -------- Factory -------------------------------------------------

    /// <summary>
    /// Constructs the exact 6x6 sample maze from the assignment description.
    /// Only open (non-wall) cells appear in the dictionary.
    /// Each value tuple is (canGoLeft, canGoRight, canGoUp, canGoDown).
    /// </summary>
    public static Maze CreateSampleMaze()
    {
        //                                  Left   Right  Up     Down
        var map = new Dictionary<(int, int), (bool, bool, bool, bool)>
        {
            // y = 1
            { (1, 1), (false, true,  false, true)  },
            { (2, 1), (true,  false, false, false) },
            { (4, 1), (false, true,  false, false) },
            { (5, 1), (true,  true,  false, true)  },
            { (6, 1), (true,  false, false, false) },

            // y = 2
            { (1, 2), (false, true,  true,  true)  },
            { (2, 2), (true,  false, false, true)  },
            { (5, 2), (false, false, true,  true)  },

            // y = 3
            { (2, 3), (false, true,  true,  true)  },
            { (4, 3), (false, true,  false, true)  },
            { (5, 3), (true,  true,  true,  false) },
            { (6, 3), (true,  false, false, true)  },

            // y = 4
            { (1, 4), (false, true,  false, true)  },
            { (2, 4), (true,  true,  true,  false) },
            { (3, 4), (true,  true,  false, false) },
            { (4, 4), (true,  false, true,  true)  },
            { (5, 4), (false, false, false, true)  },

            // y = 5
            { (1, 5), (false, false, true,  true)  },
            { (3, 5), (false, false, false, true)  },
            { (5, 5), (false, false, true,  false) },

            // y = 6
            { (1, 6), (false, false, true,  false) },
            { (3, 6), (false, false, true,  false) },
            { (5, 6), (false, true,  false, false) },
            { (6, 6), (true,  false, true,  false) },
        };

        return new Maze(map, startX: 1, startY: 1);
    }
}
