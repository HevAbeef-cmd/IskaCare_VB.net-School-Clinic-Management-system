<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class VisitDetailsForm
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

    Friend WithEvents lblGender As System.Windows.Forms.Label
    Friend WithEvents cboGender As System.Windows.Forms.ComboBox
    Friend WithEvents lblStudentID As System.Windows.Forms.Label
    Friend WithEvents txtStudentID As System.Windows.Forms.TextBox
    Friend WithEvents lblYear As System.Windows.Forms.Label
    Friend WithEvents cboYear As System.Windows.Forms.ComboBox
    Friend WithEvents lblDoctor As System.Windows.Forms.Label
    Friend WithEvents txtDoctor As System.Windows.Forms.TextBox

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        lblType = New Label()
        cboType = New ComboBox()
        lblName = New Label()
        txtName = New TextBox()
        lblAge = New Label()
        numAge = New NumericUpDown()
        lblHeight = New Label()
        numHeight = New NumericUpDown()
        lblWeight = New Label()
        numWeight = New NumericUpDown()
        lblCourse = New Label()
        cboCourse = New ComboBox()
        lblJob = New Label()
        txtJob = New TextBox()
        lblContact = New Label()
        txtContact = New TextBox()
        lblReason = New Label()
        txtReason = New TextBox()
        lblSickness = New Label()
        txtSickness = New TextBox()
        lblInjury = New Label()
        txtInjury = New TextBox()
        lblPrescription = New Label()
        txtPrescription = New ComboBox()
        lblTimeIn = New Label()
        dtpTimeIn = New DateTimePicker()
        lblTimeOut = New Label()
        chkTimeOut = New CheckBox()
        dtpTimeOut = New DateTimePicker()
        btnSave = New Button()
        btnCancel = New Button()
        lblGender = New Label()
        cboGender = New ComboBox()
        lblStudentID = New Label()
        txtStudentID = New TextBox()
        lblYear = New Label()
        cboYear = New ComboBox()
        lblDoctor = New Label()
        txtDoctor = New TextBox()
        CType(numAge, ComponentModel.ISupportInitialize).BeginInit()
        CType(numHeight, ComponentModel.ISupportInitialize).BeginInit()
        CType(numWeight, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblType
        ' 
        lblType.AutoSize = True
        lblType.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblType.Location = New Point(23, 27)
        lblType.Name = "lblType"
        lblType.Size = New Size(100, 20)
        lblType.TabIndex = 0
        lblType.Text = "Patient Type:"
        ' 
        ' cboType
        ' 
        cboType.DropDownStyle = ComboBoxStyle.DropDownList
        cboType.FormattingEnabled = True
        cboType.Items.AddRange(New Object() {"Student", "Employee", "Other"})
        cboType.Location = New Point(171, 27)
        cboType.Margin = New Padding(3, 4, 3, 4)
        cboType.Name = "cboType"
        cboType.Size = New Size(285, 28)
        cboType.TabIndex = 1
        ' 
        ' lblName
        ' 
        lblName.AutoSize = True
        lblName.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblName.Location = New Point(23, 73)
        lblName.Name = "lblName"
        lblName.Size = New Size(55, 20)
        lblName.TabIndex = 2
        lblName.Text = "Name:"
        ' 
        ' txtName
        ' 
        txtName.Location = New Point(171, 73)
        txtName.Margin = New Padding(3, 4, 3, 4)
        txtName.Name = "txtName"
        txtName.Size = New Size(285, 27)
        txtName.TabIndex = 3
        ' 
        ' lblAge
        ' 
        lblAge.AutoSize = True
        lblAge.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblAge.Location = New Point(23, 120)
        lblAge.Name = "lblAge"
        lblAge.Size = New Size(41, 20)
        lblAge.TabIndex = 4
        lblAge.Text = "Age:"
        ' 
        ' numAge
        ' 
        numAge.Location = New Point(171, 120)
        numAge.Margin = New Padding(3, 4, 3, 4)
        numAge.Maximum = New Decimal(New Integer() {150, 0, 0, 0})
        numAge.Name = "numAge"
        numAge.Size = New Size(114, 27)
        numAge.TabIndex = 5
        ' 
        ' lblHeight
        ' 
        lblHeight.AutoSize = True
        lblHeight.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblHeight.Location = New Point(23, 167)
        lblHeight.Name = "lblHeight"
        lblHeight.Size = New Size(97, 20)
        lblHeight.TabIndex = 6
        lblHeight.Text = "Height (cm):"
        ' 
        ' numHeight
        ' 
        numHeight.DecimalPlaces = 1
        numHeight.Location = New Point(171, 167)
        numHeight.Margin = New Padding(3, 4, 3, 4)
        numHeight.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        numHeight.Name = "numHeight"
        numHeight.Size = New Size(114, 27)
        numHeight.TabIndex = 7
        ' 
        ' lblWeight
        ' 
        lblWeight.AutoSize = True
        lblWeight.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblWeight.Location = New Point(23, 213)
        lblWeight.Name = "lblWeight"
        lblWeight.Size = New Size(97, 20)
        lblWeight.TabIndex = 8
        lblWeight.Text = "Weight (kg):"
        ' 
        ' numWeight
        ' 
        numWeight.DecimalPlaces = 1
        numWeight.Location = New Point(171, 213)
        numWeight.Margin = New Padding(3, 4, 3, 4)
        numWeight.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        numWeight.Name = "numWeight"
        numWeight.Size = New Size(114, 27)
        numWeight.TabIndex = 9
        ' 
        ' lblCourse
        ' 
        lblCourse.AutoSize = True
        lblCourse.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblCourse.Location = New Point(23, 260)
        lblCourse.Name = "lblCourse"
        lblCourse.Size = New Size(61, 20)
        lblCourse.TabIndex = 10
        lblCourse.Text = "Course:"
        ' 
        ' cboCourse
        ' 
        cboCourse.DropDownStyle = ComboBoxStyle.DropDownList
        cboCourse.FormattingEnabled = True
        cboCourse.Items.AddRange(New Object() {"BEED", "BPA", "BPA-FA", "BS-Account", "BSAM", "BS-Archi", "BSBA-FM", "BSBA-MM", "BS-Bio", "BSCE", "BSED-MT", "BSEE", "BSHM", "BSIT", "BSND", "BSOA", "DCVET", "DCET", "DEET", "DIT", "DOMT-LOM", "DOMT-MOM"})
        cboCourse.Location = New Point(171, 260)
        cboCourse.Margin = New Padding(3, 4, 3, 4)
        cboCourse.Name = "cboCourse"
        cboCourse.Size = New Size(285, 28)
        cboCourse.TabIndex = 11
        ' 
        ' lblJob
        ' 
        lblJob.AutoSize = True
        lblJob.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblJob.Location = New Point(23, 307)
        lblJob.Name = "lblJob"
        lblJob.Size = New Size(38, 20)
        lblJob.TabIndex = 12
        lblJob.Text = "Job:"
        ' 
        ' txtJob
        ' 
        txtJob.Location = New Point(171, 307)
        txtJob.Margin = New Padding(3, 4, 3, 4)
        txtJob.Name = "txtJob"
        txtJob.Size = New Size(285, 27)
        txtJob.TabIndex = 13
        ' 
        ' lblContact
        ' 
        lblContact.AutoSize = True
        lblContact.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblContact.Location = New Point(23, 353)
        lblContact.Name = "lblContact"
        lblContact.Size = New Size(130, 20)
        lblContact.TabIndex = 14
        lblContact.Text = "Contact Number:"
        ' 
        ' txtContact
        ' 
        txtContact.Location = New Point(171, 353)
        txtContact.Margin = New Padding(3, 4, 3, 4)
        txtContact.Name = "txtContact"
        txtContact.Size = New Size(285, 27)
        txtContact.TabIndex = 15
        ' 
        ' lblReason
        ' 
        lblReason.AutoSize = True
        lblReason.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblReason.Location = New Point(23, 400)
        lblReason.Name = "lblReason"
        lblReason.Size = New Size(124, 20)
        lblReason.TabIndex = 16
        lblReason.Text = "Reason for Visit:"
        ' 
        ' txtReason
        ' 
        txtReason.Location = New Point(171, 400)
        txtReason.Margin = New Padding(3, 4, 3, 4)
        txtReason.Name = "txtReason"
        txtReason.Size = New Size(285, 27)
        txtReason.TabIndex = 17
        ' 
        ' lblSickness
        ' 
        lblSickness.AutoSize = True
        lblSickness.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblSickness.Location = New Point(23, 447)
        lblSickness.Name = "lblSickness"
        lblSickness.Size = New Size(71, 20)
        lblSickness.TabIndex = 18
        lblSickness.Text = "Sickness:"
        ' 
        ' txtSickness
        ' 
        txtSickness.Location = New Point(171, 447)
        txtSickness.Margin = New Padding(3, 4, 3, 4)
        txtSickness.Name = "txtSickness"
        txtSickness.Size = New Size(285, 27)
        txtSickness.TabIndex = 19
        ' 
        ' lblInjury
        ' 
        lblInjury.AutoSize = True
        lblInjury.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblInjury.Location = New Point(23, 493)
        lblInjury.Name = "lblInjury"
        lblInjury.Size = New Size(55, 20)
        lblInjury.TabIndex = 20
        lblInjury.Text = "Injury:"
        ' 
        ' txtInjury
        ' 
        txtInjury.Location = New Point(171, 493)
        txtInjury.Margin = New Padding(3, 4, 3, 4)
        txtInjury.Name = "txtInjury"
        txtInjury.Size = New Size(285, 27)
        txtInjury.TabIndex = 21
        ' 
        ' lblPrescription
        ' 
        lblPrescription.AutoSize = True
        lblPrescription.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblPrescription.Location = New Point(23, 540)
        lblPrescription.Name = "lblPrescription"
        lblPrescription.Size = New Size(97, 20)
        lblPrescription.TabIndex = 22
        lblPrescription.Text = "Prescription:"
        ' 
        ' 
        ' txtPrescription
        ' 
        txtPrescription.DropDownStyle = ComboBoxStyle.DropDown
        txtPrescription.Location = New Point(171, 540)
        txtPrescription.Margin = New Padding(3, 4, 3, 4)
        txtPrescription.Name = "txtPrescription"
        txtPrescription.Size = New Size(285, 28)
        txtPrescription.TabIndex = 23
        ' 
        ' lblTimeIn
        ' 
        lblTimeIn.AutoSize = True
        lblTimeIn.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblTimeIn.Location = New Point(23, 587)
        lblTimeIn.Name = "lblTimeIn"
        lblTimeIn.Size = New Size(66, 20)
        lblTimeIn.TabIndex = 24
        lblTimeIn.Text = "Time In:"
        ' 
        ' dtpTimeIn
        ' 
        dtpTimeIn.CustomFormat = "MM/dd/yyyy hh:mm tt"
        dtpTimeIn.Format = DateTimePickerFormat.Custom
        dtpTimeIn.Location = New Point(171, 587)
        dtpTimeIn.Margin = New Padding(3, 4, 3, 4)
        dtpTimeIn.Name = "dtpTimeIn"
        dtpTimeIn.Size = New Size(228, 27)
        dtpTimeIn.TabIndex = 25
        ' 
        ' lblTimeOut
        ' 
        lblTimeOut.AutoSize = True
        lblTimeOut.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblTimeOut.Location = New Point(23, 633)
        lblTimeOut.Name = "lblTimeOut"
        lblTimeOut.Size = New Size(78, 20)
        lblTimeOut.TabIndex = 26
        lblTimeOut.Text = "Time Out:"
        ' 
        ' chkTimeOut
        ' 
        chkTimeOut.AutoSize = True
        chkTimeOut.Location = New Point(137, 636)
        chkTimeOut.Margin = New Padding(3, 4, 3, 4)
        chkTimeOut.Name = "chkTimeOut"
        chkTimeOut.Size = New Size(18, 17)
        chkTimeOut.TabIndex = 27
        chkTimeOut.UseVisualStyleBackColor = True
        ' 
        ' dtpTimeOut
        ' 
        dtpTimeOut.CustomFormat = "MM/dd/yyyy hh:mm tt"
        dtpTimeOut.Format = DateTimePickerFormat.Custom
        dtpTimeOut.Location = New Point(171, 633)
        dtpTimeOut.Margin = New Padding(3, 4, 3, 4)
        dtpTimeOut.Name = "dtpTimeOut"
        dtpTimeOut.Size = New Size(228, 27)
        dtpTimeOut.TabIndex = 28
        ' 
        ' btnSave
        ' 
        btnSave.BackColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnSave.Cursor = Cursors.Hand
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnSave.ForeColor = Color.White
        btnSave.Location = New Point(709, 693)
        btnSave.Margin = New Padding(3, 4, 3, 4)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(91, 40)
        btnSave.TabIndex = 29
        btnSave.Text = "Save"
        btnSave.UseVisualStyleBackColor = False
        ' 
        ' btnCancel
        ' 
        btnCancel.BackColor = Color.White
        btnCancel.Cursor = Cursors.Hand
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnCancel.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        btnCancel.Location = New Point(823, 693)
        btnCancel.Margin = New Padding(3, 4, 3, 4)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(91, 40)
        btnCancel.TabIndex = 30
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = False
        ' 
        ' lblGender
        ' 
        lblGender.AutoSize = True
        lblGender.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblGender.Location = New Point(480, 27)
        lblGender.Name = "lblGender"
        lblGender.Size = New Size(65, 20)
        lblGender.TabIndex = 31
        lblGender.Text = "Gender:"
        ' 
        ' cboGender
        ' 
        cboGender.DropDownStyle = ComboBoxStyle.DropDownList
        cboGender.FormattingEnabled = True
        cboGender.Items.AddRange(New Object() {"Male", "Female", "Other"})
        cboGender.Location = New Point(629, 27)
        cboGender.Margin = New Padding(3, 4, 3, 4)
        cboGender.Name = "cboGender"
        cboGender.Size = New Size(285, 28)
        cboGender.TabIndex = 32
        ' 
        ' lblStudentID
        ' 
        lblStudentID.AutoSize = True
        lblStudentID.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblStudentID.Location = New Point(480, 73)
        lblStudentID.Name = "lblStudentID"
        lblStudentID.Size = New Size(88, 20)
        lblStudentID.TabIndex = 33
        lblStudentID.Text = "Student ID:"
        ' 
        ' txtStudentID
        ' 
        txtStudentID.Location = New Point(629, 73)
        txtStudentID.Margin = New Padding(3, 4, 3, 4)
        txtStudentID.Name = "txtStudentID"
        txtStudentID.Size = New Size(285, 27)
        txtStudentID.TabIndex = 34
        ' 
        ' lblYear
        ' 
        lblYear.AutoSize = True
        lblYear.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblYear.Location = New Point(480, 120)
        lblYear.Name = "lblYear"
        lblYear.Size = New Size(44, 20)
        lblYear.TabIndex = 35
        lblYear.Text = "Year:"
        ' 
        ' cboYear
        ' 
        cboYear.DropDownStyle = ComboBoxStyle.DropDownList
        cboYear.FormattingEnabled = True
        cboYear.Items.AddRange(New Object() {"1st", "2nd", "3rd", "4th", "Ladderize", "Overstaying"})
        cboYear.Location = New Point(629, 120)
        cboYear.Margin = New Padding(3, 4, 3, 4)
        cboYear.Name = "cboYear"
        cboYear.Size = New Size(285, 28)
        cboYear.TabIndex = 36
        ' 
        ' lblDoctor
        ' 
        lblDoctor.AutoSize = True
        lblDoctor.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblDoctor.Location = New Point(480, 167)
        lblDoctor.Name = "lblDoctor"
        lblDoctor.Size = New Size(129, 20)
        lblDoctor.TabIndex = 37
        lblDoctor.Text = "Doctor Assigned:"
        ' 
        ' txtDoctor
        ' 
        txtDoctor.Location = New Point(629, 167)
        txtDoctor.Margin = New Padding(3, 4, 3, 4)
        txtDoctor.Name = "txtDoctor"
        txtDoctor.ReadOnly = True
        txtDoctor.Size = New Size(285, 27)
        txtDoctor.TabIndex = 38
        ' 
        ' VisitDetailsForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(971, 800)
        Controls.Add(lblGender)
        Controls.Add(cboGender)
        Controls.Add(lblStudentID)
        Controls.Add(txtStudentID)
        Controls.Add(lblYear)
        Controls.Add(cboYear)
        Controls.Add(lblDoctor)
        Controls.Add(txtDoctor)
        Controls.Add(btnCancel)
        Controls.Add(btnSave)
        Controls.Add(dtpTimeOut)
        Controls.Add(chkTimeOut)
        Controls.Add(lblTimeOut)
        Controls.Add(dtpTimeIn)
        Controls.Add(lblTimeIn)
        Controls.Add(txtPrescription)
        Controls.Add(lblPrescription)
        Controls.Add(txtInjury)
        Controls.Add(lblInjury)
        Controls.Add(txtSickness)
        Controls.Add(lblSickness)
        Controls.Add(txtReason)
        Controls.Add(lblReason)
        Controls.Add(txtContact)
        Controls.Add(lblContact)
        Controls.Add(txtJob)
        Controls.Add(lblJob)
        Controls.Add(cboCourse)
        Controls.Add(lblCourse)
        Controls.Add(numWeight)
        Controls.Add(lblWeight)
        Controls.Add(numHeight)
        Controls.Add(lblHeight)
        Controls.Add(numAge)
        Controls.Add(lblAge)
        Controls.Add(txtName)
        Controls.Add(lblName)
        Controls.Add(cboType)
        Controls.Add(lblType)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(3, 4, 3, 4)
        MaximizeBox = False
        MinimizeBox = False
        Name = "VisitDetailsForm"
        StartPosition = FormStartPosition.CenterParent
        Text = "Visit Details"
        CType(numAge, ComponentModel.ISupportInitialize).EndInit()
        CType(numHeight, ComponentModel.ISupportInitialize).EndInit()
        CType(numWeight, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents cboType As System.Windows.Forms.ComboBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblAge As System.Windows.Forms.Label
    Friend WithEvents numAge As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblHeight As System.Windows.Forms.Label
    Friend WithEvents numHeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblWeight As System.Windows.Forms.Label
    Friend WithEvents numWeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblCourse As System.Windows.Forms.Label
    Friend WithEvents cboCourse As System.Windows.Forms.ComboBox
    Friend WithEvents lblJob As System.Windows.Forms.Label
    Friend WithEvents txtJob As System.Windows.Forms.TextBox
    Friend WithEvents lblContact As System.Windows.Forms.Label
    Friend WithEvents txtContact As System.Windows.Forms.TextBox
    Friend WithEvents lblReason As System.Windows.Forms.Label
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents lblSickness As System.Windows.Forms.Label
    Friend WithEvents txtSickness As System.Windows.Forms.TextBox
    Friend WithEvents lblInjury As System.Windows.Forms.Label
    Friend WithEvents txtInjury As System.Windows.Forms.TextBox
    Friend WithEvents lblPrescription As System.Windows.Forms.Label
    Friend WithEvents txtPrescription As System.Windows.Forms.ComboBox
    Friend WithEvents lblTimeIn As System.Windows.Forms.Label
    Friend WithEvents dtpTimeIn As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTimeOut As System.Windows.Forms.Label
    Friend WithEvents chkTimeOut As System.Windows.Forms.CheckBox
    Friend WithEvents dtpTimeOut As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
