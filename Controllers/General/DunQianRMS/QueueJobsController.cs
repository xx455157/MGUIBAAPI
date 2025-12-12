#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
    /// 敦謙RMS指示檔控制器
    /// </summary>
    [Route("general/dunqianrms/[controller]")]
    public class QueueJobsController : GUIAppWSController
    {
        #region " 建構子：欲自行指定使用者帳號時才需加入 "
        
        // 建構子
        public QueueJobsController()
        {
            // 改變執行服務的使用者帳號
            this.WSUser = "DQRMS";
        }

        #endregion

        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlQueueJob BlQueueJob => new BlQueueJob(ClientContent);

        #endregion

        #region " 授權存取 API "

        /// <summary>
        /// 取得待上傳RMS貯列資料
        /// </summary>
        /// <returns>RMS貯列資料泛型集合物件</returns>
        [HttpGet()]
        public IEnumerable<MdRmsQueueJob> GetData()
        {
            return BlQueueJob.GetJobs();
        }

        /// <summary>
        /// 取得待上傳RMS貯列資料
        /// </summary>
        /// <returns>RMS貯列資料泛型集合物件</returns>
        [HttpGet("{jobType}")]
        public IEnumerable<MdRmsQueueJob> GetData(string jobType)
        {
            return BlQueueJob.GetJobs(jobType.ToUpper());
        }

        /// <summary>
        /// 更新指示檔狀態
        /// </summary>
        /// <param name="jobType">RMRT、RMIV、RMBK三種類別</param>
        /// <param name="sendAt">處理時間(yyyy-MM-ddThh:mm:ss.fff)</param>
        /// <param name="status">狀態:空白、1~4</param>
		/// <returns>系統規範訊息物件</returns>
        [HttpPut("{jobType}/{sendAt}/{status}")]
        public MdApiMessage UpdateJobStatus(string jobType,string sendAt, string status)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlQueueJob.UpdateJobStatus(jobType, sendAt, status);

                // 回應前端新增成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 更新指示檔狀態
        /// </summary>
        /// <param name="jobId">指示檔Pkey</param>
        /// <param name="status">狀態:空白、1~4</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("{jobId}/{status}")]
        public MdApiMessage UpdateJobStatus(decimal jobId, string status)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlQueueJob.UpdateJobStatus(jobId, status);

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
