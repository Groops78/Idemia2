﻿Public Class AlerteHorsScope
    Inherits System.Web.UI.Page

    Dim current_user As Utilisateur

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not CType(Session("Utilisateur"), Utilisateur) Is Nothing) Then

            current_user = CType(Session("Utilisateur"), Utilisateur)

            If (Not Page.IsPostBack()) Then

                'chargement des statuts

                ddlStatutAlerte.DataTextField = "Text"
                ddlStatutAlerte.DataValueField = "Value"

                ddlStatutAlerte.DataSource = DBFunctions.getStatutsAlerte()
                ddlStatutAlerte.DataBind()

                ddlStatutAlerte.Items.Insert(0, New ListItem("", ""))

                ddlStatutAlerte.SelectedValue = Session("filtre_statut_alerte")

            End If

            'Liste alertes
            ChargerAlertes(ddlStatutAlerte.SelectedValue)

        End If




    End Sub

    Private Sub ChargerAlertes(id_statut As String)


        Dim liste_alertes_facture As List(Of AlerteFacture) = DBFunctions.getAlertesFacture("", current_user, True, id_statut, True)
        litAlertes.Text = ""

        If (liste_alertes_facture.Count = 0) Then

            litAlertes.Text = "<tr><td colspan=""8"" style=""text-align:center;"">Aucune alerte</td></tr>"

        Else

            For Each alerte As AlerteFacture In liste_alertes_facture

                litAlertes.Text += "<tr class=""odd gradeX"">" &
                                                "<td>" & alerte.DocId & "</td>" &
                                                "<td>" & alerte.Motif.Libelle & "</td>" &
                                                "<td>" & alerte.Commentaire & "</td>" &
                                                "<td>" & alerte.Emetteur.Prenom & " " & alerte.Emetteur.Nom & "</td>" &
                                                "<td>" & alerte.DateAlerte & "</td>" &
                                                "<td>" & alerte.ResoluPar.Prenom & " " & alerte.ResoluPar.Nom & "</td>" &
                                                "<td>" & alerte.DateResolution & "</td>" &
                                                "<td>" & alerte.Statut.Libelle & "</td>" &
                                            "</tr>"
            Next

        End If

    End Sub

    Private Sub ddlStatutAlerte_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatutAlerte.SelectedIndexChanged

        If (Not current_user Is Nothing) Then

            Session("filtre_statut_alerte") = ddlStatutAlerte.SelectedValue

            ChargerAlertes(ddlStatutAlerte.SelectedValue)

        Else

            Response.Redirect("Connexion.aspx")

        End If

    End Sub


End Class