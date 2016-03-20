using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

public class TransactionParser : MonoBehaviour {

    public void ParseText() {
        var bankSelection = GameObject.Find("BankDropdown").GetComponent<Dropdown>();
        var transactionInputField = GameObject.Find("TransactionInputField").GetComponent<InputField>();

        var dataServiceManager = GameObject.Find("DBManagerContainer").GetComponent("DataServiceManager") as DataServiceManager;

        var transactionDataService = dataServiceManager.GetDataService("Transaction");
        var eventDataService = dataServiceManager.GetDataService("Event");

        //(eventDataService as Events).CreateTable();
        //(transactionDataService as Transactions).DropTable();
        //(transactionDataService as Transactions).CreateTable();


        var events = eventDataService.List();

        var parsedTransactions = new List<Transaction>();
        if (bankSelection.options[bankSelection.value].text == "OK/Q8")
        {
            parsedTransactions.AddRange(ParseOKQ8Text(transactionInputField.text, eventDataService, events.Cast<Event>()));
        }

        transactionDataService.Add(parsedTransactions.Cast<IEntity>());
        parsedTransactions = new List<Transaction>();
    }

    private IEnumerable<Transaction> ParseOKQ8Text(string text, IDataService eventDataService, IEnumerable<Event> events)
    {
        var rows = text.Split(Environment.NewLine.ToCharArray()).Where(r => r.Length > 0);
        return rows.Select(r => { return ParseOKQ8Row(r, eventDataService, events); });
    }

    private Transaction ParseOKQ8Row(string row, IDataService eventDataService, IEnumerable<Event> events)
    {
        // Example: 160104 ICA SUPERMARKET MORON, SKELLEFTEA 285,56
        var firstSpaceIndex = 6;
        var lastSpaceIndex = row.LastIndexOf(' ');

        var eventText = row.Substring(firstSpaceIndex + 1, lastSpaceIndex - firstSpaceIndex - 1);
        var evnt = events.FirstOrDefault(e => e.Text == eventText);

        var eventId = 0;
        if (evnt == null)
        {
            eventId = eventDataService.Add(new Event() { Text = eventText });
        } else
        {
            eventId = evnt.Id;
        }

        return new Transaction()
        {
            Date = DateTime.ParseExact("20" + row.Substring(0, firstSpaceIndex), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None),
            EventId = eventId,
            Amount = double.Parse(row.Substring(lastSpaceIndex + 1, row.Length - lastSpaceIndex - 1))
        };
    }
}
