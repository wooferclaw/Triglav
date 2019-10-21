using System.Collections.Generic;

namespace Triglav.Layers
{
    public class SoundBank
    {
        public Dictionary<Layer, Dictionary<string, string>> Bank { get; set; } = new Dictionary<Layer, Dictionary<string, string>>();


        public void For(Layer layer, Dictionary<string, string> bank)
        {
            switch (layer)
            {
                case Layer.Alexa:
                    Bank[Layer.Alexa] = bank;
                    break;
                case Layer.Alice:
                    Bank[Layer.Alice] = bank;
                    break;
            }
        }
        public string Get(Layer layer, string soundName)
        {
            return Bank[layer][soundName];
        }
        public static SoundBank DefaultSoundBank { get; set; }
        public static SoundBank GetDefaultSoundBank()
        {
            return DefaultSoundBank ??= new SoundBank();
        }
    }
}