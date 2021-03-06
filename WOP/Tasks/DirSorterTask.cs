﻿using System;
using System.IO;
using System.Windows.Controls;
using WOP.Objects;
using WOP.TasksUI;

namespace WOP.Tasks {
  public class DirSorterTask : SkeletonTask {
    private string currentDir;
    private string currentDirComplete;
    private int dirCount;
    private int pixInDir;
    private UserControl ui;

    public DirSorterTask()
    {
      this.Name = "In Ordner verteilen";
    }

    public override UserControl UI
    {
      get
      {
        this.ui = new DirSorterTaskUI();
        this.ui.DataContext = this;
        return this.ui;
      }
      set { this.ui = value; }
    }

    public string DirectoryPattern { get; set; }

    public int DirectoryFillCount { get; set; }

    public override ITask CloneNonDynamicStuff()
    {
      DirSorterTask t = new DirSorterTask();
      t.IsEnabled = this.IsEnabled;
      t.DirectoryPattern = this.DirectoryPattern;
      t.DirectoryFillCount = this.DirectoryFillCount;
      return t;
    }

    public override TASKWORKINGSTYLE WorkingStyleConstraint
    {
      get
      {
        return TASKWORKINGSTYLE.ALL;
      }
    }

    public override bool Process(ImageWI iwi)
    {
      bool success = false;
      if (iwi != null) {
        try {
          if (string.IsNullOrEmpty(this.currentDir) || this.pixInDir >= this.DirectoryFillCount) {
            // create new directory
            this.currentDir = this.createNewDirName();
            if (iwi.CurrentFile != null) {
              if (iwi.CurrentFile.DirectoryName != null) {
                this.currentDirComplete = Path.Combine(iwi.CurrentFile.DirectoryName, this.currentDir);
              }
              if (!Directory.Exists(currentDirComplete)) {
                Directory.CreateDirectory(this.currentDirComplete);
              }
              // TODO: what about counting files in the dir? and use that count?
              pixInDir = 0;
            }
          }
          if (!string.IsNullOrEmpty(this.currentDir)) {
            // move file
            if (iwi.CurrentFile != null) {
              string nuLocation = Path.Combine(this.currentDirComplete, iwi.CurrentFile.Name);
              if (File.Exists(nuLocation)) {
                File.Delete(nuLocation);
              }
              // handle mode
              switch (WorkingStyle) {
                case TASKWORKINGSTYLE.STRAIGHT:
                  File.Move(iwi.CurrentFile.FullName, nuLocation);
                  break;
                case TASKWORKINGSTYLE.COPYOUTPUT:
                  File.Copy(iwi.CurrentFile.FullName, nuLocation);
                  break;
                case TASKWORKINGSTYLE.COPYINPUT:
                  File.Copy(iwi.CurrentFile.FullName, nuLocation);
                  // the handovered file is the file on the new location
                  iwi.CleanUp();
                  iwi.CurrentFile = new FileInfo(nuLocation);
                  break;
                default:
                  throw new ArgumentOutOfRangeException();
              }
              pixInDir++;
            }
          }
          success = true;
        } catch (Exception ex) {
          logger.Error("{0} got exception while processing {1}: {2}", this.Name, iwi.Name, ex);
        }
      }
      return success;
    }

    private string createNewDirName()
    {
      return string.Format(this.DirectoryPattern, this.dirCount++);
    }
  }
}