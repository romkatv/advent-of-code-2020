#!/usr/bin/env zsh

emulate -L zsh -o err_exit

typeset -ri N=30000000

typeset -a init
typeset -A nums
typeset -i i=1 x zero last

IFS=, read -A init <input
for x in $init; do
  if (( x )); then
    last=nums[$x]
    nums[$x]=$((i++))
  else
    last=zero
    zero='i++'
  fi
done

for ((; i <= N; ++i)); do
  if (( last )); then
    x='i-last-1'
    last=nums[$x]
    nums[$x]=$i
  else
    last=zero
    zero=i
  fi
done

echo $x
