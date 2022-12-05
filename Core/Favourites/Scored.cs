namespace ProfileService.Core;

public class Scored : FavouriteBase, IList<string>, IEnumerable<string>
{
    public Scored(string profileId) : base(profileId)
    {
    }
    public Scored(string profileId, IList<ProfileAndFilm> films) : base(profileId, films)
    {
    }
}
