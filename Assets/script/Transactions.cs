using UnityEngine;
using System.Collections;
using SQLite4Unity3d;
using System.Collections.Generic;
using System;
using System.Linq;

public class Transaction : IEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int EventId { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }
}

public class Transactions : IDataService {

    private DatabaseHandle _databaseHandle;

    public Transactions(DatabaseHandle databaseHandle)
    {
        _databaseHandle = databaseHandle;
    }

    public string Name { get { return "Transaction"; } }

    public void DropTable()
    {
        _databaseHandle.Connection.DropTable<Transaction>();
    }

    public void CreateTable()
    {
        _databaseHandle.Connection.CreateTable<Transaction>();
    }

    public IEnumerable<IEntity> List()
    {
        return _databaseHandle.Connection.Table<Transaction>().Cast<IEntity>();
    }

    public int Add(IEntity transaction) {
        return _databaseHandle.Connection.Insert(transaction as Transaction);                
    }

    public void Add(IEnumerable<IEntity> transactions)
    {
        _databaseHandle.Connection.InsertAll(transactions.Cast<Transaction>());
    }
}
