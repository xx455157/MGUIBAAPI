#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd;
using GUICore.Web.Controllers;
using MGUIBAAPI.Models.HTLPRE;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewHTL.Models.Private.Configs;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
    /// <summary>
    /// vPOSSetup程式資料控制器
    /// </summary>
    [Route("htlpre/private/[controller]")]
	public class vPOSSetupController : GUIAppAuthController
    {
        #region " 私用屬性 "
        
        private BlConfigs BlConfigs => new BlConfigs(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        [HttpGet("page")]
        public CmPOSSetup GetUIData()
        {
            string _bkDate;
            IEnumerable<MdCode> _posIds;
            IEnumerable<MdCode> _payTypes;
            IEnumerable<MdCode> _acctCodes03;
            MdDefaultConfig _posDefaultConfig;

            BlConfigs.GetPOSSetupUiData(out _bkDate, out _posIds, out _payTypes, out _acctCodes03, out _posDefaultConfig);

            return new CmPOSSetup()
            {
                BkDate = _bkDate,
                PosIds = _posIds,
                PayTypes = _payTypes,
                FrontDeskAccts = _acctCodes03,
                Kot04Fields = MdPosKot04Dynamic.FieldDefs.Cast<DictionaryEntry>()
                    .Select(f => new MdCode
                    {
                        Id = Common.Cvr2String(f.Key),
                        Name = Common.Cvr2String(f.Value)
                    }),
                Kot10Fields = MdPosKot10Dynamic.FieldDefs.Cast<DictionaryEntry>()
                    .Select(f => new MdCode
                    {
                        Id = Common.Cvr2String(f.Key),
                        Name = Common.Cvr2String(f.Value)
                    }),
                DefaultSettings = _posDefaultConfig
            };
        }        

        #endregion

    }
}
