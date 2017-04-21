
using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyReceiveService.Model;
using ND.PolicyService.CoreLib;
using NDFront.Lib.DtoModel.autoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyReceiveService.Core.ReceivePolicy.helper
{
   public class PolicyManage19e
    {
         #region 同步政策
       public static List<PolicySyncRec> SynchronizePolicy(List<Policies> _list)
        {
            List<PolicySyncRec> errList = new List<PolicySyncRec>();
            bool flag = true;
          
            string[] arryPolicyId91e = _list.Select(o => o.PartnerPolicyId).ToArray();
            PolicySyncRecLib syncRecLib = new PolicySyncRecLib();
           // List<string> existIdList =syncRecLib.ExistPartenerIdList(arryPolicyId91e);
            syncRecLib.BatchDelPolicy(arryPolicyId91e);//先批量置为无效，然后从新添加一条
            List<PolicySyncRec> list = MapperHelper.GetEntityList<List<Policies>, List<PolicySyncRec>>(_list);
            List<PolicyDetail> listDetail = MapperHelper.GetEntityList<List<Policies>, List<PolicyDetail>>(_list);
            foreach (var item in list)
            {
                item.PartnerId = (int)SupplierType._19E;
                item.PartnerName = SupplierType._19E.ToString();
                PolicyDetail policyDeatil = listDetail.FirstOrDefault(x => x.PolicyId == item.Id);
                //if (existIdList.Contains(item.PartnerPolicyId))
                //{
                  
                //   flag = syncRecLib.Update(item, policyDeatil);//更新
                //    if(!flag)
                //    {
                //        errList.Add(item); 
                //    }
                //}
                //else
                //{
                    flag = syncRecLib.Add(item, policyDeatil);//添加
                    if(!flag)
                    {
                        errList.Add(item);
                      
                    }
                //}
            }

            return errList;
        } 
	#endregion

         #region 同步删除
         /// <summary>
         /// 同步删除
         /// </summary>
         /// <param name="_list">The _list.</param>
         /// <returns>System.Int32.</returns>
         public static List<string> SynchronizePolicyDel(List<string> _list)
         {
             List<string> errList = new List<string>();
             StringBuilder sbSQL = new StringBuilder();

             foreach (string delId in _list)
             {
                 sbSQL.Append(";Update PolicySyncRec set DelDegree=0,UpdateTime=getDate() where PartnerPolicyId='" + delId + "';Update PolicyDetail set DelDegree=0 where PartnerPolicyId='" + delId + "'");
             }

             if (sbSQL.Length > 0)
             {
                 object obj = DbHelperSQL.GetSingle(sbSQL.ToString());
                 if (obj == null) { 
                     errList.Add(sbSQL.ToString());
                     return errList;
                 }
                 else { return errList; }

             }
             sbSQL = null;

             return errList;
         }
         #endregion

    
   }

}
