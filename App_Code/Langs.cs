using System.Collections.Generic;
using YouthHousesLibrary;

public class Langs
{
    public enum TranslationsGroups
    {
        Contents,
        Tariffs,
        Legislations,
        Faqs,
        Contacts,
        Models,
        Default,
        MasterPage,
        Global,
    }

    public class LangInfo
    {
        public bool IsError { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int LangsIsActive { get; set; }
    }

    public static bool IsError
    {
        get
        {
            return DALCL.GetLangInfo.IsError;
        }
    }

    public static int Id
    {
        get
        {
            return DALCL.GetLangInfo.Id;
        }
    }

    public static string Name
    {
        get
        {
            return DALCL.GetLangInfo.Name;
        }
    }

    public static string DisplayName
    {
        get
        {
            return DALCL.GetLangInfo.DisplayName;
        }
    }

    public static int IsActive
    {
        get
        {
            return DALCL.GetLangInfo.LangsIsActive;
        }
    }

    public static string _LangDefault = MySettings.Global.Other.DefaultLang;
    public static int _LangDefaultID = MySettings.Global.Other.DefaultLangID;

    //public static string Get(string Key, TranslationsGroups translationsGroups = TranslationsGroups.Global)
    //{
    //    Dictionary<string, string> Dict = DALCL.GetLangContent();

    //    Key = translationsGroups + Key;

    //    if (Dict.ContainsKey(Key))
    //    {
    //        return Dict[Key];
    //    }
    //    else
    //    {
    //        return "";
    //    }
    //}
}