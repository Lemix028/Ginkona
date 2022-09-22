using DSharpPlus.SlashCommands;

namespace Ginkona
{
    public static class Enums
    {
        public enum TankEnum
        {
            [ChoiceName("0x Tank")]
            option1,
            [ChoiceName("1x Tank")]
            option2
        }
        public enum DamageDealerEnum
        {
            [ChoiceName("0x Damage Dealer")]
            option1,
            [ChoiceName("1x Damage Dealer")]
            option2,
            [ChoiceName("2x Damage Dealer")]
            option3
        }
        public enum HealerEnum
        {
            [ChoiceName("0x Healer")]
            option1,
            [ChoiceName("1x Healer")]
            option2
        }
        public enum ArmorstackEnum
        {
            [ChoiceName("Kette")]
            option1,
            [ChoiceName("Platte")]
            option2,
            [ChoiceName("Leder")]
            option3,
            [ChoiceName("Stoff")]
            option4,
            [ChoiceName("Keine")]
            option5
        }
        public enum KeyEnum
        {
            //[ChoiceName("DOS")]
            //option1,
            //[ChoiceName("SD")]
            //option2,
            //[ChoiceName("MOTS")]
            //option3,
            //[ChoiceName("NW")]
            //option4,
            //[ChoiceName("SOA")]
            //option5,
            //[ChoiceName("HOA")]
            //option6,
            //[ChoiceName("PF")]
            //option7,
            //[ChoiceName("TOP")]
            //option8,
            //[ChoiceName("Gambit")]
            //option9,
            //[ChoiceName("Streets")]
            //option10,
            //[ChoiceName("Random")]
            //option11
            [ChoiceName("Junkyard")]
            option1,
            [ChoiceName("Streets")]
            option2,
            [ChoiceName("Upper")]
            option3,
            [ChoiceName("Gambit")]
            option4,
            [ChoiceName("GD")]
            option5,
            [ChoiceName("Lower")]
            option6,
            [ChoiceName("ID")]
            option7,
            [ChoiceName("Workshop")]
            option8,
            [ChoiceName("Random")]
            option9
        }
        public enum RoleEnum
        {
            [ChoiceName("Tank")]
            option1,
            [ChoiceName("Damage Dealer")]
            option2,
            [ChoiceName("Healer")]
            option3
        }
        public enum ClassEnum
        {
            [ChoiceName("Demonhunter")]
            option1,
            [ChoiceName("Deathknight")]
            option2,
            [ChoiceName("Druid")]
            option3,
            [ChoiceName("Mage")]
            option4,
            [ChoiceName("Monk")]
            option5,
            [ChoiceName("Paladin")]
            option6,
            [ChoiceName("Priest")]
            option7,
            [ChoiceName("Rogue")]
            option8,
            [ChoiceName("Schaman")]
            option9,
            [ChoiceName("Warlock")]
            option10,
            [ChoiceName("Warrior")]
            option11,
            [ChoiceName("Hunter")]
            option12
        }
        //public enum ServerEnum
        //{
        //    //French
        //    [ChoiceName("Rashgarroth (Arak-Arahm, Kael'thas,Throk'Feroth)")]
        //    option1,
        //    [ChoiceName("Temple noir (Arathi, Illidan, Naxxramas)")]
        //    option2,
        //    [ChoiceName("Archimonde")]
        //    option3,
        //    [ChoiceName("Vol'jin (Chants Eternels)")]
        //    option4,
        //    [ChoiceName("Les Clairvoyants (Confrérie du Thorium, Les Sentinelles, Kirin Tor)")]
        //    option5,
        //    [ChoiceName("Conseil des Ombres (Culte de la Rive noire, La Croisade écarlate)")]
        //    option6,
        //    [ChoiceName("Elune (Varimathras)")]
        //    option7,
        //    [ChoiceName("Garona (Ner'Zhul, Sargeras)")]
        //    option8,
        //    [ChoiceName("Hyjal")]
        //    option9,
        //    [ChoiceName("Khaz Modan")]
        //    option10,
        //    [ChoiceName("Drek'Thar (Uldaman, Eitrigg, Krasus)")]
        //    option11,
        //    [ChoiceName("Marécage de Zangar (Dalaran, Cho'Gall, Eldre'Thalas, Sinstralis)")]
        //    option12,
        //    [ChoiceName("Medivh (Suramar)")]
        //    option13,
        //    [ChoiceName("Ysondre")]
        //    option14,
        //    //German
        //    [ChoiceName("Aegwynn")]
        //    option15,
        //    [ChoiceName("Alexstrasza (Nethersturm, Proudmoore, Madmortem)")]
        //    option16,
        //    [ChoiceName("Alleria (Rexxar)")]
        //    option17,
        //    [ChoiceName("Anetheron (Festung der Stürme, Gul'Dan, Kil'Jaeden, Nathrezim, Rajaxx)")]
        //    option18,
        //    [ChoiceName("Antonidas")]
        //    option19,
        //    [ChoiceName("Anub'Arak (Dalvengyr, Frostmourne, Nazjatar, Zuluhed, Aman'Thul)")]
        //    option20,
        //    [ChoiceName("Area 52 (Sen'jin, Un'Goro)")]
        //    option21,
        //    [ChoiceName("Arthas (Wrathbringer, Blutkessel, Kel'Thuzad, Vek'lor, Durotan, Tirion)")]
        //    option22,
        //    [ChoiceName("Arygos (Khaz'goroth)")]
        //    option23,
        //    [ChoiceName("Baelgun (Lothar, Azshara, Krag'jin)")]
        //    option24,
        //    [ChoiceName("Blackhand (Echsenkessel, Mal'Ganis, Taerar)")]
        //    option25,
        //    //[ChoiceName("Blackrock")]
        //    //option26,
        //    //[ChoiceName("Blackmoore (Lordaeron, Tichondrius)")]
        //    //option27,
        //    //[ChoiceName("Destromath (Gorgonnash, Mannoroth, Nefarian, Nera'thor, Ulduar, Gilneas)")]
        //    //option28,
        //    //[ChoiceName("Die Aldor")]
        //    //option29,
        //    //[ChoiceName("Die Sillberne Hand (Ewige Wacht, Syndikat, Abyssische Rat, Arguswacht, Todeskrallen, )")]//Kult der Verdammten, Konsortium
        //    //option30,
        //    //[ChoiceName("Dun Morogh (Norgannon)")]
        //    //option31,
        //    //[ChoiceName("Eredar")]
        //    //option32,
        //    //[ChoiceName("Frostwolf")]
        //    //option33,
        //    //[ChoiceName("Malygos (Malfurion)")]
        //    //option34,
        //    //[ChoiceName("Malorne (Ysera)")]
        //    //option35,
        //    //[ChoiceName("Onyxia (Dethecus, Mug'Thol, Terrordar, Theredras)")]
        //    //option36,
        //    //[ChoiceName("Teldrassil (Perenolde, Garrosh, Nozdormu, Shattrath)")]
        //    //option37,
        //    //[ChoiceName("Thrall (Ambossar, Kargath)")]
        //    //option38,
        //    //[ChoiceName("Todeswache (Zirkel des Cenarius,  Nachtwache, Der Mithrilorden, Der Rat von Dalaran, Forscherliga)")]
        //    //option39
        //}
    }
}
