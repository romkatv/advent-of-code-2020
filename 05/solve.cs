using System.Diagnostics;
using System.IO;
using System.Linq;

using static System.Console;

string[] track = File.ReadAllLines("input");
Trace.Assert(track.Length > 0);
Trace.Assert(track[0].Length > 0);
Trace.Assert(track.All(s => s.Length == track[0].Length));
Trace.Assert(track.All(s => s.All(c => c is '.' or '#')));

long trees = 0;
for (int i = 0; i != track.Length; ++i) {
  if (track[i][i * 3 % track[i].Length] == '#') ++trees;
}
WriteLine(trees);
