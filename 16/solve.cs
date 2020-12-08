using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

List<Instruction> prog = new();

foreach (string[] inst in File.ReadLines("input").Select(s => s.Split(' '))) {
  int arg = int.Parse(inst[1]);
  prog.Add(inst[0] switch {
    "nop" => new Nop() { Arg = arg },
    "acc" => new Acc() { Arg = arg },
    "jmp" => new Jmp() { Arg = arg },
    _ => throw new Exception("ILL")
  });
}

for (int i = 0; i != prog.Count; ++i) {
  Instruction orig = prog[i];
  switch (orig) {
    case Acc:
      continue;
    case Nop:
      prog[i] = new Jmp() { Arg = orig.Arg };
      break;
    case Jmp:
      prog[i] = new Nop() { Arg = orig.Arg };
      break;
  }

  int pc = 0;
  long acc = 0;
  List<bool> seen = prog.Select(_ => false).ToList();
  while (!seen[pc]) {
    seen[pc] = true;
    prog[pc].Run(ref pc, ref acc);
    if (pc == prog.Count) {
      WriteLine(acc);
      return;
    }
  }

  prog[i] = orig;
}

abstract record Instruction {
  public int Arg { init; get; }
  public abstract void Run(ref int pc, ref long acc);
};

record Nop : Instruction {
  public override void Run(ref int pc, ref long acc) {
    pc += 1;
  }
}

record Acc : Instruction {
  public override void Run(ref int pc, ref long acc) {
    acc += Arg;
    pc += 1;
  }
}

record Jmp : Instruction {
  public override void Run(ref int pc, ref long acc) {
    pc += Arg;
  }
}
