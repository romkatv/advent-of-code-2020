using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
using static System.IO.File;

long mask0 = 0, mask1 = 0;
Dictionary<long, long> mem = new();

foreach (string[] s in ReadLines("input").Select(x => x.Split(' ', '[', ']'))) {
  switch (s[0]) {
    case "mask":
      mask0 = 0;
      mask1 = 0;
      foreach (char c in s[2]) {
        mask0 <<= 1;
        mask1 <<= 1;
        switch (c) {
          case '0': mask0 |= 1; break;
          case '1': mask1 |= 1; break;
        }
      }
      break;
    case "mem":
      long addr = long.Parse(s[1]);
      long val = long.Parse(s[4]) & ~mask0 | mask1;
      if (val == 0) {
        mem.Remove(addr);
      } else {
        mem[addr] = val;
      }
      break;
  }
}

WriteLine(mem.Values.Sum());
