<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AppointmentsForm
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
        pnlTop = New Panel()
        lblTitle = New Label()
        pnlLeft = New Panel()
        calAppointments = New MonthCalendar()
        pnlAdd = New Panel()
        lblPatient = New Label()
        txtPatient = New TextBox()
        lblTime = New Label()
        dtpTime = New DateTimePicker()
        lblReason = New Label()
        txtReason = New TextBox()
        btnAdd = New Button()
        pnlRight = New Panel()
        dgvAppointments = New DataGridView()
        pnlRightTop = New Panel()
        btnDelete = New Button()
        pnlTop.SuspendLayout()
        pnlLeft.SuspendLayout()
        pnlAdd.SuspendLayout()
        pnlRight.SuspendLayout()
        CType(dgvAppointments, ComponentModel.ISupportInitialize).BeginInit()
        pnlRightTop.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlTop
        ' 
        pnlTop.BackColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        pnlTop.Controls.Add(lblTitle)
        pnlTop.Dock = DockStyle.Top
        pnlTop.Location = New Point(0, 0)
        pnlTop.Name = "pnlTop"
        pnlTop.Size = New Size(1107, 80)
        pnlTop.TabIndex = 2
        ' 
        ' lblTitle
        ' 
        lblTitle.Dock = DockStyle.Fill
        lblTitle.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(0, 0)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(1107, 80)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Appointments"
        lblTitle.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' pnlLeft
        ' 
        pnlLeft.BackColor = Color.WhiteSmoke
        pnlLeft.Controls.Add(calAppointments)
        pnlLeft.Controls.Add(pnlAdd)
        pnlLeft.Dock = DockStyle.Left
        pnlLeft.Location = New Point(0, 80)
        pnlLeft.Name = "pnlLeft"
        pnlLeft.Padding = New Padding(20)
        pnlLeft.Size = New Size(350, 649)
        pnlLeft.TabIndex = 1
        ' 
        ' calAppointments
        ' 
        calAppointments.Location = New Point(20, 20)
        calAppointments.MaxSelectionCount = 1
        calAppointments.Name = "calAppointments"
        calAppointments.TabIndex = 0
        ' 
        ' pnlAdd
        ' 
        pnlAdd.Controls.Add(lblPatient)
        pnlAdd.Controls.Add(txtPatient)
        pnlAdd.Controls.Add(lblTime)
        pnlAdd.Controls.Add(dtpTime)
        pnlAdd.Controls.Add(lblReason)
        pnlAdd.Controls.Add(txtReason)
        pnlAdd.Controls.Add(btnAdd)
        pnlAdd.Location = New Point(20, 220)
        pnlAdd.Name = "pnlAdd"
        pnlAdd.Size = New Size(310, 300)
        pnlAdd.TabIndex = 1
        ' 
        ' lblPatient
        ' 
        lblPatient.AutoSize = True
        lblPatient.Location = New Point(0, 20)
        lblPatient.Name = "lblPatient"
        lblPatient.Size = New Size(101, 20)
        lblPatient.TabIndex = 0
        lblPatient.Text = "Patient Name:"
        ' 
        ' txtPatient
        ' 
        txtPatient.Location = New Point(0, 45)
        txtPatient.Name = "txtPatient"
        txtPatient.Size = New Size(300, 27)
        txtPatient.TabIndex = 1
        ' 
        ' lblTime
        ' 
        lblTime.AutoSize = True
        lblTime.Location = New Point(0, 85)
        lblTime.Name = "lblTime"
        lblTime.Size = New Size(45, 20)
        lblTime.TabIndex = 2
        lblTime.Text = "Time:"
        ' 
        ' dtpTime
        ' 
        dtpTime.Format = DateTimePickerFormat.Time
        dtpTime.Location = New Point(0, 110)
        dtpTime.Name = "dtpTime"
        dtpTime.ShowUpDown = True
        dtpTime.Size = New Size(150, 27)
        dtpTime.TabIndex = 3
        ' 
        ' lblReason
        ' 
        lblReason.AutoSize = True
        lblReason.Location = New Point(0, 150)
        lblReason.Name = "lblReason"
        lblReason.Size = New Size(60, 20)
        lblReason.TabIndex = 4
        lblReason.Text = "Reason:"
        ' 
        ' txtReason
        ' 
        txtReason.Location = New Point(0, 175)
        txtReason.Multiline = True
        txtReason.Name = "txtReason"
        txtReason.Size = New Size(300, 60)
        txtReason.TabIndex = 5
        ' 
        ' btnAdd
        ' 
        btnAdd.BackColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.ForeColor = Color.White
        btnAdd.Location = New Point(0, 250)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New Size(150, 40)
        btnAdd.TabIndex = 6
        btnAdd.Text = "Schedule Appointment"
        btnAdd.UseVisualStyleBackColor = False
        ' 
        ' pnlRight
        ' 
        pnlRight.BackColor = Color.White
        pnlRight.Controls.Add(dgvAppointments)
        pnlRight.Controls.Add(pnlRightTop)
        pnlRight.Dock = DockStyle.Fill
        pnlRight.Location = New Point(350, 80)
        pnlRight.Name = "pnlRight"
        pnlRight.Padding = New Padding(20)
        pnlRight.Size = New Size(757, 649)
        pnlRight.TabIndex = 0
        ' 
        ' dgvAppointments
        ' 
        dgvAppointments.AllowUserToAddRows = False
        dgvAppointments.AllowUserToDeleteRows = False
        dgvAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAppointments.BackgroundColor = Color.WhiteSmoke
        dgvAppointments.ColumnHeadersHeight = 29
        dgvAppointments.Dock = DockStyle.Fill
        dgvAppointments.Location = New Point(20, 70)
        dgvAppointments.Name = "dgvAppointments"
        dgvAppointments.ReadOnly = True
        dgvAppointments.RowHeadersWidth = 51
        dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAppointments.Size = New Size(717, 559)
        dgvAppointments.TabIndex = 0
        ' 
        ' pnlRightTop
        ' 
        pnlRightTop.Controls.Add(btnDelete)
        pnlRightTop.Dock = DockStyle.Top
        pnlRightTop.Location = New Point(20, 20)
        pnlRightTop.Name = "pnlRightTop"
        pnlRightTop.Size = New Size(717, 50)
        pnlRightTop.TabIndex = 1
        ' 
        ' btnDelete
        ' 
        btnDelete.BackColor = Color.White
        btnDelete.FlatStyle = FlatStyle.Flat
        btnDelete.ForeColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnDelete.Location = New Point(0, 0)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(120, 35)
        btnDelete.TabIndex = 0
        btnDelete.Text = "Delete Selected"
        btnDelete.UseVisualStyleBackColor = False
        ' 
        ' AppointmentsForm
        ' 
        ClientSize = New Size(1107, 729)
        Controls.Add(pnlRight)
        Controls.Add(pnlLeft)
        Controls.Add(pnlTop)
        Name = "AppointmentsForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Appointments"
        pnlTop.ResumeLayout(False)
        pnlLeft.ResumeLayout(False)
        pnlAdd.ResumeLayout(False)
        pnlAdd.PerformLayout()
        pnlRight.ResumeLayout(False)
        CType(dgvAppointments, ComponentModel.ISupportInitialize).EndInit()
        pnlRightTop.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents calAppointments As System.Windows.Forms.MonthCalendar
    Friend WithEvents pnlAdd As System.Windows.Forms.Panel
    Friend WithEvents lblPatient As System.Windows.Forms.Label
    Friend WithEvents txtPatient As System.Windows.Forms.TextBox
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents dtpTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblReason As System.Windows.Forms.Label
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents pnlRightTop As System.Windows.Forms.Panel
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents dgvAppointments As System.Windows.Forms.DataGridView
End Class
