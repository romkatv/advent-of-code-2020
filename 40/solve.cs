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

int w = grid[0].In.Pixels.Length - 2;
int m = n * w;

foreach (OutputTile t in grid) t.In.Place(t.Variant);

char Pixel(int y, int x) => grid[Idx(y / w, x / w)].In.Pixels[y % w + 1][x % w + 1];

string[] img = new[] {
  "                  # ",
  "#    ##    ##    ###",
  " #  #  #  #  #  #   ",
};

List<(int y, int x)> MatchMonster(int y, int x) {
  List<(int, int)> res = new();
  for (int i = 0; i != img.Length; ++i) {
    for (int j = 0; j != img[0].Length; ++j) {
      if (img[i][j] != '#') continue;
      if (Pixel(y + i, x + j) != '#') return new List<(int, int)>();
      res.Add((y + i, x + j));
    }
  }
  return res;
}

for (int i = 0; i != 2; ++i) {
  for (int j = 0; j != 4; ++j) {
    HashSet<(int, int)> monster = new();
    for (int y = 0; y != m - img.Length; ++y) {
      for (int x = 0; x != m - img[0].Length; ++x) {
        foreach (var p in MatchMonster(y, x)) monster.Add(p);
      }
    }
    if (monster.Any()) {
      int ans = 0;
      for (int y = 0; y != m; ++y) {
        for (int x = 0; x != m; ++x) {
          if (Pixel(y, x) == '#' && !monster.Contains((y, x))) ++ans;
        }
      }
      WriteLine(ans);
      return;
    }
    img = Geometry.Rotate(img);
  }
  img = img.Reverse().ToArray();
}

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

static class Geometry {
  public static string[] Rotate(string[] src) {
    char[][] res = src[0].Select(_ => new char[src.Length]).ToArray();
    for (int i = 0; i != src.Length; ++i) {
      for (int j = 0; j != src[0].Length; ++j) {
        res[j][src.Length - i - 1] = src[i][j];
      }
    }
    return res.Select(s => new string(s)).ToArray();
  }
}

class InputTile {
  public int Id { get; init; }
  public string[] Pixels { get; set; }
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
        yield return new OutputTile(this, 4 * i + j, border.Select(Parse).ToArray());
        border = new [] { Reverse(3), border[0], Reverse(1), border[2] };
      }
      border = new [] { border[2], Reverse(1), border[0], Reverse(3) };
    }
    string Reverse(int i) => new string(border[i].Reverse().ToArray());
    string Slice(int i) => new string(Pixels.Select(s => s[i]).ToArray());
    ushort Parse(string s) => Convert.ToUInt16(s.Replace('.', '0').Replace('#', '1'), 2);
  }

  public void Place(int variant) {
    for (int i = 0; i != 2; ++i) {
      for (int j = 0; j != 4; ++j) {
        if (4 * i + j == variant) return;
        Pixels = Geometry.Rotate(Pixels);
      }
      Pixels = Pixels.Reverse().ToArray();
    }
  }
}

record OutputTile(InputTile In, int Variant, ushort[] Border);
