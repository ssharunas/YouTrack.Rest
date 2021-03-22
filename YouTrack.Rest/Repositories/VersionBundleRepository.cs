using System;
using YouTrack.Rest.Requests.Versions;

namespace YouTrack.Rest.Repositories
{
	class VersionBundleRepository : IVersionBundleRepository
	{
		private readonly IConnection connection;

		public VersionBundleRepository(IConnection connection)
		{
			this.connection = connection;
		}

		public void AddBundleVersion(string bundleName, string version, string description = null, DateTime releaseDate = default(DateTime), bool released = false, bool archived = false)
		{
			connection.Put(new AddBundleVersionRequest(bundleName, version, description, releaseDate, released, archived));
		}
	}
}
