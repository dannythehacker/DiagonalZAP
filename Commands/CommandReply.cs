using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Diagonal.ZAP
{
    public class CommandReply : IRocketCommand
    {

        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "r";

        public string Help => "Reply an SMS";

        public string Syntax => " [Message]";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "zap.reply" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer sender = (UnturnedPlayer)caller;

            if (command.Length < 1)
            {
                UnturnedChat.Say(sender, $"Erro! Use: /r {Syntax}", Color.red);
                return;
            }

            if (ZAP.sms.ContainsKey(sender.CSteamID))
            {
                UnturnedPlayer receiver = UnturnedPlayer.FromCSteamID(ZAP.sms[sender.CSteamID]);

                if (receiver == null)
                {
                    UnturnedChat.Say(sender, ZAP.Instance.Translate("no_player"), Color.red);
                    return;
                }

                if (receiver == sender)
                {
                    UnturnedChat.Say(sender, ZAP.Instance.Translate("no_yourself"), Color.red);
                    return;
                }

                string msg = string.Empty;
                int n2 = 0;
                foreach (string msg3 in command)
                {
                    n2++;
                    if (n2 != 0)
                    {
                        msg = msg + " " + msg3;
                    }
                }

                int cost = ZAP.Instance.Configuration.Instance.SMSCost;

                if (sender.Experience < cost)
                {
                    UnturnedChat.Say(sender, ZAP.Instance.Translate("not_enough_xp"), Color.red);
                    return;
                }

                if (ZAP.Instance.Configuration.Instance.SMSEffect)
                {
                    sender.TriggerEffect(ZAP.Instance.Configuration.Instance.SendEffectID);
                    receiver.TriggerEffect(ZAP.Instance.Configuration.Instance.ReceiveEffectID);
                }

                if (ZAP.sms.ContainsKey(receiver.CSteamID))
                    ZAP.sms[receiver.CSteamID] = sender.CSteamID;
                else
                    ZAP.sms.Add(receiver.CSteamID, sender.CSteamID);

                sender.Experience -= (uint)cost;
                UnturnedChat.Say(sender, ZAP.Instance.Translate("sms_pay", cost), Color.blue);
                UnturnedChat.Say(sender, ZAP.Instance.Translate("reply_sms", receiver.DisplayName), ZAP.Instance.Configuration.Instance.ChatColor);
                UnturnedChat.Say(receiver, ZAP.Instance.Translate("receive_sms", sender.DisplayName, msg), ZAP.Instance.Configuration.Instance.ChatColor);
                return;
            }
        }
    }
}
