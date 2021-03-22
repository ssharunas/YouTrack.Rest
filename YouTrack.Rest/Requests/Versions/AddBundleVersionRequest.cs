using System;

namespace YouTrack.Rest.Requests.Versions
{
	class AddBundleVersionRequest : YouTrackRequest, IYouTrackPutRequest, IYouTrackRequest
	{
		public AddBundleVersionRequest(string bundleName, string version, string description = null, DateTime releaseDate = default(DateTime), bool released = false, bool archived = false)
			: base($"/rest/admin/customfield/versionBundle/{bundleName}/{version}")
		{
			ResourceBuilder.AddParameter("description", description);

			if (releaseDate != default(DateTime))
				ResourceBuilder.AddParameter("releaseDate", releaseDate.ToFileTimeUtc().ToString());

			if (released)
				ResourceBuilder.AddParameter("released", "true");

			if (archived)
				ResourceBuilder.AddParameter("archived", "true");
		}
	}
}
