using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCenterProject.BLL;
using DCenterProject.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace DCenterProject.UI
{
    public partial class TestReqUI : System.Web.UI.Page
    {
        private List<Test> testList = new List<Test>();
        List<Test> selectedtestList = new List<Test>();

        TestReqManager testReqManager = new TestReqManager();
        public enum MessageType { Success, Error, Info, Warning };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTests();
            }
        }

        protected void ShowMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + message + "','" + type + "');", true);
        }


        public void LoadTests()   /*load all test names in the dropdown box*/
        {
            testList = testReqManager.GelAllTests();
            ViewState["tList"] = testList;
            testDropDownList.DataSource = testList;
            testDropDownList.DataTextField = "TestName";
            testDropDownList.DataValueField = "TestId";
            testDropDownList.DataBind();
            testDropDownList.Items.Insert(0, "<--Select-->");
        }

        public void ClearAll()   /*clear all test boxs*/
        {
            pnameTextBox.Text = string.Empty;
            birthDateTextbox.Text = string.Empty;
            cellNoTextBox.Text = string.Empty;
            feeTextBox.Text = string.Empty;
            totalTextBox.Text = string.Empty;
        }


        private int testID = 0;
        Test t1 = new Test();
        protected void testDropDownList_SelectedIndexChanged(object sender, EventArgs e) /*show test's fee when test is selected*/
        {
            if (testDropDownList.SelectedIndex != 0)
            {
                testID = Convert.ToInt32(testDropDownList.SelectedValue);

                /*search test's fee from test list using linq */
                List<Test> tList = (List<Test>)ViewState["tList"];
                t1 = tList.FirstOrDefault(t => t.TestId == testID);
                ViewState["selectTest"] = t1;
                feeTextBox.Text = t1.Fee.ToString();

            }
            else
            {
                ShowMessage("Test is not selected.", MessageType.Error);
                return;
            }
        }

        protected void showReqTestList(List<Test> aList) /*show patient's requested list on table*/
        {
            List<Test> testList1 = aList;//save all data into a list
            testReqGridView.DataSource = testList1; // to indicate the data source 
            testReqGridView.DataBind(); // for managing columns and its related data
            testReqGridView.ShowHeaderWhenEmpty = true;
            testReqGridView.EmptyDataText = "No data found";
        }

        private decimal TotalFee = 0;
        private int _count = 1;
        protected void testAddButton_Click(object sender, EventArgs e)
        {
            if (ViewState["selectedtestList"] != null) /*getting previous list*/
            {
                selectedtestList = ViewState["selectedtestList"] as List<Test>;
                TotalFee = Convert.ToDecimal(ViewState["TotalFee"]);
            }

            Test test1 = ViewState["selectTest"] as Test;
            selectedtestList.Add(test1); /*adding new tests in the previous list*/

            ViewState["TotalFee"] = TotalFee + test1.Fee;
            totalTextBox.Text = ViewState["TotalFee"].ToString(); /*adding fees with the total amount*/

            ViewState["selectedtestList"] = selectedtestList;
            showReqTestList(selectedtestList); /*show patient's requested list on table*/
        }

        static string dateNow = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        private string spliteDateTime = dateNow.Substring(0, 19);

        protected void saveButton_Click(object sender, EventArgs e)
        {
            string msg = "";
            Patient patient = new Patient();
            patient.PatientName = pnameTextBox.Text;
            patient.BirthDate = birthDateTextbox.Text;
            patient.CellNo = cellNoTextBox.Text;
            patient.TotalFee = Convert.ToDecimal(ViewState["TotalFee"]);

            string checkMsg = testReqManager.Check(patient);
            if (checkMsg.StartsWith("Success"))
            {
                patient.BillNo = generateBillNo(patient);
                msg = testReqManager.SavePatient(patient);



                TestRequest tReq = new TestRequest();
                tReq.PTestList = ViewState["selectedtestList"] as List<Test>;
                tReq.BillNo = patient.BillNo;
                tReq.TestReqDate = spliteDateTime.Substring(0, 10);
                tReq.TestReqTime = spliteDateTime.Substring(11, 8);
                if (msg.StartsWith("Success"))
                {
                    string msg1 = testReqManager.SaveReqTest(tReq);
                    if (msg1.StartsWith("Success"))
                    {
                        //ShowMessage("");
                        ClearAll();
                        generatePdf(patient, tReq);
                        ShowMessage(msg, MessageType.Success);
                    }
                    else
                        ShowMessage(msg, MessageType.Error);
                }
                else
                {
                    ShowMessage(msg, MessageType.Error);
                }
            }
            else
            {
                ShowMessage(checkMsg, MessageType.Error);
            }


           
            

        }

        private void generatePdf(Patient patient, TestRequest tRequest)
        {
            //generate pdf with info
            int count = 1;
            try
            {
                Document pdfDoc = new Document(PageSize.A4, 25, 10, 25, 10);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                Phrase phrase = null;
                PdfPTable table = null;


                pdfDoc.Open();

                //header table
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.3f, 0.7f });
                pdfDoc.Add(table);

                //Name and Address
                phrase = new Phrase();
                phrase.Add(new Chunk("Total Care Diagonostic Center\n\n", FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("107, Park site, Salt Lake Road,\n", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                phrase.Add(new Chunk("Seattle, USA\n", FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                pdfDoc.Add(phrase);

                Paragraph pText = new Paragraph("\n\nBill Date: " + tRequest.TestReqDate +" "+ tRequest.TestReqTime+ "\n\n" +
                                                "Bill No: " + patient.BillNo + "\n\n");
                pdfDoc.Add(pText);

                //patient info
                PdfPTable table1 = new PdfPTable(2);
                PdfPCell cell1 = new PdfPCell(new Phrase("Patient Information"));
                cell1.Colspan = 2;
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.AddCell(cell1);

                table1.AddCell("Patient Name"); table1.AddCell(patient.PatientName);
                table1.AddCell("Date of Birth "); table1.AddCell(patient.BirthDate);
                table1.AddCell("Mobile No "); table1.AddCell(patient.CellNo);
                pdfDoc.Add(table1);


                pText = new Paragraph("\n\n\n");
                pdfDoc.Add(pText);

                //test info
                PdfPTable table2 = new PdfPTable(3);
                PdfPCell cell2 = new PdfPCell(new Phrase("Test Information"));
                cell2.Colspan = 3;
                cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                table2.AddCell(cell2);

                //get requested test list
                int id = Convert.ToInt32(ViewState["PatientID"]);
                List<Test> getTestList = tRequest.PTestList;
                foreach (Test test in getTestList)
                {
                    table2.AddCell(count.ToString());
                    table2.AddCell(test.TestName);
                    table2.AddCell(test.Fee.ToString());
                    count++;
                }

                pdfDoc.Add(table2);

                //total amount
                pText = new Paragraph("\n\nTotal Fee: " + patient.TotalFee);
                pdfDoc.Add(pText);

                pdfWriter.CloseStream = false;
                pdfDoc.Close();

                //create pdf
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Patient_Report.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }


        private string generateSerialNo(int num)
        {
            int length = num.ToString().Length;
            string serialNo = "";
            switch (length)
            {
                case 1:
                    {
                        serialNo = "0000" + length;
                        break;
                    }
                case 2:
                    {
                        serialNo = "000" + length;
                        break;
                    }
                case 3:
                    {
                        serialNo = "00" + length;
                        break;
                    }
                case 4:
                    {
                        serialNo = "0" + length;
                        break;
                    }
                default:
                    serialNo = length.ToString();
                    break;
            }

            return serialNo;
        }

        private string generateBillNo(Patient patient)
        {
            if (ViewState["count"] != null) /*getting previous count*/
            {
                _count = Convert.ToInt32(ViewState["count"]);
            }

            if (ViewState["flag"] == null) /*store current dateTime*/
            {
                ViewState["flag"] = 1;
                ViewState["MM/yyyy"] = spliteDateTime.Substring(3, 7);
            }


            string newDateFormat = spliteDateTime.Replace("/", "");
            string userCellNo = patient.CellNo.Substring(patient.CellNo.Length-4); /*getting last 4 digit of cell no*/
            //string testReqNo = tReq.pTestList.Count.ToString(); /*No of tests that patient requested*/
            string serialNo = generateSerialNo(_count); /*patient serial no*/
            ViewState["count"] = _count++; /*store the count*/

            string billNo = newDateFormat.Substring(0, 8) + "-" + userCellNo + "-" + serialNo; /*generate bill no(8 digit - 4 digit - 5 digit)*/

            /*when new month starts, patients will be counted newly.*/
            if (spliteDateTime.Substring(3, 7) != (ViewState["MM/yyyy"].ToString()))
            {
                ViewState["count"] = 1;
                ViewState["flag"] = null;
            }

            return billNo;
        }

      

    }
}