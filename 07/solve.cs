using System.Collections.Generic;
using System.IO;
using System.Linq;

using static System.Console;

int res = -1;
HashSet<string> req = new();

foreach (string line in new[] {""}.Concat(File.ReadLines("input")).Append("")) {
  if (line == "") {
    if (req.Count == 0) ++res;
    req = new() { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
    continue;
  }
  foreach (string kv in line.Split(' ')) {
    req.Remove(kv.Substring(0, kv.IndexOf(':')));
  }
}

WriteLine(res);
