using System;
using System.Diagnostics;
using System.Linq;
using static System.Console;
using static System.IO.File;

string[] lines = ReadAllLines("input");
Trace.Assert(lines.Length > 0 && lines[0].Length > 0);
Trace.Assert(lines.All(x => x.Length == lines[0].Length));
Trace.Assert(lines.All(x => x.All(c => c is ('.' or 'L'))));

int H = lines.Length;
int W = lines[0].Length;
int N = lines.Sum(s => s.Count(c => c == 'L'));
int[] idx = new int[W * H];
int[] adj = new int[8 * (N + 1)];

for (int i = 0, p = 0; i != H; ++i) {
  int Idx(int y, int x) => idx[y * W + x] == 0 ? idx[y * W + x] = ++p : idx[y * W + x];
  for (int j = 0; j != W; ++j) {
    if (lines[i][j] != 'L') continue;
    int n = 8 * Idx(i, j);
    for (int dy = -1; dy != 2; ++dy) {
      for (int dx = -1; dx != 2; ++dx) {
        if (dy == 0 && dx == 0) continue;
        for (int y = i, x = j; (y += dy) >= 0 && y < H && (x += dx) >= 0 && x < W;) {
          if (lines[y][x] == 'L') {
            adj[n++] = Idx(y, x);
            break;
          }
        }
      }
    }
  }
}

Chair[] cur = new Chair[N + 1];
Chair[] next = new Chair[N + 1];

for (bool done = false; !done;) {
  done = true;
  for (int i = 1; i != N + 1; ++i) {
    int inc = 0;
    switch (cur[i]) {
      case Chair c when !c.Occupied && c.Neighbours == 0:
        next[i].Occupied = true;
        inc = 1;
        break;
      case Chair c when c.Occupied && c.Neighbours >= 5:
        next[i].Occupied = false;
        inc = -1;
        break;
      default:
        continue;
    }
    done = false;
    for (int j = 0; j != 8; ++j) next[adj[8 * i + j]].Neighbours += inc;
  }
  Array.Copy(next, cur, N + 1);
}

WriteLine(cur.Count(c => c.Occupied));

struct Chair {
  public bool Occupied { get; set; }
  public int Neighbours { get; set; }
}
