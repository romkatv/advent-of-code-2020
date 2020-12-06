using System.IO;
using System.Linq;
using static System.Console;
using static System.Convert;

WriteLine(File.ReadLines("input").Select(ParseId).Max());

int ParseId(string s) => ToInt32(
    s.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1'),
    2);
