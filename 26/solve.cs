using static System.Console;
using static System.IO.File;

long x = 0, y = -1, n = 1;

foreach (string s in ReadAllLines("input")[1].Split(',')) {
  ++y;
  if (s == "x") continue;
  int m = int.Parse(s);
  while ((x + y) % m != 0) x = x += n;
  n *= m;
}

WriteLine(x);
