using System;
using Alterdata.Domain.Entities;
using Alterdata.Domain.Enums;
using Shared.Exceptions;
using NUnit.Framework;
using Task = Alterdata.Domain.Entities.Task;
using TaskStatus = Alterdata.Domain.Enums.TaskStatus;

namespace UnitTests.Domain;

[TestFixture]
public class TaskTest
{
    [Test]
    public void Constructor_ShouldInitializeProperties()
    {
        var task = new Task("Title", "Description", DateTime.Now.AddDays(1), Guid.NewGuid());
        Assert.Multiple(() =>
        {
            Assert.That(task.Title, Is.EqualTo("Title"));
            Assert.That(task.Description, Is.EqualTo("Description"));
            Assert.That(task.Status, Is.EqualTo(TaskStatus.Pendente));
        });
    }

    [Test]
    public void Validate_ShouldThrowException_WhenTitleIsEmpty()
    {
        Assert.Throws<DomainException>(() => new Task("", "Description", DateTime.Now.AddDays(1), Guid.NewGuid()));
    }

    [Test]
    public void Validate_ShouldNotThrowException_WhenTitleIsValid()
    {
        Assert.DoesNotThrow(() => new Task("Valid Title", "Description", DateTime.Now.AddDays(1), Guid.NewGuid()));
    }

    [Test]
    public void AddTaskComment_ShouldAddComment_WhenValid()
    {
        var task = new Task("Title", "Description", DateTime.Now.AddDays(1), Guid.NewGuid());
        var comment = new TaskComment("Comment", Guid.NewGuid());
        task.AddTaskComment(comment);
        CollectionAssert.Contains(task.TasksComment, comment);
    }

    [Test]
    public void AddTaskComment_ShouldThrowException_WhenDueDateIsPast()
    {
        var task = new Task("Title", "Description", DateTime.Now.AddDays(-1), Guid.NewGuid());
        var comment = new TaskComment("Comment", Guid.NewGuid());
        Assert.Throws<DomainException>(() => task.AddTaskComment(comment));
    }

    [Test]
    public void AddTaskComment_ShouldWork_WhenDueDateIsValid()
    {
        var task = new Task("Title", "Description", DateTime.Now.AddDays(1), Guid.NewGuid());
        var comment = new TaskComment("Comment", Guid.NewGuid());
        Assert.DoesNotThrow(() => task.AddTaskComment(comment));
        CollectionAssert.Contains(task.TasksComment, comment);
    }
    
}