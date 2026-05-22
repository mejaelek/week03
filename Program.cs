using System.Diagnostics;

// ============================================================
// W03 — Sets and Maps : Console Demo
//
// This class can be called from any test's Debug run or from a
// scratch test method to manually inspect output.
//
// Use Debug.WriteLine (not Console.WriteLine) when running under
// the VS Code test runner — output appears in the Debug Console tab.
// ============================================================

public static class Demo
{
    /// <summary>Runs all five problems and prints results.</summary>
    public static void Run()
    {
        Line("========================================");
        Line("  W03 — Sets and Maps");
        Line("========================================");
        Line("");

        // ---- Problem 1 -----------------------------------------
        Line("Problem 1: FindPairs");
        string[] words = { "am", "at", "ma", "if", "fi" };
        string[] pairs = SetsAndMaps.FindPairs(words);
        Line($"  Input  : [{string.Join(", ", words)}]");
        Line($"  Pairs  : [{string.Join(", ", pairs)}]");
        Line("");

        // ---- Problem 2 -----------------------------------------
        Line("Problem 2: SummarizeDegrees");
        var degrees = SetsAndMaps.SummarizeDegrees("census.txt");
        foreach (var (deg, cnt) in degrees.OrderByDescending(d => d.Value))
            Line($"  {deg,-20} : {cnt}");
        Line("");

        // ---- Problem 3 -----------------------------------------
        Line("Problem 3: IsAnagram");
        Line($"  CAT / ACT       -> {SetsAndMaps.IsAnagram("CAT", "ACT")}");
        Line($"  DOG / GOOD      -> {SetsAndMaps.IsAnagram("DOG", "GOOD")}");
        Line($"  Ab  / bA        -> {SetsAndMaps.IsAnagram("Ab", "bA")}");
        Line($"  listen / silent -> {SetsAndMaps.IsAnagram("listen", "si lent")}");
        Line("");

        // ---- Problem 4 -----------------------------------------
        Line("Problem 4: Maze");
        var maze = Maze.CreateSampleMaze();
        Line($"  Start : ({maze.CurrX}, {maze.CurrY})");
        maze.MoveRight();    // (1,1) -> (2,1)  valid
        maze.MoveRight();    // (2,1) -> blocked, error printed
        maze.MoveLeft();     // (2,1) -> (1,1)  valid
        maze.MoveDown();     // (1,1) -> (1,2)  valid
        maze.MoveUp();       // (1,2) -> (1,1)  valid
        Line("");

        // ---- Problem 5 -----------------------------------------
        Line("Problem 5: EarthquakeDailySummary");
        string[] quakes = SetsAndMaps.EarthquakeDailySummary();
        if (quakes.Length == 0)
        {
            Line("  (No data — check network connection)");
        }
        else
        {
            Line($"  {quakes.Length} earthquakes today. Showing first 5:");
            foreach (string q in quakes.Take(5))
                Line($"  {q}");
        }
        Line("");
        Line("Done.");
    }

    // Output to both Console and the VS Code Debug Console
    private static void Line(string msg)
    {
        Console.WriteLine(msg);
        Debug.WriteLine(msg);
    }
}