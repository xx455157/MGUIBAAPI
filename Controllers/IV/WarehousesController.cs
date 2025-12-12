#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewIV;
using GUIStd.DAL.AllNewIV.Models;
using GUIStd.DAL.AllNewIV.Models.Private.RequestEntry;
using GUIStd.BLL.AllNewIV.Private;

#endregion

namespace MGUIBAAPI.Controllers.IV
{
	/// <summary>
	/// 程式資料控制器
	/// </summary>
	[Route("iv/[controller]")]
	public class WarehousesController : GUIAppAuthController
	{
		private BlWarehouse BlWarehouse => new BlWarehouse(this.ClientContent);

		#region " 共用函式 - 查詢資料 "

		/// <summary>
		/// 取得請購類別資料
		/// </summary>
		/// <param name="employeeId">員工編號</param>
		/// <param name="companyId">公司別</param>
		/// <param name="departmentId">部門別</param>
		/// <param name="dataTypes">轉單方式</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("{employeeId}")]
		public IEnumerable<MdWarehouse> GetWarehouse(string employeeId, string companyId,string departmentId,string dataTypes)
		{
			return BlWarehouse.GetWarehouse(employeeId, companyId,departmentId, CurrentUILang, dataTypes);
		}

		/// <summary>
		/// 取得請購類別資料
		/// </summary>
		/// <param name="dataTypes">轉單方式</param>
		/// <param name="includeEmptyRow">是包含空白列</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("typecodes")]
		public IEnumerable<MdWarehouse> GetWarehouse2(string dataTypes, bool includeEmptyRow=false)
		{
			return BlWarehouse.GetWarehouse2(CurrentUILang,dataTypes, includeEmptyRow);
		}

		/// <summary>
		/// 判斷請購類別是否已存在
		/// </summary>
		/// <param name="companyId">公司別</param>
		/// <param name="rqTypeId">請購類別</param>
		/// /// <param name="departmentId">部門別</param>
		/// <returns></returns>
		[HttpGet("exists/{companyId}/{rqTypeId}/{departmentId}")]
		public bool IsExist(string companyId, string rqTypeId, string departmentId )
		{
			return BlWarehouse.IsExist(companyId, rqTypeId, departmentId,CurrentUILang);
		}

		#endregion

	}
}
