using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Triglav.Entities;

namespace Triglav.Layers
{
    public class TelegramCommandContent
    {
        public string Text { get; set; }
        public string Payload { get; set; }
        //hasPicture, hasLocation etc...
    }
}