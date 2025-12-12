#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using System.Collections.Generic;
using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewHTL.Models;
using GUIStd.Models;
using GUIStd.DAL.AllNewHTL.Models.Private.QRCodeLangs;
using GUIStd.BLL.AllNewHTL.Private;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Excel
{
    /// <summary>
    /// 多語系資料 控制器
    /// </summary>
    [Route("htlpre/qrcode/[controller]")]
    public class LangsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlHTLA BlHTLA => new BlHTLA(ClientContent);
        private BlQRCodeLangs BlQRCodeLangs => new BlQRCodeLangs(ClientContent);

        #endregion

        #region " 共用函式 -  查詢資料 "

        /// <summary>
		/// 取得QRCode相關語系資料
		/// </summary>
		/// <param name="obj">查詢條件物件</param>
		/// <returns>QRCODE語系模型泛型集合物件</returns>
		[HttpPost("export")]
        public IEnumerable<MdTypeLang> GetHelp([FromBody] MdExport obj)
        {
            return BlQRCodeLangs.GetData(obj);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增/修改對應語系的資料表
        /// </summary>
        /// <param name="objs">語系之泛型模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("import")]
        public MdApiMessage BulkMerge([FromBody] IEnumerable<MdLang_w> objs)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlHTLA.BulkMerge(objs);

                // 回應前端新增成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion
    }
}
