using System.Diagnostics;
using System.Linq;
using static System.Console;
using static System.IO.File;

string[] lines = ReadAllLines("input");
Trace.Assert(lines.Length > 0 && lines[0].Length > 0);
Trace.Assert(lines.All(x => x.Length == lines[0].Length));
Trace.Assert(lines.All(x => x.All(c => c is ('.' or 'L'))));

int M = lines.Length;
int N = lines[0].Length;
char[] grid = lines
    .Append(new string('.', N))
    .Prepend(new string('.', N))
    .SelectMany(x => '.' + x + '.')
    .ToArray();
char[] next = new char[grid.Length];

char Get(int i, int j) => grid[i * (N + 2) + j];
char Set(int i, int j, char c) => next[i * (N + 2) + j] = c;

int Neighbours(int i, int j) {
  int res = 0;
  for (int y = i - 1; y != i + 2; ++y) {
    for (int x = j - 1; x != j + 2; ++x) {
      if (Get(y, x) == '#') ++res;
    }
  }
  return res;
}

for (bool done = false; !done;) {
  done = true;
  for (int i = 1; i != M + 1; ++i) {
    for (int j = 1; j != N + 1; ++j) {
      switch (Get(i, j)) {
        case 'L' when Neighbours(i, j) == 0:
          done = false;
          Set(i, j, '#');
          break;
        case '#' when Neighbours(i, j) > 4:
          done = false;
          Set(i, j, 'L');
          break;
        default:
          Set(i, j, Get(i, j));
          break;
      }
    }
  }
  char[] tmp = grid;
  grid = next;
  next = tmp;
}

WriteLine(grid.Count(c => c == '#'));
