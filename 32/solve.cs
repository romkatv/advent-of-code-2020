using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Console;
using static System.IO.File;
using static System.Linq.Enumerable;

IEnumerator<string> lines = ReadLines("input").GetEnumerator();

List<Field> fields = new();
while (lines.MoveNext() && lines.Current != "") {
  fields.Add(Field.Parse(lines.Current));
}

Trace.Assert(lines.MoveNext() && lines.MoveNext());
int[] myTicket = ParseTicket(lines.Current);
Trace.Assert(lines.MoveNext() && lines.MoveNext());

List<int[]> tickets = new() { myTicket };
while (lines.MoveNext()) tickets.Add(ParseTicket(lines.Current));
tickets.RemoveAll(t => t.Any(v => !fields.Any(f => f.IsValid(v))));

List<HashSet<int>> choices = fields
    .Select(_ => Range(0, fields.Count).ToHashSet())
    .ToList();

foreach (int[] ticket in tickets) {
  for (int i = 0; i != choices.Count; ++i) {
    choices[i].RemoveWhere(choice => !fields[choice].IsValid(ticket[i]));
  }
}

Stack<HashSet<int>> work = new(choices.Where(c => c.Count == 1));
while (work.Any()) {
  HashSet<int> done = work.Pop();
  foreach (HashSet<int> other in choices) {
    if (ReferenceEquals(other, done)) continue;
    if (other.Remove(done.First()) && other.Count == 1) work.Push(other);
  }
}

Trace.Assert(choices.All(c => c.Count == 1));

long res = Range(0, fields.Count)
    .Where(i => fields[choices[i].First()].Name.StartsWith("departure"))
    .Select(i => myTicket[i])
    .Aggregate(1L, (x, y) => x * y);

WriteLine(res);

static int[] ParseTicket(string s) => s.Split(',').Select(int.Parse).ToArray();

record Field(string Name, int Lo1, int Hi1, int Lo2, int Hi2) {
  public bool IsValid(int x) => Lo1 <= x && x <= Hi1 || Lo2 <= x && x <= Hi2;

  public static Field Parse(string s) {
    string[] x = s.Split(':');
    string[] y = x[1].Split('-', ' ');
    return new Field(
        Name: x[0],
        Lo1: int.Parse(y[1]),
        Hi1: int.Parse(y[2]),
        Lo2: int.Parse(y[4]),
        Hi2: int.Parse(y[5]));
  }
}
