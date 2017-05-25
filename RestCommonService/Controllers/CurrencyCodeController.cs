using RestCommonService.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace RestCommonService.Controllers
{
    public class CurrencyCodeController : ApiController
    {
        public IHttpActionResult Get()
        {
            DataSet dsResult = new DataSet();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["DbConnection"]))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT Code, CurrencyName FROM CurrencyCode ORDER BY Code ASC";
                cmd.CommandType = CommandType.Text;

                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dsResult);
            }

            List<CurrencyModel> currencyLst = new List<CurrencyModel>();

            foreach (DataRow dr in dsResult.Tables[0].Rows)
            {
                currencyLst.Add(new CurrencyModel()
                {
                    Name = dr["CurrencyName"].ToString(),
                    Code = dr["Code"].ToString()
                });
            }

            return Ok(currencyLst);
        }
    }
}