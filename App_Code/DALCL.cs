using YouthHousesLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using static YouthHousesLibrary.DALC;

public class DALCL
{
    public class Login
    {
        public int organizationsId;
        public string organizationsName;
        public int calendarOganizationsId;
        public int administratorsId;
        public string fullname;
        public string permissions;
    }

    public static Login _Login
    {
        get
        {
            if (HttpContext.Current.Session["Login"] != null)
            {
                return (Login)HttpContext.Current.Session["Login"];
            }
            else
            {
                return null;
            }
        }
    }

    public static Langs.LangInfo GetLangInfo
    {
        get
        {
            //Bütün dilləri alırıq bir dəfə ancaq foreach içində current dili yoxluyuruq və onun məlumatların qaytarırıq.
            //Keşdəmədə dil üzrə etməyə ehtiyac yoxdu
            Langs.LangInfo Li = new Langs.LangInfo();
            Li.IsError = true;

            string RouteCurrentLang = ConfigL._Route("lang");

            //Əgər mövcud dil keşdəki dil ilə eynidirsə birbaşa qaytaraq.
            //Mütləq bu sesiya olmalıdır.
            if (HttpContext.Current.Cache["CurrentLangClass" + RouteCurrentLang] != null)
            {
                return ((Langs.LangInfo)HttpContext.Current.Cache["CurrentLangClass" + RouteCurrentLang]);
            }

            DataTable Dt = new DataTable();

            if (HttpContext.Current.Cache["GetLangInfo"] != null)
            {
                Dt = (DataTable)HttpContext.Current.Cache["GetLangInfo"];
            }
            else
            {
                Dt = GetDataTable("*", Tools.Tables.Langs, "Where IsActive=1");

                //Error
                if (Dt == null || Dt.Rows.Count < 1)
                {
                    return Li;
                }

                HttpContext.Current.Cache["GetLangInfo"] = Dt;
            }

            foreach (DataRow Dr in Dt.Rows)
            {
                if (Dr["Name"]._ToString().ToLower() == RouteCurrentLang.ToLower())
                {
                    Li.IsError = false;
                    Li.Id = int.Parse(Dr["ID"]._ToString());
                    Li.Name = Dr["Name"]._ToString().ToLower();
                    Li.DisplayName = Dr["DisplayName"]._ToString();
                    Li.LangsIsActive = Dr["IsActive"]._ToInt16();

                    HttpContext.Current.Cache["CurrentLangClass" + RouteCurrentLang] = Li;

                    break;
                }
            }
            return Li;
        }
    }

    public static DataTable GetAllLangs()
    {
        return GetDataTable("ID,Name,DisplayName", Tools.Tables.Langs, "Order By Priority");
    }

    public static bool CheckPermission(Tools.AdministratorsMenu administratorsMenu)
    {
        if (_Login.permissions == "*")
            return true;

        return $",{_Login.permissions},".IndexOf($",{(int)administratorsMenu},") > -1;
    }

    public static DataTable GetAdministratorsMenu(int parrentId)
    {
        return GetDataTableByStoredProcedure("GetAdministratorsMenu", "AdministratorsId,ParrentId", new object[] { _Login.administratorsId, parrentId });
    }

    public static DataTable GetNewsLangsStatus()
    {
        return GetDataTable("*", Tools.Tables.NewsLangsStatus, "Order By Id asc");
    }

    public static DataTable GetNewsByID(int newsId)
    {
        return GetDataTable("*", Tools.Tables.V_News, "Id,LangsId,OrganizationsLangsId,OrganizationsId", new object[] { newsId, Langs.Id, Langs.Id, _Login.organizationsId });
    }

    public static DataTable GetNewsFilesByID(int NewsId)
    {
        return GetDataTable("Id,FilesId,Path", Tools.Tables.V_NewsFiles, "NewsId,NewsFilesTypesId", new object[] { NewsId, (int)Tools.NewsFilesTypes.Digər_şəkillər });
    }

