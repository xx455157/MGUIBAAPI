#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.GUI;
using GUIStd.BLL.GUI.Private;
using GUIStd.DAL.GUI.Models;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.Models;
using System;
using GUICore.Web.Extensions;

#endregion

namespace MGUIBAAPI.Controllers.NetGUI
{
	/// <summary>
	/// 系統參數控制器
	/// </summary>
	[Route("netgui/[controller]")]
	public class FlowsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlDU BlDU => new BlDU(ClientContent);

        private BlPetition BlPetition => new BlPetition(ClientContent);
        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得簽呈簽核流程資料
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="flowId">流程代號</param>
        /// <param name="amounts">核決金額/數量</param>
        /// <returns>簽核流程模型集合物件</returns>
        [HttpGet("{companyId}/{flowId}/{amounts}")]
		public IEnumerable<MdPetitionFlow> GetPetitionFlows(string companyId, string flowId, decimal amounts)
		{
            return BlDU.GetPetitionFlows(companyId, flowId, amounts);
        }

        #endregion

        #region " 共用屬性 - 異動資料"
        /// <summary> 
        /// 新增請購資料
        /// </summary>
        /// <param name="obj">請購資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdPetition_2 obj)
        {
            try
            {
                //// 呼叫商業元件執行修改作業
                int _result = BlPetition.ProcessInsert(obj);
                var _mdReqResult = new MdApiMessage();
                _mdReqResult.Result = true;
                _mdReqResult.Message = "Success";
                // 回應前端修改成功訊息
                //return HttpContext.Response.InsertSuccess(_result);
                return _mdReqResult;
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }
        #endregion
    }
}
