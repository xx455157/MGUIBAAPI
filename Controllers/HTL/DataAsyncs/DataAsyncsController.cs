#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.HT2020.Async;
using GUIStd.DAL.HT2020.Models.Private.Async;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTL.DataAsyncs
{

    /// <summary>
    /// 同步資料控制器
    /// </summary>
    [Route("htl/[controller]")]
    public class DataAsyncsController : GUIAppWSController
    {

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 同步資料
        /// </summary>
        /// <param name="obj">上傳資料物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage ProcessDataAsync([FromBody] object obj)
        {
            var _objStr = JsonConvert.SerializeObject(obj);
            var _MdDA = JsonConvert.DeserializeObject<List<MdDataAsync>>(_objStr);

            return new BlDataAsync(ClientContent).ProcessDataAsync(_MdDA);
        }

        #endregion
    }
}
