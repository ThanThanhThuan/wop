﻿using WOP.Objects;
using WOP.TasksUI;

namespace WOP.Tasks
{
  public class ImageRotateTask : SkeletonTask
  {
    public ImageRotateTask()
    {
      this.Name = "Drehen";
      this.UI = new ImageRotateTaskUI();
      this.UI.DataContext = this;
    }

    public override bool Process(ImageWI iwi)
    {
      ImageWorker.AutoRotateImageFI(iwi.CurrentFile, string.Empty);
      return true;
    }
  }
}