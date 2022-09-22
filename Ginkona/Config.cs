using DSharpPlus.Entities;
using System.Collections.Generic;

namespace Ginkona
{
    public static class Config
    {
        public static string Emoji_Role_Tank = "tank";
        public static string Emoji_Role_Damagedealer = "damagedealer";
        public static string Emoji_Role_Healer = "healer";


        public static string Emoji_Class_Demonhunter = "Demonhunter";
        public static string Emoji_Class_Deathknight = "Deathknight";
        public static string Emoji_Class_Druid = "Druid";
        public static string Emoji_Class_Mage = "Mage";
        public static string Emoji_Class_Monk = "Monk";
        public static string Emoji_Class_Paladin = "Paladin";
        public static string Emoji_Class_Priest = "Priest";
        public static string Emoji_Class_Rogue = "Rogue";
        public static string Emoji_Class_Schaman = "Shaman";
        public static string Emoji_Class_Warlock = "Warlock";
        public static string Emoji_Class_Warrior = "Warrior";
        public static string Emoji_Class_Hunter= "Hunter";

        public static string Emoji_General_Key = ":key:";

        public static string Url_Armorstack_Kette = "https://cdn.discordapp.com/emojis/970040699158401133.webp?size=96&quality=lossless";
        public static string Url_Armorstack_Platte = "https://cdn.discordapp.com/emojis/970055606465609758.webp?size=96&quality=lossless";
        public static string Url_Armorstack_Leder = "https://cdn.discordapp.com/emojis/970040699464601640.webp?size=96&quality=lossless";
        public static string Url_Armorstack_Stoff = "https://cdn.discordapp.com/emojis/970040699548500088.webp?size=96&quality=lossless";

        public static string Url_Author_Icon = "https://cdn.discordapp.com/attachments/637637651335217152/972195308329140274/20200811_150215.jpg";

        //public static string Url_Key_Dos = "https://github.com/Gethe/wow-ui-textures/blob/live/ENCOUNTERJOURNAL/UI-EJ-DUNGEONBUTTON-TheOtherSide.PNG?raw=true";
        //public static string Url_Key_Sd = "https://github.com/Gethe/wow-ui-textures/blob/live/ENCOUNTERJOURNAL/UI-EJ-DUNGEONBUTTON-SanguineDepths.PNG?raw=true";
        //public static string Url_Key_Mots = "https://github.com/Gethe/wow-ui-textures/blob/live/ENCOUNTERJOURNAL/UI-EJ-DUNGEONBUTTON-MistsofTirnaScithe.PNG?raw=true";
        //public static string Url_Key_Nw = "https://github.com/Gethe/wow-ui-textures/blob/live/ENCOUNTERJOURNAL/UI-EJ-DUNGEONBUTTON-NecroticWake.PNG?raw=true";
        //public static string Url_Key_Soa = "https://github.com/Gethe/wow-ui-textures/blob/live/ENCOUNTERJOURNAL/UI-EJ-DUNGEONBUTTON-SpiresofAscension.PNG?raw=true";
        //public static string Url_Key_Hoa = "https://github.com/Gethe/wow-ui-textures/blob/live/ENCOUNTERJOURNAL/UI-EJ-DUNGEONBUTTON-HallsofAtonement.PNG?raw=true";
        //public static string Url_Key_Pf = "https://github.com/Gethe/wow-ui-textures/blob/live/ENCOUNTERJOURNAL/UI-EJ-DUNGEONBUTTON-Plaguefall.PNG?raw=true";
        //public static string Url_Key_Top = "https://github.com/Gethe/wow-ui-textures/blob/live/ENCOUNTERJOURNAL/UI-EJ-DUNGEONBUTTON-TheaterofPain.PNG?raw=true";
        //public static string Url_Key_Gambit = "https://i0.wp.com/tankingfornoobs.com/wp-content/uploads/2021/12/tazavesh-banner.png?resize=640%2C214&ssl=1";
        //public static string Url_Key_Streets = "https://i0.wp.com/tankingfornoobs.com/wp-content/uploads/2021/12/tazavesh-banner.png?resize=640%2C214&ssl=1";
        

        public static string Url_Key_Junkyard = "https://github.com/Gethe/wow-ui-textures/blob/live/ENCOUNTERJOURNAL/UI-EJ-DUNGEONBUTTON-TheOtherSide.PNG?raw=true";
        public static string Url_Key_Streets = "https://i0.wp.com/tankingfornoobs.com/wp-content/uploads/2021/12/tazavesh-banner.png?resize=640%2C214&ssl=1";
        public static string Url_Key_Upper = "https://github.com/Gethe/wow-ui-textures/blob/live/ENCOUNTERJOURNAL/UI-EJ-DUNGEONBUTTON-MistsofTirnaScithe.PNG?raw=true";
        public static string Url_Key_Gambit = "https://i0.wp.com/tankingfornoobs.com/wp-content/uploads/2021/12/tazavesh-banner.png?resize=640%2C214&ssl=1";
        public static string Url_Key_GD = "https://github.com/Gethe/wow-ui-textures/blob/live/ENCOUNTERJOURNAL/UI-EJ-DUNGEONBUTTON-SpiresofAscension.PNG?raw=true";
        public static string Url_Key_Lower = "https://github.com/Gethe/wow-ui-textures/blob/live/ENCOUNTERJOURNAL/UI-EJ-DUNGEONBUTTON-HallsofAtonement.PNG?raw=true";
        public static string Url_Key_ID = "https://github.com/Gethe/wow-ui-textures/blob/live/ENCOUNTERJOURNAL/UI-EJ-DUNGEONBUTTON-Plaguefall.PNG?raw=true";
        public static string Url_Key_Workshop = "https://github.com/Gethe/wow-ui-textures/blob/live/ENCOUNTERJOURNAL/UI-EJ-DUNGEONBUTTON-TheaterofPain.PNG?raw=true";
        public static string Url_Key_Random = "https://raw.githubusercontent.com/Gethe/wow-ui-textures/live/ENCOUNTERJOURNAL/UI-EJ-DUNGEONBUTTON-Shadowlands.PNG";

