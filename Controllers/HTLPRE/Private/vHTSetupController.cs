#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.DAL.Base.Models.Reports;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewHTL.Models;
using GUIStd.DAL.AllNewHTL.Models.Private.vHTSetup;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
    /// <summary>
    /// vHTSetup程式資料控制器
    /// </summary>
    [Route("htlpre/private/[controller]")]
	public class vHTSetupController : GUIAppAuthController
    {
        #region " 私用屬性 "
        
        private BlConfigs BlConfigs => new BlConfigs(ClientContent);
        private BlHTCA BlHTCA => new BlHTCA(ClientContent);


        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 飯店基本資料設定UIData
        /// </summary>
        [HttpGet("page")]
        public MdHTSetup_h GetUIData()
        {
            return BlConfigs.GetHTSetupUiData();
        }

        /// <summary>
        /// 旅館營業資料UIData
        /// </summary>
        [HttpGet("page/hotelinfo")]
        public MdHTSetupHotelInfo_h GetUIDataForHotelInfo()
        {
            return BlConfigs.GetHTSetupUiDataForHotelInfo();
        }

        /// <summary>
        /// 出納科目篩選器UIData
        /// </summary>
        /// <returns></returns>
        [HttpGet("page/account")]
        public MdHTSetupAccount_h GetUIDataForAccount()
        {
            return BlConfigs.GetHTSetupUiDataAccount();
        }

        [HttpGet("page/code")]
        public MdHTSetupCode_h GetUIDataForCode()
        {
            return BlConfigs.GetHTSetupUiDataCode();
        }

        #endregion

        #region " 共用函式 - 報表查詢 "

        /// <summary>
        /// 產生報表檔
        /// </summary>
        /// <param name="obj">查詢條件的模型物件</param>
        /// <returns>報表檔案的資料流</returns>
        [HttpPost("report/account")]
        public async Task<IActionResult> GetReport([FromBody] MdReportQuery<MdAccount_q> obj)
        {
            // 建立報表
            var _info = await BlHTCA.GetReport(obj);

            // 回傳報表檔案
            if (_info.Contents != null)
                return HttpContext.Response.SendFile(_info.Contents, _info.FileName);

            // 回傳報表作業失敗及錯誤訊息
            if (!string.IsNullOrWhiteSpace(_info.ErrorMessage))
                return BadRequest(HttpContext.Response.SendReportFailed(_info.ErrorMessage));

            // 回傳查無符合條件資料
            return BadRequest(HttpContext.Response.SendReportNoQueryData());
        }


        #endregion

    }
}
