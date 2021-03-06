﻿using System;
using System.Drawing;
using System.IO;
using FreeImageAPI;
using MbUnit.Framework;
using WOP.Objects;
using WOP.Tasks;

namespace WOP.Tests
{
  [TestFixture]
  public class IMTest
  {
    [Test]
    public void FITest()
    {
    }

    [Test]
    public void JobTest()
    {
      Job j = Job.CreateTestJob();
      j.Start();
    }

    [Test]
    public void TagTest()
    {
      foreach (string s in Directory.GetFiles(@"..\..\..\testdata\pix", "*jpg")) {
        //FreeImage fifi = new FreeImage(s);
      }
    }

    [Test]
    public void TestIMSpeed()
    {
      foreach (string s in Directory.GetFiles(@"..\..\..\testdata\pix", "*small*")) {
        File.Delete(s);
      }
      DateTime start = DateTime.Now;
      foreach (string s in Directory.GetFiles(@"..\..\..\testdata\pix", "test*jpg")) {
        var fin = new FileInfo(s);
        var fout = new FileInfo(Path.Combine(fin.DirectoryName, "small_" + fin.Name + fin.Extension));
        ImageWorker.ShrinkImageFI(fin, fout, new Size(400, 400));
      }
      TimeSpan ts = DateTime.Now.Subtract(start);
      string d = Directory.GetCurrentDirectory();

      Assert.AreEqual(0, 1, "duration: " + ts.Seconds);
    }

    [Test]
    public void TestMetadata()
    {
      foreach (string s in Directory.GetFiles(@"..\..\..\testdata\pixrotate", "*")) {
        //FIBITMAP? dib = ImageWorker.GetJPGImageHandle(new FileInfo(s));
        ImageWorker.AutoRotateImageFI(new FileInfo(s), "blabla");
        //ImageWorker.CleanUpResources(dib);
      }
    }

    [Test]
    public void TestFormatting()
    {
      string s = string.Format("{0:000}", 1);
      s.ToString();
    }
  }
}