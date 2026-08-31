#region " 匯入的名稱空間：Framework "

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASP11;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASP11 購入日期批次調整
    /// </summary>
    [Route("as/private/[controller]")]
    public class vASP11Controller : GUIAppAuthController
    {
        private BlASP11 BlASP11 => new BlASP11(ClientContent);

        public override Enums.WebSystem SystemId { get; set; } = Enums.WebSystem.AS;

        /// <summary>
        /// 畫面預設資料（公司別、資產屬性）
        /// </summary>
        [HttpGet("page")]
        public MdASP11_h GetUIData()
        {
            return BlASP11.GetUIData();
        }

        /// <summary>
        /// 查詢筆數（大量資料確認用）
        /// </summary>
        [HttpPost("getList/count")]
        public MdASP11_Count GetListCount([FromBody] MdASP11_q queryParams)
        {
            return BlASP11.GetListCount(queryParams ?? new MdASP11_q());
        }

        /// <summary>
        /// 查詢清單（一次帶出全部）
        /// </summary>
        [HttpPost("getList")]
        public IList<MdASP11_v> GetList([FromBody] MdASP11_q queryParams)
        {
            return BlASP11.GetList(queryParams ?? new MdASP11_q());
        }

        /// <summary>
        /// 批次更新購入日期
        /// </summary>
        [HttpPost("batchUpdate")]
        public MdApiMessage BatchUpdate([FromBody] MdASP11_u body)
        {
            try
            {
                BlASP11.BatchUpdate(body);
                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_SaveOK") ?? "存檔成功");
            }
            catch (Exception ex)
            {
                return Response.SendFailed(ex.Message, ex);
            }
        }
    }
}
