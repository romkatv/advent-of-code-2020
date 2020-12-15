using System.Linq;
using static System.Console;
using static System.IO.File;

const int N = 30000000;

int i = 1;
int last = 0;
int[] nums = new int[N];

foreach (int x in ReadLines("input").First().Split(',').Select(int.Parse)) {
  last = nums[x];
  nums[x] = i++;
}

for (; ; ++i) {
  int x = last == 0 ? 0 : i - last - 1;
  if (i == N) {
    WriteLine(x);
    break;
  }
  last = nums[x];
  nums[x] = i;
}
