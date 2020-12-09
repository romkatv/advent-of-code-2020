using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Console;
using static System.IO.File;

int[] seq = ReadLines("input").Select(int.Parse).ToArray();
HashSet<int> set = seq.ToHashSet();
Trace.Assert(seq.Length == set.Count);

for (int i = 0; i < seq.Length - 2; ++i) {
  for (int j = i + 1; j < seq.Length - 1; ++j) {
    int x = 2020 - seq[i] - seq[j];
    if (set.Contains(x)) {
      WriteLine(1L * seq[i] * seq[j] * x);
      return;
    }
  }
}
