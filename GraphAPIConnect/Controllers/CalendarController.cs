using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using GraphApiConnect.Helpers;
using GraphApiConnect.Models;

namespace GraphApiConnect.Controllers
{
    public class CalendarController : Controller
    {

        // Take data and put into Index view.
        public ActionResult Index(CreateEventResponse createEventResponse, EventInfo eventInfo)
        {
            ValidateInfo(ref eventInfo);

            ViewBag.EventInfo = eventInfo;
            ViewBag.CreateEventResponse = createEventResponse;

            return View();
        }


        public async Task<ActionResult> CreateEventSubmit(EventInfo eventInfo)
        {
            // After Index method renders the View, user clicks Send Mail, which comes in here.
            ValidateInfo(ref eventInfo);

            // Create Event using the Microsoft Graph API.
            var createEventResult = await UnifiedApiHelper.CreateEventAsync(
                (string)Session[SessionKeys.Login.AccessToken],
                GenerateEvent(eventInfo.StartTime, eventInfo.EndTime, eventInfo.TimeZone, eventInfo.Subject, eventInfo.Body));

            // Reuse the Index view for events (created, not created, failed) .
            // Redirect to tell the browser to call the app back via the Index method.
            return RedirectToAction(nameof(Index), new RouteValueDictionary(new Dictionary<string, object>{
                { "Status", createEventResult.Status },
                { "StatusMessage", createEventResult.StatusMessage },
                { "StartTime", eventInfo.StartTime },
                { "EndTime", eventInfo.EndTime },
                { "Subject", eventInfo.Subject },
                { "TimeZone", eventInfo.TimeZone },
                { "Body", eventInfo.Body },
            }));
        }

        // Use the login user name or recipient email address if no user name.
        void ValidateInfo(ref EventInfo eventInfo)
        {
            var currentUser = (UserInfo)Session[SessionKeys.Login.UserInfo];

            if (eventInfo == null || string.IsNullOrEmpty(eventInfo.StartTime.ToString()) || string.IsNullOrEmpty(eventInfo.EndTime.ToString()) || string.IsNullOrEmpty(eventInfo.Subject))
            {
                eventInfo.StartTime = DateTime.Now;
                eventInfo.EndTime = DateTime.Now.AddHours(1);
                eventInfo.TimeZone = "Central Standard Time";
                eventInfo.Subject = Settings.EventSubject;
                eventInfo.Body = Settings.EventBody;
            }
        }

        // Create email with predefine body and subject.
        CreateEventRequest GenerateEvent(System.DateTime eventstart, System.DateTime eventend, string timezone, string subject, string body)
        {
            return CreateEventObject(

                eventstart: eventstart,
                eventend: eventend,
                timezone: timezone,
                subject: subject,
                body: body
            );
        }

        // Create email object in the required request format/data contract.
        private CreateEventRequest CreateEventObject(System.DateTime eventstart, System.DateTime eventend, string timezone, string subject, string body)
        {

            return new CreateEventRequest
            {

                Event = new Event
                {
                    Subject = subject,
                    Start = new DateTimeTimeZone { DateTime = eventstart.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK"), TimeZone = timezone },
                    End = new DateTimeTimeZone { DateTime = eventend.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK"), TimeZone = timezone },
                    //Start = new DateTimeTimeZone { DateTime = "2016-01-22T19:00:00", TimeZone = "Central Standard Time" },
                    //End = new DateTimeTimeZone { DateTime = "2016-01-22T19:30:00", TimeZone = "Central Standard Time" },
                    Body = new EventBody { ContentType = "Text", Content = body },
                    ShowAs = "Free",
                    IsAllDay = false,
                    IsReminderOn = false
                }

            };
        }
    }
}