using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Console;
using static System.IO.File;

IEnumerator<string> lines = ReadLines("input").GetEnumerator();

List<Field> fields = new();
while (lines.MoveNext() && lines.Current != "") {
  fields.Add(Field.Parse(lines.Current));
}

for (int i = 0; i != 4; ++i) Trace.Assert(lines.MoveNext());

long err = 0;

while (lines.MoveNext()) {
  foreach (int x in lines.Current.Split(',').Select(int.Parse)) {
    if (!fields.Any(f => f.IsValid(x))) err += x;
  }
}

WriteLine(err);

record Field(int Lo1, int Hi1, int Lo2, int Hi2) {
  public bool IsValid(int x) => Lo1 <= x && x <= Hi1 || Lo2 <= x && x <= Hi2;

  public static Field Parse(string s) {
    string[] x = s.Substring(s.IndexOf(':') + 2).Split('-', ' ');
    return new Field(int.Parse(x[0]), int.Parse(x[1]), int.Parse(x[3]), int.Parse(x[4]));
  }
}
