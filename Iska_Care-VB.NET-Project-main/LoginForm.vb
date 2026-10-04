Imports System.Windows.Forms

Public Class LoginForm
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property AuthenticatedRole As String
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property AuthenticatedUsername As String

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text

        If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please enter both username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim dbHelper As New DatabaseHelper()
            Dim role = dbHelper.AuthenticateUser(username, password)

            If Not String.IsNullOrEmpty(role) Then
                AuthenticatedRole = role
                AuthenticatedUsername = username
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Database connection error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim logoPath As String = System.IO.Path.Combine(Application.StartupPath, "piyupi.png")
            If System.IO.File.Exists(logoPath) Then
                picLogo.Image = Image.FromFile(logoPath)
            End If
        Catch ex As Exception
            ' Ignore image loading errors
        End Try
    End Sub

End Class
