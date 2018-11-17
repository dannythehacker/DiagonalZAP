using Rocket.API;
using System.Collections.Generic;
using UnityEngine;

namespace Diagonal.ZAP
{
    public class ZAPConfiguration : IRocketPluginConfiguration
    {
        public bool SMSEffect;
        public ushort SendEffectID;
        public ushort ReceiveEffectID;
        public ushort SMSCost;
        public Color ChatColor;

        public void LoadDefaults()
        {
            SMSEffect = true;
            SendEffectID = 5071;
            ReceiveEffectID = 5072;
            SMSCost = 50;
            ChatColor = Color.green;
        }
    }
}
