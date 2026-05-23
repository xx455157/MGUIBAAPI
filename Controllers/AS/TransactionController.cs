#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.Models;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.BLL.AllNewAS;
using GUIStd.DAL.AllNewAS.Models.Private.Assets;

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

        private BlAG BlAG => new BlAG(ClientContent);

        private BlAC BlAC => new BlAC(ClientContent);

        private BlAD BlAD => new BlAD(ClientContent);

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
            return BlTransaction.isExists(AF01, AF02, AF03);
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


        /// <summary>
        /// 取得單據號碼
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="purchaseDate">購入日期 (YYYYMMDD)</param>
        /// <param name="txType">異動類別</param>
        /// <returns>自動產生的購入單號</returns>
        [HttpGet("autoNumber/{companyId}/{purchaseDate}/{txType}")]
        public MdApiMessage GetAutoNumber(string companyId, string purchaseDate, string txType )
        {
            try
            {
                // 驗證日期格式
                if (string.IsNullOrEmpty(purchaseDate) || purchaseDate.Length != 8)
                    throw new Exception(Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_InvalidDateFormat"));

                // 呼叫自動取號
                string _autoNumber = BlAG.GetAutoNumber(companyId, txType, purchaseDate);

                if (string.IsNullOrEmpty(_autoNumber))
                    throw new Exception(Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_NonSetNoteNoRule"));

                // 返回成功訊息
                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_GetAutoNumberSuccess"),
                    new
                    {
                        purchaseNo = _autoNumber
                    }
                );
            }
            catch (Exception ex)
            {
                return Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_GetAutoNumberFailed"),
                    ex
                );
            }
        }

        /// <summary>
        /// 檢查購入單號是否重複
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="docNo">購入單號</param>
        /// <param name="txType">異動別</param>
        /// <returns>是否重複（true=重複, false=不重複）</returns>
        [HttpGet("checkTransNoExists/{companyId}/{docNo}/{txType}")]
        public MdApiMessage CheckTransNoExists(string companyId, string docNo, string txType)
        {
            try
            {
                bool _isExists = BlAC.IsTransNoExists(companyId, "2", txType, docNo);

                string _message = !_isExists
                    ? Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NotExistForAPI")
                    : Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_RecordExist");

                return Response.SendSuccess(
                    _message = Localization.GetValue(Enums.ResourceLang.LangAS, "PanelDescpt_PurchaseNo") + _message,
                    new
                    {
                        isDuplicate = _isExists,
                        purchaseNo = docNo
                    }
                );
            }
            catch (Exception ex)
            {
                return Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_CheckPurchaseNoFailed"),
                    ex
                );
            }
        }

        /// <summary>
        /// 檢查購入單號是否重複
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="docNo">調撥單號</param>
        /// <param name="txType">異動別</param>
        /// <returns>是否重複（true=重複, false=不重複）</returns>
        [HttpGet("checkTransNoExistsByAD/{companyId}/{docNo}/{txType}")]
        public MdApiMessage CheckTransNoExistsByAD(string companyId, string docNo, string txType)
        {
            try
            {
                bool _isExists = BlAD.IsTransNoExists(companyId, "2", txType, docNo);

                string _message = !_isExists
                    ? Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NotExistForAPI")
                    : Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_RecordExist");

                return Response.SendSuccess(
                    _message = Localization.GetValue(Enums.ResourceLang.LangAS, "PanelDescpt_AllocationNo") + _message,
                    new
                    {
                        isDuplicate = _isExists,
                        transNo = docNo
                    }
                );
            }
            catch (Exception ex)
            {
                return Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_CheckPurchaseNoFailed"),
                    ex
                );
            }
        }

        #endregion
    }
}
