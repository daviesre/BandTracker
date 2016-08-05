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

       //Act
       List<Venue> result = Venue.GetAll();
       List<Venue> testList = new List<Venue>{testVenue};

       //Assert
       Assert.Equal(testList, result);
     }
     [Fact]
     public void Test_GetAll_GetsAllVenuesFromDatabase()
     {
      //Arrange
      string name1 = "The Venue";
      Venue testVenue1 = new Venue(name1);
      testVenue1.Save();
      string name2 = "Eunev Eht";
      Venue testVenue2 = new Venue(name2);
      testVenue2.Save();

      string name3 = "Eunev Eht 2";
      Venue testVenue3 = new Venue(name3);
      testVenue3.Save();
      List<Venue> testVenues = new List<Venue> {testVenue1, testVenue3};
      //Act
      List<Venue> resultVenues = Venue.GetAll();
      //Assert
      Assert.Equal(testVenues.Count, resultVenues.Count);
     }

    public void Dispose()
    {
      Venue.DeleteAll();
    }
  }
}
