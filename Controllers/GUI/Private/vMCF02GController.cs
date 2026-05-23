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

//GUIStd.BLL.AllNewIV.Private

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// vGLM01 大部門資料維護 程式資料控制器
    /// </summary>
    [Route("gui/private/[controller]")]
    public class vMCF02GController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>BlGLM01 商業邏輯物件屬性</summary>
        private BlMCF02G BlvMCF02G => mvMCF02G = mvMCF02G ?? new BlMCF02G(ClientContent);
        private BlMCF02G mvMCF02G;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        ///  取得 qv 畫面選項資料
        /// </summary>
        /// <returns></returns>
        [HttpGet("query/uidata/qv")]
        public MdMCF02G_h GetUIData()
        {
            return BlvMCF02G.GetUIData();
        }


        /// <summary>
        ///  取得 d 畫面選項資料
        /// </summary>
        /// <returns></returns>
        [HttpGet("query/uidata/d")]
        public MdMCF02G_h2 GetUIData2()
        {
            return BlvMCF02G.GetUIData2();
        }

        /// <summary>
        /// 取得查詢資料（分頁）
        /// </summary>
        /// <param name="pageNo">頁碼（最小值 1）</param>
        /// <param name="rowsPerPage">一頁筆數</param>
        /// <param name="queryParams">查詢參數</param>
        /// <returns>分頁查詢結果</returns>
        [HttpPost("query/pages/{pageNo}")]
        public MdMCF02GQueryList_p GetQueryList([DARange(1, int.MaxValue)] int pageNo, [FromBody] MdMCF02GQuery queryParams, int rowsPerPage = 0)
        {
            return BlvMCF02G.GetQueryList(queryParams, ControlName, pageNo, ref rowsPerPage);
        }

        /// <summary>
        /// 取得 大部門 明細資料
        /// </summary>
        /// <param name="item">大部門主鍵</param>
        /// <returns>明細資料</returns>
        [HttpPost("query/detail")]
        public MdMCF02GDetailView GetDetailView([FromBody] MdDeptGroupPkey item)
        {
            return BlvMCF02G.GetDetailView(item);
        }

        /// <summary>
        /// 取得 大部門所屬部門資料
        /// </summary>
        /// <param name="deptgroup">大部門</param>
        /// <returns>明細資料</returns>
        [HttpGet("query/deptgroups")]
        public IEnumerable<MdMCF02GDeptGroups> GetDeptGroups(string deptgroup)
        {
            return BlvMCF02G.GetDeptGroups(deptgroup);
        }   

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增大部門資料
        /// </summary>
        /// <param name="obj">大部門資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdDeptGroup obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlvMCF02G.ProcessInsert(obj);
                // 回應前端新增成功訊息
                return HttpContext.Response.InsertSuccess(
                    affectedRows: _result,
                    responseData: BlvMCF02G.GetQueryViewRow(obj)
                );
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改大部門資料
        /// </summary>
        /// <param name="obj">大部門資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut]
        public MdApiMessage Update([FromBody] MdDeptGroup obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlvMCF02G.ProcessUpdate(obj);
                // 回應前端修改成功訊息
                return HttpContext.Response.UpdateSuccess(
                    affectedRows: _result,
                    responseData: BlvMCF02G.GetQueryViewRow(obj)
                );
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 修改大部門所屬部門資料
        /// </summary>
        /// <param name="obj">大部門資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("update/deptgroups")]
        public MdApiMessage UpdateDepts([FromBody] MdMCF02GDepts obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlvMCF02G.ProcessUpdateDepts(obj);
                // 回應前端修改成功訊息
                return HttpContext.Response.UpdateSuccess(affectedRows: _result);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除大部門資料
        /// </summary>
        /// <param name="key">大部門主鍵</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete]
        public MdApiMessage Delete([FromBody] MdDeptGroupPkey key)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlvMCF02G.ProcessDelete(key);

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

    }
}
