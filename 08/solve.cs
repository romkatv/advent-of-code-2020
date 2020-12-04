using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;
using static System.String;

int res = 0;
HashSet<string> req = new();

foreach (string line in File.ReadLines("input").Append("")) {
  if (line == "") {
    if (req.Count == 7) ++res;
    req.Clear();
    continue;
  }
  foreach (string[] kv in line.Split(' ').Select(x => x.Split(':'))) {
    Trace.Assert(kv.Length == 2);
    bool Re(string re) => Regex.IsMatch(kv[1], $"^({re})$");
    bool Retween(string re, string lo, string hi) =>
        Re(re) && Compare(lo, kv[1]) <= 0 && Compare(kv[1], hi) <= 0;
    bool valid = kv[0] switch {
      "byr" => Retween(@"\d{4}", "1920", "2002"),
      "iyr" => Retween(@"\d{4}", "2010", "2020"),
      "eyr" => Retween(@"\d{4}", "2020", "2030"),
      "hgt" => Retween(@"\d{3}cm", "150cm", "193cm") || Retween(@"\d{2}in", "59in", "76in"),
      "hcl" => Re(@"#[0-9a-f]{6}"),
      "ecl" => Re(@"amb|blu|brn|gry|grn|hzl|oth"),
      "pid" => Re(@"\d{9}"),
      _ => false,
    };
    if (valid) req.Add(kv[0]);
  }
}

WriteLine(res);