    public static DataTable GetNewsTypes()
    {
        return GetDataTable("NewsTypesId as Id,Name", Tools.Tables.NewsTypesLangs, "LangsId", new object[] { Langs.Id }, "Order By Id asc");
    }

    public static bool CheckNewsById(int newsId)
    {
        return GetSingleValues("Count(*)", Tools.Tables.News, "Id,IsActive", new object[] { newsId, true })._ToInt16() > 0;
    }

    public static DataTable GetUsersSocialStatusActive()
    {
        return GetDataTable("Id,Name", Tools.Tables.V_UsersSocialStatus, "LangsId,IsActive", new object[] { Langs.Id, true }, "Order By Priority asc");
    }

    public static DataTable GetUsersSocialStatusAll()
    {
        return GetDataTable("Id,Name", Tools.Tables.V_UsersSocialStatus, "LangsId", Langs.Id, "Order By Priority asc");
    }

    public static DataTable GetUsersStatus()
    {
        return GetDataTable("Id,Name", Tools.Tables.UsersStatus, "Order By Id asc");
    }

    public static DataTable GetUsersGenders()
    {
        return GetDataTable("Id,Name", Tools.Tables.V_UsersGenders, "LangsId", Langs.Id, "Order By Id asc");
    }

    public static bool CheckUsername(string username)
    {
        return GetSingleValues("COUNT(*) Count", Tools.Tables.Users, "Username,UsersStatusId", new object[] { username, (int)Tools.UsersStatus.Aktiv })._ToInt16() == 0;
    }

    public static bool CheckDocumentNumber(int usersId, string documentNumber)
    {
        return GetSingleValuesBySqlCommand($"Select COUNT(*) [Count] From {Tools.Tables.Users} Where Id!=@UsersId and DocumentNumber=@DocumentNumber and UsersStatusId=@UsersStatusId", "UsersId,DocumentNumber,UsersStatusId", new object[] { usersId, documentNumber, (int)Tools.UsersStatus.Aktiv })._ToInt16() == 0;
    }

    public static DataTable GetUsersById(int usersId)
    {
        return GetDataTable("*", Tools.Tables.V_Users, "Id,OrganizationsId,UsersSocialStatusLangsId,UsersGendersLangsId", new object[] { usersId, _Login.organizationsId, (int)Tools.Langs.AZ, (int)Tools.Langs.AZ });
    }

    public static DataTable GetUsersUsedServicesCount()
    {
        return GetDataTableBySqlCommand($"Select UsersId,Count(*)[Count] " +
                                            $"From ServicesSessions as SS " +
                                            $"Inner Join ServicesOrganizations SO ON SS.ServicesOrganizationsId = SO.Id " +
                                            $"Where SO.OrganizationsId = @OrganizationsId and ServicesSessionsStatusId in (20,30) " +
                                            $"Group By UsersId",
                                            "OrganizationsId",
                                            new object[] { DALCL._Login.organizationsId });
    }

    public static DataTable GetServices()
    {
        return GetDataTable("*", Tools.Tables.V_Services, "Order By Id desc");
    }

    public static DataTable GetServices(int langsId)
    {
        return GetDataTableBySqlCommand($"Select Id,Name From {Tools.Tables.V_Services} " +
                                        $"Where Id not in(Select ServicesId From ServicesOrganizations Where OrganizationsId=@OrganizationsId and IsActive=@IsActive) " +
                                        $"and ServicesTypesId=@ServicesTypesId and LangsId=@LangsId and IsActive=@IsActive " +
                                        $"Order By Id desc",
                                        "OrganizationsId,ServicesTypesId,LangsId,IsActive",
                                        new object[] { _Login.organizationsId, (int)Tools.ServicesTypes.Xidmətlər, langsId, true });
    }

    public static DataTable GetServicesById(int servicesId)
    {
        return GetDataTable("*", Tools.Tables.V_Services, "Id,LangsId,ServicesTypesId", new object[] { servicesId, Langs.Id, (int)Tools.ServicesTypes.Tədbirlər });
    }

