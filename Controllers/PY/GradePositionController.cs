#region " 匯入的名稱空間：Framework "

using System;
using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewPY;
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// PM／PL 職等職務與職務級距薪資（vPYTBM06），路由 py/gradeposition
    /// </summary>
    [Route("py/gradeposition")]
    public class GradePositionController : GUIAppAuthController
    {
        private BlPM BlPM => mBlPM = mBlPM ?? new BlPM(ClientContent);
        private BlPM mBlPM;

        /// <summary>
        /// PM 分頁查詢
        /// </summary>
        /// <param name="pageNo">頁碼（自 1 起）。</param>
        /// <param name="gradeCodeStart">職等代碼起（Query）。</param>
        /// <param name="gradeCodeEnd">職等代碼迄（Query）。</param>
        /// <param name="jobTitle">職稱關鍵字（Query）。</param>
        /// <param name="rowsPerPage">每頁筆數（Query，0 表示由伺服器決定）。</param>
        [HttpPost("query/pages/{pageNo}")]
        public MdPosition_p GetData(
            [DARange(1, int.MaxValue)] int pageNo,
            [FromQuery] string gradeCodeStart,
            [FromQuery] string gradeCodeEnd,
            [FromQuery] string jobTitle,
            [FromQuery] int rowsPerPage = 0)
        {
            var rpp = rowsPerPage;
            return BlPM.GetData(
                ControlName,
                pageNo,
                ref rpp,
                gradeCodeStart,
                gradeCodeEnd,
                jobTitle,
                rowsPerPage);
        }

        /// <summary>
        /// 單筆 PM 與 PL 子列（主鍵 PM01、PM02）；路徑參數與 Pattern 客戶主檔 GetRow 類似。
        /// </summary>
        /// <param name="jobClass">職等代碼（PM01）。</param>
        /// <param name="jobTitleId">職務代號（PM02）。</param>
        [HttpGet("detail/{jobClass}/{jobTitleId}")]
        public MdGradePositionDetail GetRow(string jobClass, string jobTitleId)
        {
            return BlPM.GetRow(jobClass, jobTitleId);
        }

        /// <summary>
        /// 職等代碼＋職務代號組合是否已存在（供前端即時檢核；路徑字面 <c>exists</c> 避免與明細路由混淆）。
        /// </summary>
        /// <param name="jobClass">職等代碼（PM01）。</param>
        /// <param name="jobTitleId">職務代號（PM02）。</param>
        [HttpGet("exists/{jobClass}/{jobTitleId}")]
        public bool Exists(string jobClass, string jobTitleId)
        {
            return BlPM.IsExist(jobClass, jobTitleId);
        }

        /// <summary>
        /// 依 PM01、PM02 刪除一筆 PM 及其底下全部 PL（職務級距薪資）子列。
        /// </summary>
        /// <param name="jobClass">職等代碼（PM01）。</param>
        /// <param name="jobTitleId">職務代號（PM02）。</param>
        [HttpDelete("{jobClass}/{jobTitleId}")]
        public MdApiMessage Delete(string jobClass, string jobTitleId)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlPM.ProcessDelete(jobClass, jobTitleId);
                // 回傳刪除結果
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回傳刪除失敗
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 新增 PM 與 PL（與 py/bank、py/paycodes 之 insert 路由風格一致）。
        /// </summary>
        [HttpPost("insert")]
        public MdApiMessage Insert([FromBody] MdGradePositionSaveBody obj)
        {
            try
            {
                int _result = BlPM.ProcessInsert(obj);
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改 PM 與 PL；路徑為編輯前職等／職務代號（PM01、PM02），Body 可含變更後主鍵。
        /// </summary>
        /// <param name="jobClass">編輯前職等代碼（PM01）。</param>
        /// <param name="jobTitleId">編輯前職務代號（PM02）。</param>
        [HttpPut("{jobClass}/{jobTitleId}")]
        public MdApiMessage Update(string jobClass,string jobTitleId,
            [FromBody] MdGradePositionSaveBody obj)
        {
            try
            {
                int _result = BlPM.ProcessUpdate(jobClass, jobTitleId, obj);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }
    }
}
