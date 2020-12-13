using System.Diagnostics;
using System.Linq;
using static System.Console;
using static System.IO.File;

string[] lines = ReadAllLines("input");
Trace.Assert(lines.Length == 2);

int t0 = int.Parse(lines[0]);
(int t, int id) = lines[1]
    .Split(',')
    .Where(x => x != "x")
    .Select(int.Parse)
    .Select(x => (Wait(x), x))
    .Min();
WriteLine(t * id);

int Wait(int id) => (t0 + id - 1) / id * id - t0;
