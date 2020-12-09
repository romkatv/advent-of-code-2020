using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;
using static System.IO.File;

WriteLine(ReadLines("input").Count((string s) => {
  Match m = Regex.Match(s, @"^(\d+)-(\d+) (.): (.*)$");
  string Capture(int i) => m.Groups[i].Value;
  Trace.Assert(m.Success);
  int n = Capture(4).Count(c => c == Capture(3)[0]);
  return n >= int.Parse(Capture(1)) && n <= int.Parse(Capture(2));
}));
