#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewHTL.Models;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Config
{
    /// <summary>
    /// 班別資料控制器
    /// </summary>
    [Route("htlpre/Config/[controller]")]
    public class ShiftsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlHTSH BlHTSH => new BlHTSH(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得班別資料
        /// </summary>
        /// <param name="bkDate">會計日期（格式: YYYYMMDD）</param>
        /// <param name="typeCode">類型代碼</param>
        /// <param name="workstation">工作站/機台</param>
        /// <returns>班別代碼</returns>
        [HttpGet]
        public string Get([FromQuery] string bkDate, [FromQuery] string typeCode, [FromQuery] string workstation)
        {
            return BlHTSH.GetHotelShift(bkDate, typeCode, workstation);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增班別資料
        /// </summary>
        /// <param name="obj">班別資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdHTSH obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlHTSH.ProcessInsert(obj);

                // 回應前端新增成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        #endregion
    }
}
