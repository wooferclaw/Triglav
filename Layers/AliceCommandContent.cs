using System;

namespace Triglav.Layers
{
    public class AliceCommandContent
    {
        public string[] Keywords { get; set; }
        public Screen HasScreen { get; set; } = Screen.DoesNotMatter;
        public bool CheckCommand(AliceCommand incoming)
        {
           
            throw new NotImplementedException();
        }

    }

    public enum Screen
    {
        DoesNotMatter,
        Yes,
        No
    }
}