    public static DataTable GetServicesVotesTypes()
    {
        return GetDataTable("*", Tools.Tables.V_ServicesVotesTypes, "LangsId,IsActive", new object[] { (int)Tools.Langs.AZ, true });
    }

    public static DataTable GetSurveysStatus()
    {
        return GetDataTable("*", Tools.Tables.SurveysStatus);
    }

    public static DataTable GetSurveysById(int surveysId)
    {
        return GetDataTable("*", Tools.Tables.V_Surveys, "Id,LangsId", new object[] { surveysId, Langs.Id });
    }

    public static bool CheckSurveysQuestion(int surveysId, string question)
    {
        return GetSingleValuesBySqlCommand($"Select COUNT(*) From {Tools.Tables.V_Surveys} " +
                                            $"Where Id!=@SurveysId and OrganizationsId=@OrganizationsId and LangsId=@LangsId and Question=@Question and SurveysStatusId=@SurveysStatusId",
                                             "SurveysId,OrganizationsId,LangsId,Question,SurveysStatusId",
                                             new object[] { surveysId, _Login.organizationsId, Langs.Id, question, (int)Tools.SurveysStatus.Nümayişdə })._ToInt16() > 0;
    }

    public static bool CheckSurveysAnswers(int surveysId, string answers)
    {
        return GetSingleValuesBySqlCommand($"Select COUNT(*) From {Tools.Tables.V_SurveysAnswers} " +
                                            $"Where SurveysId=@SurveysId and LangsId=@LangsId and Answer=@Answer and IsActive=@IsActive",
                                             "SurveysId,LangsId,Answer,IsActive",
                                             new object[] { surveysId, Langs.Id, answers, true })._ToInt16() > 0;
    }

    public static DataTable GetAdministratorsStatus()
    {
        return GetDataTable("*", Tools.Tables.AdministratorsStatus);
    }

    public static DataTable GetAdministratorsById(int administratorsId)
    {
        return GetDataTable("*", Tools.Tables.V_Administrators, "Id,OrganizationsId,OrganizationsLangsId", new object[] { administratorsId, _Login.organizationsId, (int)Tools.Langs.AZ });
    }

    public static bool CheckAdministratorsUserName(string username)
    {
        return GetSingleValues("COUNT(*) Count", Tools.Tables.Administrators, "Username", new object[] { username })._ToInt16() == 0;
    }

    public static DataTable GetAllOrganizations()
    {
        return GetDataTable("Id,Name", Tools.Tables.V_Organizations, "LangsId", new object[] { (int)Tools.Langs.AZ });
    }

    public static DataTable GetActiveOrganizations()
    {
        return GetDataTable("Id,Name", Tools.Tables.V_Organizations, "LangsId,IsActive", new object[] { (int)Tools.Langs.AZ, true });
    }

    public static DataTable GetSurveysSubscriptions(int surveysId)
    {
        return GetDataTableBySqlCommand("Select O.Id,Name,SS.Id SurveysSubscriptionsId,SS.IsActive " +
                                            "From V_Organizations as O Left Join " +
                                            "(Select * From SurveysSubscriptions Where SurveysId = @SurveysId) as SS on O.Id = SS.OrganizationsId " +
                                            "Where O.LangsId=@LangsId and O.IsActive=@IsActive",
                                            "SurveysId,LangsID,IsActive",
                                            new object[] { surveysId, (int)Tools.Langs.AZ, true });
    }

    public static DataTable GetServicesVotesTypesRelations(int servicesId)
    {
        return GetDataTableBySqlCommand("Select SV.Id,SV.Name,SVR.Id ServicesVotesTypesRelationsId,SVR.IsActive " +
                                        "From V_ServicesVotesTypes as SV Left Join (Select * From ServicesVotesTypesRelations " +
                                        "Where ServicesId = @ServicesId) as SVR on SV.Id = SVR.ServicesVotesTypesId " +
                                        "Where SV.LangsId = @LangsId and SV.IsActive=@IsActive",
                                        "ServicesId,LangsId,IsActive",
                                        new object[] { servicesId, (int)Tools.Langs.AZ, true });
    }

