using System;

namespace ConsoleGameTest_00
{
  public static class IDCounter
  {
    private static int LastID = 0;
    public static int ClaimID() => LastID++;
  }
}