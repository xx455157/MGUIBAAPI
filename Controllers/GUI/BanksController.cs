#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewGUI;
using GUIStd.BLL.AllNewGUI.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// 銀行基本資料（A16，A1613='3'）資料控制器
    /// </summary>
    [Route("gui/[controller]")]
    public class BanksController : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlBank BlBank => mBlBank = mBlBank ?? new BlBank(ClientContent);
        private BlBank mBlBank;

        /// <summary>vMCFBK 查詢列格式（Insert/Update responseData 用）</summary>
        private BlMCFBK BlMCFBK => mBlMCFBK = mBlMCFBK ?? new BlMCFBK(ClientContent);
        private BlMCFBK mBlMCFBK;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>主鍵存在檢查（A1613='3'）</summary>
        [HttpGet("exists/{bankCode}")]
        public bool IsExist(string bankCode)
        {
            return BlBank.IsExist(bankCode);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>新增銀行資料</summary>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdBank_w obj)
        {
            try
            {
                int _result = BlBank.ProcessInsert(obj);
                return HttpContext.Response.InsertSuccess(
                    affectedRows: _result,
                    responseData: BlMCFBK.GetQueryViewRow(obj.A1601)
                );
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>修改銀行資料</summary>
        [HttpPut]
        public MdApiMessage Update([FromBody] MdBank_w obj)
        {
            try
            {
                int _result = BlBank.ProcessUpdate(obj);
                return HttpContext.Response.UpdateSuccess(
                    affectedRows: _result,
                    responseData: BlMCFBK.GetQueryViewRow(obj.A1601)
                );
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>刪除銀行資料</summary>
        [HttpDelete("{bankCode}")]
        public MdApiMessage Delete(string bankCode)
        {
            try
            {
                int _result = BlBank.ProcessDelete(bankCode);
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion
    }
}
