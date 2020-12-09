using System.Collections.Generic;
using System.Linq;
using static System.Array;
using static System.Console;
using static System.IO.File;

const long needle = 1124361034;

long sum = 0;
long[] nums = ReadLines("input").Select(long.Parse).ToArray();
long[] sums = new[] { 0L }.Concat(nums).Select(x => sum += x).ToArray();

for (int i = 0; i != sums.Length - 1; ++i) {
  int j = BinarySearch(sums, i + 1, sums.Length - i - 1, needle + sums[i]);
  if (j > i + 1) {
    IEnumerable<long> seq = nums.Skip(i).Take(j - i);
    WriteLine(seq.Min() + seq.Max());
    return;
  }
}
