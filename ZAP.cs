using Rocket.Core.Plugins;
using SDG.Unturned;
using Rocket.API;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Rocket.API.Collections;
using System;
using System.Collections.Generic;
using Steamworks;
using System.Collections;
using UnityEngine;
using System.Threading;

namespace Diagonal.ZAP
{
    public class ZAP : RocketPlugin<ZAPConfiguration>
    {
        public static ZAP Instance;
        public static Dictionary<CSteamID, CSteamID> sms = new Dictionary<CSteamID, CSteamID>();

        #region Write
        public static void Write(string message)
        {
            Console.WriteLine(message);
        }
        public static void Write(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        #endregion

        #region Load/Unload
        protected override void Load()
        {
            Instance = this;

            #region WriteLoad

            if (Configuration.Instance.SMSEffect)
            {
                Write("SMS Effect: Enabled", ConsoleColor.Green);
                Write($"Send Effect ID: {Configuration.Instance.SendEffectID}", ConsoleColor.DarkGreen);
                Write($"Receive Effect ID: {Configuration.Instance.ReceiveEffectID}", ConsoleColor.DarkGreen);
            }
            else
            {
                Write("SMS Effect: Disabled", ConsoleColor.Red);
            }
            #endregion
        }

        protected override void Unload()
        {
            Instance = null;
        }
        #endregion

        #region Translate
        public override TranslationList DefaultTranslations =>
            new TranslationList
            {
                { "send_sms","SMS successfully sent to: {0}" },
                { "receive_sms","SMS from {0}: {1}" },
                { "reply_sms","SMS from {0} sucessfully replied!" },
                { "no_player","Player not found!" },
                { "no_player_reply","You do not even have an sms to reply!" },
                { "no_yourself","You can not send an sms to yourself!" },
                { "not_enough_xp","You do not have EXP enough to send an SMS!" },
                { "sms_pay","You paid {0} levels of experience for this SMS!" }
            };
        #endregion
    }
}
