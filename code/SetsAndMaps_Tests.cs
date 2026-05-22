using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ============================================================
// W03 — Unit Tests  (MSTest)
//
// In VS Code, open this file and use the inline buttons:
//   Run Test        — runs one test method
//   Run All Tests   — runs the whole class
//   Debug Test      — hits breakpoints; use Debug.WriteLine() for output
// ============================================================

[TestClass]
public class SetsAndMaps_Tests
{
    // =============================================================
    // Problem 1 — FindPairs
    // =============================================================

    [TestMethod]
    public void FindPairs_BasicExample_TwoPairsFound()
    {
        string[] words = { "am", "at", "ma", "if", "fi" };
        string[] result = SetsAndMaps.FindPairs(words);

        Assert.AreEqual(2, result.Length, "Expected exactly 2 symmetric pairs");

        bool hasAmMa = ContainsEither(result, "am & ma", "ma & am");
        Assert.IsTrue(hasAmMa, "Expected pair 'am & ma'");

        bool hasIfFi = ContainsEither(result, "if & fi", "fi & if");
        Assert.IsTrue(hasIfFi, "Expected pair 'if & fi'");
    }

    [TestMethod]
    public void FindPairs_UnmatchedWord_NotIncluded()
    {
        // "at" has no "ta" in the list — must not appear in results
        string[] words = { "am", "at", "ma", "if", "fi" };
        string[] result = SetsAndMaps.FindPairs(words);

        bool hasAt = Array.Exists(result, r => r.Contains("at") || r.Contains("ta"));
        Assert.IsFalse(hasAt, "'at' has no reverse so it must be absent");
    }

    [TestMethod]
    public void FindPairs_PalindromesExcluded()
    {
        string[] words = { "aa", "bb", "ab", "ba" };
        string[] result = SetsAndMaps.FindPairs(words);

        Assert.AreEqual(1, result.Length, "Only 'ab & ba' expected");
        Assert.IsFalse(
            Array.Exists(result, r => r.Contains("aa") || r.Contains("bb")),
            "Same-letter words must be excluded");
    }

    [TestMethod]
    public void FindPairs_NoPairs_EmptyResult()
    {
        string[] result = SetsAndMaps.FindPairs(new[] { "ab", "cd", "ef" });
        Assert.AreEqual(0, result.Length);
    }

    [TestMethod]
    public void FindPairs_EmptyInput_EmptyResult()
    {
        string[] result = SetsAndMaps.FindPairs(Array.Empty<string>());
        Assert.AreEqual(0, result.Length);
    }

    // =============================================================
    // Problem 2 — SummarizeDegrees
    // =============================================================

    [TestMethod]
    public void SummarizeDegrees_CensusFileExists()
    {
        Assert.IsTrue(File.Exists("census.txt"),
            "census.txt not found. Check <CopyToOutputDirectory> in Week03.csproj.");
    }

    [TestMethod]
    public void SummarizeDegrees_NonEmptyDictionary()
    {
        var result = SetsAndMaps.SummarizeDegrees("census.txt");
        Assert.IsTrue(result.Count > 0, "At least one degree should be found");
    }

    [TestMethod]
    public void SummarizeDegrees_AllCountsPositive()
    {
        var result = SetsAndMaps.SummarizeDegrees("census.txt");
        foreach (var (degree, count) in result)
            Assert.IsTrue(count > 0, $"Count for '{degree}' must be > 0");
    }

    [TestMethod]
    public void SummarizeDegrees_KnownDegreesPresent()
    {
        // These four degree types exist in the bundled census.txt
        var result = SetsAndMaps.SummarizeDegrees("census.txt");
        Assert.IsTrue(result.ContainsKey("Bachelors"), "Expected key 'Bachelors'");
        Assert.IsTrue(result.ContainsKey("Masters"), "Expected key 'Masters'");
        Assert.IsTrue(result.ContainsKey("PhD"), "Expected key 'PhD'");
        Assert.IsTrue(result.ContainsKey("High School"), "Expected key 'High School'");
    }

    [TestMethod]
    public void SummarizeDegrees_CorrectCounts()
    {
        // Counts match the bundled census.txt (25 rows):
        //   Bachelors: 9, Masters: 6, PhD: 5, High School: 5
        var result = SetsAndMaps.SummarizeDegrees("census.txt");
        Assert.AreEqual(9, result["Bachelors"], "Bachelors count");
        Assert.AreEqual(6, result["Masters"], "Masters count");
        Assert.AreEqual(5, result["PhD"], "PhD count");
        Assert.AreEqual(5, result["High School"], "High School count");
    }

    // =============================================================
    // Problem 3 — IsAnagram
    // =============================================================

    [TestMethod]
    public void IsAnagram_CatAct_True()
        => Assert.IsTrue(SetsAndMaps.IsAnagram("CAT", "ACT"));

    [TestMethod]
    public void IsAnagram_DogGood_False()
        => Assert.IsFalse(SetsAndMaps.IsAnagram("DOG", "GOOD"));

    [TestMethod]
    public void IsAnagram_MixedCase_True()
        => Assert.IsTrue(SetsAndMaps.IsAnagram("Ab", "bA"));

