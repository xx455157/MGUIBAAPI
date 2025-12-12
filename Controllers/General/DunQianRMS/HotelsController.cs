#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Models;
using GUIStd.BLL.AllNewHTL.Client.DunQian;
using GUIStd.DAL.AllNewHTL.Models.Client.DunQian;

#endregion

namespace MGUIBAAPI.Controllers.General.DunQianRMS
{
    /// <summary>
    /// 敦謙RMS場域控制器
    /// </summary>
    [Route("general/dunqianrms/[controller]")]
    public class HotelsController : GUIAppWSController
    {
        #region " 建構子：欲自行指定使用者帳號時才需加入 "
        
        // 建構子
        public HotelsController()
        {
            // 改變執行服務的使用者帳號
            this.WSUser = "DQRMS";
        }

        #endregion

        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlHotel BlHotel => new BlHotel(ClientContent);

        #endregion

        #region " 授權存取 API "

        [HttpGet()]
        public MdRmsHotel GetData()
        {
            return BlHotel.GetData();
        }

        [HttpPut()]
        public MdApiMessage SetData(MdRmsHotel obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlHotel.SetData(obj);

                // 回應前端新增成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion

    }
}
