using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;
using static System.IO.File;
using static System.Linq.Enumerable;

Dictionary<string, HashSet<string>> allergen2foods = new();

foreach (string line in ReadLines("input")) {
  Match m = Regex.Match(line,@"^(([^ ]+) )+\(contains (([^,]+)(, )?)+\)$");
  Trace.Assert(m.Success);
  IEnumerable<string> Capture(int i) => m.Groups[i].Captures.Select(c => c.Value);
  IEnumerable<string> Allergens() => Capture(4);
  IEnumerable<string> Ingredients() => Capture(2);
  foreach (string allergen in Allergens()) {
    if (allergen2foods.TryGetValue(allergen, out HashSet<string> candidates)) {
      candidates.IntersectWith(Ingredients());
    } else {
      allergen2foods.Add(allergen, Ingredients().ToHashSet());
    }
  }
}

List<(string Allergen, string Ingredient)> ans = new();

while (true) {
  var kv = allergen2foods.FirstOrDefault(kv => kv.Value.Count == 1);
  if (kv.Key == null) break;
  ans.Add((kv.Key, kv.Value.First()));
  allergen2foods.Remove(kv.Key);
  foreach (HashSet<string> foods in allergen2foods.Values) {
    foods.Remove(kv.Value.First());
  }
}

WriteLine(string.Join(',', ans.OrderBy(x => x.Allergen).Select(x => x.Ingredient)));
