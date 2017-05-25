using RestCommonService.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace RestCommonService.Controllers
{
    public class CountryController : ApiController
    {
        public IHttpActionResult Get()
        {
            DataSet dsResult = new DataSet();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["DbConnection"]))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT CountryOrAreaName, ISOALPHA2Code, ISOALPHA3Code, ISONumericCode FROM CountryCode ORDER BY CountryOrAreaName ASC";
                cmd.CommandType = CommandType.Text;

                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dsResult);
            }

            List<CountryModel> countryLst = new List<CountryModel>();

            foreach (DataRow dr in dsResult.Tables[0].Rows)
            {
                countryLst.Add(new CountryModel()
                {
                    Name = dr["CountryOrAreaName"].ToString(),
                    ISOALPHA2Code = dr["ISOALPHA2Code"].ToString(),
                    ISOALPHA3Code = dr["ISOALPHA3Code"].ToString(),
                    ISONumericCode = dr["ISONumericCode"].ToString()
                });
            }

            return Ok(countryLst);
        }
    }
}