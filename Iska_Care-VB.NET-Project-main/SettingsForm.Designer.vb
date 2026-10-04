<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SettingsForm
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
        pnlTop = New Panel()
        btnBackup = New Button()
        btnRestore = New Button()
        lblInfo = New Label()
        pnlTop.SuspendLayout()
        SuspendLayout()
        ' 
        ' lblTitle
        ' 
        lblTitle.Dock = DockStyle.Fill
        lblTitle.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(0, 0)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(500, 80)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Database Settings"
        lblTitle.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' pnlTop
        ' 
        pnlTop.BackColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        pnlTop.Controls.Add(lblTitle)
        pnlTop.Dock = DockStyle.Top
        pnlTop.Location = New Point(0, 0)
        pnlTop.Name = "pnlTop"
        pnlTop.Size = New Size(500, 80)
        pnlTop.TabIndex = 0
        ' 
        ' btnBackup
        ' 
        btnBackup.BackColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnBackup.FlatStyle = FlatStyle.Flat
        btnBackup.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnBackup.ForeColor = Color.White
        btnBackup.Location = New Point(50, 150)
        btnBackup.Name = "btnBackup"
        btnBackup.Size = New Size(180, 50)
        btnBackup.TabIndex = 1
        btnBackup.Text = "Backup Database"
        btnBackup.UseVisualStyleBackColor = False
        ' 
        ' btnRestore
        ' 
        btnRestore.BackColor = Color.White
        btnRestore.FlatStyle = FlatStyle.Flat
        btnRestore.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnRestore.ForeColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnRestore.Location = New Point(260, 150)
        btnRestore.Name = "btnRestore"
        btnRestore.Size = New Size(180, 50)
        btnRestore.TabIndex = 2
        btnRestore.Text = "Restore Database"
        btnRestore.UseVisualStyleBackColor = False
        ' 
        ' lblInfo
        ' 
        lblInfo.Location = New Point(50, 100)
        lblInfo.Name = "lblInfo"
        lblInfo.Size = New Size(400, 40)
        lblInfo.TabIndex = 3
        lblInfo.Text = "Use these tools to backup your clinic data to a safe location, or restore it from a previous backup file."
        ' 
        ' SettingsForm
        ' 
        ClientSize = New Size(500, 250)
        Controls.Add(pnlTop)
        Controls.Add(btnBackup)
        Controls.Add(btnRestore)
        Controls.Add(lblInfo)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        Name = "SettingsForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Settings"
        pnlTop.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnBackup As System.Windows.Forms.Button
    Friend WithEvents btnRestore As System.Windows.Forms.Button
    Friend WithEvents lblInfo As System.Windows.Forms.Label
End Class
