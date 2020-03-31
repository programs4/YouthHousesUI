<%@ Application Language="C#" %>

<%@ Import Namespace="YouthHousesLibrary" %>

<script RunAt="server">

    void Application_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError().GetBaseException();
        DALC.ErrorLog("YouthHouses Global.asax sehv verdi: " + ex.Message + "  |  Source: " + ex.Source);

        //Master Page-də səhv çıxan da
        if (Request.Url.ToString().ToLower().IndexOf("/error") > -1)
        {
            Response.Write("Error 404");
            Response.End();
        }

        Response.Redirect("/error");
        Response.End();
    }


    void Application_Start(object sender, EventArgs e)
    {
        System.Web.Routing.RouteCollection Collection = new System.Web.Routing.RouteCollection();

        //Main     
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("Login", "modules", "~/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("Main", "{lang}/modules", "~/modules/default.aspx"));

        //News
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("News", "{lang}/modules/news", "~/modules/news/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("NewsPage", "{lang}/modules/news/{pagenum}", "~/modules/news/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("NewsOperations", "{lang}/modules/news/operations/{type}", "~/modules/news/operations/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("NewsOperationsEdit", "{lang}/modules/news/operations/{type}/{id}", "~/modules/news/operations/default.aspx"));

        //Surveys
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("Surveys", "{lang}/modules/surveys", "~/modules/surveys/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("SurveysPage", "{lang}/modules/surveys/{pagenum}", "~/modules/surveys/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("SurveysOperations", "{lang}/modules/surveys/operations/{type}/{id}", "~/modules/surveys/operations/default.aspx"));

        //Xidmətlər
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("Services", "{lang}/modules/services/{servicestypesid}", "~/modules/services/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("ServicesOperations", "{lang}/modules/services/operations/{servicestypesid}/{type}/{id}", "~/modules/services/operations/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("ServicesOrganizations", "{lang}/modules/services/operations/organizations", "~/modules/services/organizations/default.aspx"));

        //Tədbirlər
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("Conferences", "{lang}/modules/conferences", "~/modules/conferences/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("ConferencesOperations", "{lang}/modules/conferences/operations/{type}/{id}", "~/modules/conferences/operations/default.aspx"));


        //Qeydiyyat
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("Registration", "{lang}/modules/registration/", "~/modules/registration/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("RegistrationOperations", "{lang}/modules/registration/operations/{type}/{id}", "~/modules/registration/operations/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("ListServicesUsed", "{lang}/modules/registration/listservicesused/{id}", "~/modules/registration/listservicesused/default.aspx"));

        //Devices
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("Devices", "{lang}/modules/devices/", "~/modules/devices/default.aspx"));

        //AdministratorsList
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("Administrators", "{lang}/modules/administrators", "~/modules/administrators/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("AdministratorsPage", "{lang}/modules/administrators/{pagenum}", "~/modules/administrators/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("AdministratorsOperations", "{lang}/modules/administrators/operations/{type}/{id}", "~/modules/administrators/operations/default.aspx"));

        //Calendar
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("Calendar", "{lang}/modules/calendar", "~/modules/calendar/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("CalendarManagment", "{lang}/modules/calendarmanagment/{type}/{id}", "~/modules/calendarmanagment/default.aspx"));
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("CalendarOrganizations", "{lang}/modules/calendarorganizations", "~/modules/calendarorganizations/default.aspx"));

        //Reports
        System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("Reports", "{lang}/modules/reports/", "~/modules/reports/default.aspx"));
        //System.Web.Routing.RouteTable.Routes.Add(Collection.MapPageRoute("ReportsReportname", "{lang}/modules/reports/{reportname}", "~/modules/reports/default.aspx"));

    }

</script>
