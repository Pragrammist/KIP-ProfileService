using System.Data;
using System.Collections;


namespace ProfileService.Core;

public abstract class FavouriteBase : IList<string>, IEnumerable<string>
{
    protected readonly IList<ProfileAndFilm> _films = null!;
    protected readonly string _profileId = null!;
    protected FavouriteBase(string profileId)
    {
        _profileId = profileId;
        _films = new List<ProfileAndFilm>();
    }

    protected FavouriteBase(string profileId, IList<ProfileAndFilm> films)
    {
        _films = films;
    }
    public string this[int index] { get => _films[index].FilmID; set => Insert(index, value); }

    public int Count => _films.Count;

    public bool IsReadOnly => false;

    private ProfileAndFilm CreateFilmItem(string filmId) => new ProfileAndFilm(filmId, _profileId);

    private void AddFilmValidation(string filmId)
    {
        if (_films.FirstOrDefault(t => t.FilmID == filmId) != null)
            throw new FavouriteFilmAlreadyExistsException(filmId);
    }

    public void Add(string filmId)
    {
        AddFilmValidation(filmId);
        _films.Add(CreateFilmItem(filmId));
    }

    public void Clear() => _films.Clear();


    public bool Contains(string filmId) => _films.FirstOrDefault(t => t.FilmID == filmId) is not null;


    public void CopyTo(string[] array, int arrayIndex) => throw new NotImplementedException("FUCK YOU MICROSOFT");





    public IEnumerator<string> GetEnumerator() => _films.Select(t => t.FilmID).GetEnumerator();


    public int IndexOf(string filmId)
    {
        for (int i = 0; i < _films.Count; i++)
        {
            if (_films[i].FilmID == filmId)
                return i;
        }
        return -1;
    }

    public void Insert(int index, string filmId)
    {
        AddFilmValidation(filmId);
        if (index >= Count)
            throw new IndexOutOfRangeFavouriteException(index, Count);

        _films[index] = CreateFilmItem(filmId);
    }


    public bool Remove(string filmId) => _films.Remove(CreateFilmItem(filmId));


    public void RemoveAt(int index) => _films.RemoveAt(index);


    IEnumerator IEnumerable.GetEnumerator() => _films.Select(t => t.FilmID).GetEnumerator();
}
