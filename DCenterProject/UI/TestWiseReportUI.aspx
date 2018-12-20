<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestWiseReportUI.aspx.cs" Inherits="DCenterProject.UI.TestWiseReportUI" %>

<!DOCTYPE html>

<!DOCTYPE html>

<html lang="en">
    <head runat="server">
        <title>Test Wise Report</title>

        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
        <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />

        <link href="../Content/bootstrap.min.css" rel="stylesheet" />


        <script src="../Scripts/jquery-1.9.1.min.js"></script>
        <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
        <script src="../Scripts/bootstrap.min.js"></script>
        <link href="../Content/bootstrap.css" rel="stylesheet" />

        <style>
        /* Remove the navbar's default margin-bottom and rounded borders */
            .navbar {
                margin-bottom: 0;
                border-radius: 0;
            }

            /* Add a gray background color and some padding to the footer */

            footer {
                background-color: #f2f2f2;
                padding: 25px;
            }

            .dropdown {
                cursor: pointer;
                border: none;
                outline: none;
                color: gray;
                background-color: inherit;
                font-family: inherit;
                margin: 0;
            }

            .messagealert {
                width: 50%;
                font-size: 15px;
            }
        </style>
    
        <script type="text/javascript">
            function ShowMessage(message, messagetype) {
                var cssclass;
                switch (messagetype) {
                    case 'Success':
                        cssclass = 'alert-success';
                        break;
                    case 'Error':
                        cssclass = 'alert-danger';
                        break;
                    case 'Warning':
                        cssclass = 'alert-warning';
                        break;
                    default:
                        cssclass = 'alert-info';
                }
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
            }
        </script>
        
        <script>
            $(function () {
                $(".datepicker").datepicker({
                    dateFormat: 'dd/mm/yy'
                });
            });
        </script>
    </head>
    <body>
    
    <div>
    <form id="form1" runat="server">
        <nav class="navbar navbar-inverse">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#">Total Care</a>
            </div>
            <div class="collapse navbar-collapse" id="myNavbar">
                <ul class="nav navbar-nav">
                    <li class="active"><a href="#">Home</a></li>
                    <li><a href="#">About Us</a></li>
                    <li><a href="#">News & Media</a></li>
                    <li><a href="#">Services</a></li>
                    <li><a href="#">Contact</a></li>


                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" type="link">Management System<span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="#">Test type setup</a></li>
                            <li><a href="#">Test setup</a></li>
                            <li><a href="#">Test request entry</a></li>
                            <li class="divider"></li>
                            <li><a href="#">Payment</a></li>
                            <li class="divider"></li>
                            <li><a href="#">Test wise report</a></li>
                            <li><a href="#">Type wise report</a></li>
                            <li><a href="#">Unpaid bill report</a></li>
                        </ul>
                    </li>
                </ul>
                <ul class="nav navbar-nav navbar-right">
                    <li><a href="#"><span class="glyphicon glyphicon-log-in"></span> Login</a></li>
                </ul>
            </div>
        </div>
    </nav>
        
    <div class="container">
        <br><br>
        <h3 style="color: #4f8dbe">Test Wise Report</h3><br>
        <div class="messagealert" id="alert_container"></div>
    </div>
        
    <br>
        
   
    <div class="container">
            <div class="form-group">
                <asp:Label ID="fromDateLabel" class="control-label col-sm-2" runat="server" Text="From Date"></asp:Label>&nbsp;
                <asp:TextBox ID="fromDateTextBox" class="col-sm-4" runat="server" CssClass="datepicker"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Date is required!" ControlToValidate="fromDateTextBox" ForeColor="red"></asp:RequiredFieldValidator>
                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" runat="server" ErrorMessage="Please input correct format" ControlToValidate="pnameTextBox" ForeColor="red" ValidationExpression="^[a-zA-Z . \- ]*$"></asp:RegularExpressionValidator>--%>
               <%--<asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="pnameTextBox" ID="RegularExpressionValidatorMinLength" ValidationExpression="^[\s\S]{30,}$" runat="server" ErrorMessage="Name must be at least 3 characters long."></asp:RegularExpressionValidator>--%>
            </div>
        
            <div class="form-group">
                <asp:Label ID="toDateLabel" class="control-label col-sm-2" runat="server" Text="To Date"></asp:Label>
                <asp:TextBox ID="toDateTextbox" class="col-sm-4" runat="server" CssClass="datepicker"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Date is required!" ControlToValidate="toDateTextbox" ForeColor="red"></asp:RequiredFieldValidator>
                            
                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" runat="server" ErrorMessage="Please input correct format" ControlToValidate="txtDateofBirth" ForeColor="red" ValidationExpression="^[0-9 \/ ]*$"></asp:RegularExpressionValidator>--%>
                <%--<asp:TextBox ID="birthDateTextBox" class="col-sm-4" runat="server"></asp:TextBox>&nbsp--%>
                
            </div>
        
            
            <div class="form-group">
                <div class="col-sm-offset-2">
                    <asp:Button ID="showButton" CssClass="btn btn-primary" runat="server" Text="Show" OnClick="showButton_Click" />
                </div>
            </div>
        <br><br>

        
        

       <%-- <h4 style="color: #4f8dbe"><strong>Test List</strong></h4><br>--%>
        
        <div>
            <asp:GridView ID="testGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped" GridLines="None">
                <Columns>
                    <asp:TemplateField HeaderText="Serial No">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="code" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Test Name">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="code" Text='<%#Eval("TestName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Total Test">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="code" Text='<%#Eval("Fee") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Total Amount">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="code" Text='<%#Eval("Fee") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
            
             <div class="form-group">
                <asp:Label ID="totalLabel" class="col-sm-2" runat="server" Text="Total"></asp:Label>&nbsp;
                <asp:TextBox ID="totalTextBox" class="col-sm-4" runat="server"></asp:TextBox>
                  <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="Total amount is required!" ControlToValidate="totalTextBox" ForeColor="red"></asp:RequiredFieldValidator>--%>
            </div>
            
            <div class="form-group">
                <div class="col-sm-offset-2">
                    <asp:Button ID="createPdfButton" CssClass="btn btn-primary" runat="server" Text="Create report" OnClick="createPdfButton_Click"/>
                </div>
            </div>
        </div>
 </div>

    <br><br>

    <footer class="container-fluid text-center">
        <p>&copy; Total Care Diagnostic Center</p>
    </footer></form></div>
    
</body>
</html>
