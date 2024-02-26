using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Data; // 引入此命名空間以便使用 ApplicationDbContext
using System.Collections.Generic;
using System.Linq;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;
        private readonly APIDataRepository _apiDataRepo;

        public HomeController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://data.moa.gov.tw/Service/OpenData/TransService.aspx?UnitId=H0YfVLB35Ueh&IsTransData=1");

            ApplicationDbContext dbContext = new ApplicationDbContext();
            _apiDataRepo = new APIDataRepository(dbContext);
        }

        public ActionResult Index()
        {
            try
            {
                var APIData = _apiDataRepo.GetAPIData();
                return View(APIData);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        public ActionResult SendRequest()
        {
            // 取得API資料
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress).Result;
            //確認是否取得成功回傳
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                List<APIDataModel> APIData = JsonConvert.DeserializeObject<List<APIDataModel>>(data);

                var oldData = _apiDataRepo.GetAPIData();
                // 比較 APIData 和 OldData，找出相同的項目並從 APIData 中刪除
                foreach (var oldItem in oldData)
                {
                    var matchedItem = APIData.FirstOrDefault(item => item.Marst_Title == oldItem.Marst_Title && item.Marst_Content == oldItem.Marst_Content);
                    if (matchedItem != null)
                    {
                        APIData.Remove(matchedItem);
                    }
                }
                ViewBag.Message = _apiDataRepo.SaveAPIDataToDatabase(APIData);
                //重撈一次資料庫資料回傳給view
                var allData = _apiDataRepo.GetAPIData().ToList();

                return View("Index", allData);
            }
            else
            {
                ViewBag.Message = "獲取資料失敗.";
                return View("Index");
            }
        }

        
    }
}
