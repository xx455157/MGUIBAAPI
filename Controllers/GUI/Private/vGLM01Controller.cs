#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.Models;
using GUIStd.BLL.AllNewGUI.Private;
using GUIStd.DAL.AllNewGUI.Models.Private.GUI.vGLM01;
using GUIStd.DAL.GUI.Models.Private.vSCR01;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewIV.Models;
using GUIStd.BLL.AllNewIV.Private;
using System.Collections.Generic;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// vGLM01 會計科目維護 程式資料控制器
    /// </summary>
    [Route("gui/private/[controller]")]
    public class vGLM01Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>BlGLM01 商業邏輯物件屬性</summary>
        private BlGLM01 BlGLM01 => mBlGLM01 = mBlGLM01 ?? new BlGLM01(ClientContent);
        private BlGLM01 mBlGLM01;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        ///  取得 qv 畫面選項資料
        /// </summary>
        /// <returns></returns>
        [HttpGet("query/uidata/qv")]
        public MdGLM01_h GetUIData()
        {
            return BlGLM01.GetUIData();
        }


        /// <summary>
        ///  取得 d 畫面選項資料
        /// </summary>
        /// <returns></returns>
        [HttpGet("query/uidata/d")]
        public MdGLM01_h2 GetUIData2()
        {
            return BlGLM01.GetUIData2();
        }

        /// <summary>
        /// 取得查詢資料（分頁）
        /// </summary>
        /// <param name="pageNo">頁碼（最小值 1）</param>
        /// <param name="rowsPerPage">一頁筆數</param>
        /// <param name="queryParams">查詢參數</param>
        /// <returns>分頁查詢結果</returns>
        [HttpPost("query/pages/{pageNo}")]
        public MdGLM01QueryList_p GetQueryList([DARange(1, int.MaxValue)] int pageNo, [FromBody] MdGLM01Query queryParams, int rowsPerPage = 0)
        {
            return BlGLM01.GetQueryList(queryParams, ControlName, pageNo, rowsPerPage);
        }

        /// <summary>
        /// 取得 會計科目 明細資料
        /// </summary>
        /// <param name="item">會計科目</param>
        /// <returns>明細資料（科目 + 財務屬性）</returns>
        [HttpPost("query/detail")]
        public MdGLM01DetailView GetDetailView([FromBody] MdAccountingCodePkey item)
        {
            return BlGLM01.GetDetailView(item);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增 會計科目 資料
        /// </summary>
        /// <param name="obj">會計科目資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdGLM01DetailData obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlGLM01.ProcessInsert(obj);
                // 回應前端新增成功訊息
                return HttpContext.Response.InsertSuccess(
                    affectedRows: _result,
                    responseData: BlGLM01.GetQueryViewRow(obj)
                );
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改 會計科目 資料
        /// </summary>
        /// <param name="obj">會計科目資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut]
        public MdApiMessage Update([FromBody] MdGLM01DetailData obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlGLM01.ProcessUpdate(obj);
                // 回應前端修改成功訊息
                //return HttpContext.Response.UpdateSuccess(_result);
                return HttpContext.Response.UpdateSuccess(
                    affectedRows: _result,
                    responseData: BlGLM01.GetQueryViewRow(obj)
                );
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除 會計科目 資料
        /// </summary>
        /// <param name="key">會計科目資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete]
        public MdApiMessage Delete([FromBody] MdAccountingCodePkey key)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlGLM01.ProcessDelete(key);

                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端刪除失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion

        #region " 共用函式 - 複製科目 "

        /// <summary>
        /// 複製會計科目到其他公司
        /// </summary>
        /// <param name="request">複製請求參數</param>
        /// <returns>系統規範訊息物件（包含複製結果）</returns>
        [HttpPost("copy")]
        public MdApiMessage CopyAccounts([FromBody] MdGLM01CopyRequest request)
        {
            try
            {
                // 呼叫商業元件執行複製作業
                MdGLM01CopyResult _result = BlGLM01.ProcessCopy(request);
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
