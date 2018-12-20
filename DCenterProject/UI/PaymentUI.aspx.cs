using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCenterProject.BLL;
using DCenterProject.Model;

namespace DCenterProject.UI
{
    public partial class PaymentUI : System.Web.UI.Page
    {
        PaymentManager pManager = new PaymentManager();

        public enum MessageType { Success, Error, Info, Warning };

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            string billNo = billTextBox.Text;
            ViewState["Bill_No"] = billNo;
            Payment payment = pManager.GetDetailsWithBillNo(billNo);
            if (payment.BillNo.StartsWith("Not") || payment.BillNo.StartsWith("Wrong")) /*check bill number*/
            {
                ShowMessage(payment.BillNo, MessageType.Error);
                return;
            }
            if (payment.PTestList.Count == 0) /*check if any tests exist against this bill number*/
            {
                ShowMessage("No Tests found!", MessageType.Error);
                return;
            }

            List<Test> aList = payment.PTestList;
            decimal totalAmount = aList.Sum(t => t.Fee);
            ViewState["TotalAmount"] = totalAmount;

            /*bind Patient tests with table*/
            if (aList.Count > 0)
            {
                patientTestGridView.DataSource = aList;
                patientTestGridView.DataBind();
            }
            else
            {
              ClearTable();
            }
            
            

            pNameTextLabel.Text = payment.Patient_name;
            billDateTextLabel.Text = payment.BillDate;
            totalFeeTextLabel.Text = totalAmount + " Taka";
            paidTextLabel.Text = (totalAmount - payment.Total) + " Taka";
            dueTextLabel.Text = payment.Total + " Taka";
            ViewState["Due"] = payment.Total;
        }


        protected void payButton_Click(object sender, EventArgs e)
        {
            Payment p1 = new Payment();
            p1.BillNo = ViewState["Bill_No"].ToString();
            p1.Paid = Convert.ToDecimal(amountTextBox.Text);
            p1.Due = Convert.ToDecimal(ViewState["Due"]);
            p1.Total = Convert.ToDecimal(ViewState["TotalAmount"]);

            string msg = pManager.UpdateTotalAmount(p1);
            if (msg.StartsWith("Success"))
            {
                ShowMessage("Total amount updated.", MessageType.Success);
                amountTextBox.Text = string.Empty;
                paidTextLabel.Text = (p1.Total - p1.Due)+ " Taka";
                dueTextLabel.Text = p1.Due + " Taka";
                ViewState["Due"] = p1.Due;
            }
            else
            {
                ShowMessage(msg, MessageType.Error);
            }
        }

        protected void ShowMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + message + "','" + type + "');", true);
        }

        public void ClearAll()
        {
            billTextBox.Text = string.Empty;
            pNameTextLabel.Text = string.Empty;
            billDateTextLabel.Text = string.Empty;
            totalFeeTextLabel.Text = string.Empty;
            paidTextLabel.Text = string.Empty;
            dueTextLabel.Text = String.Empty;
            ClearTable();
           
        }

        public void ClearTable()
        {
            patientTestGridView.DataSource = null;
            patientTestGridView.DataBind();
            patientTestGridView.ShowHeaderWhenEmpty = true;
            patientTestGridView.EmptyDataText = "No data found";
        }

        protected void refreshButton_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

    }
}