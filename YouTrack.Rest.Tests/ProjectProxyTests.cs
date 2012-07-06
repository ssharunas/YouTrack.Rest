﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSubstitute;
using NUnit.Framework;
using YouTrack.Rest.Deserialization;
using YouTrack.Rest.Factories;
using YouTrack.Rest.Requests;
using YouTrack.Rest.Tests.Repositories;

namespace YouTrack.Rest.Tests
{
    class ProjectProxyTests : TestFor<ProjectProxy>
    {
        private IConnection connection;
        private List<Rest.Deserialization.Issue> issues;
        private const string ProjectId = "FOOBAR";

        protected override ProjectProxy CreateSut()
        {
            connection = Mock<IConnection>();
            return new ProjectProxy(ProjectId, connection, Mock<IIssueRequestFactory>());
        }

        protected override void SetupDependencies()
        {
            issues = new List<Rest.Deserialization.Issue>();
            issues.Add(new DeserializedIssueMock(Mock<IIssue>()));

            connection.Get<List<Rest.Deserialization.Issue>>(Arg.Any<GetIssuesInAProjectRequest>()).Returns(issues);
        }

        [Test]
        public void IdIsAssigned()
        {
            Assert.That(Sut.Id, Is.EqualTo(ProjectId));
        }

        [Test]
        public void ConnectionIsCalledOnGetIssues()
        {
            Sut.GetIssues();

            connection.Received().Get<List<Rest.Deserialization.Issue>>(Arg.Any<GetIssuesInAProjectRequest>());
        }

        [Test]
        public void ConnectionIsCalledOnGetIssuesWithFilter()
        {
            Sut.GetIssues("filter");

            connection.Received().Get<List<Rest.Deserialization.Issue>>(Arg.Any<GetIssuesInAProjectRequest>());
        }
    }
}
