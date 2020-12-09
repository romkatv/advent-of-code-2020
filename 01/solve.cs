using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Console;
using static System.IO.File;

HashSet<int> seen = new();
foreach (int n in ReadLines("input").Select(int.Parse)) {
  int m = 2020 - n;
  if (seen.Contains(m)) {
    WriteLine(n * m);
    return;
  }
  Trace.Assert(seen.Add(n));
}
