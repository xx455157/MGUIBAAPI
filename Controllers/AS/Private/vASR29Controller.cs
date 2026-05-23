#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd;
using GUIStd.Attributes;
using GUIStd.DAL.Base.Models.Reports;
using GUIStd.DAL.AllNewAS.Models.Private.vASR27;
using GUIStd.DAL.AllNewAS.Models.Private.Apportionment;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASR29分攤折舊程式資料控制器
    /// </summary>
    [Route("as/private/[controller]")]
	public class vASR29Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlApportionment BlApportionment => new BlApportionment(ClientContent);

        #endregion

        #region " 共用屬性 "

        /// <summary>
        /// 報表的系統代號
        /// </summary>
        public override Enums.WebSystem SystemId { get; set; } = Enums.WebSystem.AS;

        #endregion

        #region " 共用函式 - 查詢資料 "


        /// <summary>
        /// 查詢頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("page")]
        public MdASR27_h GetUIData()
        {
            return BlApportionment.GetUIData<MdASR27_h>();
        }

        /// <summary>
        /// 產生報表檔
        /// </summary>
        /// <param name="obj">查詢條件的模型物件</param>
        /// <returns>報表檔案的資料流</returns>
        [HttpPost("report")]
        public async Task<IActionResult> GetReport([FromBody] MdReportQuery<MdApportTrans_q> obj)
        {
            obj.Query.IsCultureEN = (CurrentUILang == Common.CultureEN ? "1" : "0");
            // 建立報表
            var _info = await BlApportionment.GetReport(obj);

            // 回傳報表檔案
            if (_info.Contents != null)
            {
                return HttpContext.Response.SendFile(_info.Contents, _info.FileName);
            }
            // 回傳報表作業失敗及錯誤訊息
            if (!string.IsNullOrWhiteSpace(_info.ErrorMessage))
                return BadRequest(HttpContext.Response.SendReportFailed(_info.ErrorMessage));

            // 回傳查無符合條件資料
            return BadRequest(HttpContext.Response.SendReportNoQueryData());
        }


        #endregion


    }
}
