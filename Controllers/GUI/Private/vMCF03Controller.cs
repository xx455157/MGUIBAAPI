#region " 匯入的名稱空間：Framework "

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.Models;
using GUIStd.BLL.AllNewGUI.Private;
using GUIStd.DAL.GUI.Models.Private.vSCR01;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewIV.Models;
using GUIStd.BLL.AllNewIV.Private;
using GUIStd.DAL.AllNewIV.Models.Private.vMCF02G;
using GUIStd.DAL.AllNewGUI.Models.Private.vMCF02G;
using GUIStd.DAL.AllNewGUI.Models.Private.GUI.vMCF03;
using GUIStd.DAL.AllNewGUI.Models.Private.GUI.vGLM01;

//GUIStd.BLL.AllNewIV.Private

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// 編碼原則 資料維護 程式資料控制器
    /// </summary>
    [Route("gui/private/[controller]")]
    public class vMCF03Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>BlGLM01 商業邏輯物件屬性</summary>
        private BlMCF03 BlMCF03 => mMCF03 = mMCF03 ?? new BlMCF03(ClientContent);
        private BlMCF03 mMCF03;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        ///  取得畫面選項資料
        /// </summary>
        /// <returns></returns>
        [HttpGet("query/uidata")]
        public MdMCF03_h GetUIData()
        {
            return BlMCF03.GetMCF03_UIData();
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 複製編碼原則到其他公司
        /// </summary>
        /// <param name="request">複製請求參數</param>
        /// <returns>系統規範訊息物件（包含複製結果）</returns>
        [HttpPost("copy")]
        public MdApiMessage RuleCopy([FromBody] MdMCF03CopyRequest request)
        {
            try
            {
                // 呼叫商業元件執行複製作業
                MdMCF03CopyResult _result = BlMCF03.ProcessRuleCopy(request);
                // 回應前端複製成功訊息
                return HttpContext.Response.UpdateSuccess(
                    affectedRows: _result.TotalCount,
                    responseData: _result
                );
            }
            catch (Exception ex)
            {
                // 回應前端複製失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion

    }
}
