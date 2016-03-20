using UnityEngine;
using System.Collections;
using SQLite4Unity3d;
using System.Collections.Generic;
using System;
using System.Linq;

public class Event : IEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Text { get; set; }
}

public class Events : IDataService {

    private DatabaseHandle _databaseHandle;

    public Events(DatabaseHandle databaseHandle)
    {
        _databaseHandle = databaseHandle;
    }

    public string Name { get { return "Event"; } }

    public void DropTable()
    {
        _databaseHandle.Connection.DropTable<Event>();
    }

    public void CreateTable()
    {
        _databaseHandle.Connection.CreateTable<Event>();
    }

    public IEnumerable<IEntity> List()
    {
        return _databaseHandle.Connection.Table<Event>().AsEnumerable().Cast<IEntity>().ToList();
    }

    public int Add(IEntity evnt) {
        return _databaseHandle.Connection.Insert(evnt as Event);                
    }

    public void Add(IEnumerable<IEntity> events)
    {
        _databaseHandle.Connection.InsertAll(events.Cast<Event>());
    }
}
