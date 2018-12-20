using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCenterProject.BLL;
using Type = DCenterProject.Model.Type;

namespace DCenterProject.UI
{
    public partial class TypeUI : System.Web.UI.Page
    {
        TypeManager typeManager = new TypeManager();
        public enum MessageType { Success, Error, Info, Warning };
        protected void Page_Load(object sender, EventArgs e)
        {
            showTypeList();
        }

        protected void ShowMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + message + "','" + type + "');", true);
        }

        protected void typeSaveButton_Click(object sender, EventArgs e)
        {
            Type type = new Type();
            if (typeTextBox.Text == "")
                ShowMessage("Please enter type name" , MessageType.Error);
            else
            {
                type.TypeName = typeTextBox.Text;
                string msg = typeManager.Save(type);

                if (msg.StartsWith("Success"))
                {
                    showTypeList();
                    ShowMessage("Type name saved.", MessageType.Success);
                    typeTextBox.Text = string.Empty;
                }
                else
                    ShowMessage(msg, MessageType.Error);
            }
        }


        protected void showTypeList()
        {
            List<Type> typeList = typeManager.GetAllTypes(); //save all data into a list
            typeGridView.DataSource = typeList; // to indicate the data source 
            typeGridView.DataBind(); // for managing columns and its related data
            typeGridView.ShowHeaderWhenEmpty = true;
            typeGridView.EmptyDataText = "No data found";
        }

    }
}