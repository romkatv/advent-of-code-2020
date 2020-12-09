using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

const int W = 25;

long[] cypher = File.ReadLines("input").Select(long.Parse).ToArray();
Dictionary<long, int> window = new();
int i = 0;

void MoveWindow() {
  window[cypher[i]] = window.GetValueOrDefault(cypher[i]) + 1;
  if (++i > W && --window[cypher[i - W - 1]] == 0) window.Remove(cypher[i - W - 1]);
}

while (i != W) MoveWindow();

for (;; MoveWindow()) {
  long n = cypher[i];
  for (int j = i - W; ; ++j) {
    if (j == i) {
      WriteLine(n);
      return;
    }
    long m = n - cypher[j];
    if (window.GetValueOrDefault(m) > (n == 2 * m ? 1 : 0)) break;
  }
}
