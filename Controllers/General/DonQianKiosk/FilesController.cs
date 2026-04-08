#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewHTL.Models;
using GUIStd.Models;
using GUICore.Web;
using GUIStd;

#endregion

namespace MGUIBAAPI.Controllers.General.DonQianKiosk
{
	/// <summary>
	/// 【需經驗證】敦謙自助報到機 - 檔案管理控制器
	/// </summary>
	[Route("general/DonQianKiosk/[controller]")]
	public class FilesController : GUIAppWSController
	{
		#region " 建構子 "

		/// <summary>
		/// 建構子
		/// </summary>
		public FilesController()
		{
			// 改變執行服務的使用者帳號為 KIOSK
			this.WSUser = "KIOSK";
		}

		#endregion

		#region " 私用屬性 "

		/// <summary>
		/// 商業邏輯物件屬性
		/// </summary>
		private BlKiosk BlKiosk => new BlKiosk(ClientContent);

		#endregion

		#region " 共用函式 - 查詢資料 "

		// 此控制器無查詢功能

		#endregion

		#region " 共用函式 - 異動資料 "

		/// <summary>
		/// 上傳旅客證件掃描資料及 OCR 辨識結果
		/// </summary>
		/// <param name="domain">飯店代號（選填）</param>
		/// <param name="booking_number">訂單號碼或住宿碼</param>
		/// <param name="obj">OCR 掃描資料物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPost]
		public async Task<MdApiMessage> UploadOCRData(
			[FromQuery] string domain,
			[FromQuery] string booking_number,
			[FromForm] MdKioskOCR obj)
		{
			try
			{
				// 將上傳的檔案放入系統共用模型物件的 FormFiles 屬性中
				var _formFiles = new MdFormData()
				{
					FormFiles = obj.image
				};

				// 將所有圖檔轉換為 Base64 編碼格式
				var _files = await WebFunc.ConvertFormFileToBase64Image(_formFiles);

				// 呼叫商業元件儲存 OCR 檔案資料
				int _result = BlKiosk.SaveOCRFiles(booking_number, obj.OCR, _files.ToArray()[0]);

				// 回應前端上傳成功訊息
				return HttpContext.Response.InsertSuccess(_result);
			}
			catch (Exception ex)
			{
				// 回應前端上傳失敗訊息
				return HttpContext.Response.InsertFailed(ex);
			}
		}

		#endregion
	}
}
