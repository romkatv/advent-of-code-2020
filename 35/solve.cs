using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Console;
using static System.IO.File;
using static System.Linq.Enumerable;

long res = ReadLines("input")
    .Select(expr => Expr(Tokens(expr).AsEnumerable().GetEnumerator()))
    .Sum();
WriteLine(res);

string[] Tokens(string expr) => expr.Replace("(", "( ").Replace(")", " )").Split(' ');

long Term(IEnumerator<string> tokens) {
  Trace.Assert(tokens.MoveNext());
  return tokens.Current == "(" ? Expr(tokens) : long.Parse(tokens.Current);
}

long Expr(IEnumerator<string> tokens) {
  long res = Term(tokens);
  while (tokens.MoveNext()) {
    switch (tokens.Current) {
      case "+": res += Term(tokens); break;
      case "*": res *= Term(tokens); break;
      case ")": return res;
    }
  }
  return res;
}
