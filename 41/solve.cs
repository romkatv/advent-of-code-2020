using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;
using static System.IO.File;
using static System.Linq.Enumerable;

Dictionary<string, int> food2count = new();
Dictionary<string, HashSet<string>> allergen2foods = new();

foreach (string line in ReadLines("input")) {
  Match m = Regex.Match(line,@"^(([^ ]+) )+\(contains (([^,]+)(, )?)+\)$");
  Trace.Assert(m.Success);
  IEnumerable<string> Capture(int i) => m.Groups[i].Captures.Select(c => c.Value);
  IEnumerable<string> Allergens() => Capture(4);
  IEnumerable<string> Ingredients() => Capture(2);
  foreach (string food in Ingredients()) {
    food2count[food] = food2count.GetValueOrDefault(food) + 1;
  }
  foreach (string allergen in Allergens()) {
    if (allergen2foods.TryGetValue(allergen, out HashSet<string> candidates)) {
      candidates.IntersectWith(Ingredients());
    } else {
      allergen2foods.Add(allergen, Ingredients().ToHashSet());
    }
  }
}

HashSet<string> bad = allergen2foods.Values.SelectMany(x => x).ToHashSet();
WriteLine(food2count.Where(kv => !bad.Contains(kv.Key)).Select(kv => kv.Value).Sum());
