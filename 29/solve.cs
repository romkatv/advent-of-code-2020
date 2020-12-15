using System.Collections.Generic;
using System.Linq;
using static System.Console;
using static System.IO.File;

const int N = 2020;

List<int> nums = ReadLines("input")
    .First()
    .Split(',')
    .Select(int.Parse)
    .ToList();

for (int n; (n = nums.Count) < N;) {
  int x = nums.Last();
  int i = nums.FindLastIndex(n - 2, n - 1, y => x == y);
  nums.Add(i >= 0 ? n - i - 1 : 0);
}

WriteLine(nums[N - 1]);
