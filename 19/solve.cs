using System.Linq;
using static System.Console;
using static System.IO.File;

int prev = 0;
int[] diff = new int[3];

foreach (int cur in ReadLines("input").Select(int.Parse).OrderBy(x => x)) {
  ++diff[cur - prev - 1];
  prev = cur;
}

WriteLine(diff[0] * (diff[2] + 1));
