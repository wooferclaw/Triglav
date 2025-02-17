﻿using System.Collections.Generic;

namespace Triglav.Models.Alice
{
        public class Button
        {
            public string Title { get; set; }
            public Dictionary<string, string> Payload { get; set; }
            public string Url { get; set; }
            public bool Hide { get; set; } = true;
        }

        public class Response
        {
            public string Text { get; set; }
            public string Tts { get; set; }
            public List<Button> Buttons { get; set; }
            public bool EndSession { get; set; }
        }

        public class AliceResponse
        {
            public Response Response { get; set; } = new Response();
            public AliceSession Session { get; set; }
            public string Version { get; set; }
    }
}