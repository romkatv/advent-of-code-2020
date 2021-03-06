﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using static System.Console;

string[] track = File.ReadAllLines("input");
Trace.Assert(track.Length > 0);
Trace.Assert(track[0].Length > 0);
Trace.Assert(track.All(s => s.Length == track[0].Length));
Trace.Assert(track.All(s => s.All(c => c is '.' or '#')));

long Descend(Slope s) {
  long trees = 0;
  for (int x = 0, y = 0; y < track.Length; x += s.DX, y += s.DY) {
    if (track[y][x % track[y].Length] == '#') ++trees;
  }
  return trees;
}

Slope[] slopes = new[] {
  new Slope(1, 1),
  new Slope(3, 1),
  new Slope(5, 1),
  new Slope(7, 1),
  new Slope(1, 2),
};

WriteLine(slopes.Select(Descend).Aggregate((a, b) => a * b));

record Slope(int DX, int DY);