    [TestMethod]
    public void IsAnagram_WithSpaces_True()
        => Assert.IsTrue(SetsAndMaps.IsAnagram("listen", "si lent"));

    [TestMethod]
    public void IsAnagram_DifferentLengths_False()
        => Assert.IsFalse(SetsAndMaps.IsAnagram("hello", "hell"));

    [TestMethod]
    public void IsAnagram_IdenticalWord_True()
        => Assert.IsTrue(SetsAndMaps.IsAnagram("test", "test"));

    [TestMethod]
    public void IsAnagram_SingleCharSameCase_True()
        => Assert.IsTrue(SetsAndMaps.IsAnagram("a", "A"));

    [TestMethod]
    public void IsAnagram_CompletelyDifferent_False()
        => Assert.IsFalse(SetsAndMaps.IsAnagram("abc", "xyz"));

    // =============================================================
    // Problem 4 — Maze
    // =============================================================

    [TestMethod]
    public void Maze_StartPosition_Is_1_1()
    {
        var m = Maze.CreateSampleMaze();
        Assert.AreEqual(1, m.CurrX, "Start X = 1");
        Assert.AreEqual(1, m.CurrY, "Start Y = 1");
    }

    [TestMethod]
    public void Maze_MoveRight_From_1_1_Succeeds()
    {
        // (1,1) has Right = true  → should reach (2,1)
        var m = Maze.CreateSampleMaze();
        m.MoveRight();
        Assert.AreEqual(2, m.CurrX, "X after MoveRight");
        Assert.AreEqual(1, m.CurrY, "Y unchanged");
    }

    [TestMethod]
    public void Maze_MoveDown_From_1_1_Succeeds()
    {
        // (1,1) has Down = true  → should reach (1,2)
        var m = Maze.CreateSampleMaze();
        m.MoveDown();
        Assert.AreEqual(1, m.CurrX, "X unchanged");
        Assert.AreEqual(2, m.CurrY, "Y after MoveDown");
    }

    [TestMethod]
    public void Maze_MoveLeft_From_1_1_Blocked()
    {
        // (1,1) has Left = false  → position unchanged
        var m = Maze.CreateSampleMaze();
        m.MoveLeft();
        Assert.AreEqual(1, m.CurrX, "X unchanged after blocked left");
        Assert.AreEqual(1, m.CurrY, "Y unchanged after blocked left");
    }

    [TestMethod]
    public void Maze_MoveUp_From_1_1_Blocked()
    {
        // (1,1) has Up = false  → position unchanged
        var m = Maze.CreateSampleMaze();
        m.MoveUp();
        Assert.AreEqual(1, m.CurrX, "X unchanged after blocked up");
        Assert.AreEqual(1, m.CurrY, "Y unchanged after blocked up");
    }

    [TestMethod]
    public void Maze_MultiStep_ValidPath()
    {
        // BFS-confirmed valid path:
        //   (1,1) --down--> (1,2) --right--> (2,2) --down--> (2,3)
        var m = Maze.CreateSampleMaze();
        m.MoveDown();   // (1,1) → (1,2)  Down=true at (1,1)
        m.MoveRight();  // (1,2) → (2,2)  Right=true at (1,2)
        m.MoveDown();   // (2,2) → (2,3)  Down=true at (2,2)
        Assert.AreEqual(2, m.CurrX, "X = 2 at end of path");
        Assert.AreEqual(3, m.CurrY, "Y = 3 at end of path");
    }

    [TestMethod]
    public void Maze_InvalidMoveDoesNotChangePosition()
    {
        // After moving right to (2,1), Right is false there → stays at (2,1)
        var m = Maze.CreateSampleMaze();
        m.MoveRight();          // (1,1) → (2,1)
        int xBefore = m.CurrX;
        int yBefore = m.CurrY;
        m.MoveRight();          // blocked at (2,1)
        Assert.AreEqual(xBefore, m.CurrX, "X unchanged after blocked move");
        Assert.AreEqual(yBefore, m.CurrY, "Y unchanged after blocked move");
    }

    // =============================================================
    // Problem 5 — EarthquakeDailySummary  (live network data)
    // =============================================================

    [TestMethod]
    public void EarthquakeDailySummary_ReturnsNonNullArray()
    {
        string[] result = SetsAndMaps.EarthquakeDailySummary();
        Assert.IsNotNull(result, "Must not return null");
    }

    [TestMethod]
    public void EarthquakeDailySummary_EntriesFormatted()
    {
        string[] result = SetsAndMaps.EarthquakeDailySummary();

        if (result.Length == 0)
        {
            Debug.WriteLine("EarthquakeDailySummary: 0 results — network may be offline.");
            return;
        }

        foreach (string entry in result)
            Assert.IsTrue(entry.Contains(" - Mag "),
                $"Format error — expected 'PLACE - Mag VALUE', got: '{entry}'");
    }

    // =============================================================
    // Helper
    // =============================================================

    private static bool ContainsEither(string[] arr, string a, string b)
        => Array.IndexOf(arr, a) >= 0 || Array.IndexOf(arr, b) >= 0;
}
