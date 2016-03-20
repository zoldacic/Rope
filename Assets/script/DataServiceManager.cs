using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DataServiceManager : MonoBehaviour
{
    List<IDataService> _dataServices;
    DatabaseHandle _databaseHandle;

    void Start()
    {
        _dataServices = new List<IDataService>();
        _databaseHandle = new DatabaseHandle();
    }

    public void RegisterDataService(IDataService dataService)
    {
        _dataServices.Add(dataService);
    }

    public IDataService GetDataService(string name)
    {
        var dataService = _dataServices.FirstOrDefault(d => d.Name == name);
        if (dataService == null)
        {
            if (name == "Transaction")
            {
                dataService = new Transactions(_databaseHandle);
                RegisterDataService(dataService);
            } else if (name == "Event")
            {
                dataService = new Events(_databaseHandle);
                RegisterDataService(dataService);
            }
        }

        return dataService;
    }
}
	
