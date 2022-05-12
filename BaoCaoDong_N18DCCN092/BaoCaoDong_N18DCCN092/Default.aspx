<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BaoCaoDong_N18DCCN092._Default" %>

<%@ Register assembly="DevExpress.Web.v21.2, Version=21.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
<div id="main" >
    <div id="database-content">
        <asp:Panel ID="Panel1" runat="server"   Height="200px" HorizontalAlign="Center">
    <asp:DropDownList ID="DropDownList1" CssClass="dropup"  runat="server"   OnSelectedIndexChanged="cbDatabase_SelectedIndexChanged" Height="50px" Width="20%" >
     </asp:DropDownList>
        </asp:Panel>
</div>

      <div id="table-content">
                <asp:Panel ID="PanelChonBang" runat="server" BackColor="#99CCFF"  Height="400px" HorizontalAlign="Left">
                     <br />
                    <asp:Label ID="LabelChonBang" CssClass="center-block " Font-Bold="true" runat="server" Text="Chọn TABLE cần in báo cáo: "></asp:Label>
                  
                    <br />
                    <asp:CheckBoxList ID="CheckBoxTable" runat="server" Height="20px"  Width="100%" OnSelectedIndexChanged="CheckBoxTable_SelectedIndexChanged">
                    

                        </asp:CheckBoxList>
                   

                   
                    <br/>
                   
                    <br />
                    
                </asp:Panel>
            </div>
         <div id="column-content">
                <asp:Panel ID="PanelChonCot" runat="server" BackColor="#FFCCCC" ForeColor="Maroon" Height="250px" CssClass="center-block" Wrap="False">

                    <br />
                    <asp:Label ID="LabelChonCot" CssClass="center-block" runat="server" Text="Chọn COLUMN cần in báo cáo: " Font-Bold="True"></asp:Label>
                    <br />
                 
                    <asp:CheckBoxList ID="CheckBoxListColumn" CssClass="col-lg-2" AutoPostBack="true" runat="server" Height="50px"  Width="500px" OnSelectedIndexChanged="CheckBoxListColumn_SelectedIndexChanged" CellPadding="10" CellSpacing="10" RepeatColumns="5" RepeatDirection="Horizontal">              
                    </asp:CheckBoxList>
                  <%--  <asp:CheckBoxList ID="CheckBoxListColumn" runat="server" Height="20px"  Width="100%" OnSelectedIndexChanged="CheckBoxTable_SelectedIndexChanged" RepeatDirection="Horizontal" RepeatColumns="3">
                    

                        </asp:CheckBoxList>--%>

                  
                   
              
                    

                    

                  
                   
              
                    

                </asp:Panel>
            </div>
      

                <div id="query-content">
                <asp:Panel ID="PanelGridViewColumn" runat="server"  ForeColor="#6600cc" Height="500px" BackColor="#FFFF99" ValidateRequestMode="Inherit" Direction="NotSet" HorizontalAlign="Center">                
                    <br />
                    <asp:GridView ID="GridView1" runat="server" Width="70%" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center"  >
                       <Columns>
                        
                  
                       
                    
                               
              
                   
                           <asp:TemplateField HeaderText="Điều Kiện">
                               <ItemTemplate>
                                   <asp:TextBox ID="TextInputDieuKien" runat="server" Width="100%"></asp:TextBox>
                               </ItemTemplate>
                           </asp:TemplateField>

                              <asp:TemplateField HeaderText="Chọn Hàm Thống Kê" >
                               <ItemTemplate>
                                   <asp:DropDownList  ID="Cb_DieuKien" runat="server" Width="100%" >
                                        <asp:ListItem Text="Chọn hàm" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="COUNT" Value="COUNT"></asp:ListItem>
                                        <asp:ListItem Text="SUM" Value="SUM"></asp:ListItem>
                                        <asp:ListItem Text="MIN" Value="MIN"></asp:ListItem>
                                        <asp:ListItem Text="MAX" Value="MAX"></asp:ListItem>                                     
                                        <asp:ListItem Text="AVG" Value="AVG"></asp:ListItem>
                                       <asp:ListItem Text="GROUP_BY" Value="GROUP BY"></asp:ListItem>
                                   </asp:DropDownList>
                                </ItemTemplate>
                           </asp:TemplateField>   
                           
                            <asp:TemplateField HeaderText="Điều kiện Having">
                               <ItemTemplate>
                                   <asp:TextBox ID="TextInputDieuKienHaving" runat="server" Width="100%"></asp:TextBox>
                               </ItemTemplate>
                           </asp:TemplateField>

                           

                       </Columns>
                         <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                      
                    </asp:GridView>


                    <br />          
                    <br />
                    <br />
                    <asp:Button ID="btnTaoTruyVan" CssClass="btn-success" runat="server"  Text="Tạo câu truy vấn" OnClick="btnTaoTruyVan_Click" Font-Bold="True" />
                    <br />
                    <br />
                    <asp:Label ID="CauTruyVan" runat="server" BorderColor="#3333FF" BorderStyle="Double" Font-Bold="true" >Câu truy vấn</asp:Label>
                 
                    <br />
                     <br />
                     <asp:Button ID="btnReport" CssClass="btn-danger" runat="server" BorderColor="#6600FF" BorderStyle="Double" Font-Bold="True" OnClick="ButtonReport_Click" Text="Tạo REPORT" />
                </asp:Panel>
            </div>
</div>


</asp:Content>
