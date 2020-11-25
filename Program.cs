using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using weaver;

namespace EcologyWorkflowService
{
    class Program
    {

        private static string workflowId = "6721";
        private static string workflowName = "物料编码转换审批单";
        private static int userId = 3835;

        static async Task Main(string[] args)
        {
            WorkflowBaseInfo workflowBaseInfo = new WorkflowBaseInfo();//工作流信息
            workflowBaseInfo.workflowId = workflowId;//流程ID
            workflowBaseInfo.workflowName = workflowName;//流程名称

            WorkflowRequestInfo workflowRequestInfo = new WorkflowRequestInfo();//工作流程请求信息

            workflowRequestInfo.canEdit = true;
            workflowRequestInfo.canView = true;
            workflowRequestInfo.requestLevel = "0";
            workflowRequestInfo.creatorId = userId.ToString();
            workflowRequestInfo.requestName = $"{workflowName}-杨科-{ DateTime.Now:yyyy-MM-dd}";
            workflowRequestInfo.workflowBaseInfo = workflowBaseInfo;

            #region 创建流程信息

            //创建主表字段信息
            List<WorkflowRequestTableField> mainTableField01 = new List<WorkflowRequestTableField>
            {
                new WorkflowRequestTableField
                {
                    fieldName = "sqr",
                    fieldValue = userId.ToString(),
                    view = true,
                    edit = true
                } ,
                new WorkflowRequestTableField
                {
                    fieldName = "sqrq",
                    fieldValue = DateTime.Now.ToString("yyyy-MM-dd"),
                    view = true,
                    edit = true
                } ,
                new WorkflowRequestTableField
                {
                    fieldName = "szgs",
                    fieldValue = "243",
                    view = true,
                    edit = true
                } ,
                new WorkflowRequestTableField
                {
                    fieldName = "szbm",
                    fieldValue = "1448",
                    view = true,
                    edit = true
                } ,
                new WorkflowRequestTableField
                {
                    fieldName = "gc1",
                    fieldValue = "5",
                    view = true,
                    edit = true
                } ,
                new WorkflowRequestTableField
                {
                    fieldName = "kcdd1",
                    fieldValue = "600",
                    view = true,
                    edit = true
                } ,
            };

            //创建明细表字段信息
            List<WorkflowRequestTableField> detailTableField01 = new List<WorkflowRequestTableField>
            {
                new WorkflowRequestTableField
                {
                    fieldName = "wl",
                    fieldValue = "3011100017",
                    view = true,
                    edit = true
                } ,
                new WorkflowRequestTableField
                {
                    fieldName = "wlms",
                    fieldValue = "二级板_6-12_A_负",
                    view = true,
                    edit = true
                } ,
                new WorkflowRequestTableField
                {
                    fieldName = "dw",
                    fieldValue = "PC",
                    view = true,
                    edit = true
                } ,
                new WorkflowRequestTableField
                {
                    fieldName = "wlzygz1",
                    fieldValue = "3011100014",
                    view = true,
                    edit = true
                } ,
                new WorkflowRequestTableField
                {
                    fieldName = "wlmszygz",
                    fieldValue = "二级板_8-10_A_正",
                    view = true,
                    edit = true
                } ,
                new WorkflowRequestTableField
                {
                    fieldName = "dw1",
                    fieldValue = "PC",
                    view = true,
                    edit = true
                } ,
                new WorkflowRequestTableField
                {
                    fieldName = "sl",
                    fieldValue = "10",
                    view = true,
                    edit = true
                }
            };


            //添加表单主表记录
            List<WorkflowRequestTableRecord> mainTableRecord = new List<WorkflowRequestTableRecord>
            {
                new WorkflowRequestTableRecord
                {
                    workflowRequestTableFields = mainTableField01.ToArray()
                }
            };

            //添加表单明细记录
            List<WorkflowRequestTableRecord> detailTableRecord = new List<WorkflowRequestTableRecord>
            {
                new WorkflowRequestTableRecord
                {
                    workflowRequestTableFields = detailTableField01.ToArray()
                }
            };

            //主表构造赋值
            WorkflowMainTableInfo mainTableInfo = new WorkflowMainTableInfo
            {
                requestRecords = mainTableRecord.ToArray(),
            };

            //明细表构造赋值
            List<WorkflowDetailTableInfo> detailTableInfo = new List<WorkflowDetailTableInfo>
            {
                new WorkflowDetailTableInfo
                {
                        workflowRequestTableRecords = detailTableRecord.ToArray()
                }
            };

            workflowRequestInfo.workflowMainTableInfo = mainTableInfo;
            workflowRequestInfo.workflowDetailTableInfos = detailTableInfo.ToArray();

            #endregion

            //创建明细表信息

            #region 开始调用接口
            // 创建 HTTP 绑定对象
            var binding = new BasicHttpBinding
            {
                //设置最大传输接受数量
                MaxReceivedMessageSize = 2147483647
            };

            // 根据 WebService 的 URL 构建终端点对象
            var endpoint = new EndpointAddress("http://10.0.10.210/services/WorkflowService");
            // 创建调用接口的工厂，注意这里泛型只能传入接口 添加服务引用时生成的 webservice的接口 一般是 (XXXSoap)
            var factory = new ChannelFactory<WorkflowServicePortType>(binding, endpoint);
            //factory.Credentials.UserName.UserName = AppSettings.Configuration["ServiceAuth:UserNmae"];
            //factory.Credentials.UserName.Password = AppSettings.Configuration["ServiceAuth:PassWord"];
            // 从工厂获取具体的调用实例

            
            var client = factory.CreateChannel();


            Console.WriteLine(await client.doCreateWorkflowRequestAsync(workflowRequestInfo, userId));

            #endregion

            Console.ReadKey() ;
        }
    }
}
