using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BandTracker
{
  public class Venue
  {
    //Properties
    private int _id;
    private string _name;

    //Constructors
    public Venue(string newName, int Id = 0)
    {
      _name = newName;
      _id = Id;
    }
    //Getters and Setters
    public string GetName()
    {
      return _name;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }

    public int GetId()
    {
      return _id;
    }
    //Other Methods

    public static List<Venue> GetAll()
    {
      List<Venue> testList = new List<Venue>{};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Venue nextVenue = new Venue(name, id);
        testList.Add(nextVenue);
      }
      if(rdr!=null)
      {
        rdr.Close();
      }
      if(conn!=null)
      {
        conn.Close();
      }
      return testList;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE from venues; DELETE from venues_bands;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
