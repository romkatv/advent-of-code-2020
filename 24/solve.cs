using static System.Math;
using static System.Console;
using static System.IO.File;

int x = 0, y = 0, dx = 10, dy = 1;

foreach (string line in ReadLines("input")) {
  int arg = int.Parse(line.Substring(1));
  switch (line[0]) {
    case 'N':
      dy += arg;
      break;
    case 'S':
      dy -= arg;
      break;
    case 'E':
      dx += arg;
      break;
    case 'W':
      dx -= arg;
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
