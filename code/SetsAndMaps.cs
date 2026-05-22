using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

// ============================================================
// W03 — Sets and Maps
// Problems 1, 2, 3, 5  (Problem 4 is in Maze.cs)
// ============================================================

public static class SetsAndMaps
{
    // --------------------------------------------------------
    // Problem 1 — Find Pairs with Sets  O(n)
    // --------------------------------------------------------

    /// <summary>
    /// Given a list of two-letter, lower-case words (no duplicates),
    /// returns all symmetric pairs in the form "xy &amp; yx".
    ///
    /// Words whose two letters are identical ("aa", "bb", …) are skipped
    /// because the duplicate-free assumption means their reverse can never
    /// appear separately.
    ///
    /// Time complexity: O(n) — one pass, one HashSet lookup per word.
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        var seen = new HashSet<string>();
        var results = new List<string>();

        foreach (string word in words)
        {
            // Skip same-letter words — they cannot have a valid partner
            if (word[0] == word[1])
                continue;

            string reversed = $"{word[1]}{word[0]}";

            if (seen.Contains(reversed))
                // reversed was seen first, so report it as the left word
                results.Add($"{reversed} & {word}");
            else
                seen.Add(word);
        }

        return results.ToArray();
    }

    // --------------------------------------------------------
    // Problem 2 — Degree Summary
    // --------------------------------------------------------

    /// <summary>
    /// Reads a CSV file and counts how many records hold each degree type
    /// found in column index 4 (0-based).
    /// Degree names are discovered dynamically from the file content.
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (string line in File.ReadLines(filename))
        {
            string[] cols = line.Split(',');

            if (cols.Length > 4)
            {
                string degree = cols[4].Trim();

                if (string.IsNullOrEmpty(degree))
                    continue;

                if (degrees.ContainsKey(degree))
                    degrees[degree]++;
                else
                    degrees[degree] = 1;
            }
        }

        return degrees;
    }

    // --------------------------------------------------------
    // Problem 3 — Anagram Check
    // --------------------------------------------------------

    /// <summary>
    /// Returns true when <paramref name="word1"/> and
    /// <paramref name="word2"/> are anagrams.
    ///
    /// Rules applied before comparison:
    ///   • Spaces are removed.
    ///   • Letter case is ignored.
    ///
    /// Algorithm: build a letter-frequency Dictionary for word1,
    /// then decrement it while scanning word2.  Any missing key or
    /// negative count means the words are not anagrams.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Normalise
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        if (word1.Length != word2.Length)
            return false;

        // Count letters in word1
        var freq = new Dictionary<char, int>();
        foreach (char c in word1)
        {
            if (freq.ContainsKey(c))
                freq[c]++;
            else
                freq[c] = 1;
        }

        // Subtract letters using word2
        foreach (char c in word2)
        {
            if (!freq.ContainsKey(c))
                return false;   // letter not present in word1

            freq[c]--;

            if (freq[c] < 0)
                return false;   // word2 has more of this letter than word1
        }

        return true;
    }

    // --------------------------------------------------------
    // Problem 5 — Earthquake Daily Summary
    // --------------------------------------------------------

    /// <summary>
    /// Fetches today's earthquake data from the USGS all-day GeoJSON feed
    /// and returns an array of strings formatted as
    ///   "PLACE - Mag MAGNITUDE"
    ///
    /// Returns an empty array when the network is unavailable.
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string Url =
            "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        string json;
        try
        {
            using var client = new HttpClient();
            json = client.GetStringAsync(Url).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[EarthquakeDailySummary] Network error: {ex.Message}");
            return Array.Empty<string>();
        }

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var collection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        if (collection?.Features == null)
            return Array.Empty<string>();

        var results = new List<string>();
        foreach (Feature feature in collection.Features)
        {
            string place = feature.Properties?.Place ?? "Unknown location";
            double? mag = feature.Properties?.Mag;
            results.Add($"{place} - Mag {mag}");
        }

        return results.ToArray();
    }
}

// --------------------------------------------------------
// JSON model classes for Problem 5
// --------------------------------------------------------

/// <summary>Root GeoJSON object returned by USGS.</summary>
public class FeatureCollection
{
    [JsonPropertyName("features")]
    public List<Feature>? Features { get; set; }
}

/// <summary>One earthquake event inside the feature array.</summary>
public class Feature
{
    [JsonPropertyName("properties")]
    public EarthquakeProperties? Properties { get; set; }
}

/// <summary>The two fields we display for each earthquake.</summary>
public class EarthquakeProperties
{
    [JsonPropertyName("place")]
    public string? Place { get; set; }

    [JsonPropertyName("mag")]
    public double? Mag { get; set; }
}
