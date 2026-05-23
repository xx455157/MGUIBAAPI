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
    /// 【需經驗證】ARTHPY.PD 所得類別資料控制器（vPYTBM01），路由 py/incomecategory
    /// </summary>
    [Route("py/incomecategory")]
    public class IncomeCategoryController : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlPD BlPD => mBlPD = mBlPD ?? new BlPD(ClientContent);
        private BlPD mBlPD;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得分頁所得類別資料
        /// </summary>
        [HttpPost("query/pages/{pageNo}")]
        public MdPD_p GetData([DARange(1, int.MaxValue)] int pageNo, int rowsPerPage = 0)
        {
            return BlPD.GetData(ControlName, pageNo, ref rowsPerPage);
        }

        /// <summary>
        /// 判斷所得類別代碼是否已存在（須在 {categoryCode} 之前註冊，避免 exists 被誤判為代碼）
        /// </summary>
        [HttpGet("exists/{categoryCode}")]
        public bool IsExist(string categoryCode)
        {
            return BlPD.IsExist(categoryCode);
        }

        /// <summary>
        /// 取得單筆所得類別
        /// </summary>
        [HttpGet("{categoryCode}")]
        public MdPD GetRow(string categoryCode)
        {
            return BlPD.GetRow(categoryCode);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增所得類別
        /// </summary>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdPD obj)
        {
            try
            {
                int _result = BlPD.ProcessInsert(obj);
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改所得類別
        /// </summary>
        [HttpPut("{categoryCode}")]
        public MdApiMessage Update(string categoryCode, [FromBody] MdPD obj)
        {
            if (!categoryCode.EqualsIgnoreCase(obj.PD01))
                return HttpContext.Response.UpdateFailedWhenKeyNotSame();

            try
            {
                int _result = BlPD.ProcessUpdate(categoryCode, obj);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除所得類別
        /// </summary>
        [HttpDelete("{categoryCode}")]
        public MdApiMessage Delete(string categoryCode)
        {
            try
            {
                int _result = BlPD.ProcessDelete(categoryCode);
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