        //public static DiscordColor Color_Key_Dos = new DiscordColor(123, 48, 214);
        //public static DiscordColor Color_Key_Sd = new DiscordColor(178, 59, 13);
        //public static DiscordColor Color_Key_Mots = new DiscordColor(145, 87, 87);
        //public static DiscordColor Color_Key_Nw = new DiscordColor(49, 109, 90);
        //public static DiscordColor Color_Key_Soa = new DiscordColor(71, 168, 247);
        //public static DiscordColor Color_Key_Hoa = new DiscordColor(165, 69, 123);
        //public static DiscordColor Color_Key_Pf = new DiscordColor(131, 196, 82);
        //public static DiscordColor Color_Key_Top = new DiscordColor(27, 124, 79);
        //public static DiscordColor Color_Key_Gambit = new DiscordColor(83, 152, 231);
        //public static DiscordColor Color_Key_Streets = new DiscordColor(83, 152, 231);
       
        public static DiscordColor Color_Key_Junkyard = new DiscordColor(123, 48, 214);
        public static DiscordColor Color_Key_Streets = new DiscordColor(178, 59, 13);
        public static DiscordColor Color_Key_Upper = new DiscordColor(145, 87, 87);
        public static DiscordColor Color_Key_Gambit = new DiscordColor(49, 109, 90);
        public static DiscordColor Color_Key_GD = new DiscordColor(71, 168, 247);
        public static DiscordColor Color_Key_Lower = new DiscordColor(165, 69, 123);
        public static DiscordColor Color_Key_ID = new DiscordColor(131, 196, 82);
        public static DiscordColor Color_Key_Workshop = new DiscordColor(27, 124, 79);
        public static DiscordColor Color_Key_Random = new DiscordColor(208, 116, 35);

        public static DiscordColor Color_Slavery = new DiscordColor(77, 8, 138);



        public static ulong Id_Role_Member = 964264107450695710;
        public static ulong Id_Search_Category = 974810553212096522;
        public static ulong Id_Voice_Category = 974820240766689301;
        public static ulong Id_Text_Botspam = 973686367601168394;
        public static ulong Id_Channel_Log = 976551240622288917;
        public static ulong Id_Category_Slavery = 978752910735327322;

        public static ulong Role_Slave = 978633840610406410;
        public static ulong Role_RaidBooster = 966987751465578496;

        public static ulong Role_Booster_General = 964264107450695710;

        public static ulong Role_Booster_2000 = 1008475359664152666;
        public static ulong Role_Booster_2100 = 1008475231867895839;
        public static ulong Role_Booster_2200 = 973683497367642192;
        public static ulong Role_Booster_2300 = 973683552577269801;
        public static ulong Role_Booster_2400 = 973683601310892092;
        public static ulong Role_Booster_2500 = 973683640766718022;
        public static ulong Role_Booster_2600 = 973683675185172500;
        public static ulong Role_Booster_2700 = 973683710174044160;
        public static ulong Role_Booster_2800 = 973683755510272040;
        public static ulong Role_Booster_2900 = 973683808425615410;
        public static ulong Role_Booster_3000 = 973683851509506109;
        public static ulong Role_Booster_3100 = 973683889044340798;
        public static ulong Role_Booster_3200 = 973683931104825344;
        public static ulong Role_Booster_3300 = 973683980949921872;
        public static ulong Role_Booster_3400 = 973684022985257000;
        public static ulong Role_Booster_3500 = 973684118669910016;
        public static ulong Role_Booster_3600 = 973684158805180466;
        public static ulong Role_Booster_3700 = 973684212257390683;
        public static ulong Role_Booster_3800 = 973684253424508928;
        public static ulong Role_Booster_3900 = 973684309284225034;
        public static ulong Role_Booster_4000 = 973684361213927564;
        public static ulong Role_Management = 964493847436599316;

        public static List<ulong> Role_Booster = new List<ulong>() { Role_Booster_2000,Role_Booster_2100,Role_Booster_2200, Role_Booster_2300, Role_Booster_2400,
                Role_Booster_2500, Role_Booster_2600, Role_Booster_2700, Role_Booster_2800, Role_Booster_2900, Role_Booster_3000,
                Role_Booster_3100, Role_Booster_3200, Role_Booster_3300, Role_Booster_3400, Role_Booster_3500, Role_Booster_3600,
                Role_Booster_3700, Role_Booster_3800, Role_Booster_3900, Role_Booster_4000};
    }
}
