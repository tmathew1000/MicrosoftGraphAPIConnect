using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraphApiConnect.Models
{
    public enum CreateEventStatusEnum
    {
        NotCreated,
        Created,
        Failed
    }
    // Data / schema contracts between this app and the Office 365 unified API server.
    public class CreateEventResponse
    {
        public CreateEventStatusEnum Status { get; set; }
        public string StatusMessage { get; set; }
    }

    public class CreateEventRequest
    {
        public Event Event { get; set; }

    }
    public class Event
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public DateTimeTimeZone Start { get; set; }
        public DateTimeTimeZone End { get; set; }
        public string ShowAs { get; set; }
        public bool IsAllDay { get; set; }
        public EventBody Body { get; set; }
        public bool IsReminderOn { get; set; }

        //public string bodyPreview { get; set; }
        //public string changeKey { get; set; }
        //public string iCalUId { get; set; }
        //public string importance { get; set; }
        //public bool hasAttachments { get; set; }

        //public List<attendee> attendees { get; set; }
        //public itemBody body { get; set; }
        //public string bodyPreview { get; set; }
        //public List<string> categories { get; set; }
        //public string changeKey { get; set; }
        //public string createdDateTime { get; set; }

        //"attendees": odata.type": "microsoft.graph.attendee"}],

        //"categories": [[{"@"string"],
        //"createdDateTime": "String (timestamp)",
        //"hasAttachments": true,
        //"isCancelled": true,
        //"isOrganizer": true,
        //"lastModifiedDateTime": "String (timestamp)",
        //"location": {"@odata.type": "microsoft.graph.location"},
        //"organizer": {"@odata.type": "microsoft.graph.recipient"},
        //"originalEndTimeZone": "string",
        //"originalStart": "String (timestamp)",
        //"originalStartTimeZone": "string",
        //"recurrence": {"@odata.type": "microsoft.graph.patternedRecurrence"},
        //"reminderMinutesBeforeStart": 1024,
        //"responseRequested": true,
        //"responseStatus": {"@odata.type": "microsoft.graph.responseStatus"},
        //"sensitivity": "String",
        //"seriesMasterId": "string",
        //"showAs": "String",
        //"type": "String",
        //"webLink": "string"
    }
    public class DateTimeTimeZone
    {
        public string DateTime { get; set; }
        public string TimeZone { get; set; }
    }

    public class EventBody
    {
        public string ContentType { get; set; }
        public string Content { get; set; }
        
    }
    public class EventInfo
    {
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public String TimeZone { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
