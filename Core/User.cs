using System.Data;
using System.Collections;


namespace ProfileService.Core;

//TODO: Exceptions if will needed

public class User
{

    private User(){}
    public User(string id, string login, string email, string password)
    {
        Id = id;
        Login = login;
        Email = email;
        Password = password;
    }
    public string Id { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password  {get; set; } = null!;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}
public class Profile
{    

    
    IList<ProfileAndFilm> _willWatchTables = new List<ProfileAndFilm>();
    IList<ProfileAndFilm> _scoredTables = new List<ProfileAndFilm>();
    IList<ProfileAndFilm> _watchedTables= new List<ProfileAndFilm>();
    IList<ChildProfile> _childs = new List<ChildProfile>();


    WillWatch? _willWatch;

    Scored? _scored;


    Watched? _watched;

    public Profile(string id, User user)
    {
        Id = id;
        User = user;
    }
    private Profile(){}

    public string Id {get; set;} = null!;


    public User User {get; set;} = null!;


    public IList<ChildProfile> Childs {  get => _childs; }


    public WillWatch WillWatch { get {
        if(_willWatch != null)
            return _willWatch;
        else{  
            _willWatch = new WillWatch(Id, _willWatchTables);
            return _willWatch;
        }
    } }
    

    public Scored Scored { get{
        if(_scored != null)
            return _scored;
        else{
            _scored = new Scored(Id, _scoredTables);
            return _scored;
        }
    } }


    public Watched Watched { get{
        if(_watched != null)
            return _watched;
            
        else{
            _watched = new Watched(Id, _watchedTables);
            return _watched;
        }
    } }
}

public enum Gender {
    MALE = default,
    FEMALE = 1
}

public class ChildProfile
{
    private ChildProfile(){}
    public ChildProfile(string id, int age, Gender gender = default)
    {
        Age = age;
        Id = id;
        Gender = gender;
    }
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


public class FavouriteFilmAlreadyExistsException : Exception{
    public FavouriteFilmAlreadyExistsException(object value) : base($"{value} already in favourite"){
        
    }
}
public class IndexOutOfRangeFavouriteException : Exception
{
    public IndexOutOfRangeFavouriteException(int index, int count) : base($"{index} >= {count}"){

    }
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
    public string this[int index] { get => _films[index].FilmID; set => Insert(index, value);}

    public int Count => _films.Count;

    public bool IsReadOnly => false;

    private ProfileAndFilm CreateFilmItem(string filmId) => new ProfileAndFilm(filmId, _profileId);

    private void AddFilmValidation(string filmId)
    {
        if(_films.FirstOrDefault(t => t.FilmID == filmId) != null)
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
        for(int i = 0; i < _films.Count; i++){
            if(_films[i].FilmID == filmId)
            return i;
        }
        return -1;
    }

    public void Insert(int index, string filmId){
        AddFilmValidation(filmId);
        if(index >= Count)
            throw new IndexOutOfRangeFavouriteException(index, Count);

        _films[index] = CreateFilmItem(filmId);
    }
    

    public bool Remove(string filmId) => _films.Remove(CreateFilmItem(filmId));
    

    public void RemoveAt(int index) => _films.RemoveAt(index);
    

    IEnumerator IEnumerable.GetEnumerator() => _films.Select(t => t.FilmID).GetEnumerator();
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
