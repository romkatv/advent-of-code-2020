using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

List<Instruction> prog = new();

foreach (string[] inst in File.ReadLines("input").Select(s => s.Split(' '))) {
  int arg = int.Parse(inst[1]);
  prog.Add(inst[0] switch {
    "nop" => new Instruction(0,   1),
    "acc" => new Instruction(arg, 1),
    "jmp" => new Instruction(0,   arg),
    _ => throw new Exception("ILL")
  });
}

int pc = 0;
long acc = 0;
List<bool> seen = prog.Select(_ => false).ToList();
while (!seen[pc]) {
  seen[pc] = true;
  acc += prog[pc].Acc;
  pc += prog[pc].Pc;
}
WriteLine(acc);

record Instruction(int Acc, int Pc);
