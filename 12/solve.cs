using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

int res = 0;
List<char> ans = null;

foreach (string line in File.ReadLines("input").Append("")) {
  if (line == "") {
    res += ans.Count;
    ans = null;
  } else if (ans == null) {
    ans = line.ToList();
  } else {
    HashSet<char> chars = line.ToHashSet();
    ans.RemoveAll(c => !chars.Contains(c));
  }
}

WriteLine(res);
