using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Console;
using static System.IO.File;
using static System.Linq.Enumerable;

using Rule = System.Func<string, System.Collections.Generic.IEnumerable<string>>;

Dictionary<int, Rule> rules = new();
IEnumerator<string> lines = ReadLines("input").GetEnumerator();

while (lines.MoveNext() && lines.Current != "") {
  string[] kv = lines.Current.Split(':', StringSplitOptions.TrimEntries);
  Trace.Assert(kv.Length == 2);
  kv[1] = kv[0] switch {
    "8"  => "42 | 42 8",
    "11" => "42 31 | 42 11 31",
    _    => kv[1],
  };
  rules[int.Parse(kv[0])] = kv[1][0] == '"' ? Literal(kv[1]) : Expr(kv[1]);
}

int res = 0;

while (lines.MoveNext()) {
  if (rules[0].Invoke(lines.Current).Contains("")) ++res;
}

WriteLine(res);

Rule Literal(string rule) {
  Trace.Assert(rule.First() == '"' && rule.Last() == '"' && rule.Length > 2);
  string lit = rule.Substring(1, rule.Length - 2);
  return (string msg) =>
      msg.StartsWith(lit) ? new[] { msg.Substring(lit.Length) } : new string[0];
}

Rule Term(string rule) {
  int[] factors = rule.Split(' ').Select(int.Parse).ToArray();
  Trace.Assert(factors.Length > 0);
  IEnumerable<string> Match(string msg) {
    Stack<(int, string)> stack = new();
    stack.Push((0, msg));
    while (stack.Count > 0) {
      (int i, string s) = stack.Pop();
      foreach (string r in rules[factors[i]].Invoke(s)) {
        if (i == factors.Length - 1) {
          yield return r;
        } else {
          stack.Push((i + 1, r));
        }
      }
    }
  }
  return Match;
}

Rule Expr(string rule) {
  Rule[] terms = rule
      .Split('|', StringSplitOptions.TrimEntries)
      .Select(Term)
      .ToArray();
  Trace.Assert(terms.Length > 0);
  return (string msg) => terms.SelectMany(t => t.Invoke(msg));
}
