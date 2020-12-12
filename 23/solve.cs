using static System.Math;
using static System.Console;
using static System.IO.File;

int x = 0, y = 0, dx = 1, dy = 0;

foreach (string line in ReadLines("input")) {
  int arg = int.Parse(line.Substring(1));
  switch (line[0]) {
    case 'N':
      y += arg;
      break;
    case 'S':
      y -= arg;
      break;
    case 'E':
      x += arg;
      break;
    case 'W':
      x -= arg;
      break;
    case 'L':
      for (; arg != 0; arg -= 90) (dx, dy) = (-dy, dx);
      break;
    case 'R':
      for (; arg != 0; arg -= 90) (dx, dy) = (dy, -dx);
      break;
    case 'F':
      x += arg * dx;
      y += arg * dy;
      break;
  }
}

WriteLine(Abs(x) + Abs(y));
