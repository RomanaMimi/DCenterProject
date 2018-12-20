using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using DCenterProject.Gateway;
using DCenterProject.Model;

namespace DCenterProject.BLL
{
    public class PaymentManager
    {
        PaymentGateway pGateway = new PaymentGateway();

        public Payment GetDetailsWithBillNo(string billNo)
        {
            Payment pay1 = new Payment();
            if (billNo.Length == 19)
            {
                pay1 = pGateway.GetDetailsWithBillNo(billNo);
                if (pay1.BillNo != billNo)
                {
                    pay1.BillNo = "Not Found.";
                    return pay1;
                }
                return pay1;
            }

            pay1.BillNo = "Wrong bill number! Enter the correct one.";
            return pay1;

        }

        public string UpdateTotalAmount(Payment pay)
        {
            if (pay.Paid == 0)
                return "Paid amount is 0 taka";

            if (pay.Paid > pay.Due)
            {
                return "Paid amount is more than Due amount";
            }
            else
            {
                decimal due = pay.Due - pay.Paid;
                pay.Due = due;
                
                int rowAffected = pGateway.UpdateTotalAmount(pay);
                if (rowAffected > 0)
                    return "Success";
                else
                {
                    return "Total amount update failed.";
                }
                
            }
            
        }
    }
}