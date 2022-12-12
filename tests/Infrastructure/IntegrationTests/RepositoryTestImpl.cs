using Xunit;
using System.Threading.Tasks;
using FluentAssertions;
using Infrastructure;
using Appservices.CreateProfileDtos;
using Appservices.CreateChildProfileDtos;
using MongoDB.Driver;
using ProfileService.Core;

namespace IntegrationTests;

[Collection("MongoDb")]
public class ProfileRepositoryTestImpl
{
    IMongoCollection<Profile> _repo;
    CreateProfileDto createProfile => new CreateProfileDto {Email = "someemail", Login = "somelogin", Password = "somepassword"};
    CreateChildProfileDto createChildProfileDto(string profileId) => new CreateChildProfileDto { Age = 0, Gender = 0, Name = "name", ProfileId = profileId};
    ProfileRepositoryImpl _profileRepo;
    public ProfileRepositoryTestImpl(MongoDbTestBase dbFixture)
    {
        _repo = dbFixture.Repo;  
        _profileRepo = new ProfileRepositoryImpl(_repo);
        MapsterBuilder.ConfigureMapster();
    }
    [Fact]
    public async Task CreateProfile()
    {
        var profile = await _profileRepo.CreateProfile(createProfile);
        profile.Should().NotBeNull();
    }

    [Fact]
    public async Task AddChildProfile()
    {
        var profileData = new CreateProfileDto {Email = "someemail2", Login = "somelogin2", Password = "somepassword2"};
        
        var profile = await _profileRepo.CreateProfile(profileData);

        var result = await _profileRepo.AddChildProfile(createChildProfileDto(profile.Id));
        
        result.Should().BeTrue();
    }
     [Fact]
    public async Task AddTheSameChildProfile()
    {
        var profileData = new CreateProfileDto {Email = "someemail2", Login = "somelogin2", Password = "somepassword2"};
        
        var profile = await _profileRepo.CreateProfile(profileData);

        var result = await _profileRepo.AddChildProfile(createChildProfileDto(profile.Id));
        
        var result2 = await _profileRepo.AddChildProfile(createChildProfileDto(profile.Id));

        result.Should().BeTrue();
        result2.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteChildProfile()
    {
        var profile = await _profileRepo.CreateProfile(createProfile);
        var childProfile = createChildProfileDto(profile.Id);

        var resultAdd = await _profileRepo.AddChildProfile(childProfile);
        
        var result = await _profileRepo.RemoveChildProfile(profile.Id, childProfile.Name);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task FetchProfile()
    {
        var profile = await _profileRepo.CreateProfile(createProfile);

        var foundProfile = await _profileRepo.FetchProfile(profile.Id);

        foundProfile.Should().NotBeNull();
    }

    [Fact]
    public async Task FetchProfileBy()
    {
        var before = await _profileRepo.CountBy(); 
        var profile = await _profileRepo.CreateProfile(createProfile);


        var byEmail = await _profileRepo.CountBy(createProfile.Email);
        var byLogin = await _profileRepo.CountBy(login:createProfile.Login);
        var by = await _profileRepo.CountBy(createProfile.Email, createProfile.Login);
        var all = await _profileRepo.CountBy();


        byEmail.Should().BeGreaterThan(0);
        byLogin.Should().BeGreaterThan(0);
        by.Should().BeGreaterThan(0);
        all.Should().BeGreaterThan(before);
    }
}