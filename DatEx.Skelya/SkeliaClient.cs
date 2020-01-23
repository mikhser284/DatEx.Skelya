using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using DatEx.Skelya.DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DatEx.Skelya
{
    public class SkelyaClient 
    {
        private readonly HttpClient HttpClient;

        public SkelyaClient(String baseAddress)
        {
            HttpClient = GetConfiguredClient(new Uri(baseAddress));
        }

        private HttpClient GetConfiguredClient(Uri baseAddress)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = baseAddress;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        private QueryResult<T> GetRequest<T>(String query) //where T : TM1Object
        {
            HttpResponseMessage response = HttpClient.GetAsync(query).Result;
            response.EnsureSuccessStatusCode();
            String result = response.Content.ReadAsStringAsync().Result;
            #if DEBUG
            result = JToken.Parse(result).ToString(Formatting.Indented); 
            #endif
            return JsonConvert.DeserializeObject<QueryResult<T>>(result);
        }

        //private QueryResult<T> PostRequest<T>(String query)
        //{
        //    HttpResponseMessage response = HttpClient.PostAsync(query).Result;
        //    throw new NotImplementedException();
        //}

        public QueryResult<User> GetUsers() => GetRequest<User>("users");

        public QueryResult<Trigger> GetTriggers() => GetRequest<Trigger>("triggers");

        public QueryResult<IdentifiedEvent> GetEvents() => GetRequest<IdentifiedEvent>("events");

        public QueryResult<DataSector> GetDataSectors() => GetRequest<DataSector>("data_sectors");

        public QueryResult<IdentifiedDevice> GetDevices() => GetRequest<IdentifiedDevice>("devices");

        public QueryResult<EventLogRecord> GetEventLogRecords
            (DateTime? startDate = null, DateTime? endDate = null
            , Int32? offset = null, Int32? limit = null
            , String orderDate = null
            , Boolean? triggerful = null)
        {
            List<String> parameters = new List<string>();
            parameters.AddHttpParameter("orderDate", "desc");
            parameters.AddHttpParameter("start_date", startDate);
            parameters.AddHttpParameter("end_date", endDate);
            parameters.AddHttpParameter("offset", offset);
            parameters.AddHttpParameter("limit", limit);
            parameters.AddHttpParameter("triggerful", triggerful);
            //
            return GetRequest<EventLogRecord>("events/log".AsParametrizedHttpRequest(parameters)); 
        }

        public QueryResult<Snapshot> GetSnapshot(Int32? deviceId = null, String eventId = null, Int32? logId = null)
        {
            List<String> parameters = new List<string>();
            parameters.AddHttpParameter("device_id", deviceId);
            parameters.AddHttpParameter("event_id", eventId);
            parameters.AddHttpParameter("log_id", logId);
            //
            return GetRequest<Snapshot>("events/snapshot".AsParametrizedHttpRequest(parameters));
        }
    }

    public static class Ext_HttpUtils
    {
        public static List<String> AddHttpParameter(this List<String> parametersList, String paramName, DateTime? paramValue)
        {
            if (paramValue != null) parametersList.Add($"{paramName}={paramValue:yyyy-MM-dd}T{paramValue:HH:mm:ss}Z");
            return parametersList;
        }

        public static List<String> AddHttpParameter(this List<String> parametersList, String paramName, String paramValue)
        {
            if (!String.IsNullOrEmpty(paramValue)) parametersList.Add($"{paramName}={paramValue}");
            return parametersList;
        }

        public static List<String> AddHttpParameter(this List<String> parametersList, String paramName, Int32? paramValue)
        {
            if (paramValue != null) parametersList.Add($"{paramName}={paramValue}");
            return parametersList;
        }

        public static List<String> AddHttpParameter(this List<String> parametersList, String paramName, Boolean? paramValue)
        {
            if(paramValue != null) parametersList.Add($"{paramName}={(paramValue == false ? 0 : 1)}");
            return parametersList;
        }

        public static String AsParametrizedHttpRequest(this String request, List<String> parametersList)
        {   
            return request + ((parametersList != null && parametersList.Count > 0) ? $"?{String.Join('&', parametersList)}" : "");
        }
    }
}
