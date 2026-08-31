#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.BLL.GUI;
using GUIStd.DAL.GUI.Models;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.Sys
{
    /// <summary>
    /// 系統登入控制器
    /// </summary>
    [Route("sys/[controller]")]
    public class LoginController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlLogin BlLogin => new BlLogin(ClientContent);
        private BlA08 BlA08 => new BlA08(ClientContent);

        #endregion

        #region " 共用函式 - 查詢 "

        /// <summary>
        /// 使用者帳號登入驗證
        /// </summary>
        /// <param name="obj">登入使用者資訊模型物件</param>
        /// <returns>系統回應模型物件</returns>
        [AllowAnonymous]
        [HttpPost]
        public MdApiMessage DoLogin(MdLoginInfo obj)
        {
            try
            {
                // 取得登入者資料
                var (_result, _failedMessage) = BlLogin.GetRowForLoginUserAsync(obj, FrontKeyFile, SSOKeyFile);

                // 登入失敗，傳送驗證失敗訊息回前端
                if (_result == null)
                    return HttpContext.Response.SendFailed(_failedMessage);

                // 登入成功，傳送使用者資訊及相關授權資料模型物件回前端
                return HttpContext.Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_LoginSuccess"), _result);
            }
            catch (Exception ex)
            {
                // 當出現錯誤時，回傳登入失敗的訊息
                return HttpContext.Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_LoginFailed"), ex);
            }
        }

        /// <summary>
        /// 取得頁面首次載入所需的所有輔助資料
        /// </summary>
        /// <returns>私有頁面輔助資料模型物件</returns>
        [HttpGet("paged")]
        public MdConfig_h GetUIData() => BlLogin.GetUIData();

        #endregion

        #region " 共用函式 - 異動 "


        /// <summary>
        /// 執行密碼變更作業
        /// </summary>
        /// <param name="obj">變更密碼模型物件</param>
        /// <returns>系統回應模型物件</returns>
        [HttpPost("changepassword")]
        public MdApiMessage DoChange(MdChangePwd obj)
        {
            try
            {
                // 取得登入者資料
                var (_result, _failedMessage) = BlA08.ProcessChangePwd(obj, FrontKeyFile);

                // 密碼變更失敗，傳送失敗訊息回前端
                if (!_result)
                    return HttpContext.Response.SendFailed(_failedMessage);

                // 回應前端變更密碼成功訊息
                return HttpContext.Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_ChangePwdSuccess"));
            }
            catch (Exception ex)
            {
                // 當出現錯誤時，回傳變更密碼失敗的訊息
                return HttpContext.Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_ChangePwdFailed"), ex);
            }
        }

        #endregion
    }
}