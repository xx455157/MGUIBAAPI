#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewPY;
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.Extensions;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// ARTHPY.PO 銀行資料控制器（vPYTBM09），路由 py/bank
    /// </summary>
    [Route("py/bank")]
    public class BankController : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlPO BlPO => mBlPO = mBlPO ?? new BlPO(ClientContent);
        private BlPO mBlPO;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得分頁銀行資料（可選：銀行代號起迄、名稱關鍵字）
        /// </summary>
        [HttpPost("query/pages/{pageNo}")]
        public MdPO_p GetData(
            [DARange(1, int.MaxValue)] int pageNo,
            int rowsPerPage = 0,
            string bankCodeStart = null,
            string bankCodeEnd = null,
            string bankNameKeyword = null)
        {
            return BlPO.GetData(
                ControlName,
                pageNo,
                ref rowsPerPage,
                bankCodeStart,
                bankCodeEnd,
                bankNameKeyword);
        }

        /// <summary>
        /// 判斷銀行代號是否已存在（須在單筆路由之前註冊）
        /// </summary>
        /// <param name="bankCode">銀行代號（PO01）</param>
        [HttpGet("exists/{bankCode}")]
        public bool IsExist(string bankCode)
        {
            return BlPO.IsExist(bankCode);
        }

        /// <summary>
        /// 取得單筆銀行
        /// </summary>
        /// <param name="bankCode">銀行代號（PO01）</param>
        [HttpGet("{bankCode}")]
        public MdPO GetRow(string bankCode)
        {
            return BlPO.GetRow(bankCode);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增銀行
        /// </summary>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdPO obj)
        {
            try
            {
                int _result = BlPO.ProcessInsert(obj);
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改銀行
        /// </summary>
        /// <param name="bankCode">原銀行代號（路徑與 Body 鍵值須一致）</param>
        [HttpPut("{bankCode}")]
        public MdApiMessage Update(string bankCode, [FromBody] MdPO obj)
        {
            if (!bankCode.EqualsIgnoreCase(obj.PO01))
                return HttpContext.Response.UpdateFailedWhenKeyNotSame();

            try
            {
                int _result = BlPO.ProcessUpdate(bankCode, obj);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除銀行
        /// </summary>
        /// <param name="bankCode">銀行代號（PO01）</param>
        [HttpDelete("{bankCode}")]
        public MdApiMessage Delete(string bankCode)
        {
            try
            {
                int _result = BlPO.ProcessDelete(bankCode);
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
