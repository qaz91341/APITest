using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using WebApplication2.Data;
using System.Web.Mvc;

namespace WebApplication2.Models
{
    [Table("APIData")]
    public class APIDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int oid { get; set; }
        [Column("MarstTitle")]
        public string Marst_Title { get; set; }
        [Column("MarstContent")]
        public string Marst_Content { get; set; }
    }


    public class APIDataRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public APIDataRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<APIDataModel> GetAPIData()
        {
            return _dbContext.APIData.ToList();
        }

        public String SaveAPIDataToDatabase(List<APIDataModel> APIData)
        {
            try
            {
                _dbContext.APIData.AddRange(APIData);
                _dbContext.SaveChanges();
                return ("資料已新增完成");
            }
            catch (Exception ex)
            {
                return("新增資料失敗: " + ex.Message);
            }
        }
    }
}
