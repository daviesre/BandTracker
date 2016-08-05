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

      List<Venue> testVenues = new List<Venue> {testVenue1, testVenue2};
      //Act
      List<Venue> resultVenues = Venue.GetAll();
      //Assert
      Assert.Equal(testVenues.Count, resultVenues.Count);
    }

    [Fact]
    public void Test_Delete_DeletesVenueFromDatabase()
    {
      //Arrange
      string name1 = "Das Venue";
      Venue testVenue1 = new Venue(name1);
      testVenue1.Save();
      string name2 = "Der Venue";
      Venue testVenue2 = new Venue(name2);
      testVenue2.Save();

      List<Venue> testVenue = new List<Venue> {};
      //Act
      testVenue1.Delete();
      testVenue2.Delete();
      List<Venue> resultVenue = Venue.GetAll();

      //Assert
      Assert.Equal(testVenue, resultVenue);
    }

    [Fact]
    public void Test_Update_UpdatesVenueInDatabase()
    {
      //Arrange
      string name = "Venue Uno";
      Venue testVenue = new Venue(name);
      testVenue.Save();
      string newName = "Venue Dos";

      //Act
      testVenue.Update(newName);

      string result = testVenue.GetName();

      //Assert
      Assert.Equal(newName, result);
    }

    [Fact]
    public void Test_Find_FindsVenueInDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("Bien Venue");
      testVenue.Save();
      //Act
      int testId = testVenue.GetId();
      Venue foundVenue = Venue.Find(testId);
      //Assert
      Assert.Equal(testVenue.GetName(), foundVenue.GetName());
    }

    [Fact]
    public void Test_AddBand_AddsBandToVenue()
    {
     //Arrange
     Venue testVenue = new Venue("La Venue");
     testVenue.Save();

     Band testBand1 = new Band("The Hows");
     testBand1.Save();
     Band testBand2 = new Band("Nancy Razor");
     testBand2.Save();
     //Act
     List<Band> resultList = new List<Band>{};
     resultList.Add(testBand1);
     List<Band> testList = new List<Band>{testBand1};
     //Assert
     Assert.Equal(testList, resultList);
    }

    [Fact]
    public void Test_GetBands_GetsAllBandsInThisVenue()
    {
      //Arrange
      Venue newVenue = new Venue("La Venue");
      newVenue.Save();
      Band name1Band = new Band("Ima Band");
      name1Band.Save();
      Band name2Band = new Band("Nancy Razor");
      name2Band.Save();
      newVenue.AddBand(name1Band);
      newVenue.AddBand(name2Band);
      List<Band> testVenueBands = new List<Band> {name1Band, name2Band};
      //Act
      List<Band> resultVenueBands = newVenue.GetBands();
      //Assert
      Assert.Equal(testVenueBands.Count, resultVenueBands.Count);

    }

    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }
  }
}
