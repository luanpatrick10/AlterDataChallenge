using Alterdata.Domain.Entities;
using NUnit.Framework;
using System;
using Shared.Exceptions;

namespace UnitTests.Domain
{
    public class ProjectTest
    {
        [Test]
        public void Constructor_ShouldSetProperties_WhenValidArguments()
        {
            var project = new Project("Test Project", "Test Description");
            Assert.Multiple(() =>
            {
                Assert.That(project.Name, Is.EqualTo("Test Project"));
                Assert.That(project.Description, Is.EqualTo("Test Description"));
            });
        }

        [Test]
        public void Constructor_ShouldThrowException_WhenNameIsNullOrEmpty()
        {
            Assert.Throws<DomainException>(() => new Project(null, "desc"));
            Assert.Throws<DomainException>(() => new Project("", "desc"));
        }

        [Test]
        public void Constructor_ShouldThrowException_WhenDescriptionIsNullOrEmpty()
        {
            Assert.Throws<DomainException>(() => new Project("name", null));
            Assert.Throws<DomainException>(() => new Project("name", ""));
        }
    }
}