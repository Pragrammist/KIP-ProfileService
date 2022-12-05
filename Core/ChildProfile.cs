namespace ProfileService.Core;

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
