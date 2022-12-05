namespace ProfileService.Core;

public class Watched : FavouriteBase, IList<string>, IEnumerable<string>
{
    public Watched(string profileId) : base(profileId)
    {
    }
    public Watched(string profileId, IList<ProfileAndFilm> films) : base(profileId, films)
    {
    }
}
