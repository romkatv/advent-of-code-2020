using System.Diagnostics;
using System.Linq;
using static System.Console;
using static System.IO.File;
using static System.Linq.Enumerable;

const int N = 6;

string[] lines = ReadAllLines("input");
Trace.Assert(lines.Length > 0 && lines[0].Length > 0);
Trace.Assert(lines.All(s => s.Length == lines[0].Length));
Trace.Assert(lines.All(s => s.All(c => c is '.' or '#')));

int w = 2 * N + 2 + lines[0].Length;
int h = 2 * N + 2 + lines.Length;
int d = 2 * N + 2 + 1;
byte[] grid = new byte[w * h * d];

int Idx(int x, int y, int z) => z * w * h + y * w + x;

for (int y = 0; y != lines.Length; ++y) {
  for (int x = 0; x != lines[y].Length; ++x) {
    if (lines[y][x] == '#') grid[Idx(N + 1 + x, N + 1 + y, N + 1)] = 1;
  }
}

int PopCount(int x, int y, int z) {
  int res = 0;
  for (int dx = -1; dx != 2; ++dx) {
    for (int dy = -1; dy != 2; ++dy) {
      for (int dz = -1; dz != 2; ++dz) {
        res += grid[Idx(x + dx, y + dy, z + dz)];
      }
    }
  }
  return res;
}

for (int i = 0; i != N; ++i) {
  byte[] next = new byte[grid.Length];
  for (int x = 1; x != w - 1; ++x) {
    for (int y = 1; y != h - 1; ++y) {
      for (int z = 1; z != d - 1; ++z) {
        switch (PopCount(x, y, z)) {
          case 3: next[Idx(x, y, z)] = 1; break;
          case 4: next[Idx(x, y, z)] = grid[Idx(x, y, z)]; break;
        }
      }
    }
  }
  grid = next;
}

WriteLine(grid.Sum(x => (int)x));
