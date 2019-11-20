﻿Public Class Form2
    Public WithEvents MenuButton As New Button

    Public Sub CreateWinForm() Handles MyBase.Load

        Dim WinMsg As New TextBox With {
            .Text = "Well done! You solved the maze.",
            .Name = "Win Message",
            .Location = New Point(50, 250),
            .Size = New Size(400, 100),
            .Font = New Font("Jokerman", 18, FontStyle.Regular),
            .ReadOnly = True,
            .Visible = True,
            .BorderStyle = BorderStyle.None,
            .BackColor = Color.GreenYellow
        }
        MenuButton.Size = New Size(200, 100)
        MenuButton.Font = New Font("Times New Roman", 16, FontStyle.Bold)
        MenuButton.Text = "Main Menu"
        MenuButton.Name = "Menu Button"
        MenuButton.Visible = True
        MenuButton.Location = New Point(150, 100)
        Controls.Add(MenuButton)
        Controls.Add(WinMsg)
    End Sub

    Private Sub MenuClick(ByVal sender As Object, ByVal e As EventArgs) Handles MenuButton.Click
        Form3.BackColor = Color.Orange
        Form3.Visible = True
        module1.HideWin()
    End Sub

End Class