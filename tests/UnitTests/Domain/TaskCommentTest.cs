using System;
using Alterdata.Domain.Entities;
using NUnit.Framework;

namespace UnitTests.Domain;

[TestFixture]
public class TaskCommentTest
{
    [Test]
    public void Constructor_ShouldInitializeProperties()
    {
        var comment = new TaskComment("Test comment");
        Assert.AreEqual("Test comment", comment.Text);
        Assert.LessOrEqual(comment.CreateAt, DateTime.Now);
        Assert.IsNull(comment.UpdateAt);
    }

    [Test]
    public void Validate_ShouldNotThrowException_WhenValid()
    {
        var comment = new TaskComment("Valid comment");
        Assert.DoesNotThrow(() => comment.Validate());
    }
}
