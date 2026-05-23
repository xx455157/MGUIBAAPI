#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewPY;
using GUIStd.Extensions;
using GUIStd.Models;
using GUIStd.DAL.AllNewPY.Models;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// ARTHPY.PN 執行業務資料控制器（vPYTBM08），路由 py/professionalpractice
    /// </summary>
    [Route("py/professionalpractice")]
    public class ProfessionalPracticeController : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlPN BlPN => mBlPN = mBlPN ?? new BlPN(ClientContent);
        private BlPN mBlPN;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得分頁執行業務資料（可選：業務代號起迄、名稱關鍵字、所得類別代碼）
        /// </summary>
        [HttpPost("query/pages/{pageNo}")]
        public MdPN_p GetData(
            [DARange(1, int.MaxValue)] int pageNo,
            int rowsPerPage = 0,
            string bizCodeStart = null,
            string bizCodeEnd = null,
            string bizNameKeyword = null,
            string incomeCategoryCode = null)
        {
            return BlPN.GetData(
                ControlName,
                pageNo,
                ref rowsPerPage,
                bizCodeStart,
                bizCodeEnd,
                bizNameKeyword,
                incomeCategoryCode);
        }

        /// <summary>
        /// 判斷執行業務代號是否已存在（須在單筆路由之前註冊）
        /// </summary>
        /// <param name="practiceCode">執行業務代號（PN01）</param>
        [HttpGet("exists/{practiceCode}")]
        public bool IsExist(string practiceCode)
        {
            return BlPN.IsExist(practiceCode);
        }

        /// <summary>
        /// 取得單筆執行業務
        /// </summary>
        /// <param name="practiceCode">執行業務代號（PN01）</param>
        [HttpGet("{practiceCode}")]
        public MdPN GetRow(string practiceCode)
        {
            return BlPN.GetRow(practiceCode);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增執行業務
        /// </summary>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdPN obj)
        {
            try
            {
                int _result = BlPN.ProcessInsert(obj);
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改執行業務
        /// </summary>
        /// <param name="practiceCode">原執行業務代號（路徑與 Body 鍵值須一致）</param>
        [HttpPut("{practiceCode}")]
        public MdApiMessage Update(string practiceCode, [FromBody] MdPN obj)
        {
            if (!practiceCode.EqualsIgnoreCase(obj.PN01))
                return HttpContext.Response.UpdateFailedWhenKeyNotSame();

            try
            {
                int _result = BlPN.ProcessUpdate(practiceCode, obj);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除執行業務
        /// </summary>
        /// <param name="practiceCode">執行業務代號（PN01）</param>
        [HttpDelete("{practiceCode}")]
        public MdApiMessage Delete(string practiceCode)
        {
            try
            {
                int _result = BlPN.ProcessDelete(practiceCode);
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
