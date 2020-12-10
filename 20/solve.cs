using System.Linq;
using static System.Console;
using static System.IO.File;

int[] jolts = ReadLines("input")
    .Select(int.Parse)
    .OrderBy(x => -x)
    .Append(0)
    .ToArray();

long[] ways = new long[jolts.Length];
ways[0] = 1;

for (int i = 1; i != jolts.Length; ++i) {
  for (int j = i - 1; j >= 0 && jolts[j] - jolts[i] <= 3; --j) {
    ways[i] += ways[j];
  }
}

WriteLine(ways.Last());
