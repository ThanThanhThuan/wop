﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WOP.Tasks;

namespace WOP.Objects {
  public enum TASKPOS
  {
    FIRST,
    MIDDLE,
    LAST
  }

  [Flags]
  public enum TASKWORKINGSTYLE
  {
    STRAIGHT = 1,
    COPYOUTPUT = 2,
    COPYINPUT = 4,
    ALL = 7
  }


  public class TaskEventArgs : EventArgs
  {
    public ITask Task;
    public IWorkItem WorkItem;

    public TaskEventArgs(ITask task, IWorkItem wi)
    {
      this.Task = task;
      this.WorkItem = wi;
    }
  }

  public interface ITask : INotifyPropertyChanged
  {
    Queue<IWorkItem> WorkItems { get; }
    ITask NextTask { get; set; }
    string Name { get; }
    UserControl UI { get; set; }
    bool IsEnabled { get; set; }
    bool CanChangePosition { get; }
    Job ParentJob { get; set; }
    TASKPOS Position { get; set; }
    TASKWORKINGSTYLE WorkingStyle { get; set; }
    TASKWORKINGSTYLE WorkingStyleConstraint { get;}
    Visibility UIVisibility { get; }
    event EventHandler<TaskEventArgs> WIProcessed;
    void Start();
    void Pause();
    ITask CloneNonDynamicStuff();
  }
}