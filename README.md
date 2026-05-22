 # W03 — Sets and Maps

## Project Structure

```
week03/
├── .vscode/
│   ├── launch.json        ← Run/Debug config (select "Week 03 — Sets and Maps")
│   └── tasks.json         ← Build and Test tasks
└── code/
    ├── Week03.csproj      ← .NET 8 project file
    ├── Program.cs         ← Console demo of all five problems
    ├── SetsAndMaps.cs     ← ✅ Problems 1, 2, 3, 5
    ├── Maze.cs            ← ✅ Problem 4
    ├── SetsAndMaps_Tests.cs ← All MSTest unit tests
    └── census.txt         ← Sample census data for Problem 2
```

---

## Problems Implemented

### Problem 1 — FindPairs (O(n) with a HashSet)
```
Input:  [am, at, ma, if, fi]
Output: ["am & ma", "if & fi"]
```
- Iterates the word list once → **O(n)**
- Uses a `HashSet<string>` to check if the reverse has already been seen
- Skips palindrome words (e.g. `"aa"`) automatically

### Problem 2 — SummarizeDegrees
- Reads `census.txt` line by line
- Splits on comma, grabs column index **4** (0-based)
- Accumulates counts in a `Dictionary<string, int>`
- Degree names are discovered automatically from the file

### Problem 3 — IsAnagram
- Normalises both words (lowercase, strip spaces)
- Builds a letter-frequency `Dictionary<char, int>` for word1
- Decrements it while scanning word2; returns false on any mismatch
- `"CAT"/"ACT"` → true | `"DOG"/"GOOD"` → false | `"Ab"/"bA"` → true

### Problem 4 — Maze Navigation
- `MoveLeft / MoveRight / MoveUp / MoveDown` each look up the current
  `(x, y)` position in the maze dictionary and check the appropriate
  `bool` flag before updating `CurrX` / `CurrY`
- Invalid moves print an error message and leave the position unchanged
- `Maze.CreateSampleMaze()` builds the 6×6 sample from the assignment

### Problem 5 — EarthquakeDailySummary
- Fetches live JSON from the USGS all-day feed:
  `https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson`
- Deserialises into `FeatureCollection → Feature → EarthquakeProperties`
- Returns strings formatted as `"PLACE - Mag MAGNITUDE"`

---

## Running the Project

### Run all tests
```bash
dotnet test week03/code/Week03.csproj --logger "console;verbosity=detailed"
```

### Run the console demo
```bash
dotnet run --project week03/code/Week03.csproj
```

### In VS Code
1. Open the Run and Debug panel (`Ctrl+Shift+D`)
2. Select **"Week 03 — Sets and Maps"** from the dropdown
3. Click the green ▶ Start Debugging button

To run individual tests, open `SetsAndMaps_Tests.cs` and click the
**Run Test** or **Debug Test** buttons that appear above each method.

---

## Key Concepts Used

| Problem | Data Structure | Time Complexity |
|---------|---------------|----------------|
| FindPairs | `HashSet<string>` | **O(n)** |
| SummarizeDegrees | `Dictionary<string,int>` | O(n) |
| IsAnagram | `Dictionary<char,int>` | O(n) |
| Maze | `Dictionary<(int,int),(bool,bool,bool,bool)>` | O(1) per move |
| Earthquake | JSON → custom classes | O(n) entries |