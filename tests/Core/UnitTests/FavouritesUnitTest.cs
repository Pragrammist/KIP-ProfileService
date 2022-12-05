using Xunit;
using ProfileService.Core;
using FluentAssertions;
using System.Collections.Generic;
using System.Collections;

namespace UnitTests;

public class FavouriteUnitTest
{
    [Fact]
    public void TestFavourite(){
        WillWatch w = new WillWatch("someId");
        w.Add("qwe");
        w.Count.Should().BeGreaterThan(0);
    }
}