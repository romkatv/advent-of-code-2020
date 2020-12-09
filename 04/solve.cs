using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;
using static System.IO.File;

WriteLine(ReadLines("input").Count((string s) => {
  Match m = Regex.Match(s, @"^(\d+)-(\d+) (.): (.*)$");
  string Capture(int i) => m.Groups[i].Value;
  bool Eq(int i) => Capture(4)[int.Parse(Capture(i)) - 1] == Capture(3)[0];
  Trace.Assert(m.Success);
  return Eq(1) != Eq(2);
}));
