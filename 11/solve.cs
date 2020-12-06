using System.Collections.Generic;
using System.IO;
using System.Linq;

using static System.Console;

int res = 0;
HashSet<char> ans = new();

foreach (string line in File.ReadLines("input").Append("")) {
  if (line == "") {
    res += ans.Count;
    ans.Clear();
  } else {
    foreach (char c in line) ans.Add(c);
  }
}

WriteLine(res);
