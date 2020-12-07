using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

Dictionary<string, List<Bags>> bags = File
    .ReadLines("input")
    .Select(ParseBag)
    .ToDictionary(x => x.Type, x => x.Content);

WriteLine(ContentCount("shiny gold", new()) - 1);

long ContentCount(string type, Dictionary<string, long> memo) {
  if (memo.TryGetValue(type, out long res)) return res;
  res = 1 + bags[type].Select(x => x.Count * ContentCount(x.Type, memo)).Sum();
  memo.Add(type, res);
  return res;
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
