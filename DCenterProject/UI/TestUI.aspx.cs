using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCenterProject.BLL;
using DCenterProject.Model;
using DCenterProject.Model.ViewModel;
using Type = DCenterProject.Model.Type;

namespace DCenterProject.UI
{
    public partial class TestUI : System.Web.UI.Page
    {

        private TestManager testManager = new TestManager();
        private TypeManager typeManager = new TypeManager();
        public enum MessageType { Success, Error, Info, Warning };
        protected void Page_Load(object sender, EventArgs e)
        {
            showTestWithTypeList();

            if (!IsPostBack)
            {
                LoadTypes();
            }
        }

        protected void ShowMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + message + "','" + type + "');", true);
        }

        public void LoadTypes()
        {
            List<Type> typeList = typeManager.GetAllTypes();
            typeDropDownList.DataSource = typeList;
            typeDropDownList.DataTextField = "TypeName";
            typeDropDownList.DataValueField = "TypeId";
            typeDropDownList.DataBind();
            typeDropDownList.Items.Insert(0, "<--Select-->");
        }

        protected void testSaveButton_Click(object sender, EventArgs e)
        {
            Test test1 = new Test();
            if (testTextBox.Text == "" || feeTextBox.Text == "")
            {
                ShowMessage("Please enter your data", MessageType.Error);
                return;
            }

            if (!Regex.IsMatch(feeTextBox.Text, @"^\d+$"))
            {
                ShowMessage("Fee must be numeric value.", MessageType.Error);
                return;
            }

            if (typeDropDownList.SelectedValue == "")
            {
                ShowMessage("Type is not selected.", MessageType.Error);
                return;
            }
            else
            {
                test1.TestName = testTextBox.Text;
                test1.Fee = Convert.ToDecimal(feeTextBox.Text);
                test1.TypeId = Convert.ToInt32(typeDropDownList.SelectedValue);

                string msg = testManager.Save(test1);
                if (msg.StartsWith("Success"))
                {
                    showTestWithTypeList();
                    ShowMessage("Test name saved.", MessageType.Success);
                    testTextBox.Text = string.Empty;
                    feeTextBox.Text = string.Empty;
                    LoadTypes();
                }
                else
                    ShowMessage(msg, MessageType.Error);
            }
        }

        protected void showTestWithTypeList()
        {
            List<ViewAllTestWithType> testList = testManager.GetViewAllTestWithType(); //save all data into a list
            testGridView.DataSource = testList; // to indicate the data source 
            testGridView.DataBind(); // for managing columns and its related data
            testGridView.ShowHeaderWhenEmpty = true;
            testGridView.EmptyDataText = "No data found";
        }
    }
}