using System;

namespace YouTrack.Rest.Repositories
{
	public interface IVersionBundleRepository
	{
		void AddBundleVersion(string bundleName, string version, string description = null, DateTime releaseDate = default(DateTime), bool released = false, bool archived = false);
	}
}
