using System.Collections.Generic;
using System.Linq;
using static System.Console;
using static System.IO.File;

long mask1 = 0;
List<int> fpos = new();
Dictionary<long, long> mem = new();

foreach (string[] s in ReadLines("input").Select(x => x.Split(' ', '[', ']'))) {
  switch (s[0]) {
    case "mask":
      mask1 = 0;
      fpos.Clear();
      string mask = s[2];
      for (int i = 0; i != mask.Length; ++i) {
        mask1 <<= 1;
        switch (mask[i]) {
          case '1': mask1 |= 1; break;
          case 'X': fpos.Add(mask.Length - i - 1); break;
        }
      }
      fpos.Reverse();
      break;
    case "mem":
      long val = long.Parse(s[4]);
      long tmpl = long.Parse(s[1]) | mask1;
      for (long packed = 0; packed != 1L << fpos.Count; ++packed) {
        long addr = tmpl;
        for (int i = 0; i != fpos.Count; ++i) {
          if ((packed & (1L << i)) == 0) {
            addr &= ~(1L << fpos[i]);
          } else {
            addr |= 1L << fpos[i];
          }
        }
        if (val == 0) {
          mem.Remove(addr);
        } else {
          mem[addr] = val;
        }
      }
      break;
  }
}

WriteLine(mem.Values.Sum());
