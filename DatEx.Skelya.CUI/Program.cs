using System;
using System.Collections.Generic;
using System.Linq;
using DatEx.Skelya;
using DatEx.Skelya.DataModel;

namespace DatEx.Skelya.CUI
{
    class Program
    {
        public static AppSettings AppSettings = null;
        public static SkelyaClient Client = null;

        static void Main(string[] args)
        {
            AppSettings = AppSettings.Load();
            Client = new SkelyaClient(AppSettings.HttpAddressOf.SkelyaServer);

            //ShowEventLogs(Client);
            //ShowDevices(Client);
            //ShowUsers(Client);
            ShowEventsAndTriggers(Client);
            //ShowDataSectors(Client);
        }

        public static void UpdateUserInfo(SkelyaClient client)
        {

        }

        public static void ShowEventLogs(SkelyaClient client)
        {
            List<EventLogRecord> eventLogRecords = client.GetEventLogRecords(new DateTime(2019, 12, 21, 10, 09, 19), new DateTime(2019, 12, 21, 10, 09, 21)).Items;

            foreach(var r in eventLogRecords)
            {
                Console.WriteLine($"{r.DateTime:yyyy.MM.dd HH:mm:ss} - {r.Description}");
            }
        }

        public static void ShowDevices(SkelyaClient client) 
        {
            foreach(var dev in client.GetDevices().Items.Select(x => x.Device).OrderBy(x => x.DataSector.Id))
                Console.WriteLine($"{dev}\n\n");
        }

        public static void ShowDataSectors(SkelyaClient client)
        {
            client.GetDataSectors().Items.ForEach(x => Console.WriteLine(x));
        }

        public static void ShowEventsAndTriggers(SkelyaClient client)
        {
            QueryResult<Trigger> triggers = client.GetTriggers();
            Dictionary<String, List<Trigger>> triggersDict = new Dictionary<String, List<Trigger>>();
            foreach(var trigger in triggers.Items)
            {
                if(!triggersDict.ContainsKey(trigger.EventId)) triggersDict.Add(trigger.EventId, new List<Trigger>());
                triggersDict[trigger.EventId].Add(trigger);
            }
            QueryResult<IdentifiedEvent> events = client.GetEvents();
            foreach(var @event in events.Items.Select(x => x.Event))
            {
                String eventId = @event.Id;
                Console.WriteLine($"EventName:  {@event.Name}");
                Console.WriteLine($"EventType:  {@event.Type}");
                Console.WriteLine("EventTriggers:");
                List<Trigger> eventTriggers = null;
                if(!triggersDict.TryGetValue(eventId, out eventTriggers)) Console.WriteLine("   <none>");
                else foreach(var trigger in eventTriggers) Console.WriteLine($"   {trigger.Name} = {trigger.Value}");
                Console.WriteLine("\n\n");
            }
        }

        public static void ShowUsers(SkelyaClient client)
        {
            client.GetUsers().Items.ForEach(x => Console.WriteLine(x));
        }
    }
}
