using INSS.FIP.QA.Automation.TestFramework;
using INSS.FIP.QA.Automation.Helpers;
using System;
using System.Collections.Generic;



namespace INSS.FIP.QA.Automation.Data
{
    public class SqlQueries
    {
        private static readonly string ConnectionString = WebDriverFactory.Config["DBConnectionString"];


        public static void createCI_IPRecord()
        {
            string SQLQuery1 = "  insert into ci_ip(IP_NO, title, initials, surname, forenames, licensing_body, date_first_auth, date_auth_withdrawn, reason_withdrawn, renewal_date, orig_visit_date, planned_visit_date, authorised, no_of_cases, ip_email_address, include_on_internet, registered_firm_name, registered_address_line_1, registered_address_line_2, registered_address_line_3, registered_address_line_4, registered_post_code, registered_phone, registered_fax)";
            string SQLQuery2 = SQLQuery1 + "values(" + Constants.IPNumber + ",'" + Constants.title + "','A','" + Constants.surname +"','" + Constants.firstName + "','" + Constants.authorisingBody +"','1900-01-01 00:00:00.000','1900-01-01 00:00:00.000','','1900-01-01 00:00:00.000','1900-01-01 00:00:00.000','1900-01-01 00:00:00.000','',0,'" + Constants.email + "','Y','" + Constants.companyName + "','11 Test Street','Grenwich','Waltham','" + Constants.town + "', 'B43 6JN','" + Constants.telephone + "','" + Constants.fax + "')";

            SqlDatabaseConncetionHelper.ExecuteSqlCommand(SQLQuery2, ConnectionString);
        }

        public static void DeleteCI_CPRecord()
        {
            string DeleteSQL = "delete from CI_IP where ip_no = 9999";
            SqlDatabaseConncetionHelper.ExecuteSqlCommand(DeleteSQL, ConnectionString);
        }
    }
}
