<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TypeUI.aspx.cs" Inherits="DCenterProject.UI.TypeUI" %>

<!DOCTYPE html>

<html lang="en">
    <head runat="server">
        <title>Type Setup</title>

        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

        <link href="../Content/bootstrap.min.css" rel="stylesheet" />


        <script src="../Scripts/jquery-1.9.1.min.js"></script>
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
        <h3 style="color: #4f8dbe">Test Type Setup</h3><br>
        <div class="messagealert" id="alert_container"></div>
        <%-- <div class="alert alert-info" runat="server" ID="typeMsgLabel">
            <strong>No message found.</strong>
        </div>--%>
        
    </div>
        
    <br>
    <div class="container">
            <div class="form-group">
                <asp:Label ID="typeLabel" class="control-label col-sm-2" runat="server" Text="Type Name"></asp:Label>&nbsp;
                <asp:TextBox ID="typeTextBox" class="col-sm-4" runat="server"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <div class="col-sm-offset-2">
                    <%--<button type="submit" class="btn btn-primary" runat="server" OnClick="typeSaveButton_Click">Save</button>--%>
                    <asp:Button ID="typeSaveButton" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="typeSaveButton_Click" />
                </div>
            </div>
        <br>
        
        

         <h4 style="color: #4f8dbe"><strong>Type List</strong></h4><br>
        
        <div>
            <asp:GridView ID="typeGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped" GridLines="None">
                <Columns>
                    <asp:TemplateField HeaderText="Serial No">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="code" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Type Code">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="code" Text='<%#Eval("TypeId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Type Name">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="code" Text='<%#Eval("TypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        
        <%--<table class="table">
            <thead>
                <tr>
                    <th>Serial No.</th>
                    <th>Type Name</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td runat="server" ID="code1" Text="">John</td>
                    <td runat="server" ID="code2" Text="">Doe</td>
                </tr>
            </tbody>
        </table>--%>
 </div>

    <br><br>

    <footer class="container-fluid text-center">
        <p>&copy; Total Care Diagnostic Center</p>
    </footer></form></div>
    
</body>
</html>
