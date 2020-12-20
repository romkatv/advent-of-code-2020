using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Console;
using static System.IO.File;
using static System.Linq.Enumerable;

(int n, List<List<OutputTile>> tiles) = ParseInput();
OutputTile[] grid = new OutputTile[n * n];

int Idx(int y, int x) => y * n + x;

for (int y = 0; y != n; ++y) {
  for (int x = 0; x != n; ++x) {
    List<OutputTile> candidates =
        x > 0 ? tiles[grid[Idx(y, x - 1)].Border[1]] :
        y > 0 ? tiles[grid[Idx(y - 1, x)].Border[2]] :
        tiles.SelectMany(a => a).ToList();
    candidates.RemoveAll(a => a.In.Used);
    Match(0, x, y - 1);
    Match(1, x + 1, y);
    Match(2, x, y + 1);
    Match(3, x - 1, y);
    void Match(int i, int x, int y) {
      if (x < 0 || x >= n || y < 0 || y >= n) {
        candidates.RemoveAll(a => tiles[a.Border[i]].Any(b => b.In.Id != a.In.Id));
      } else if (i == 0 || i == 3) {
        candidates.RemoveAll(a => a.Border[i] != grid[Idx(y, x)].Border[(i + 2) % 4]);
      }
    }
    Trace.Assert(candidates.Count == (x == 0 && y == 0 ? 32 : 1));
    grid[Idx(y, x)] = candidates[0];
    candidates[0].In.Used = true;
  }
}

long ans = 1L *
  grid[0].In.Id *
  grid[n - 1].In.Id *
  grid[n * n - 1].In.Id *
  grid[n * n - n].In.Id;
WriteLine(ans);

static (int n, List<List<OutputTile>> tiles) ParseInput() {
  int n = 0;
  List<List<OutputTile>> tiles = new();
  IEnumerator<string> lines = ReadLines("input").GetEnumerator();
  while (lines.MoveNext()) {
    foreach (OutputTile tile in InputTile.Parse(lines).OutputTiles()) {
      foreach (ushort b in tile.Border) {
        while (tiles.Count <= b) tiles.Add(new List<OutputTile>());
        tiles[b].Add(tile);
      }
    }
    ++n;
  }
  Trace.Assert(Math.Sqrt(n) * Math.Sqrt(n) == n);
  n = (int)Math.Sqrt(n);
  return (n, tiles);
}

class InputTile {
  public int Id { get; init; }
  public string[] Pixels { get; init; }
  public bool Used { get; set; }

  public static InputTile Parse(IEnumerator<string> lines) {
    int id = int.Parse(lines.Current.Split(' ', ':')[1]);
    List<string> pixels = new();
    while (lines.MoveNext() && lines.Current != "") pixels.Add(lines.Current);
    return new InputTile() { Id = id, Pixels = pixels.ToArray() };
  }

  public IEnumerable<OutputTile> OutputTiles() {
    string[] border = new [] {
      Pixels.First(),
      Slice(Pixels[0].Length - 1),
      Pixels.Last(),
      Slice(0)
    };
    for (int i = 0; i != 2; ++i) {
      for (int j = 0; j != 4; ++j) {
        yield return new OutputTile(this, border.Select(Parse).ToArray());
        border = new [] { Reverse(3), border[0], Reverse(1), border[2] };
      }
      border = new [] { Reverse(0), border[3], Reverse(2), border[1] };
    }
    string Reverse(int i) => new string(border[i].Reverse().ToArray());
    string Slice(int i) => new string(Pixels.Select(s => s[i]).ToArray());
    ushort Parse(string s) => Convert.ToUInt16(s.Replace('.', '0').Replace('#', '1'), 2);
  }
}

record OutputTile(InputTile In, ushort[] Border);
