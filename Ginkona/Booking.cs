using DSharpPlus.Entities;
using System.Collections.Generic;
using static Ginkona.Commands;
using static Ginkona.Enums;

namespace Ginkona
{
    public class Booking
    {
        public DiscordMember Advertiser { get; }
        public KeyEnum Key { get; }
        public double KeyLevel { get; }
        public double Pot { get; }
        public double Cut { get; }
        public string Server { get; }
        public string Whisper { get; }
        public ArmorstackEnum Armorstack { get; }
        public TankEnum Tank { get; }
        public DamageDealerEnum Dd { get; }
        public HealerEnum Healer { get; }
        public string Note { get; }
        public List<string> SelectedRoles { get; }
        public DiscordMessage Message { get; }
        public bool KeyHolderIsSet { get; set; }
        public DiscordMember KeyHolder { get; set; }
        public DiscordChannel Voice { get; set; }

        public List<SignInObject> SignInObjects { get; }
        public Booking(DiscordMember adv, KeyEnum key, double keyLevel, double pot, double cut, string server, string whisper, ArmorstackEnum armorstack, TankEnum tank, DamageDealerEnum dd, HealerEnum healer, DiscordMessage msg, string note = "")
        {
            Advertiser = adv;
            Key = key;
            Pot = pot;
            Cut = cut;
            Server = server;
            Whisper = whisper;
            Armorstack = armorstack;
            Tank = tank;
            Dd = dd;
            Healer = healer;
            Note = note;
            SelectedRoles = new List<string>();
            SignInObjects = new List<SignInObject>();
            Message = msg;
            KeyLevel = keyLevel;
            KeyHolderIsSet = false;
        }
    }
}
