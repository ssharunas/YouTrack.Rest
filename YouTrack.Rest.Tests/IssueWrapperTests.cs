﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using YouTrack.Rest.Deserialization;

namespace YouTrack.Rest.Tests
{
    class IssueWrapperTests : TestFor<Deserialization.Issue>
    {
        private IConnection connection;
        private List<Comment> comments;
        private const string IssueId = "FOO-BAR";
        private const string ProjectShortName = "FOO";
        private const string Summary = "Summary";
        private const string Type = "Bug";
        private const int CommentsCount = 1;
        private const int NumberInProject = 3;
        private const string Description = "desc";
        private const string Priority = "urgent";
        private const string ReporterName = "reporter";
        private const string UpdaterName = "updater";
        private const string Subsystem = "sub";
        private const string State = "foobar";
        private const int VotesCount = 2;
        private const int UpdatedMillis = 2123;
        private const int CreatedMillis = 1234;

        protected override void SetupDependencies()
        {
            comments = CreateComments();

            Sut.Id = IssueId;
            Sut.Fields = CreateFields();
            Sut.Comments = comments;
            connection = Mock<IConnection>();
        }

        private static List<Comment> CreateComments()
        {
            return new List<Comment>() { new Comment()};
        }

        private List<Field> CreateFields()
        {
            List<Field> fields = new List<Field>();

            fields.Add(CreateField("projectShortName", ProjectShortName));
            fields.Add(CreateField("summary", Summary));
            fields.Add(CreateField("Type", Type));
            fields.Add(CreateField("commentsCount", CommentsCount.ToString()));
            fields.Add(CreateField("created", CreatedMillis.ToString()));
            fields.Add(CreateField("description", Description));
            fields.Add(CreateField("numberInProject", NumberInProject.ToString()));
            fields.Add(CreateField("priority", Priority));
            fields.Add(CreateField("reporterName", ReporterName));
            fields.Add(CreateField("updaterName", UpdaterName));
            fields.Add(CreateField("state", State));
            fields.Add(CreateField("subsystem", Subsystem));
            fields.Add(CreateField("updated", UpdatedMillis.ToString()));
            fields.Add(CreateField("votes", VotesCount.ToString()));

            return fields;
        }

        private static Field CreateField(string name, string value)
        {
            Value_ value_ = new Value_() {Value = value};

            Field field = new Field() {Name = name, Values = new List<Value_> { value_ }};

            return field;
        }

        [Test]
        public void IdIsWrapped()
        {
            Assert.That(Sut.GetIssue(connection).Id, Is.EqualTo(IssueId));
        }

        [Test]
        public void ProjectShortNameIsWrapped()
        {
            Assert.That(Sut.GetIssue(connection).ProjectShortName, Is.EqualTo(ProjectShortName));
        }

        [Test]
        public void SummaryIsWrapped()
        {
            Assert.That(Sut.GetIssue(connection).Summary, Is.EqualTo(Summary));
        }

        [Test]
        public void TypeIsWrapped()
        {
            Assert.That(Sut.GetIssue(connection).Type, Is.EqualTo(Type));
        }

        [Test]
        public void CommentsCountIsWrapped()
        {
            Assert.That(Sut.GetIssue(connection).CommentsCount, Is.EqualTo(CommentsCount));
        }

        [Test]
        public void CreatedIsWrapped()
        {
            Assert.That(GetYouTrackMilliseconds(Sut.GetIssue(connection).Created), Is.EqualTo(CreatedMillis));
        }

        [Test]
        public void DescriptionIsWrapped()
        {
            Assert.That(Sut.GetIssue(connection).Description, Is.EqualTo(Description));
        }

        [Test]
        public void NumberInProjectIsWrapped()
        {
            Assert.That(Sut.GetIssue(connection).NumberInProject, Is.EqualTo(NumberInProject));
        }

        [Test]
        public void ReporterNameIsWrapped()
        {
            Assert.That(Sut.GetIssue(connection).ReporterName, Is.EqualTo(ReporterName));
        }

        [Test]
        public void StateIsWrapped()
        {
            Assert.That(Sut.GetIssue(connection).State, Is.EqualTo(State));
        }

        [Test]
        public void SubsystemIsWrapped()
        {
            Assert.That(Sut.GetIssue(connection).Subsystem, Is.EqualTo(Subsystem));
        }

        [Test]
        public void UpdatedIsWrapped()
        {
            Assert.That(GetYouTrackMilliseconds(Sut.GetIssue(connection).Updated), Is.EqualTo(UpdatedMillis));
        }

        private double GetYouTrackMilliseconds(DateTime value)
        {
            return value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        [Test]
        public void UpdaterNameIsWrapped()
        {
            Assert.That(Sut.GetIssue(connection).UpdaterName, Is.EqualTo(UpdaterName));
        }

        [Test]
        public void VotesCountIsWrapped()
        {
            Assert.That(Sut.GetIssue(connection).VotesCount, Is.EqualTo(VotesCount));
        }

        [Test]
        public void CommentsAreSet()
        {
            Assert.That(Sut.GetIssue(connection).Comments, Is.EquivalentTo(comments));
        }

    }
}
