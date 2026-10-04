Imports System.Windows.Forms

Public Class AboutForm
    Inherits Form

    Public Sub New()
        InitializeComponent()

        ' Load creativecode.png logo dynamically
        Try
            Dim exePath As String = Application.StartupPath
            Dim rootPath As String = exePath
            For i As Integer = 1 To 3
                Dim parent = System.IO.Directory.GetParent(rootPath)
                If parent IsNot Nothing Then
                    rootPath = parent.FullName
                End If
            Next

            Dim imagePath As String = System.IO.Path.Combine(rootPath, "creativecode.png")
            If System.IO.File.Exists(imagePath) Then
                picLogo.Image = Image.FromFile(imagePath)
            Else
                ' Fallback: check startup path directly
                imagePath = System.IO.Path.Combine(exePath, "creativecode.png")
                If System.IO.File.Exists(imagePath) Then
                    picLogo.Image = Image.FromFile(imagePath)
                End If
            End If
        Catch ex As Exception
            ' Safe fallback to avoid runtime errors if image is missing
        End Try
    End Sub

    Private Sub lblJon_Click(sender As Object, e As EventArgs) Handles lblJon.Click

    End Sub

    Private Sub lblGian_Click(sender As Object, e As EventArgs) Handles lblGian.Click

    End Sub

    Private Sub lblPaulo_Click(sender As Object, e As EventArgs) Handles lblPaulo.Click

    End Sub

    Private Sub picLogo_Click(sender As Object, e As EventArgs) Handles picLogo.Click

    End Sub

    Private Sub AboutForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
