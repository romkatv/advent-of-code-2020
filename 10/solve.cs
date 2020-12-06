using System.IO;
using System.Linq;
using static System.Console;
using static System.Convert;

int? prev = null;
foreach (int id in File.ReadLines("input").Select(ParseId).OrderBy(x => x)) {
  if (id == prev + 2) {
    WriteLine(id - 1);
    return 0;
  }
  prev = id;
}

return 1;

int ParseId(string s) => ToInt32(
    s.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1'),
    2);
