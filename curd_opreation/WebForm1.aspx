<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="dbconnect.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Registration Form</title>
    <style>
        .form-container {
            width: 50%;
            margin: auto;
            text-align: left;
        }
        .form-field {
            margin-bottom: 15px;
            display: flex;
            align-items: center;
        }
        .form-label {
            width: 150px;
            font-weight: bold;
            margin-right: 10px;
            text-align: right;
        }
        .form-input {
            width: 200px;
        }
        .form-buttons {
            text-align: center;
            margin-top: 20px;
        }
        .form-buttons input {
            margin: 0 5px;
        }
        table {
            margin: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <h1 style="text-align: center;">Student Registration Form</h1>
            <table>
                <tr>
                    <td class="form-label">First Name:</td>
                    <td><asp:TextBox ID="Fname" runat="server" CssClass="form-input"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="form-label">Middle Name:</td>
                    <td><asp:TextBox ID="Mname" runat="server" CssClass="form-input"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="form-label">Last Name:</td>
                    <td><asp:TextBox ID="Lname" runat="server" CssClass="form-input"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="form-label">Course:</td>
                    <td>
                        <asp:CheckBoxList ID="Course" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>HTML</asp:ListItem>
                            <asp:ListItem>CSS</asp:ListItem>
                            <asp:ListItem>Javascript</asp:ListItem>
                            <asp:ListItem>Angular</asp:ListItem>
                            <asp:ListItem>C#</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="form-label">Gender:</td>
                    <td>
                        <asp:RadioButtonList ID="Gender" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                            <asp:ListItem>Others</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="form-label">Blood Group:</td>
                    <td>
                        <asp:DropDownList ID="Bloodgroup" runat="server">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>O+ve</asp:ListItem>
                            <asp:ListItem>AB+ve</asp:ListItem>
                            <asp:ListItem>B+ve</asp:ListItem>
                            <asp:ListItem>AB-ve</asp:ListItem>
                            <asp:ListItem>B-ve</asp:ListItem>
                            <asp:ListItem>A+ve</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="form-label">Phone:</td>
                    <td><asp:TextBox ID="Phone" runat="server" CssClass="form-input"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="form-label">Email:</td>
                    <td><asp:TextBox ID="Email" runat="server" CssClass="form-input"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="form-label">Age:</td>
                    <td><asp:TextBox ID="Age" runat="server" CssClass="form-input"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="form-label">Address:</td>
                    <td><asp:TextBox ID="Address" runat="server" CssClass="form-input"></asp:TextBox></td>
                </tr>
            </table>
            <div class="form-buttons">
                <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />

            </div>
            <br />
<asp:GridView ID="GridViewStudents" runat="server" AutoGenerateColumns="False" DataKeyNames="Student_ID" OnSelectedIndexChanged="GridViewStudents_SelectedIndexChanged" OnRowUpdating="GridViewStudents_RowUpdating" OnRowEditing="GridViewStudents_RowEditing" OnRowDeleting="GridViewStudents_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
   <Columns>
        <asp:BoundField DataField="Student_ID" HeaderText="Student_ID" InsertVisible="False" ReadOnly="True" SortExpression="Student_ID" />
        <asp:BoundField DataField="First_Name" HeaderText="First_Name" SortExpression="First_Name" />
        <asp:BoundField DataField="Middle_Name" HeaderText="Middle_Name" SortExpression="Middle_Name" />
        <asp:BoundField DataField="Last_Name" HeaderText="Last_Name" SortExpression="Last_Name" />
        <asp:BoundField DataField="Course" HeaderText="Course" SortExpression="Course" />
        <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
        <asp:BoundField DataField="BloodGroup" HeaderText="BloodGroup" SortExpression="BloodGroup" />
        <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
        <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
        <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" CausesValidation="False"></asp:LinkButton>
                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" CausesValidation="False"></asp:LinkButton>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" Text="Update" CausesValidation="True"></asp:LinkButton>
                <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" CausesValidation="False"></asp:LinkButton>
            </EditItemTemplate>
        </asp:TemplateField>
    </Columns>
    <AlternatingRowStyle BackColor="White" />
    <EditRowStyle BackColor="#7C6F57" />
    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#E3EAEB" />
    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#F8FAFA" />
    <SortedAscendingHeaderStyle BackColor="#246B61" />
    <SortedDescendingCellStyle BackColor="#D4DFE1" />
    <SortedDescendingHeaderStyle BackColor="#15524A" />
</asp:GridView>


        </div>
    </form>
</body>
</html>
