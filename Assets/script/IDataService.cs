using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IDataService
{
    string Name { get; }
    int Add(IEntity entity);
    void Add(IEnumerable<IEntity> entities);
    IEnumerable<IEntity> List();
}
