using Rocket.Core.Plugins;
using SDG.Unturned;
using Rocket.API;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Rocket.API.Collections;
using System;
using System.Collections.Generic;

namespace Diagonal.ZAP
{
    public class ZAP : RocketPlugin<ZAPConfiguration>
    {
        public static ZAP Instance;
        public Dictionary<ulong, MessageTargets> PlayerList;

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
        
        public void SetPlayerData(UnturnedPlayer plyr, UnturnedPlayer Target)
        {
            if (!this.PlayerList.ContainsKey(Target.CSteamID.m_SteamID))
            {
                this.PlayerList[Target.CSteamID.m_SteamID] = new MessageTargets();
            }
            if (!this.PlayerList.ContainsKey(plyr.CSteamID.m_SteamID))
            {
                this.PlayerList[plyr.CSteamID.m_SteamID] = new MessageTargets();
            }
            this.PlayerList[Target.CSteamID.m_SteamID].ReplyTo = plyr.CSteamID.m_SteamID;
            this.PlayerList[plyr.CSteamID.m_SteamID].MessageTo = Target.CSteamID.m_SteamID;
        }
        
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
                { "not_enough_xp","You do not have EXP enough to send an SMS!" }
            };
        #endregion
    }
}
