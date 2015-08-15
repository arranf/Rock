﻿<%@ Page Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" Inherits="Rock.Web.UI.RockPage" %>

<asp:Content ID="ctFeature" ContentPlaceHolderID="feature" runat="server">
    <section>
            <div class="row">
                <div class="col-md-12">
                    <Rock:Zone Name="Feature" runat="server" />
                </div>
            </div>
    </section>
</asp:Content>

<asp:Content ID="ctMain" ContentPlaceHolderID="main" runat="server">
    
	<main class="container-fluid">
        
        <!-- Start Content Area -->
        
        <!-- Ajax Error -->
        <div class="alert alert-danger ajax-error" style="display:none">
            <p><strong>Error</strong></p>
            <span class="ajax-error-message"></span>
        </div>

        <div class="row">
            <div class="col-md-12">
                <Rock:Zone Name="Sub Feature" runat="server" />
            </div>
        </div>

        <div class="row">
            <div class="col-md-4">
                <Rock:Zone Name="Section B" runat="server" />
            </div>
            <div class="col-md-4">
                <Rock:Zone Name="Section C" runat="server" />
            </div>
            <div class="col-md-4">
                <Rock:Zone Name="Section D" runat="server" />
            </div>
        </div>

                <div class="row">
            <div class="col-md-4">
                <Rock:Zone Name="Left Side Feature" runat="server" />
            </div>
            <div class="col-md-8">
                <Rock:Zone Name="Right Side Feature" runat="server" />
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <Rock:Zone Name="Section A" runat="server" />
            </div>
        </div>
        <!-- End Content Area -->

	</main>
        
</asp:Content>

<asp:Content ID="ctMiddle" ContentPlaceHolderID="middle" runat="server">
    <section>
            <div class="row">
                <div class="col-md-12">
                    <Rock:Zone Name="Middle" runat="server" />
                </div>
            </div>
    </section>
</asp:Content>

<asp:Content ID="ctMain2" ContentPlaceHolderID="mainSecond" runat="server">
    <section class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <Rock:Zone Name="Second Main" runat="server" />
                </div>
            </div>
    </section>
</asp:Content>

