<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AboutProduct.aspx.cs" Inherits="main_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<style> 
html { overflow-y:hidden; }  

</style>
    <title>无标题页</title>

</head>
<body  bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" style="background-color:#f8fcff;">
    <form id="form1" runat="server">
    <div>
        <table id="aboutthisprocduct"   align=center valign=middle cellpadding=9 cellspacing=0 style="top: 0px;font:12px; position: absolute;" >
            <tr>
                <td style="" colspan="4" rowspan="2">
           
                    <asp:Image ID="imgSoftName" runat="server" ImageUrl="~/images/Version.jpg" Visible="False"/><br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td style="width: 374px;">
                </td>
                <td style="width: 1200px;  font-size: 12pt;">
                    <asp:Label ID="LabVersion" runat="server" Text="软件版本号" Font-Names="宋体" Width=100% Font-Bold="True"></asp:Label></td>
                <td style="width: 729px;">
                </td>
                <td style="width: 540px;">
                </td>
            </tr>
            <tr>
                <td style="width: 374px">
                    &nbsp;</td>
                <td style="width: 1200px">
                    <asp:Label ID="lblSoftwareName" runat="server" Font-Bold="True" Font-Names="宋体" Text="软件名称"
                        Width="100%"></asp:Label></td>
                <td align="center" rowspan="1" style="width: 729px">
                    </td>
                <td align="center" rowspan="1" style="width: 540px">
                    </td>
            </tr>
            <tr>
                <td style="width: 374px">
                </td>
                <td style="width: 1200px; font-size: 12pt;">
                    <asp:Label ID="labCompany" runat="server" Text="公司名" Font-Names="宋体" Width=100% Font-Bold="true"></asp:Label></td>
                <td align="center" rowspan="6" style="width: 729px">
                                    <asp:Image ID="imgLogo" runat="server" ImageAlign="Middle" ImageUrl="../../../images/thok_LOGO.jpg" /></td>
                <td align="center" rowspan="6" style="width: 540px">
                </td>
            </tr>
            <tr>
                <td style="width: 374px">
                </td>
                <td style="width: 1200px; font-size: 12pt;">
                    <asp:Label ID="labCompanyAddress" runat="server" Text="公司地址" Font-Names="宋体" Width=100% Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 374px">
                </td>
                <td style="width: 1200px; font-size: 12pt;">
                    <asp:Label ID="labCompanyTelephone" runat="server" Text="公司电话" Font-Names="宋体" Width=100% Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 374px; height: 19px">
                </td>
                <td style="width: 1200px; height: 19px; font-size: 12pt;">
                    <asp:Label ID="labCompanyFax" runat="server" Text="公司传真" Font-Names="宋体" Width=100% Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 374px">
                </td>
                <td style="width: 1200px; font-size: 12pt;">
                    <asp:Label ID="labCompanyEmail" runat="server" Text="公司电邮" Font-Names="宋体"  Font-Bold="true"></asp:Label>
                    <asp:LinkButton ID="lbtnCompanyEmail" runat="server" OnClick="lbtnCompanyEmail_Click" >service@skyseaok.com</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 374px">
                </td>
                <td style="width: 1200px; font-size: 12pt;">
                    <asp:Label ID="labCompanyWeb" runat="server" Text="公司网页" Font-Names="宋体" Font-Bold="true"></asp:Label>
                    <asp:LinkButton ID="lbtnCompanyWeb" runat="server" OnClick="lbtnCompanyWeb_Click">www.skyseaok.com</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 374px">
                    &nbsp;</td>
                <td style="width: 1200px">
                    &nbsp;</td>
                <td align="center" rowspan="1" style="width: 729px">
                    </td>
                <td align="center" rowspan="1" style="width: 540px">
                    </td>
            </tr>
            <tr>
                <td style="height: 19px;  width: 374px;">
                &nbsp;
                    </td>
                <td style="height: 19px;  width: 1200px;">
                    </td>
                <td align="center" rowspan="1" style="height: 19px;width: 729px;">
                    </td>
                <td align="center" rowspan="1" style="height: 19px; width: 540px;">
                    </td>
            </tr>
            <tr>
                <td align="center" colspan="1" style="height: 19px; width: 374px;">
                </td>
                <td align="center" colspan="2" style="height: 19px;font-size: 12pt;">
                    <asp:Label ID="labCopyrigth" runat="server" Font-Names="宋体" Width=100% Font-Bold="True"></asp:Label></td>
                <td align="center" colspan="1" style="height: 19px;width: 540px; font-size: 12pt;">
                    <asp:LinkButton ID="lbtnQuit" runat="server" OnClick="lbtnQuit_Click" ForeColor="#404040">返回主界面</asp:LinkButton></td>
            </tr>
            <tr>
                <td align="center" colspan="1" style="height: 19px;width: 374px;">
                &nbsp;
                    </td>
                <td align="center" colspan="2" style="height: 19px;">
                    </td>
                <td align="center" colspan="1" style="height: 19px; width: 540px;">
                    </td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
