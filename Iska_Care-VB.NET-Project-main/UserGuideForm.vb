Imports System.Windows.Forms
Imports System.Drawing
Imports System.Collections.Generic

Public Class UserGuideForm
    Inherits Form

    Private ReadOnly mainForm As Form1
    Private ReadOnly dbHelper As DatabaseHelper
    Private currentStep As Integer = 1
    
    Private demoForm As VisitDetailsForm = Nothing
    Private demoInventoryForm As InventoryForm = Nothing
    Private demoAppointmentsForm As AppointmentsForm = Nothing
    
    Private pnlTop As Panel
    Private lblGuideTitle As Label
    Private lblGuideSubtitle As Label
    
    Private pnlBottom As Panel
    Private chkDontShow As CheckBox
    Private btnBack As Button
    Private btnNext As Button
    Private btnSkip As Button
    
    Private pnlMain As Panel
    Private pnlStepContent As FlowLayoutPanel
    Private lblStepHeader As Label

    Public Sub New(parent As Form1)
        MyBase.New()
        mainForm = parent
        dbHelper = New DatabaseHelper()
        InitializeFormControls()
        ShowStep(1)
    End Sub

    Private Sub InitializeFormControls()
        Me.Size = New Size(620, 480)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.StartPosition = FormStartPosition.Manual
        Me.Text = "Iska Care — User Guide & Onboarding"
        Me.BackColor = Color.WhiteSmoke
        
        ' Top Panel
        pnlTop = New Panel()
        pnlTop.BackColor = Color.FromArgb(128, 0, 0)
        pnlTop.Height = 85
        pnlTop.Dock = DockStyle.Top
        
        lblGuideTitle = New Label()
        lblGuideTitle.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold)
        lblGuideTitle.ForeColor = Color.White
        lblGuideTitle.Location = New Point(20, 15)
        lblGuideTitle.Size = New Size(560, 25)
        lblGuideTitle.Text = "Clinic Assistant User Guide"
        pnlTop.Controls.Add(lblGuideTitle)
        
        lblGuideSubtitle = New Label()
        lblGuideSubtitle.Font = New Font("Segoe UI", 10.0F)
        lblGuideSubtitle.ForeColor = Color.FromArgb(240, 240, 240)
        lblGuideSubtitle.Location = New Point(20, 45)
        lblGuideSubtitle.Size = New Size(560, 20)
        pnlTop.Controls.Add(lblGuideSubtitle)
        
        Me.Controls.Add(pnlTop)
        
        ' Bottom Panel
        pnlBottom = New Panel()
        pnlBottom.BackColor = Color.White
        pnlBottom.Height = 70
        pnlBottom.Dock = DockStyle.Bottom
        
        ' Top border line for bottom panel
        Dim borderLine As New Panel()
        borderLine.BackColor = Color.LightGray
        borderLine.Height = 1
        borderLine.Dock = DockStyle.Top
        pnlBottom.Controls.Add(borderLine)
        
        chkDontShow = New CheckBox()
        chkDontShow.Font = New Font("Segoe UI", 9.5F)
        chkDontShow.ForeColor = Color.Black
        chkDontShow.Location = New Point(20, 20)
        chkDontShow.Size = New Size(280, 25)
        chkDontShow.Text = "Don't show this guide on startup again"
        pnlBottom.Controls.Add(chkDontShow)
        
        btnBack = New Button()
        btnBack.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnBack.FlatStyle = FlatStyle.Flat
        btnBack.FlatAppearance.BorderSize = 1
        btnBack.FlatAppearance.BorderColor = Color.FromArgb(128, 0, 0)
        btnBack.ForeColor = Color.FromArgb(128, 0, 0)
        btnBack.BackColor = Color.White
        btnBack.Location = New Point(310, 15)
        btnBack.Size = New Size(80, 35)
        btnBack.Text = "Back"
        btnBack.Cursor = Cursors.Hand
        AddHandler btnBack.Click, AddressOf btnBack_Click
        pnlBottom.Controls.Add(btnBack)
        
        btnNext = New Button()
        btnNext.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnNext.FlatStyle = FlatStyle.Flat
        btnNext.FlatAppearance.BorderSize = 0
        btnNext.ForeColor = Color.White
        btnNext.BackColor = Color.FromArgb(128, 0, 0)
        btnNext.Location = New Point(400, 15)
        btnNext.Size = New Size(90, 35)
        btnNext.Text = "Next"
        btnNext.Cursor = Cursors.Hand
        AddHandler btnNext.Click, AddressOf btnNext_Click
        pnlBottom.Controls.Add(btnNext)
        
        btnSkip = New Button()
        btnSkip.Font = New Font("Segoe UI", 10.0F)
        btnSkip.FlatStyle = FlatStyle.Flat
        btnSkip.FlatAppearance.BorderSize = 1
        btnSkip.FlatAppearance.BorderColor = Color.Gray
        btnSkip.ForeColor = Color.Gray
        btnSkip.BackColor = Color.White
        btnSkip.Location = New Point(500, 15)
        btnSkip.Size = New Size(80, 35)
        btnSkip.Text = "Skip"
        btnSkip.Cursor = Cursors.Hand
        AddHandler btnSkip.Click, AddressOf btnSkip_Click
        pnlBottom.Controls.Add(btnSkip)
        
        Me.Controls.Add(pnlBottom)
        
        ' Main Panel
        pnlMain = New Panel()
        pnlMain.BackColor = Color.White
        pnlMain.Dock = DockStyle.Fill
        pnlMain.Padding = New Padding(25)
        
        pnlStepContent = New FlowLayoutPanel()
        pnlStepContent.FlowDirection = FlowDirection.TopDown
        pnlStepContent.WrapContents = False
        pnlStepContent.Dock = DockStyle.Fill
        pnlStepContent.AutoScroll = True
        
        pnlMain.Controls.Add(pnlStepContent)
        Me.Controls.Add(pnlMain)
    End Sub

    Private Sub ShowStep(stepNumber As Integer)
        ' Close demo form if transitioning away from Step 3
        If stepNumber <> 3 AndAlso demoForm IsNot Nothing Then
            If Not demoForm.IsDisposed Then
                demoForm.Close()
            End If
            demoForm = Nothing
        End If

        ' Close demo inventory form if transitioning away from Step 4
        If stepNumber <> 4 AndAlso demoInventoryForm IsNot Nothing Then
            If Not demoInventoryForm.IsDisposed Then
                demoInventoryForm.Close()
            End If
            demoInventoryForm = Nothing
        End If

        ' Close demo appointments form if transitioning away from Step 5
        If stepNumber <> 5 AndAlso demoAppointmentsForm IsNot Nothing Then
            If Not demoAppointmentsForm.IsDisposed Then
                demoAppointmentsForm.Close()
            End If
            demoAppointmentsForm = Nothing
        End If

        currentStep = stepNumber
        lblGuideSubtitle.Text = $"Step {currentStep} of 6: " & GetStepTitle(currentStep)
        
        ' Update buttons
        btnBack.Enabled = (currentStep > 1)
        If currentStep = 6 Then
            btnNext.Text = "Finish"
        Else
            btnNext.Text = "Next"
        End If
        
        ' Clear content
        pnlStepContent.Controls.Clear()
        
        ' Header text
        lblStepHeader = New Label()
        lblStepHeader.Font = New Font("Segoe UI", 11.5F, FontStyle.Bold)
        lblStepHeader.ForeColor = Color.FromArgb(128, 0, 0)
        lblStepHeader.Size = New Size(540, 25)
        lblStepHeader.Text = GetStepHeader(currentStep)
        lblStepHeader.Margin = New Padding(0, 0, 0, 15)
        pnlStepContent.Controls.Add(lblStepHeader)
        
        ' Description paragraph
        Dim lblDesc As New Label()
        lblDesc.Font = New Font("Segoe UI", 10.0F)
        lblDesc.ForeColor = Color.FromArgb(64, 64, 64)
        lblDesc.Size = New Size(540, 45)
        lblDesc.Text = GetStepDescription(currentStep)
        lblDesc.Margin = New Padding(0, 0, 0, 15)
        pnlStepContent.Controls.Add(lblDesc)
        
        ' Add bullet items
        Dim bullets = GetStepBullets(currentStep)
        For Each bullet In bullets
            Dim bulletPanel As New Panel()
            bulletPanel.Size = New Size(540, 48)
            bulletPanel.Margin = New Padding(0, 0, 0, 8)
            
            Dim lblBulletIcon As New Label()
            lblBulletIcon.Font = New Font("Segoe UI", 12.0F)
            lblBulletIcon.ForeColor = Color.FromArgb(128, 0, 0)
            lblBulletIcon.Location = New Point(5, 5)
            lblBulletIcon.Size = New Size(25, 25)
            lblBulletIcon.Text = bullet.Icon
            bulletPanel.Controls.Add(lblBulletIcon)
            
            Dim lblBulletTitle As New Label()
            lblBulletTitle.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
            lblBulletTitle.ForeColor = Color.Black
            lblBulletTitle.Location = New Point(35, 5)
            lblBulletTitle.Size = New Size(495, 18)
            lblBulletTitle.Text = bullet.Title
            bulletPanel.Controls.Add(lblBulletTitle)
            
            Dim lblBulletDesc As New Label()
            lblBulletDesc.Font = New Font("Segoe UI", 9.5F)
            lblBulletDesc.ForeColor = Color.DimGray
            lblBulletDesc.Location = New Point(35, 23)
            lblBulletDesc.Size = New Size(495, 22)
            lblBulletDesc.Text = bullet.Description
            bulletPanel.Controls.Add(lblBulletDesc)
            
            pnlStepContent.Controls.Add(bulletPanel)
        Next

        ' Call highlighting in Form1
        If mainForm IsNot Nothing Then
            mainForm.HighlightSidebarButtonsForStep(currentStep)
        End If

        ' Spawning of modeless forms
        If currentStep = 3 Then
            If demoForm Is Nothing OrElse demoForm.IsDisposed Then
                Dim defaultAttending As String = ""
                If mainForm IsNot Nothing Then
                    defaultAttending = mainForm.CurrentUsername
                End If
                demoForm = New VisitDetailsForm(Nothing, defaultAttending, True)
                demoForm.StartPosition = FormStartPosition.Manual
                demoForm.Show(Me)
            End If
            PositionFormsCentrally(demoForm)
        ElseIf currentStep = 4 Then
            If demoInventoryForm Is Nothing OrElse demoInventoryForm.IsDisposed Then
                demoInventoryForm = New InventoryForm(True)
                demoInventoryForm.StartPosition = FormStartPosition.Manual
                demoInventoryForm.Show(Me)
            End If
            PositionFormsCentrally(demoInventoryForm)
        ElseIf currentStep = 5 Then
            If demoAppointmentsForm Is Nothing OrElse demoAppointmentsForm.IsDisposed Then
                demoAppointmentsForm = New AppointmentsForm(True)
                demoAppointmentsForm.StartPosition = FormStartPosition.Manual
                demoAppointmentsForm.Show(Me)
            End If
            PositionFormsCentrally(demoAppointmentsForm)
        Else
            CenterGuideAlone()
        End If
    End Sub

    Private Sub PositionFormsCentrally(demo As Form)
        If demo Is Nothing OrElse demo.IsDisposed Then Return
        
        Dim screenArea As Rectangle = Screen.FromControl(Me).WorkingArea
        Dim gap As Integer = 15
        Dim totalWidth As Integer = Me.Width + gap + demo.Width
        
        If totalWidth <= screenArea.Width Then
            Dim startX As Integer = screenArea.X + (screenArea.Width - totalWidth) \ 2
            
            Me.Left = startX
            Me.Top = screenArea.Y + (screenArea.Height - Me.Height) \ 2
            
            demo.Left = startX + Me.Width + gap
            demo.Top = screenArea.Y + (screenArea.Height - demo.Height) \ 2
        Else
            ' Overlap slightly if screen is too small
            Dim startX As Integer = screenArea.X + 10
            Me.Left = startX
            Me.Top = screenArea.Y + (screenArea.Height - Me.Height) \ 2
            
            demo.Left = Math.Min(screenArea.Right - demo.Width - 10, startX + Me.Width - 100)
            demo.Top = screenArea.Y + (screenArea.Height - demo.Height) \ 2
        End If
    End Sub

    Private Sub CenterGuideAlone()
        Dim screenArea As Rectangle = Screen.FromControl(Me).WorkingArea
        Me.Left = screenArea.X + (screenArea.Width - Me.Width) \ 2
        Me.Top = screenArea.Y + (screenArea.Height - Me.Height) \ 2
    End Sub

    Private Function GetStepTitle(stepNumber As Integer) As String
        Select Case stepNumber
            Case 1 : Return "Welcome Overview"
            Case 2 : Return "Dashboard Module"
            Case 3 : Return "Add New Visit Module (Interactive Demo)"
            Case 4 : Return "Inventory Module (Interactive Demo)"
            Case 5 : Return "Appointments Module (Interactive Demo)"
            Case 6 : Return "Excel Reports & Analytics"
            Case Else : Return ""
        End Select
    End Function

    Private Function GetStepHeader(stepNumber As Integer) As String
        Select Case stepNumber
            Case 1 : Return "Welcome to Iska Care Clinic Assistant!"
            Case 2 : Return "Dashboard: Daily Clinic Visual Overview"
            Case 3 : Return "Add New Visit: Interactive Diagnosis Demo"
            Case 4 : Return "Inventory: Stock Levels & Warnings"
            Case 5 : Return "Appointments: Follow-up Scheduling"
            Case 6 : Return "Excel Export: Styled Clinical Reports"
            Case Else : Return ""
        End Select
    End Function

    Private Function GetStepDescription(stepNumber As Integer) As String
        Select Case stepNumber
            Case 1 : Return "This application helps clinic nurses and staff manage patient records, track medicine inventory, organize appointments, and analyze patterns automatically."
            Case 2 : Return "The Dashboard (highlighted in gold in the sidebar) displays today's metrics and daily patient distributions at a glance:"
            Case 3 : Return "The Add New Visit module lets you document patient visits. We have opened a Tutorial Demo form on the right so you can see the intelligent features:"
            Case 4 : Return "The Inventory module (highlighted in gold) allows you to monitor medicine availability and receive stock counts. We have opened a Tutorial Demo form on the right:"
            Case 5 : Return "The Appointments module (highlighted in gold) organizes scheduled dental and medical sessions on a day-by-day calendar. We have opened a Tutorial Demo form on the right:"
            Case 6 : Return "Produce beautifully formatted Excel files to analyze visitor trends and share insights with university administration:"
            Case Else : Return ""
        End Select
    End Function

    Private Structure BulletItem
        Public Icon As String
        Public Title As String
        Public Description As String
        Public Sub New(i As String, t As String, d As String)
            Icon = i
            Title = t
            Description = d
        End Sub
    End Structure

    Private Function GetStepBullets(stepNumber As Integer) As List(Of BulletItem)
        Dim list As New List(Of BulletItem)()
        Select Case stepNumber
            Case 1
                list.Add(New BulletItem("📋", "Patient Records Management", "Check in patients, log symptoms, diagnoses, prescriptions, and follow-ups."))
                list.Add(New BulletItem("💊", "Stock Tracking & Alerts", "Keep count of medical supplies and receive warnings when inventory runs low."))
                list.Add(New BulletItem("📅", "Appointments Schedules", "Add follow-up appointments and track scheduled visits in a simple calendar."))
                list.Add(New BulletItem("🧠", "Smart Learning Assistant", "Speed up clinic workflows with automated recommendations based on historical data."))
            Case 2
                list.Add(New BulletItem("📊", "Real-Time Statistics", "Displays today's total visits, student count, and employee count."))
                list.Add(New BulletItem("🔔", "Low Stock Alerts", "Highlights warning notifications for critically low-stock medicines."))
                list.Add(New BulletItem("📅", "Today's Appointments", "Lists all follow-ups and scheduled visits for the day."))
                list.Add(New BulletItem("📈", "Visual Trends Chart", "Displays daily visit counts dynamically inside the application."))
            Case 3
                list.Add(New BulletItem("✍️", "Enter Patient Details", "Select the type (Student/Employee) and fill in the patient name (e.g. 'Juan Dela Cruz')."))
                list.Add(New BulletItem("🏷️", "Adaptive Diagnosis Suggestions", "We typed 'headache' in Reason. See how matching diagnoses suggestions appear below Sickness!"))
                list.Add(New BulletItem("⚡", "High-Confidence Auto-Fill", "If a suggestion has >80% confidence, the sickness is filled automatically to save clicks."))
                list.Add(New BulletItem("📦", "Stock-Aware Prescriptions", "Recommended medicines appear under Prescription, highlighting stock counts and low warnings."))
            Case 4
                list.Add(New BulletItem("💊", "Stock Counts", "Search and track total stock quantities of all medicines (e.g. Amoxicillin, 50 left)."))
                list.Add(New BulletItem("➕", "Add/Update Stock", "Log new batches of supplies case-insensitively to prevent duplicates."))
                list.Add(New BulletItem("⚠️", "Low-Stock Warnings", "Medicines with fewer than 10 units left trigger alerts to prompt reorders."))
                list.Add(New BulletItem("🗑️", "Delete & Edit Items", "Easily edit names, stock levels, or remove obsolete items."))
            Case 5
                list.Add(New BulletItem("📅", "Date-Based Tracking", "Select any date on the calendar to view appointments scheduled for that day."))
                list.Add(New BulletItem("➕", "Schedule Appointments", "Easily register patient names, target date, and reason for visits."))
                list.Add(New BulletItem("❌", "Cancel/Delete Booking", "Remove completed or cancelled appointments with one click."))
                list.Add(New BulletItem("⚡", "Dashboard Integration", "Active appointments for today automatically show up on the main dashboard."))
            Case 6
                list.Add(New BulletItem("📥", "One-Click Styled Export", "Use the 'Export' button on the toolbar to export all visits into a styled Excel file."))
                list.Add(New BulletItem("🎨", "Professional Aesthetics", "The generated reports are stylized in PUP Maroon with clean, readable layouts."))
                list.Add(New BulletItem("📊", "Summary Statistics Panel", "Provides automatic summaries of student versus employee visits in the workbook."))
                list.Add(New BulletItem("📈", "Interactive Excel Chart", "Automatically draws a native column chart of monthly visit counts inside the Excel file."))
        End Select
        Return list
    End Function

    Private Sub btnBack_Click(sender As Object, e As EventArgs)
        If currentStep > 1 Then
            ShowStep(currentStep - 1)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs)
        If currentStep < 6 Then
            ShowStep(currentStep + 1)
        Else
            FinishGuide()
        End If
    End Sub

    Private Sub btnSkip_Click(sender As Object, e As EventArgs)
        FinishGuide()
    End Sub

    Private Sub FinishGuide()
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        ' Close demo forms if open
        If demoForm IsNot Nothing AndAlso Not demoForm.IsDisposed Then
            demoForm.Close()
        End If
        If demoInventoryForm IsNot Nothing AndAlso Not demoInventoryForm.IsDisposed Then
            demoInventoryForm.Close()
        End If
        If demoAppointmentsForm IsNot Nothing AndAlso Not demoAppointmentsForm.IsDisposed Then
            demoAppointmentsForm.Close()
        End If

        ' Save setting if checkbox is checked
        If chkDontShow.Checked AndAlso mainForm IsNot Nothing AndAlso Not String.IsNullOrEmpty(mainForm.CurrentUsername) Then
            Dim key As String = "ShowGuide_" & mainForm.CurrentUsername
            dbHelper.SetConfig(key, "0")
        End If

        ' Ensure highlight is off when user closes
        If mainForm IsNot Nothing Then
            mainForm.HighlightSidebarButtonsForStep(0)
        End If
        MyBase.OnFormClosing(e)
    End Sub
End Class
