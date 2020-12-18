using System.Linq;
using static System.Console;
using static System.IO.File;
using static System.Linq.Enumerable;

long res = ReadLines("input")
    .Select(expr => Eval(expr.Replace("(", "( ").Replace(")", " )").Split(' ')))
    .Sum();
WriteLine(res);

long Eval(string[] tokens) {
  int i = 0;
  return Expr();

  long Expr() {
    long res = Term();
    while (i != tokens.Length) {
      if (tokens[i++] == "*") {
        res *= Term();
      } else {
        break;
      }
    }
    return res;
  }

  long Factor() => tokens[i++] == "(" ? Expr() : long.Parse(tokens[i-1]);

  long Term() {
    long res = Factor();
    while (i != tokens.Length && tokens[i] == "+") {
      ++i;
      res += Factor();
    }
    return res;
  }
}
