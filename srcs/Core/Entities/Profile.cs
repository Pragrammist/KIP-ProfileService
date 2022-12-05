namespace ProfileService.Core;

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
