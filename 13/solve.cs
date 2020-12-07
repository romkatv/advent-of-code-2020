using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

Dictionary<string, List<string>> in2out = new();
foreach (Bag bag in File.ReadLines("input").Select(ParseBag)) {
  foreach (Bags inner in bag.Content) {
    if (in2out.TryGetValue(inner.Type, out List<string> outer)) {
      outer.Add(bag.Type);
    } else {
      in2out.Add(inner.Type, new List<string>() { bag.Type });
    }
  }
}

HashSet<string> visited = new();
VisitAllOuter("shiny gold");
WriteLine(visited.Count - 1);

void VisitAllOuter(string type) {
  if (!visited.Add(type)) return;
  foreach (string outer in in2out.GetValueOrDefault(type, new())) {
    VisitAllOuter(outer);
  }
}

static Bag ParseBag(string s) {
  Match m = Regex.Match(
    s,
    @"^(.+?) bags contain (?:(?:no other bags)|(?:(\d+) (.+?) bag[s]?(?:, )?)+).$");
  Trace.Assert(m.Success);
  Trace.Assert(m.Groups.Count == 4);
  CaptureCollection qty = m.Groups[2].Captures;
  CaptureCollection type = m.Groups[3].Captures;
  Trace.Assert(qty.Count == type.Count);
  Bag res = new(m.Groups[1].Value, new List<Bags>());
  for (int i = 0; i != m.Groups[2].Captures.Count; ++i) {
    res.Content.Add(new Bags(type[i].Value, int.Parse(qty[i].Value)));
  }
  return res;
}

record Bags(string Type, int Count);
record Bag(string Type, List<Bags> Content);
