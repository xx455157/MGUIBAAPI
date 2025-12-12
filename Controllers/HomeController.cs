#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Diagnostics;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd;
using MGUIBAAPI.Models;

#endregion

namespace MGUIBAAPI.Controllers
{
    /// <summary>
    /// 首頁控制器
    /// </summary>
    [SwaggerIgnore]
	public class HomeController : Controller
	{
		/// <summary>
		/// 檢視首頁
		/// </summary>
		public IActionResult Index()
		{
			ViewData["Title"] = "首頁";

			return View();
		}

		/// <summary>
		/// 檢視關於頁面
		/// </summary>
		public IActionResult About()
		{
			ViewData["Title"] = "公司簡介";
			ViewData["Message"] = @"
金旭資訊股份有限公司主要以""資訊服務""為創立之理念，主要提供之服務項目為：
商用套裝軟體建置，資料倉儲建置，ASP，區域網路建置，系統整合，系統整體規劃，電腦化顧問，專案管理。
多年來之努力，已獲得商用市場廣大用戶之肯定，高品質的整體解決方案，與完備的售後服務，確已創造了最佳的客戶滿意度。
同時就以人為本的專業軟體公司而言，積極培訓累積經驗，也促使本公司贏得了同仁的高滿意度。";
			ViewData["Message2"] = @"
今後的努力，本公司將在已有的基礎上追求卓越，運用高水準服務，協助國家既有資訊的提昇，提高資訊服務層面，
使國人更能迅速確實地獲取資訊，使我們的國家在國際市場之競爭能力淵遠流長。
這需要豐富的資訊管理經驗和技術，更要發揮團隊精神群策群力來完成。";
			ViewData["Message3"] = @"
企業的堅實壯大是每一個成員的努力及廣大社會的支持，本公司擁有一流的 技術與服務的水準，願將所有的成就與努力回饋社會，
與大眾共享，今後當以更積極的行動與態度提供資訊服務，引領國人走向資訊時代的領域。";

			return View();
		}

		/// <summary>
		/// 檢視聯繫頁面
		/// </summary>
		public IActionResult Contact()
		{
			ViewData["Title"] = "聯絡我們";
			ViewData["cAddress"] = "地址：";
			ViewData["cPhone"] = "電話：";
			ViewData["cEMail"] = "E-Mail：";
			ViewData["Address"] = Config.AppSettings.CompanyInfo.Address;
			ViewData["Phone"] = Config.AppSettings.CompanyInfo.Phone;
			ViewData["EMail"] = Config.AppSettings.CompanyInfo.ServiceMail;

			return View();
		}

		/// <summary>
		/// 檢視錯誤頁面
		/// </summary>
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
