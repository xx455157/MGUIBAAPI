#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.Attributes;
using GUIStd.Extensions;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// 程式資料控制器
    /// </summary>
    [Route("gui/[controller]")]
    public class CompaniesController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlA01 BlA01 => new BlA01(ClientContent);

        private BlSINI BlSINI => new BlSINI(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得公司別輔助資料
        /// </summary>
        /// <param name="fullName">是否顯示公司全名,否的話顯示簡稱</param>
        /// <param name="includeEmptyRow">是否包含空白列</param>
        /// <param name="includeId">是否包含代碼</param>
        /// <param name="authorized">是否檢核授權</param>
        /// <param name="system">系統別 GL、IV、PY、AS</param>
        /// <returns>公司別資料代碼模型集合物件</returns>
        [HttpGet("help")]
        public IEnumerable<MdCode> GetHelp([FromQuery] bool fullName = false, [FromQuery] bool includeEmptyRow = false, [FromQuery] bool includeId = false,
             [FromQuery] bool authorized = false, [FromQuery] string system = "")
        {
			return BlA01.GetHelp(fullName, includeEmptyRow, includeId, system, authorized);
        }

        /// <summary>
        /// 取得分頁頁次的公司基本資料
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="queryParams">查詢參數</param>
        /// <returns>分頁公司資料模型物件</returns>
        [HttpPost("query/pages/{pageNo}")]
		public MdCompanies_p GetData([DARange(1, int.MaxValue)] int pageNo,[FromBody] MdCompany_q queryParams)
		{
            return BlA01.GetData(
				string.IsNullOrWhiteSpace(queryParams.A0101) ? "" : queryParams.A0101, 
				string.IsNullOrWhiteSpace(queryParams.A0102) ? "" : queryParams.A0102, 
				ControlName,
				pageNo
			);
		}

		/// <summary>
		/// 取得唯一的公司資料
		/// </summary>
		/// <param name="companyId">公司別路徑參數</param>
		/// <returns>公司資料模型物件</returns>
		[HttpGet("{companyId}")]
		public MdCompany_d GetRow(string companyId)
		{
			return BlA01.GetRow(companyId);
		}

		/// <summary>
		/// 判斷公司別是否已存在
		/// </summary>
		/// <param name="companyId">公司別路徑參數</param>
		/// <returns></returns>
		[HttpGet("exists/{companyId}")]
		public bool IsExist(string companyId)
		{
			return BlA01.IsExist(companyId);
		}

        /// <summary>
        /// 取得系統產品輔助資料
        /// </summary>
        /// <returns>系統產品資料代碼模型集合物件</returns>
        [HttpGet("help/systemProduct")]
        public IEnumerable<MdCode> GetHelpSystemProduct()
        {
            return BlSINI.GetCodeByLanguage(section:"SystemProduct", topic:"");
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="obj">公司資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
		public MdApiMessage Insert([FromBody] MdCompany_w obj)
		{
			try
			{
				// 呼叫商業元件執行新增作業
				int _result = BlA01.ProcessInsert(obj);

                // 回應前端新增成功訊息
                //return HttpContext.Response.InsertSuccess(_result);
                MdCompanies_p _A01s = BlA01.GetData(A0101:obj.A0101,A0102:"",  funcName:ControlName , pageNo:1);
				if (_A01s.datas != null) {
					if (_A01s.datas.Count() > 0)
					{
						return HttpContext.Response.InsertSuccess(
							affectedRows: _result,
							responseData: _A01s.datas.ToArray()[0]
                        );
                    }
				}
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
		/// <param name="companyId">公司別</param>
		/// <param name="obj">公司資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPut("{companyId}")]
		public MdApiMessage Update(string companyId, [FromBody] MdCompany_w obj)
		{
			// 檢查鍵值路徑參數與本文中的鍵值是否相同
			if (!companyId.EqualsIgnoreCase(obj.A0101))
			{
				// 回應前端修改失敗訊息
				return HttpContext.Response.UpdateFailedWhenKeyNotSame();
			}

			try
			{
                // 呼叫商業元件執行修改作業
                int _result = BlA01.ProcessUpdate(companyId, obj);

                // 回應前端修改成功訊息
                MdCompanies_p _A01s = BlA01.GetData(A0101: obj.A0101, A0102: "", funcName: ControlName, pageNo: 1);
                if (_A01s.datas != null)
                {
                    if (_A01s.datas.Count() > 0)
                    {
                        return HttpContext.Response.UpdateSuccess(
                            affectedRows: _result,
                            responseData: _A01s.datas.ToArray()[0]
                        );
                    }
                }
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
		/// <param name="companyId">公司別</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpDelete("{companyId}")]
		public MdApiMessage Delete(string companyId)
		{
			try
			{
                // 呼叫商業元件執行刪除作業
                int _result = BlA01.ProcessDelete(companyId);
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
