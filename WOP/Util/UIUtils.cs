﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WOP.Util
{
  public class UIUtils
  {
    /// Return Type: BOOL->int  
    ///X: int  
    ///Y: int  
    [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "SetCursorPos")]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
    public static extern bool SetCursorPos(int X, int Y);
  }
}
