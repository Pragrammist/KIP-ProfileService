namespace ProfileService.Core;

public sealed class WillWatch : FavouriteBase, IList<string>, IEnumerable<string>
{
    public WillWatch(string profileId) : base(profileId)
    {
    }
    public WillWatch(string profileId, IList<ProfileAndFilm> films) : base(profileId, films)
    {
    }
}
