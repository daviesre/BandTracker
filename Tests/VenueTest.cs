using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Xunit;

namespace BandTracker
{
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_EmptyAtFirst()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_EqualOverrideTrueForSameName()
    {
      //Arrange, Act
      Venue firstVenue = new Venue("Best Place Ever");
      Venue secondVenue = new Venue("Best Place Ever");

      //Assert
      Assert.Equal(firstVenue, secondVenue);
    }

    [Fact]
     public void Test_Save()
     {
       //Arrange
       Venue testVenue = new Venue("Cool Venue");
       testVenue.Save();

       Venue testVenue2 = new Venue("Cooler Venue");

       //Act
       List<Venue> result = Venue.GetAll();
       List<Venue> testList = new List<Venue>{testVenue2};

       //Assert
       Assert.Equal(testList, result);
     }

    public void Dispose()
    {
      Venue.DeleteAll();
    }
  }
}
