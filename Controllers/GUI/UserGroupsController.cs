#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.Models;
using GUIStd.Extensions;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// 使用者群組資料控制器
    /// </summary>
    [Route("gui/[controller]")]
    public class UserGroupsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 群組商業邏輯物件屬性
        /// </summary>
        private BlA06 BlA06 => mBlA06 = mBlA06 ?? new BlA06(ClientContent);
        private BlA06 mBlA06;

        /// <summary>
        /// 群組成員商業邏輯物件屬性
        /// </summary>
        private BlA62 BlA62 => mBlA62 = mBlA62 ?? new BlA62(ClientContent);
        private BlA62 mBlA62;

        /// <summary>
        /// 程式商業邏輯物件屬性
        /// </summary>
        private BlA10 BlA10 => mBlA10 = mBlA10 ?? new BlA10(ClientContent);
        private BlA10 mBlA10;

        /// <summary>
        /// 群組授權程式商業邏輯物件屬性
        /// </summary>
        private BlA07 BlA07 => mBlA07 = mBlA07 ?? new BlA07(ClientContent);
        private BlA07 mBlA07;

        #endregion

        #region " 共用函式 - 查詢資料 "


        /// <summary>
        /// 取得指定系統的系統功能代碼清單
        /// </summary>
        /// <param name="systemId">系統代號</param>
        /// <returns>系統功能代碼模型集合物件</returns>
        [HttpGet("help/categories/{systemId}")]
        public IEnumerable<MdCode> GetSystemCategories(string systemId)
        {
            return BlA10.GetSystemCategories(systemId);
        }

        /// <summary>
        /// 取得程式資料（分頁）
        /// </summary>
        /// <param name="pageNo">查詢頁次（若 <= 0 表示取得全部資料）</param>
        /// <param name="systemId">系統代號 (A1003)</param>
        /// <param name="categoryId">系統功能代碼 (A1005) - 可選</param>
        /// <param name="queryText">查詢文字（可選，模糊比對 A1001、A1002）</param>
        /// <returns>程式資料分頁模型物件</returns>
        [HttpGet("help/programs/pages/{pageNo}")]
        public MdCode_p GetPrograms(
            [DARange(0, int.MaxValue)] int pageNo,
            [FromQuery][DARequired] string systemId = "",
            [FromQuery] string categoryId = "",
            [FromQuery] string queryText = "")
        {
            return BlA10.GetSHelp(pageNo, ControlName, systemId, categoryId, queryText);
        }


        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="obj">群組資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdCode obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlA06.ProcessInsert(obj);

                // 回應前端新增成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改資料
        /// </summary>
        /// <param name="groupId">群組代號</param>
        /// <param name="obj">群組資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("{groupId}")]
        public MdApiMessage Update(string groupId, [FromBody] MdCode obj)
        {
            // 檢查鍵值路徑參數與本文中的鍵值是否相同
            if (!groupId.EqualsIgnoreCase(obj.Id))
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailedWhenKeyNotSame();
            }

            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlA06.ProcessUpdate(groupId, obj);

                // 回應前端修改成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="groupId">群組代號</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{groupId}")]
        public MdApiMessage Delete(string groupId)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlA06.ProcessDelete(groupId);

                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端刪除失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 複製群組權限資料
        /// </summary>
        /// <param name="obj">複製資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("copy")]
        public MdApiMessage Copy([FromBody] MdCopy obj)
        {
            try
            {
                // 呼叫商業元件執行複製作業
                int _result = BlA06.ProcessCopy(obj.From, obj.To, obj.ToName);

                // 回應前端複製成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端複製失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        #endregion

        #region " 共用函式 - 群組成員 "

        /// <summary>
        /// 取得指定群組的成員清單
        /// </summary>
        /// <param name="groupId">群組代號</param>
        /// <returns>使用者資料模型集合物件</returns>
        [HttpGet("members/help/{groupId}")]
        public IEnumerable<MdUser> GetMembers(string groupId)
        {
            return BlA62.GetMembers(groupId);
        }

        /// <summary>
        /// 新增群組成員資料（支援單筆或多筆）
        /// </summary>
        /// <param name="groupId">群組代號</param>
        /// <param name="request">新增成員請求模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("members/{groupId}")]
        public MdApiMessage InsertMembers(string groupId, [FromBody] MdAddMembersRequest request)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlA62.ProcessInsert(groupId, request.EmployeeIds);

                // 回應前端新增成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 刪除群組成員資料（支援單筆或多筆）
        /// </summary>
        /// <param name="groupId">群組代號</param>
        /// <param name="request">刪除成員請求模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("members/{groupId}")]
        public MdApiMessage DeleteMembers(string groupId, [FromBody] MdAddMembersRequest request)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlA62.ProcessDelete(groupId, request.EmployeeIds);

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


        #region " 共用函式 - 群組授權 "

        /// <summary>
        /// 取得群組授權程式清單（分頁）
        /// </summary>
        /// <param name="groupId">群組代號</param>
        /// <param name="pageNo">查詢頁次（若 <= 0 表示不分頁）</param>
        /// <param name="systemId">系統代號 (A1003)</param>
        /// <param name="categoryId">系統功能代碼 (A1005) - 可選</param>
        /// <param name="programId">程式代號 (A1001) - 可選</param>
        /// <param name="onlyAuthorized">是否只查詢已授權的資料（Y/N，預設N）</param>
        /// <returns>程式授權資料分頁模型物件</returns>
        [HttpGet("programs/{groupId}")]
        public MdProgramAuth_p GetGroupPrograms(
            string groupId,
            [FromQuery][DARange(0, int.MaxValue)] int pageNo = 0,
            [FromQuery] string systemId = "",
            [FromQuery] string categoryId = "",
            [FromQuery] string programId = "",
            [FromQuery] string onlyAuthorized = "N")
        {
            return BlA10.GetGroupPrograms(pageNo, ControlName, groupId, systemId, categoryId, programId, onlyAuthorized);
        }

        /// <summary>
        /// 更新群組授權程式清單
        /// </summary>
        /// <param name="groupId">群組代號</param>
        /// <param name="programs">程式授權資料集合</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("programs/{groupId}")]
        public MdApiMessage UpdateGroupPrograms(string groupId, [FromBody] List<MdProgramAuth> programs)
        {
            try
            {
                // 呼叫商業元件執行更新作業
                int _result = BlA07.ProcessUpdateAuth(groupId, programs);

                // 回應前端更新成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端更新失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion

    }
}


