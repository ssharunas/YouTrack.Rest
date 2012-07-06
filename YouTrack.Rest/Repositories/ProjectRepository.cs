using YouTrack.Rest.Exceptions;
using YouTrack.Rest.Factories;
using YouTrack.Rest.Requests.Projects;

namespace YouTrack.Rest.Repositories
{
    class ProjectRepository : IProjectRepository
    {
        private readonly IConnection connection;
        private readonly IProjectFactory projectFactory;

        //TODO: Get rid of this dependency
        private readonly IIssueRequestFactory issueRequestFactory;

        public ProjectRepository(IConnection connection, IProjectFactory projectFactory, IIssueRequestFactory issueRequestFactory)
        {
            this.connection = connection;
            this.projectFactory = projectFactory;
            this.issueRequestFactory = issueRequestFactory;
        }

        public IProjectProxy GetProjectProxy(string projectid)
        {
            return new ProjectProxy(projectid, connection, issueRequestFactory);
        }

        public IProject CreateProject(string projectId, string projectName, string projectLeadLogin, int startingNumber = 1, string description = null)
        {
            connection.Put(new CreateNewProjectRequest(projectId, projectName, projectLeadLogin, startingNumber, description));

            return projectFactory.CreateProject(projectId, connection);
        }

        public bool ProjectExists(string projectId)
        {
            //Relies on the "not found" exception if project doesn't exist. Could use some improving.

            try
            {
                connection.Get(new GetProjectRequest(projectId));

                return true;
            }
            catch (RequestNotFoundException)
            {
                return false;
            }
        }

        public void DeleteProject(string projectid)
        {
            connection.Delete(new DeleteProjectRequest(projectid));
        }
    }
}