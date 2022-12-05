using System.Data;
using System.Collections;


namespace ProfileService.Core;

//TODO: Exceptions if will needed

public class User
{
    public string Id { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password  {get; set; } = null!;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}
public class Profile
{    
    IList<ProfileAndFilm>? _willWatchTables;
    IList<ProfileAndFilm>? _scoredTables;
    IList<ProfileAndFilm>? _watchedTables;

    WillWatch? _willWatch;

    Scored? _scored;

    Watched? _watched;

    public string Id {get; set;} = null!;

    public User User {get; set;} = null!;

    public bool HasChilds => Childs is not null && Childs.Count > 0;
    
    public bool HasScored => _scored is not null && _scored.Count > 0;

    public bool HasWatched => _watched is not null && _watched.Count > 0;

    public IList<ChildProfile>? Childs {get; private set;}

    public WillWatch? WillWatch { get {
        if(_willWatch != null)
            return _willWatch;
            
        else if(_willWatchTables != null && _willWatch == null){
            _willWatch = new WillWatch(Id, _willWatchTables);
            return _willWatch;
        }
        else
            return null; 
    } }
    
    public Scored? Scored { get{
        if(_scored != null)
            return _scored;
            
        else if(_scoredTables != null && _scored == null){
            _scored = new Scored(Id, _scoredTables);
            return _scored;
        }
        else
            return null;
    } }

    public Watched? Watched { get{
        if(_watched != null)
            return _watched;
            
        else if(_watchedTables != null && _watched == null){
            _watched = new Watched(Id, _watchedTables);
            return _watched;
        }
        else
            return null;
    } }
}

public enum Gender {
    MALE = default,
    FEMALE = 1
}

public class ChildProfile
{
    int _age;
    public string Id {get; set;} = null!;
    public int Age {get => _age; set{
        switch(value){
            case < 6:
                _age = 0;
                break;
            case < 12:
                _age = 6;
                break;
            case < 16:
                _age = 12;
                break;
            case >= 16:
                _age = 16;
                break;
        }
    }} 
    public Gender Gender {get; set;}
}


public record class ProfileAndFilm
{
    public ProfileAndFilm(string filmId, string profileId){
        FilmID = filmId;
        ProfileId = profileId;
    }

    public string FilmID {get; init;} = null!;

    public string ProfileId {get; init;} = null!;

}


public abstract class FavouriteBase : IList<string>, IEnumerable<string>
{
    protected readonly IList<ProfileAndFilm> _films = null!;
    protected readonly string _profileId  = null!;
    protected FavouriteBase(string profileId){
        _profileId = profileId;
        _films = new List<ProfileAndFilm>();
    }

    protected FavouriteBase(string profileId, IList<ProfileAndFilm> films){
        _films = films;
    }
    public string this[int index] { get => _films[index].FilmID; set => throw new NotSupportedException($"TODO:{typeof(WillWatch)}"); }

    public int Count => _films.Count;

    public bool IsReadOnly => false;

    private ProfileAndFilm CreateFilmItem(string filmId) => new ProfileAndFilm(_profileId, filmId);

    private void AddFilmValidation(string filmId)
    {
        if(_films.FirstOrDefault(t => t.FilmID == filmId) != null)
            throw new InvalidOperationException($"TODO:{typeof(WillWatch)}");
    }

    public void Add(string filmId)
    {
        AddFilmValidation(filmId);
        _films.Add(CreateFilmItem(filmId));
    }

    public void Clear() => _films.Clear();
    

    public bool Contains(string filmId) => _films.FirstOrDefault(t => t.FilmID == filmId) is not null;
    

    public void CopyTo(string[] array, int arrayIndex) 
    {
        var items = new ProfileAndFilm[]{};
        _films.CopyTo(items, arrayIndex);
        array = items.Select(t => t.FilmID).ToArray();
    }
    
   
    public IEnumerator<string> GetEnumerator() {
        
        foreach (var film in _films)
            yield return film.FilmID;
    }
    

    public int IndexOf(string filmId)
    {
        for(int i = 0; i < _films.Count; i++){
            if(_films[i].FilmID == filmId)
            return i;
        }
        return -1;
    }

    public void Insert(int index, string filmId) => throw new NotSupportedException();
    

    public bool Remove(string filmId) => _films.Remove(CreateFilmItem(filmId));
    

    public void RemoveAt(int index) => _films.RemoveAt(index);
    

    IEnumerator IEnumerable.GetEnumerator()
    {
        foreach (var film in _films)
            yield return film.FilmID;
    }
}



public sealed class WillWatch : FavouriteBase, IList<string>, IEnumerable<string>
{
    public WillWatch(string profileId) : base(profileId){
    }
    public WillWatch(string profileId, IList<ProfileAndFilm> films) : base(profileId, films){
    }
}

public class Scored : FavouriteBase, IList<string>, IEnumerable<string>
{
    public Scored(string profileId): base(profileId) {
    }
    public Scored(string profileId, IList<ProfileAndFilm> films) : base(profileId, films){
    }
}

public class Watched : FavouriteBase, IList<string>, IEnumerable<string>
{
    public Watched(string profileId): base(profileId){
    }
    public Watched(string profileId, IList<ProfileAndFilm> films) : base(profileId, films){
    }
}
