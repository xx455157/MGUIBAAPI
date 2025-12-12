#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.DAL.AllNewAS.Models.Private.Assets;
using GUICore.Web.Extensions;
using System;
using GUIStd.Models;
using GUIStd.DAL.AllNewAS.Models.Private.vASM12;
using GUIStd;
using GUIStd.BLL.AllNewAS.Private;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// 單號原則控制器
    /// </summary>
    [Route("as/[controller]")]
    public class TransactionController : GUIAppAuthController
    {

        #region " 私用屬性 "

        ///// <summary>
        ///// 商業邏輯物件屬性
        ///// </summary>
        private BlTransaction BlTransaction => new BlTransaction(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "
        /// <summary>
        /// 取得單號原則
        /// </summary>
        /// <param name="company"></param>
        [HttpGet("getdata/{company}")]
        public IEnumerable<MdTransaction> GetData(string company)
        {
            return BlTransaction.GetData(company);
        }
        /*
        [HttpGet("delete/{AE01}/{AE02}")]
        public MdApiMessage Delete(string AF01, string AF02)
        {
            try
            {
                int _result = BlTransaction.Delete(AF01, AF02);
                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        [HttpPost("insert")]
        public MdApiMessage Insert([FromBody] MdASM12_q queryParams)
        {
            try
            {
                int _result = BlTransaction.Insert(queryParams);
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }
        */
        /// <summary>
        /// 更新單號原則
        /// </summary>
        /// <param name="queryParams"></param>
        [HttpPost("update")]
        public MdApiMessage Update([FromBody] MdASM12_q queryParams)
        {
            try
            {
                int _result = BlTransaction.Update(queryParams);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 複製單號原則
        /// </summary>
        /// <param name="queryParams"></param>
        [HttpPost("copy")]
        public IActionResult Copy([FromBody] MdASM12c_q queryParams)
        {
            var _res = new MdApiMessage();
            try 
            {
                int _result = BlTransaction.Copy(queryParams.AE01 , queryParams.targetCompanies);
                _res.Result = true;
                _res.Message = Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_CopySuccess", true);
                return Ok( _res );
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_CopyFail", true);
                return BadRequest(_res);
            }
}

        /// <summary>
        /// 單號原則是否存在
        /// </summary>
        /// <param name="AF01"></param>
        /// <param name="AF02"></param>
        [HttpGet("isexist/{AF01}/{AF02}")]
        public bool isExists(string AF01, string AF02)
        {
            return BlTransaction.isExists(AF01, AF02);
        }
        /// <summary>
        /// 單號原則的前綴是否存在
        /// </summary>
        /// <param name="AF01"></param>
        /// <param name="AF02"></param>
        /// <param name="AF03"></param>
        [HttpGet("isprefixexist/{AF01}/{AF03}/{AF02?}")]
        public bool IsPrefixExist(string AF01, string AF02, string AF03)
        {
            if (string.IsNullOrEmpty(AF02))
                AF02 = "";
            return BlTransaction.IsPrefixExist(AF01, AF02, AF03);
        }

        /// <summary>
        /// 單號原則單筆取得
        /// </summary>
        /// <param name="AF01"></param>
        /// <param name="AF02"></param>
        [HttpGet("getrow/{AF01}/{AF02}")]
        public MdTransaction GetRow(string AF01, string AF02)
        {
            return BlTransaction.GetRow(AF01, AF02);
        }

        /// <summary>
        /// 單號原則是否已使用
        /// </summary>
        /// <param name="AF01"></param>
        /// <param name="AF02"></param>
        [HttpGet("ishasrecord/{AF01}/{AF02?}")]
        public bool isHasRecord(string AF01, string AF02)
        {
            if (string.IsNullOrEmpty(AF02))
                AF02 = "";

            return BlTransaction.isHasRecord(AF01, AF02);
        }

        
        #endregion
    }
}