    public static DataTable GetCalendarTypes()
    {
        return GetDataTable("*", Tools.Tables.CalendarTypes, "IsActive", new object[] { true });
    }

    public static DataTable GetCalendarById(int calendarId)
    {
        return DALC.GetDataTable("*", Tools.Tables.V_Calendar, "Id", calendarId);
    }

    public static DataTable GetCalendarOrganizations()
    {
        return GetDataTable("*", Tools.Tables.CalendarOganizations, "OrganizationsId", _Login.organizationsId, "Order By IsActive desc, Id desc");
    }

    public static DataTable GetCalendarOrganizationsById(int calendarOrganizationsId)
    {
        return DALC.GetDataTable("*", Tools.Tables.CalendarOganizations, "Id", calendarOrganizationsId);
    }

    public static bool CheckCalendarOrganizationsName(int calendarOganizationsId, string organizationsName)
    {
        return GetSingleValuesBySqlCommand($"Select COUNT(*) Count From {Tools.Tables.CalendarOganizations} " +
                                            $"Where OrganizationsId=@OrganizationsId and Name=@Name and " +
                                            $"Id not in(@calendarOganizationsId)",
                                            "OrganizationsId,Name,calendarOganizationsId",
                                            new object[] { _Login.organizationsId, organizationsName, calendarOganizationsId })._ToInt16() == 0;
    }

    public static bool CheckCalendarOrganizationsShortName(int calendarOganizationsId, string organizationsShortName)
    {

        return GetSingleValuesBySqlCommand($"Select COUNT(*) Count From {Tools.Tables.CalendarOganizations} " +
                                         $"Where OrganizationsId=@OrganizationsId and ShortName=@ShortName and " +
                                         $"Id not in(@calendarOganizationsId)",
                                         "OrganizationsId,ShortName,calendarOganizationsId",
                                         new object[] { _Login.organizationsId, organizationsShortName, calendarOganizationsId })._ToInt16() == 0;
        // return GetSingleValues("COUNT(*) Count", Tools.Tables.CalendarOganizations, "OrganizationsId,Shortname", new object[] { _Login.organizationsId, organizationsShortName })._ToInt16() == 0;
    }

    public static int InsertFileUpload(int filesFoldersId, int filesTypesId, string path, string fileName, int contentLength)
    {
        var Dictionary = new Dictionary<string, object>()
        {
            {"FilesFoldersId",filesFoldersId},
            {"FilesTypesId",filesTypesId},
            {"Path",path},
            {"Name",fileName},
            {"ContentLength",contentLength},
            {"IsActive",true},
            {"CreatedDate",DateTime.Now},
        };

        return InsertDatabase(Tools.Tables.Files, Dictionary);
    }

    public static int DeleteFiles(Tools.Tables tableName, int filesId)
    {
        using (SqlCommand com = new SqlCommand())
        {
            com.CommandText = $"Delete From " +
                  $"{tableName} " +
                  $"Where Id=@ProductsId";
            com.Parameters.Add("@ProductsId", SqlDbType.Int).Value = filesId;
            com.Connection = SqlConn;

            try
            {
                com.Connection.Open();
                return com.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                DALC.ErrorLog(string.Format("DALCA.DeleteFiles [{0}] catch error: {1}", tableName, er.Message));
                return -1;
            }
            finally
            {
                com.Connection.Close();
                com.Connection.Dispose();
            }
        }
    }

    public static void ErrorLog(string LogText, bool IsSendMail = false)
    {
        DALC.ErrorLog(LogText, HttpContext.Current.Request.Url.ToString(), HttpContext.Current.Request.UserHostAddress.ToString(), IsSendMail);
    }
}