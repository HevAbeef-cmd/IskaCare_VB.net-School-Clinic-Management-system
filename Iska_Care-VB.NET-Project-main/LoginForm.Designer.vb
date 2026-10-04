<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LoginForm
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        lblTitle = New Label()
        lblUsername = New Label()
        txtUsername = New TextBox()
        lblPassword = New Label()
        txtPassword = New TextBox()
        btnLogin = New Button()
        btnExit = New Button()
        pnlTop = New Panel()
        picLogo = New PictureBox()
        pnlTop.SuspendLayout()
        CType(picLogo, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblTitle
        ' 
        lblTitle.Font = New Font("Segoe UI", 11.0!, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(0, 105)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(400, 45)
        lblTitle.TabIndex = 0
        lblTitle.Text = "PUP Lopez Medical and Dental Services"
        lblTitle.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblUsername
        ' 
        lblUsername.AutoSize = True
        lblUsername.Font = New Font("Segoe UI", 10F)
        lblUsername.Location = New Point(50, 210)
        lblUsername.Name = "lblUsername"
        lblUsername.Size = New Size(74, 19)
        lblUsername.TabIndex = 1
        lblUsername.Text = "Username:"
        ' 
        ' txtUsername
        ' 
        txtUsername.Font = New Font("Segoe UI", 10F)
        txtUsername.Location = New Point(50, 240)
        txtUsername.Name = "txtUsername"
        txtUsername.Size = New Size(300, 25)
        txtUsername.TabIndex = 2
        ' 
        ' lblPassword
        ' 
        lblPassword.AutoSize = True
        lblPassword.Font = New Font("Segoe UI", 10F)
        lblPassword.Location = New Point(50, 290)
        lblPassword.Name = "lblPassword"
        lblPassword.Size = New Size(70, 19)
        lblPassword.TabIndex = 3
        lblPassword.Text = "Password:"
        ' 
        ' txtPassword
        ' 
        txtPassword.Font = New Font("Segoe UI", 10F)
        txtPassword.Location = New Point(50, 320)
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(300, 25)
        txtPassword.TabIndex = 4
        txtPassword.UseSystemPasswordChar = True
        ' 
        ' btnLogin
        ' 
        btnLogin.BackColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnLogin.FlatStyle = FlatStyle.Flat
        btnLogin.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnLogin.ForeColor = Color.White
        btnLogin.Location = New Point(50, 380)
        btnLogin.Name = "btnLogin"
        btnLogin.Size = New Size(140, 40)
        btnLogin.TabIndex = 5
        btnLogin.Text = "Login"
        btnLogin.UseVisualStyleBackColor = False
        ' 
        ' btnExit
        ' 
        btnExit.BackColor = Color.White
        btnExit.FlatStyle = FlatStyle.Flat
        btnExit.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnExit.ForeColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnExit.Location = New Point(210, 380)
        btnExit.Name = "btnExit"
        btnExit.Size = New Size(140, 40)
        btnExit.TabIndex = 6
        btnExit.Text = "Exit"
        btnExit.UseVisualStyleBackColor = False
        ' 
        ' pnlTop
        ' 
        pnlTop.BackColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        pnlTop.Controls.Add(picLogo)
        pnlTop.Controls.Add(lblTitle)
        pnlTop.Dock = DockStyle.Top
        pnlTop.Location = New Point(0, 0)
        pnlTop.Name = "pnlTop"
        pnlTop.Size = New Size(400, 170)
        pnlTop.TabIndex = 0
        ' 
        ' picLogo
        ' 
        picLogo.Location = New Point(160, 15)
        picLogo.Name = "picLogo"
        picLogo.Size = New Size(80, 80)
        picLogo.SizeMode = PictureBoxSizeMode.Zoom
        picLogo.TabIndex = 1
        picLogo.TabStop = False
        ' 
        ' LoginForm
        ' 
        ClientSize = New Size(400, 470)
        Controls.Add(pnlTop)
        Controls.Add(lblUsername)
        Controls.Add(txtUsername)
        Controls.Add(lblPassword)
        Controls.Add(txtPassword)
        Controls.Add(btnLogin)
        Controls.Add(btnExit)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        Name = "LoginForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "PUP Lopez Medical and Dental Services — Login"
        pnlTop.ResumeLayout(False)
        CType(picLogo, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblUsername As System.Windows.Forms.Label
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents picLogo As System.Windows.Forms.PictureBox
End Class
