using System;
using System.Collections.Generic;
using Triglav.Models.Alexa;

namespace Triglav.Models.Alexa
{
    public class Application
    {
        public string ApplicationId { get; set; }
    }

    public class User
    {
        public string UserId { get; set; }
    }

    public class Session
    {
        public bool New { get; set; }
        public string SessionId { get; set; }
        public Application Application { get; set; }
        public User User { get; set; }
    }

    public class AudioPlayer
    {
    }

    public class SupportedInterfaces
    {
        public AudioPlayer AudioPlayer { get; set; }
    }

    public class Device
    {
        public SupportedInterfaces SupportedInterfaces { get; set; }
    }

    public class System
    {
        public Application Application { get; set; }
        public User User {get;set;}
        public Device Device { get; set; }
    }

    public class Context
    {
        public System System { get; set; }
    }

    public class Intent
    {
        public string Name { get; set; }
        public string ConfirmationStatus { get; set; }
        public Dictionary<string,string> Slots { get; set; }
    }

    public class Request
    {
        public string Type { get; set; }
        public string RequestId { get; set; }
        public DateTime Timestamp { get; set; }
        public string DialogState { get; set; }
        public string Locale { get; set; }
        public Intent Intent { get; set; }
    }

    public class AlexaRequest
    {
        public string Version { get; set; }
        public Session Session { get; set; }
        public Context Context { get; set; }
        public Request Request { get; set; }
    }

}