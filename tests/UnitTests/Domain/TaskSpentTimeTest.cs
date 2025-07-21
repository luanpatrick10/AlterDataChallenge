using System;
using Alterdata.Domain.Entities;
using NUnit.Framework;

namespace UnitTests.Domain;

[TestFixture]
public class TaskSpentTimeTest
{
    [Test]
    public void Constructor_ShouldInitializeProperties()
    {
        var started = DateTime.Now;
        var finished = started.AddHours(1);
        var spentTime = new TaskSpentTime(started, finished);
        Assert.AreEqual(started, spentTime.StartedAt);
        Assert.AreEqual(finished, spentTime.FinishedAt);
    }

    [Test]
    public void ValidateSpentTime_ShouldThrowException_WhenFinishedBeforeStarted()
    {
        var started = DateTime.Now;
        var finished = started.AddMinutes(-10);
        var spentTime = new TaskSpentTime(started, started.AddMinutes(1));
        spentTime.SetSpentTime(started, finished, TimeSpan.FromMinutes(10));
        Assert.Throws<ArgumentException>(() => spentTime.ValidateSpentTime());
    }

    [Test]
    public void SetSpentTime_ShouldUpdateProperties_WhenValid()
    {
        var started = DateTime.Now;
        var finished = started.AddHours(2);
        var spentTime = new TaskSpentTime(started, finished);
        var newStarted = started.AddMinutes(10);
        var newFinished = newStarted.AddHours(1);
        spentTime.SetSpentTime(newStarted, newFinished, TimeSpan.FromHours(1));
        Assert.AreEqual(newStarted, spentTime.StartedAt);
        Assert.AreEqual(newFinished, spentTime.FinishedAt);
    }
}
