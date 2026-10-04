Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Collections.Generic
Imports System.IO
Imports ClosedXML.Excel

Public Class Form1
    Private dbHelper As DatabaseHelper
    Private ReadOnly PUPMaroon As Color = Color.FromArgb(128, 0, 0)

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property CurrentUsername As String
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property CurrentUserRole As String

    Public Sub New()
        InitializeComponent()
        ' Pra Full screen sya
        Me.WindowState = FormWindowState.Maximized

        ' login eto, dapat may role-based access control hehe
        Dim login As New LoginForm()
        If login.ShowDialog() <> DialogResult.OK Then
            Environment.Exit(0)
            Return
        End If

        ' pra sa role-based access control, eto lang muna, pwede pang i-expand sa future
        Dim userRole As String = login.AuthenticatedRole
        Dim username As String = login.AuthenticatedUsername
        Me.CurrentUsername = username
        Me.CurrentUserRole = userRole

        Dim displayTitle As String = ""
        Select Case userRole
            Case "Doctor"
                displayTitle = $"Dr. {username}"
            Case "Dentist"
                displayTitle = $"Dr. {username}"
            Case "Nurse"
                displayTitle = $"Nurse {username}"
            Case "Admin"
                displayTitle = $"Admin {username}"
            Case Else
                displayTitle = username
        End Select

        lblUserProfile.Text = $"Welcome, {displayTitle} - Clinic {userRole}"

        If userRole <> "Admin" Then
            btnSettings.Visible = False
            btnManageUsers.Visible = False
        End If

        SetupCharts()

        'design ng datagridview, pwede pang i-improve sa future kung gusto nyo haha
        dgvVisits.EnableHeadersVisualStyles = False
        dgvVisits.ColumnHeadersDefaultCellStyle.BackColor = PUPMaroon
        dgvVisits.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvVisits.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        dgvVisits.ColumnHeadersHeight = 40
        dgvVisits.DefaultCellStyle.Font = New Font("Segoe UI", 10)
        dgvVisits.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 230, 230)
        dgvVisits.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvVisits.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        dgvVisits.RowTemplate.Height = 35
        ' koneksyon sa database, eto yung pinaka-importante, dito nagkakaroon ng error kapag hindi nakaconnect sa MySQL, kaya may try-catch block
        Try
            dbHelper = New DatabaseHelper()
            LoadData()
        Catch ex As Exception 'sure ka??
            MessageBox.Show("Could not connect to database. Is Laragon / MySQL running? Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Create and configure btnGuide dynamically in pnlSidebar
        Dim btnGuide As New Button()
        btnGuide.Cursor = Cursors.Hand
        btnGuide.Dock = DockStyle.Bottom
        btnGuide.FlatAppearance.BorderSize = 0
        btnGuide.FlatAppearance.MouseOverBackColor = Color.FromArgb(160, 0, 0)
        btnGuide.FlatStyle = FlatStyle.Flat
        btnGuide.Font = New Font("Segoe UI", 11.0F)
        btnGuide.ForeColor = Color.White
        btnGuide.Height = 50
        btnGuide.Name = "btnGuide"
        btnGuide.Padding = New Padding(20, 0, 0, 0)
        btnGuide.Text = "User Guide"
        btnGuide.TextAlign = ContentAlignment.MiddleLeft
        btnGuide.UseVisualStyleBackColor = True
        
        AddHandler btnGuide.Click, AddressOf btnGuide_Click
        pnlSidebar.Controls.Add(btnGuide)
        pnlSidebar.Controls.SetChildIndex(btnGuide, 0)

        ' Add Handler for Shown event to show onboarding guide automatically on first load
        AddHandler Me.Shown, AddressOf Form1_Shown
        
        ' Change Export button text to "Monthly Report"
        btnExport.Text = "Monthly Report"
    End Sub

    ' Eto yung function na naglo-load ng data sa datagridview, may option to filter by search term at queue-only WOW ano AHAHHAHAHa
    Private Sub LoadData(Optional searchTerm As String = "")
        If dbHelper Is Nothing Then Return
        Try
            Dim records = dbHelper.GetAllVisits()
            If chkQueueOnly.Checked Then
                records = records.FindAll(Function(r) Not r.TimeOut.HasValue)
            End If
            If Not String.IsNullOrWhiteSpace(searchTerm) Then
                records = records.FindAll(Function(r) Not String.IsNullOrEmpty(r.PatientName) AndAlso r.PatientName.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
            End If
            dgvVisits.DataSource = records

            ' Normalize DataGridView to only show essential information for nurses
            If dgvVisits.Columns.Count > 0 Then
                For Each col As DataGridViewColumn In dgvVisits.Columns
                    col.Visible = False
                Next

                With dgvVisits
                    .Columns("Id").Visible = True
                    .Columns("Id").HeaderText = "Visit ID"
                    .Columns("Id").Width = 80
                    .Columns("Id").DisplayIndex = 0

                    .Columns("PatientName").Visible = True
                    .Columns("PatientName").HeaderText = "Patient Name"
                    .Columns("PatientName").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    .Columns("PatientName").DisplayIndex = 1

                    .Columns("PatientType").Visible = True
                    .Columns("PatientType").HeaderText = "Type"
                    .Columns("PatientType").Width = 100
                    .Columns("PatientType").DisplayIndex = 2

                    .Columns("Sickness").Visible = True
                    .Columns("Sickness").HeaderText = "Condition"
                    .Columns("Sickness").Width = 150
                    .Columns("Sickness").DisplayIndex = 3

                    .Columns("TimeIn").Visible = True
                    .Columns("TimeIn").HeaderText = "Time In"
                    .Columns("TimeIn").DefaultCellStyle.Format = "MMM dd, hh:mm tt"
                    .Columns("TimeIn").Width = 150
                    .Columns("TimeIn").DisplayIndex = 4

                    .Columns("TimeOut").Visible = True
                    .Columns("TimeOut").HeaderText = "Time Out"
                    .Columns("TimeOut").DefaultCellStyle.Format = "MMM dd, hh:mm tt"
                    .Columns("TimeOut").Width = 150
                    .Columns("TimeOut").DisplayIndex = 5

                    .Columns("DoctorAssigned").Visible = True
                    .Columns("DoctorAssigned").HeaderText = "Doctor Assigned"
                    .Columns("DoctorAssigned").Width = 150
                    .Columns("DoctorAssigned").DisplayIndex = 6
                End With
            End If

            dgvVisits.ClearSelection() 'piipigilan na ma-select yung first row after mag-load ng data, para hindi agad may naka-highlight sa record

            UpdateRecentActivities(records)
            UpdateCharts(records)

            ' pang update ng statistics sa dashboard, eto yung mga labels sa taas, pwede pang i-improve sa future kung gusto nyo haha 
            ' (sana all ni u-update)
            Dim numStudents = dbHelper.GetTotalStudents()
            Dim numEmployees = dbHelper.GetTotalEmployees()
            lblStatVisitsCount.Text = dbHelper.GetTotalVisitsToday().ToString()
            lblStatStudentsCount.Text = numStudents.ToString()
            lblStatEmployeesCount.Text = numEmployees.ToString()

            ' 1. Low Inventory Alerts
            Dim inventory = dbHelper.GetInventory()
            Dim lowStockItems = inventory.Where(Function(i) i.StockQuantity <= 10).ToList()
            If lowStockItems.Count > 0 Then
                lblStatAlertsCount.Text = lowStockItems.Count.ToString()
                pnlStatAlerts.BackColor = Color.LightCoral
                lblStatAlertsCount.ForeColor = Color.White
                lblStatAlertsTitle.ForeColor = Color.White
            Else
                lblStatAlertsCount.Text = "0"
                pnlStatAlerts.BackColor = Color.White
                lblStatAlertsCount.ForeColor = Color.Green
                lblStatAlertsTitle.ForeColor = Color.Green
            End If

            ' 2. Today's Appointments
            UpdateTodayAppointments()

        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        End Try
    End Sub

    ' Eto yung event handler para sa search box, every time mag-change yung text, magre-refresh yung data sa grid base sa ni search
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadData(txtSearch.Text)
    End Sub
    ' Eto yung event handler para sa "Add Visit" button, magbubukas ng new form para mag-input ng details ng visit
    ' ,tapos pag na-save, mag-iinsert sa database at magre-refresh na ang data sa grid
    Private Sub btnAddVisit_Click(sender As Object, e As EventArgs) Handles btnAddVisit.Click
        If dbHelper Is Nothing Then Return

        ' Determine display title of the logged in user to pre-populate DoctorAssigned
        Dim defaultAttending As String = ""
        Select Case Me.CurrentUserRole
            Case "Doctor"
                defaultAttending = $"Dr. {Me.CurrentUsername}"
            Case "Dentist"
                defaultAttending = $"Dr. {Me.CurrentUsername}"
            Case "Nurse"
                defaultAttending = $"Nurse {Me.CurrentUsername}"
            Case "Admin"
                defaultAttending = $"Admin {Me.CurrentUsername}"
            Case Else
                defaultAttending = Me.CurrentUsername
        End Select

        Using detailsForm As New VisitDetailsForm(Nothing, defaultAttending)
            If detailsForm.ShowDialog(Me) = DialogResult.OK Then
                Try
                    dbHelper.InsertVisit(detailsForm.Record)
                    If Not String.IsNullOrWhiteSpace(detailsForm.Record.Prescription) Then
                        dbHelper.DeductInventory(detailsForm.Record.Prescription.Trim(), 1)
                    End If
                    LoadData()
                Catch ex As Exception
                    MessageBox.Show("Error adding record: " & ex.Message)
                End Try
            End If
        End Using
    End Sub


    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        ' Eto yung event handler para sa "Edit" button, magbubukas ng new form na may pre-filled details ng selected record,
        ' pwede i-edit tapos pag na-save mag-uupdate sa database at magre-refresh ang data sa grid
        If dbHelper Is Nothing Then Return
        If dgvVisits.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a record to edit.")
            Return
        End If
        If dgvVisits.SelectedRows.Count > 1 Then
            MessageBox.Show("Please select only one record to edit.")
            Return
        End If

        Dim selectedRecord As VisitRecord = CType(dgvVisits.SelectedRows(0).DataBoundItem, VisitRecord)
        Using detailsForm As New VisitDetailsForm(selectedRecord)
            If detailsForm.ShowDialog(Me) = DialogResult.OK Then
                Try
                    dbHelper.UpdateVisit(detailsForm.Record)
                    LoadData()
                Catch ex As Exception
                    MessageBox.Show("Error updating record: " & ex.Message)
                End Try
            End If
        End Using
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        ' Eto yung event handler para sa "Delete" button, magde-delete ng selected records sa database at magre-refresh ang data sa grid
        If dbHelper Is Nothing Then Return

        Dim recordsToDelete As New List(Of VisitRecord)()

        For Each row As DataGridViewRow In dgvVisits.SelectedRows
            Dim selectedRecord As VisitRecord = CType(row.DataBoundItem, VisitRecord)
            If selectedRecord IsNot Nothing Then
                recordsToDelete.Add(selectedRecord)
            End If
        Next

        If recordsToDelete.Count = 0 Then
            MessageBox.Show("Please select one or more records to delete.", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If MessageBox.Show($"Are you sure you want to delete {recordsToDelete.Count} selected record(s)?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                For Each rec In recordsToDelete
                    dbHelper.DeleteVisit(rec.Id)
                Next
                LoadData()
            Catch ex As Exception
                MessageBox.Show("Error deleting record(s): " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadData(txtSearch.Text)
    End Sub

    Private Sub chkQueueOnly_CheckedChanged(sender As Object, e As EventArgs) Handles chkQueueOnly.CheckedChanged
        LoadData(txtSearch.Text)
    End Sub


    Private Sub btnCheckOut_Click(sender As Object, e As EventArgs) Handles btnCheckOut.Click
        ' Eto yung event handler para sa "Check Out" button, magse-set ng TimeOut ng selected record sa current time,
        ' tapos mag-uupdate sa database at magre-refresh ang data sa grid
        If dbHelper Is Nothing Then Return
        If dgvVisits.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select patient(s) in queue to check out.")
            Return
        End If

        Dim recordsToCheckOut As New List(Of VisitRecord)()
        For Each row As DataGridViewRow In dgvVisits.SelectedRows
            Dim selectedRecord As VisitRecord = CType(row.DataBoundItem, VisitRecord)
            If Not selectedRecord.TimeOut.HasValue Then
                recordsToCheckOut.Add(selectedRecord)
            End If
        Next

        If recordsToCheckOut.Count = 0 Then
            MessageBox.Show("The selected patient(s) are already checked out.", "Already Checked Out", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If MessageBox.Show($"Are you sure you want to check out {recordsToCheckOut.Count} patient(s)?", "Confirm Check Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                For Each record In recordsToCheckOut
                    record.TimeOut = DateTime.Now
                    dbHelper.UpdateVisit(record)
                Next
                LoadData(txtSearch.Text)
                MessageBox.Show("Patient(s) checked out successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error checking out patient(s): " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        ShowDashboard()
    End Sub

    Private Sub lblTitle_Click(sender As Object, e As EventArgs) Handles lblTitle.Click
    End Sub


    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        If dgvVisits.Rows.Count = 0 Then
            MessageBox.Show("No records to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using sfd As New SaveFileDialog()
            sfd.Filter = "Excel Files|*.xlsx"
            sfd.FileName = "ClinicVisits_Export_" & DateTime.Now.ToString("yyyyMMdd") & ".xlsx"
            If sfd.ShowDialog() = DialogResult.OK Then
                Try
                    Dim allRecords = dbHelper.GetAllVisits()

                    Using workbook As New XLWorkbook()
                        Dim maroon = XLColor.FromArgb(128, 0, 0)
                        Dim darkMaroon = XLColor.FromArgb(80, 0, 0)
                        Dim gold = XLColor.FromArgb(218, 165, 32)
                        Dim lightRose = XLColor.FromArgb(252, 245, 245)

                        ' ══════════════════════════════════════
                        '  SHEET 1 — Visit Records
                        ' ══════════════════════════════════════
                        Dim ws = workbook.Worksheets.Add("Visit Records")
                        Dim numCols As Integer = dgvVisits.Columns.Count
                        Dim lastColLetter As String = ws.Cell(1, numCols).Address.ColumnLetter

                        ' Rows 1-4: PUP Maroon Header
                        Dim Merge = Sub(r As Integer, v As String, sz As Single, fg As XLColor, bg As XLColor, bold As Boolean)
                                        ws.Range($"A{r}:{lastColLetter}{r}").Merge()
                                        Dim c = ws.Cell(r, 1)
                                        c.Value = v
                                        c.Style.Font.Bold = bold
                                        c.Style.Font.FontSize = sz
                                        c.Style.Fill.BackgroundColor = bg
                                        c.Style.Font.FontColor = fg
                                        c.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                        c.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                    End Sub

                        ' Row 1: Tall logo row (logo centered at top)
                        ws.Row(1).Height = 55
                        ws.Range($"A1:{lastColLetter}1").Merge()
                        ws.Cell("A1").Style.Fill.BackgroundColor = maroon

                        ' Rows 2-5: PUP text header block
                        Merge(2, "Republic of the Philippines", 9, XLColor.White, maroon, True)
                        Merge(3, "POLYTECHNIC UNIVERSITY OF THE PHILIPPINES", 13, gold, maroon, True)
                        Merge(4, "Lopez Campus", 10, XLColor.White, maroon, True)
                        Merge(5, "PUP Lopez Medical And Dental Services", 10, gold, maroon, True)
                        ws.Row(2).Height = 14 : ws.Row(3).Height = 20 : ws.Row(4).Height = 14 : ws.Row(5).Height = 14
                        ws.Range($"A6:{lastColLetter}6").Merge()
                        ws.Cell("A6").Style.Fill.BackgroundColor = maroon
                        ws.Row(6).Height = 5
                        Merge(7, $"CLINIC VISIT RECORDS — Exported: {DateTime.Now:MMMM dd, yyyy hh:mm tt}", 9, XLColor.White, XLColor.FromArgb(160, 20, 20), True)
                        ws.Row(8).Height = 5

                        ' Row 9: Column headers
                        Dim hdrRow As Integer = 9
                        For colIdx As Integer = 0 To numCols - 1
                            Dim hc = ws.Cell(hdrRow, colIdx + 1)
                            hc.Value = dgvVisits.Columns(colIdx).HeaderText
                            hc.Style.Font.Bold = True
                            hc.Style.Font.FontSize = 12
                            hc.Style.Fill.BackgroundColor = darkMaroon
                            hc.Style.Font.FontColor = XLColor.White
                            hc.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            hc.Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                        Next
                        ws.Row(hdrRow).Height = 26

                        ' Data rows (starting row 8)
                        For rowIdx As Integer = 0 To dgvVisits.Rows.Count - 1
                            Dim rNum As Integer = rowIdx + hdrRow + 1
                            ws.Row(rNum).Height = 20
                            For colIdx As Integer = 0 To numCols - 1
                                Dim val = dgvVisits.Rows(rowIdx).Cells(colIdx).Value
                                Dim dc = ws.Cell(rNum, colIdx + 1)
                                dc.Style.Font.FontSize = 11
                                dc.Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                                If rowIdx Mod 2 = 0 Then dc.Style.Fill.BackgroundColor = lightRose
                                If val IsNot Nothing Then
                                    If dgvVisits.Columns(colIdx).HeaderText = "ContactNumber" Then
                                        dc.Style.NumberFormat.Format = "@"
                                        dc.Value = val.ToString()
                                    ElseIf TypeOf val Is DateTime Then
                                        dc.Value = CType(val, DateTime)
                                        dc.Style.DateFormat.Format = "mm/dd/yyyy hh:mm AM/PM"
                                    Else
                                        dc.Value = val.ToString()
                                    End If
                                End If
                            Next
                        Next
                        ws.Columns().AdjustToContents()

                        ' Embed piyupi logo — centered in row 1
                        Try
                            Dim logoPath As String = Path.Combine(Application.StartupPath, "piyupi.png")
                            If File.Exists(logoPath) Then
                                Dim img = ws.AddPicture(logoPath)
                                img.Placement = ClosedXML.Excel.Drawings.XLPicturePlacement.Move
                                Dim imgWidthPx As Integer = 50
                                Dim imgHeightPx As Integer = 50
                                img.Width = imgWidthPx
                                img.Height = imgHeightPx

                                ' Calculate exact horizontal center of the columns
                                Dim totalWidthInChars As Double = 0
                                Dim colWidths(numCols) As Double
                                For colIdx As Integer = 1 To numCols
                                    Dim w As Double = ws.Column(colIdx).Width
                                    If w <= 0 Then w = 8.43
                                    colWidths(colIdx) = w
                                    totalWidthInChars += w
                                Next

                                Dim logoWidthInChars As Double = imgWidthPx / 7.2
                                Dim targetLeftInChars As Double = (totalWidthInChars - logoWidthInChars) / 2

                                Dim currentAccumulatedWidth As Double = 0
                                Dim targetCol As Integer = 1
                                Dim offsetInPixels As Integer = 0

                                For colIdx As Integer = 1 To numCols
                                    Dim colWidth As Double = colWidths(colIdx)
                                    If currentAccumulatedWidth + colWidth >= targetLeftInChars Then
                                        targetCol = colIdx
                                        Dim charOffset As Double = targetLeftInChars - currentAccumulatedWidth
                                        offsetInPixels = CInt(charOffset * 7.2)
                                        Exit For
                                    End If
                                    currentAccumulatedWidth += colWidth
                                Next

                                ' Ensure boundaries are sane
                                targetCol = Math.Max(1, Math.Min(targetCol, numCols))
                                offsetInPixels = Math.Max(0, offsetInPixels)

                                img.MoveTo(ws.Cell(1, targetCol), offsetInPixels, 3)
                            End If
                        Catch
                        End Try

                        ws.SheetView.FreezeRows(hdrRow)

                        ' ══════════════════════════════════════
                        '  SHEET 2 — Summary & Analytics
                        ' ══════════════════════════════════════
                        Dim ws2 = workbook.Worksheets.Add("Summary & Analytics")

                        Dim SecHdr = Sub(r As Integer, title As String)
                                         ws2.Range($"A{r}:B{r}").Merge()
                                         Dim c2 = ws2.Cell(r, 1)
                                         c2.Value = title
                                         c2.Style.Font.Bold = True : c2.Style.Font.FontSize = 11
                                         c2.Style.Fill.BackgroundColor = maroon
                                         c2.Style.Font.FontColor = XLColor.White
                                         c2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                         c2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                         ws2.Row(r).Height = 20
                                     End Sub

                        Dim TblHdr = Sub(r As Integer, c1 As String, c2 As String)
                                         Dim h1 = ws2.Cell(r, 1) : h1.Value = c1
                                         h1.Style.Font.Bold = True : h1.Style.Fill.BackgroundColor = darkMaroon : h1.Style.Font.FontColor = XLColor.White
                                         Dim h2 = ws2.Cell(r, 2) : h2.Value = c2
                                         h2.Style.Font.Bold = True : h2.Style.Fill.BackgroundColor = darkMaroon : h2.Style.Font.FontColor = XLColor.White
                                         h2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                         h2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                     End Sub

                        ' Sheet 2 title
                        ws2.Range("A1:B1").Merge()
                        Dim t1 = ws2.Cell("A1")
                        t1.Value = "PUP Lopez Campus — PUP Lopez Medical And Dental Services Analytics Report"
                        t1.Style.Font.Bold = True : t1.Style.Font.FontSize = 12
                        t1.Style.Fill.BackgroundColor = maroon : t1.Style.Font.FontColor = gold
                        t1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        t1.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        t1.Style.Alignment.WrapText = True
                        ws2.Row(1).Height = 36
                        ws2.Range("A2:B2").Merge()
                        Dim t2 = ws2.Cell("A2")
                        t2.Value = $"Generated: {DateTime.Now:MMMM dd, yyyy}"
                        t2.Style.Font.FontSize = 9 : t2.Style.Fill.BackgroundColor = XLColor.FromArgb(160, 20, 20) : t2.Style.Font.FontColor = XLColor.White
                        t2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        t2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        ws2.Row(2).Height = 16

                        Dim cr As Integer = 4

                        ' 1. Overview Stats
                        SecHdr(cr, "OVERVIEW STATISTICS") : cr += 1
                        Dim stats() As (String, Integer) = {
                            ("Total Visit Records", allRecords.Count),
                            ("Student Visits", allRecords.Where(Function(r2) r2.PatientType = "Student").Count()),
                            ("Employee Visits", allRecords.Where(Function(r2) r2.PatientType = "Employee").Count()),
                            ("Checked Out", allRecords.Where(Function(r2) r2.TimeOut.HasValue).Count()),
                            ("Still in Queue", allRecords.Where(Function(r2) Not r2.TimeOut.HasValue).Count()),
                            ("Visits Today", allRecords.Where(Function(r2) r2.TimeIn.Date = DateTime.Today).Count())
                        }
                        For Each st In stats
                            ws2.Cell(cr, 1).Value = st.Item1 : ws2.Cell(cr, 1).Style.Font.Bold = True
                            ws2.Cell(cr, 2).Value = st.Item2
                            ws2.Cell(cr, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            ws2.Cell(cr, 2).Style.Font.Bold = True : ws2.Cell(cr, 2).Style.Font.FontColor = maroon
                            If cr Mod 2 = 0 Then ws2.Cell(cr, 1).Style.Fill.BackgroundColor = lightRose : ws2.Cell(cr, 2).Style.Fill.BackgroundColor = lightRose
                            cr += 1
                        Next
                        cr += 1

                        ' 2. Top Conditions
                        SecHdr(cr, "TOP CONDITIONS / SICKNESS") : cr += 1
                        TblHdr(cr, "Condition / Diagnosis", "No. of Cases") : cr += 1
                        Dim sickList = allRecords.Where(Function(r2) Not String.IsNullOrWhiteSpace(r2.Sickness)) _
                            .GroupBy(Function(r2) r2.Sickness.Trim()) _
                            .Select(Function(g) (g.Key, g.Count())) _
                            .OrderByDescending(Function(x) x.Item2).Take(10).ToList()
                        Dim s1 As Integer = cr
                        For Each it In sickList
                            ws2.Cell(cr, 1).Value = it.Key : ws2.Cell(cr, 2).Value = it.Item2
                            ws2.Cell(cr, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            If cr Mod 2 = 0 Then ws2.Cell(cr, 1).Style.Fill.BackgroundColor = lightRose : ws2.Cell(cr, 2).Style.Fill.BackgroundColor = lightRose
                            cr += 1
                        Next
                        Dim e1 As Integer = cr - 1 : cr += 1

                        ' 3. Top Prescriptions
                        SecHdr(cr, "TOP PRESCRIBED MEDICINES (Rx)") : cr += 1
                        TblHdr(cr, "Medicine / Prescription", "Times Prescribed") : cr += 1
                        Dim rxList = allRecords.Where(Function(r2) Not String.IsNullOrWhiteSpace(r2.Prescription)) _
                            .GroupBy(Function(r2) r2.Prescription.Trim()) _
                            .Select(Function(g) (g.Key, g.Count())) _
                            .OrderByDescending(Function(x) x.Item2).Take(10).ToList()
                        Dim s2 As Integer = cr
                        For Each it In rxList
                            ws2.Cell(cr, 1).Value = it.Key : ws2.Cell(cr, 2).Value = it.Item2
                            ws2.Cell(cr, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            If cr Mod 2 = 0 Then ws2.Cell(cr, 1).Style.Fill.BackgroundColor = lightRose : ws2.Cell(cr, 2).Style.Fill.BackgroundColor = lightRose
                            cr += 1
                        Next
                        Dim e2 As Integer = cr - 1 : cr += 1

                        ' 4. Top Reasons for Visit
                        SecHdr(cr, "TOP REASONS FOR VISIT") : cr += 1
                        TblHdr(cr, "Reason", "Count") : cr += 1
                        Dim reasonList = allRecords.Where(Function(r2) Not String.IsNullOrWhiteSpace(r2.ReasonForVisit)) _
                            .GroupBy(Function(r2) r2.ReasonForVisit.Trim()) _
                            .Select(Function(g) (g.Key, g.Count())) _
                            .OrderByDescending(Function(x) x.Item2).Take(8).ToList()
                        For Each it In reasonList
                            ws2.Cell(cr, 1).Value = it.Key : ws2.Cell(cr, 2).Value = it.Item2
                            ws2.Cell(cr, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            If cr Mod 2 = 0 Then ws2.Cell(cr, 1).Style.Fill.BackgroundColor = lightRose : ws2.Cell(cr, 2).Style.Fill.BackgroundColor = lightRose
                            cr += 1
                        Next
                        cr += 1

                        ' 5. Patient Type Breakdown
                        SecHdr(cr, "PATIENT TYPE BREAKDOWN") : cr += 1
                        TblHdr(cr, "Patient Type", "Count") : cr += 1
                        For Each it In allRecords.Where(Function(r2) Not String.IsNullOrWhiteSpace(r2.PatientType)) _
                            .GroupBy(Function(r2) r2.PatientType.Trim()) _
                            .Select(Function(g) (g.Key, g.Count())) _
                            .OrderByDescending(Function(x) x.Item2).ToList()
                            ws2.Cell(cr, 1).Value = it.Key : ws2.Cell(cr, 2).Value = it.Item2
                            ws2.Cell(cr, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            If cr Mod 2 = 0 Then ws2.Cell(cr, 1).Style.Fill.BackgroundColor = lightRose : ws2.Cell(cr, 2).Style.Fill.BackgroundColor = lightRose
                            cr += 1
                        Next
                        cr += 1

                        ' 6. Gender Breakdown
                        SecHdr(cr, "GENDER BREAKDOWN") : cr += 1
                        TblHdr(cr, "Gender", "Count") : cr += 1
                        For Each it In allRecords.Where(Function(r2) Not String.IsNullOrWhiteSpace(r2.Gender)) _
                            .GroupBy(Function(r2) r2.Gender.Trim()) _
                            .Select(Function(g) (g.Key, g.Count())) _
                            .OrderByDescending(Function(x) x.Item2).ToList()
                            ws2.Cell(cr, 1).Value = it.Key : ws2.Cell(cr, 2).Value = it.Item2
                            ws2.Cell(cr, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            If cr Mod 2 = 0 Then ws2.Cell(cr, 1).Style.Fill.BackgroundColor = lightRose : ws2.Cell(cr, 2).Style.Fill.BackgroundColor = lightRose
                            cr += 1
                        Next
                        cr += 1

                        ' 7. Daily Admissions (last 14 days)
                        SecHdr(cr, "DAILY ADMISSIONS — LAST 14 DAYS") : cr += 1
                        TblHdr(cr, "Date", "Visits") : cr += 1
                        Dim ds As Integer = cr
                        For Each d In Enumerable.Range(0, 14).Select(Function(i) DateTime.Today.AddDays(-13 + i))
                            Dim cnt = allRecords.Where(Function(r2) r2.TimeIn.Date = d).Count()
                            ws2.Cell(cr, 1).Value = d.ToString("MMM dd, yyyy")
                            ws2.Cell(cr, 2).Value = cnt
                            ws2.Cell(cr, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            If cr Mod 2 = 0 Then ws2.Cell(cr, 1).Style.Fill.BackgroundColor = lightRose : ws2.Cell(cr, 2).Style.Fill.BackgroundColor = lightRose
                            cr += 1
                        Next
                        Dim de As Integer = cr - 1

                        ws2.Column(1).Width = 36
                        ws2.Column(2).Width = 20

                        workbook.SaveAs(sfd.FileName)
                    End Using

                    MessageBox.Show("Export complete!" & vbCrLf & "Sheet 1: Visit Records (with PUP header & logo)" & vbCrLf & "Sheet 2: Summary & Analytics (7 sections)", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show("Error exporting file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    Private Sub btnInventory_Click(sender As Object, e As EventArgs) Handles btnInventory.Click
        Using f As New InventoryForm()
            f.ShowDialog(Me)
        End Using
        LoadData()
    End Sub

    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        Using f As New SettingsForm()
            f.ShowDialog(Me)
        End Using
        LoadData()
    End Sub

    Private Sub btnManageUsers_Click(sender As Object, e As EventArgs) Handles btnManageUsers.Click
        Using f As New UserManagementForm()
            f.ShowDialog(Me)
        End Using
        LoadData()
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        If MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Application.Restart()
        End If
    End Sub

    Private Sub btnAppointments_Click(sender As Object, e As EventArgs) Handles btnAppointments.Click
        Using f As New AppointmentsForm()
            f.ShowDialog(Me)
        End Using
        LoadData()
    End Sub



    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        ' Eto yung event handler para sa "View History" button, magfi-filter ng data sa grid para ipakita lahat ng records ng selected patient
        If dgvVisits.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a patient to view their history.", "View History", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If dgvVisits.SelectedRows.Count > 1 Then
            MessageBox.Show("Please select only one patient to view their history.", "View History", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim selectedRecord As VisitRecord = CType(dgvVisits.SelectedRows(0).DataBoundItem, VisitRecord)

        ' Set the search box to the patient's exact name
        txtSearch.Text = selectedRecord.PatientName

        ' Filter the grid using LoadData
        LoadData(txtSearch.Text)
    End Sub




    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If dbHelper Is Nothing Then Return

        ' See if there is a selected row in the main dashboard grid to pre-select
        Dim preselectedRecord As VisitRecord = Nothing
        If dgvVisits.SelectedRows.Count = 1 Then
            preselectedRecord = CType(dgvVisits.SelectedRows(0).DataBoundItem, VisitRecord)
        End If

        Using selectForm As New SelectVisitForm(preselectedRecord)
            If selectForm.ShowDialog(Me) = DialogResult.OK Then
                Dim recordsToPrint As List(Of VisitRecord) = selectForm.SelectedRecords
                If recordsToPrint Is Nothing OrElse recordsToPrint.Count = 0 Then Return

                ' Load the piyupi logo once
                Dim pupLogo As Image = Nothing
                Try
                    Dim logoPath As String = Path.Combine(Application.StartupPath, "piyupi.png")
                    If File.Exists(logoPath) Then
                        pupLogo = Image.FromFile(logoPath)
                    End If
                Catch
                End Try

                ' 4 slips per page — 2 columns x 2 rows
                Dim printIndex As Integer = 0

                Dim pd As New PrintDocument()
                pd.DefaultPageSettings.Landscape = False

                AddHandler pd.PrintPage, Sub(s, ev)
                                             Dim g = ev.Graphics
                                             Dim pageLeft As Single = ev.MarginBounds.Left
                                             Dim pageTop As Single = ev.MarginBounds.Top
                                             Dim pageWidth As Single = ev.MarginBounds.Width
                                             Dim pageHeight As Single = ev.MarginBounds.Height

                                             ' 2 cols x 2 rows — 4 square slips per page
                                             Dim gapX As Single = 8   ' gap between columns
                                             Dim gapY As Single = 8   ' gap between rows
                                             Dim cardW As Single = (pageWidth - gapX) / 2
                                             Dim cardH As Single = cardW   ' square cards

                                             Dim slotsFilled As Integer = 0

                                             Do While printIndex < recordsToPrint.Count AndAlso slotsFilled < 4
                                                 Dim col As Integer = slotsFilled Mod 2          ' 0=left, 1=right
                                                 Dim row As Integer = slotsFilled \ 2           ' 0=top,  1=bottom
                                                 Dim cx As Single = pageLeft + col * (cardW + gapX)
                                                 Dim cy As Single = pageTop + row * (cardH + gapY)
                                                 Dim rec As VisitRecord = recordsToPrint(printIndex)
                                                 DrawMedicalSlipCard(g, rec, pupLogo, cx, cy, cardW, cardH)
                                                 printIndex += 1
                                                 slotsFilled += 1
                                             Loop

                                             ev.HasMorePages = (printIndex < recordsToPrint.Count)
                                         End Sub

                Dim ppd As New PrintPreviewDialog()
                ppd.Document = pd
                ppd.Width = 850
                ppd.Height = 1100
                ppd.ShowDialog()

                If pupLogo IsNot Nothing Then pupLogo.Dispose()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Draws a single compact medical excuse slip card within the given bounding box.
    ''' </summary>
    ''' <summary>
    ''' Draws a single compact medical excuse slip card (quarter-page) within the given bounding box.
    ''' </summary>
    Private Sub DrawMedicalSlipCard(g As Graphics, rec As VisitRecord, logo As Image,
                                    x As Single, y As Single, w As Single, h As Single)
        ' ── Pens & brushes ─────────────────────────────────────────────
        Dim maroon As New SolidBrush(Color.FromArgb(128, 0, 0))
        Dim maroonPen As New Pen(Color.FromArgb(128, 0, 0), 1)
        Dim thinPen As New Pen(Color.FromArgb(190, 190, 190), 0.5)
        Dim dashPen As New Pen(Color.LightGray, 0.5!) With {.DashStyle = Drawing2D.DashStyle.Dash}

        ' ── Fonts — all scaled down for quarter-page size ───────────────
        Dim fntBoldLbl As New Font("Arial", 5.5, FontStyle.Bold)     ' field labels
        Dim fntVal As New Font("Arial", 5.5, FontStyle.Regular)      ' field values
        Dim fntHeader As New Font("Arial", 6.0, FontStyle.Bold)      ' PUP name
        Dim fntSub As New Font("Arial", 5.0, FontStyle.Regular)      ' republic / campus
        Dim fntSlipTitle As New Font("Arial", 7.0, FontStyle.Bold)   ' MEDICAL EXCUSE SLIP
        Dim fntValName As New Font("Arial", 6.0, FontStyle.Regular)  ' patient name (slightly larger)

        Dim centerFmt As New StringFormat() With {.Alignment = StringAlignment.Center}

        ' ── Outer card border ──────────────────────────────────────────
        g.DrawRectangle(maroonPen, x, y, w, h)

        ' ── Header stripe — logo centered on top, text below ───────────
        Dim logoSize As Single = 28          ' smaller logo for quarter-page
        Dim logoTextGap As Single = 2
        Dim textBlockH As Single = 9 + 10 + 7 + 7   ' 4 lines of text
        Dim headerPad As Single = 4
        Dim headerH As Single = headerPad + logoSize + logoTextGap + textBlockH + headerPad
        g.FillRectangle(maroon, x, y, w, headerH)

        ' Logo centered
        If logo IsNot Nothing Then
            Dim lx0 As Single = x + (w / 2) - (logoSize / 2)
            g.DrawImage(logo, lx0, y + headerPad, logoSize, logoSize)
        End If

        ' PUP text below logo
        Dim txtY As Single = y + headerPad + logoSize + logoTextGap
        g.DrawString("Republic of the Philippines", fntSub, Brushes.White, New RectangleF(x, txtY, w, 9), centerFmt)
        txtY += 9
        g.DrawString("POLYTECHNIC UNIVERSITY OF THE PHILIPPINES", fntHeader, Brushes.White, New RectangleF(x, txtY, w, 10), centerFmt)
        txtY += 10
        g.DrawString("Lopez Campus", fntSub, Brushes.White, New RectangleF(x, txtY, w, 7), centerFmt)
        txtY += 7
        g.DrawString("PUP Lopez Medical And Dental Services", fntSub, Brushes.White, New RectangleF(x, txtY, w, 7), centerFmt)

        ' ── Slip title ─────────────────────────────────────────────────
        Dim titleY As Single = y + headerH + 3
        g.DrawString("MEDICAL EXCUSE SLIP", fntSlipTitle, maroon, New RectangleF(x, titleY, w, 12), centerFmt)

        ' Separator
        Dim sepY As Single = titleY + 13
        g.DrawLine(maroonPen, x + 3, sepY, x + w - 3, sepY)

        ' ── Fields ─────────────────────────────────────────────────────
        Dim fy As Single = sepY + 4
        Dim lx As Single = x + 5
        Dim rx As Single = x + w / 2 + 3
        Dim lineH As Single = 12     ' tighter row height

        ' Date | Time In
        g.DrawString("Date:", fntBoldLbl, Brushes.Black, lx, fy)
        g.DrawString(rec.TimeIn.ToString("MM/dd/yyyy"), fntVal, Brushes.Black, lx + 26, fy)
        g.DrawString("Time In:", fntBoldLbl, Brushes.Black, rx, fy)
        g.DrawString(rec.TimeIn.ToString("hh:mm tt"), fntVal, Brushes.Black, rx + 32, fy)
        fy += lineH

        ' Patient Name
        g.DrawString("Name:", fntBoldLbl, Brushes.Black, lx, fy)
        g.DrawString(rec.PatientName, fntValName, Brushes.Black, lx + 30, fy)
        fy += lineH

        ' Type | Age
        g.DrawString("Type:", fntBoldLbl, Brushes.Black, lx, fy)
        g.DrawString(rec.PatientType, fntVal, Brushes.Black, lx + 26, fy)
        g.DrawString("Age:", fntBoldLbl, Brushes.Black, rx, fy)
        g.DrawString(rec.Age, fntVal, Brushes.Black, rx + 22, fy)
        fy += lineH

        ' Course / Job
        If rec.PatientType = "Student" Then
            g.DrawString("Course/Yr:", fntBoldLbl, Brushes.Black, lx, fy)
            g.DrawString($"{rec.Course} {rec.Year}", fntVal, Brushes.Black, lx + 46, fy)
        ElseIf rec.PatientType = "Employee" Then
            g.DrawString("Job:", fntBoldLbl, Brushes.Black, lx, fy)
            g.DrawString(rec.Job, fntVal, Brushes.Black, lx + 22, fy)
        End If
        fy += lineH

        ' Thin divider
        g.DrawLine(thinPen, lx, fy, x + w - 5, fy)
        fy += 4

        ' Reason
        g.DrawString("Reason:", fntBoldLbl, Brushes.Black, lx, fy)
        g.DrawString(rec.ReasonForVisit, fntVal, Brushes.Black, lx + 38, fy)
        fy += lineH

        ' Diagnosis
        g.DrawString("Diagnosis:", fntBoldLbl, Brushes.Black, lx, fy)
        g.DrawString(rec.Sickness, fntVal, Brushes.Black, lx + 46, fy)
        fy += lineH

        ' Prescription
        g.DrawString("Rx:", fntBoldLbl, Brushes.Black, lx, fy)
        g.DrawString(rec.Prescription, fntVal, Brushes.Black, lx + 18, fy)
        fy += lineH + 2

        ' ── Signature ─────────────────────────────────────────────────
        Dim sigW As Single = w * 0.55
        Dim sigX As Single = x + w - sigW - 4
        Dim attending As String = If(String.IsNullOrWhiteSpace(rec.DoctorAssigned), "Nurse / Doctor", rec.DoctorAssigned)
        g.DrawString(attending, fntBoldLbl, maroon, New RectangleF(sigX, fy, sigW, 10), centerFmt)
        g.DrawLine(New Pen(Color.Black, 0.5), sigX, fy + 12, sigX + sigW, fy + 12)
        g.DrawString("Attending Nurse / Doctor", fntSub, Brushes.Gray, New RectangleF(sigX, fy + 15, sigW, 8), centerFmt)

        ' ── Dashed cut border ─────────────────────────────────────────
        g.DrawLine(dashPen, x, y + h, x + w, y + h)

        ' ── Cleanup ───────────────────────────────────────────────────
        maroon.Dispose() : maroonPen.Dispose() : thinPen.Dispose() : dashPen.Dispose()
        fntBoldLbl.Dispose() : fntVal.Dispose() : fntHeader.Dispose()
        fntSub.Dispose() : fntSlipTitle.Dispose() : fntValName.Dispose()
    End Sub

    Private Sub lblStatEmployeesCount_Click(sender As Object, e As EventArgs) Handles lblStatEmployeesCount.Click

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim logoPath As String = System.IO.Path.Combine(Application.StartupPath, "piyupi.png")
            If System.IO.File.Exists(logoPath) Then
                picSidebarLogo.Image = Image.FromFile(logoPath)
            End If
        Catch ex As Exception
            ' Ignore image loading errors
        End Try
    End Sub

    Private formsPlotLine As ScottPlot.WinForms.FormsPlot
    Private formsPlotBar As ScottPlot.WinForms.FormsPlot
    Private formsPlotPie As ScottPlot.WinForms.FormsPlot


    ' Eto yung function para i-setup yung charts sa dashboard,
    ' nag-create tayo ng 3 FormsPlot controls para sa line, bar,
    ' at pie charts, tapos ni-add natin sila sa panel na pnlCharts
    Private Sub SetupCharts()
        formsPlotLine = New ScottPlot.WinForms.FormsPlot() With {.Dock = DockStyle.Left, .Width = 500}
        formsPlotBar = New ScottPlot.WinForms.FormsPlot() With {.Dock = DockStyle.Left, .Width = 500}
        formsPlotPie = New ScottPlot.WinForms.FormsPlot() With {.Dock = DockStyle.Fill}

        pnlCharts.Controls.Add(formsPlotPie)
        pnlCharts.Controls.Add(formsPlotBar)
        pnlCharts.Controls.Add(formsPlotLine)
    End Sub


    ' Eto yung function para i-update yung charts sa dashboard base sa data ng visit records,
    ' nag-clear muna ng existing plots, tapos nag-generate ng new data para sa bawat chart at ni-plot gamit ang ScottPlot library.

    ' ano ngaba and ScottPlot? Eto yung open-source plotting library para sa .NET na nagpapadali ng pag-create ng interactive
    ' at high-performance charts sa Windows Forms, WPF, at Avalonia applications. Sa code na ito,
    ' ginagamit natin ang ScottPlot para mag-plot ng line chart para sa weekly admissions,
    ' bar chart para sa queue by time period, at pie chart para sa top conditions. (Wow galing noh?)
    Private Sub UpdateCharts(records As List(Of VisitRecord))
        If formsPlotLine Is Nothing Then Return

        formsPlotLine.Plot.Clear()
        formsPlotBar.Plot.Clear()
        formsPlotPie.Plot.Clear()

        ' Weekly Admissions (Line Chart)
        Dim last7Days = Enumerable.Range(0, 7).Select(Function(i) DateTime.Today.AddDays(-i)).Reverse().ToList()
        Dim counts = last7Days.Select(Function(d) records.Where(Function(r) r.TimeIn.Date = d).Count()).ToArray()
        Dim datesAsDouble = last7Days.Select(Function(d) d.ToOADate()).ToArray()
        If datesAsDouble.Length > 0 Then
            Dim sig = formsPlotLine.Plot.Add.Scatter(datesAsDouble, counts.Select(Function(c) CDbl(c)).ToArray())
            sig.Color = ScottPlot.Color.FromHex("#c40202")
            formsPlotLine.Plot.Axes.DateTimeTicksBottom()
            formsPlotLine.Plot.Title("Weekly Admissions")
        End If

        ' Queue by Time Period (Bar Chart)
        Dim morning = records.Where(Function(r) r.TimeIn.Hour >= 6 AndAlso r.TimeIn.Hour < 12).Count()
        Dim afternoon = records.Where(Function(r) r.TimeIn.Hour >= 12 AndAlso r.TimeIn.Hour < 18).Count()
        Dim evening = records.Where(Function(r) r.TimeIn.Hour >= 18).Count()
        Dim barPlot = formsPlotBar.Plot.Add.Bars(New Double() {morning, afternoon, evening})
        formsPlotBar.Plot.Title("Queue by Time Period")

        ' Top Conditions (Pie Chart)
        Dim conditions = records.Where(Function(r) Not String.IsNullOrEmpty(r.Sickness)).GroupBy(Function(r) r.Sickness).Select(Function(g) New With {.Name = g.Key, .Count = g.Count()}).OrderByDescending(Function(x) x.Count).Take(5).ToList()
        If conditions.Count > 0 Then
            Dim pieSlices As New List(Of ScottPlot.PieSlice)()
            For Each c In conditions
                pieSlices.Add(New ScottPlot.PieSlice() With {.Value = c.Count, .Label = c.Name})
            Next
            formsPlotPie.Plot.Add.Pie(pieSlices)
            formsPlotPie.Plot.Title("Top Conditions")
        End If

        formsPlotLine.Refresh()
        formsPlotBar.Refresh()
        formsPlotPie.Refresh()
    End Sub


    ' Eto yung function para i-update yung "Recent Activities" panel sa dashboard,
    ' nag-clear muna ng existing controls sa panel, tapos nag-add ng new labels para sa pinaka-recent 5 visits base sa TimeIn ng records.
    ' Kung walang recent visits, magpapakita ng message na "No recent visits."
    Private Sub UpdateRecentActivities(records As List(Of VisitRecord))
        If pnlRecent Is Nothing Then Return
        pnlRecent.Controls.Clear()
        Dim recentLabel As New Label With {.Text = "Recent Activities", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .ForeColor = Color.Maroon, .AutoSize = True, .Margin = New Padding(0, 0, 0, 10)}
        pnlRecent.Controls.Add(recentLabel)

        Dim scrollBarWidth As Integer = SystemInformation.VerticalScrollBarWidth
        Dim cardWidth As Integer = pnlRecent.Width - pnlRecent.Padding.Left - pnlRecent.Padding.Right - scrollBarWidth - 6

        Dim recentVisits = records.OrderByDescending(Function(r) r.TimeIn).Take(5).ToList()
        If recentVisits.Count = 0 Then
            pnlRecent.Controls.Add(New Label With {.Text = "No recent visits.", .AutoSize = True})
        Else
            For Each visit In recentVisits
                Dim visitLabel As New Label With {
                    .Text = $"{visit.PatientName}{vbCrLf}{visit.Sickness}{vbCrLf}{visit.TimeIn.ToString("MMM d, yyyy")}",
                    .AutoSize = True,
                    .Margin = New Padding(0, 0, 0, 10),
                    .Padding = New Padding(10),
                    .BackColor = Color.WhiteSmoke,
                    .Font = New Font("Segoe UI", 10.5F),
                    .MinimumSize = New Size(cardWidth, 0),
                    .MaximumSize = New Size(cardWidth, 0)
                }
                pnlRecent.Controls.Add(visitLabel)
            Next
        End If
    End Sub

    Private Sub UpdateTodayAppointments()

        ' Eto yung function para i-update yung "Today's Appointments" panel sa dashboard,
        ' nag-clear muna ng existing controls sa panel, tapos nag-add ng new labels para sa lahat ng
        ' appointments na naka-schedule sa current date base sa data mula sa database.
        If pnlTodayAppointments Is Nothing Then Return
        pnlTodayAppointments.Controls.Clear()
        Dim apptLabel As New Label With {.Text = "Today's Appointments", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .ForeColor = Color.Maroon, .AutoSize = True, .Margin = New Padding(0, 0, 0, 10)}
        pnlTodayAppointments.Controls.Add(apptLabel)

        Dim scrollBarWidth As Integer = SystemInformation.VerticalScrollBarWidth
        Dim cardWidth As Integer = pnlTodayAppointments.Width - pnlTodayAppointments.Padding.Left - pnlTodayAppointments.Padding.Right - scrollBarWidth - 6

        Dim todayAppts = dbHelper.GetAppointments(DateTime.Today)
        If todayAppts.Count = 0 Then
            pnlTodayAppointments.Controls.Add(New Label With {.Text = "No appointments today.", .AutoSize = True})
        Else
            For Each appt In todayAppts
                Dim lbl As New Label With {
                    .Text = $"{appt.PatientName}{vbCrLf}{appt.Reason}{vbCrLf}{appt.AppointmentDate.ToString("hh:mm tt")}",
                    .AutoSize = True,
                    .Margin = New Padding(0, 0, 0, 10),
                    .Padding = New Padding(10),
                    .BackColor = Color.WhiteSmoke,
                    .Font = New Font("Segoe UI", 10.5F),
                    .MinimumSize = New Size(cardWidth, 0),
                    .MaximumSize = New Size(cardWidth, 0)
                }
                pnlTodayAppointments.Controls.Add(lbl)
            Next
        End If
    End Sub

    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        Using f As New AboutForm()
            f.ShowDialog(Me)
        End Using
    End Sub

    Private Sub ShowDashboard()
        ' Show original dashboard controls
        dgvVisits.Visible = True
        pnlRightSidebar.Visible = True
        pnlCharts.Visible = True
        pnlStats.Visible = True

        ' Show visit header controls
        lblSearch.Visible = True
        txtSearch.Visible = True

        btnDelete.Visible = True
        btnDelete.Enabled = True

        btnEdit.Visible = True
        btnRefresh.Visible = True
        btnExport.Visible = True
        btnPrint.Visible = True
        btnHistory.Visible = True
        btnCheckOut.Visible = True
        chkQueueOnly.Visible = True

        ' Update header title
        lblHeaderTitle.Text = "Dashboard"

        LoadData()
    End Sub

    Private Sub pnlRecent_Paint(sender As Object, e As PaintEventArgs) Handles pnlRecent.Paint

    End Sub

    Public Sub HighlightSidebarButtonsForStep(stepNumber As Integer)
        ' Reset all to normal
        btnDashboard.BackColor = Color.Transparent
        btnAddVisit.BackColor = Color.Transparent
        btnInventory.BackColor = Color.Transparent
        btnAppointments.BackColor = Color.Transparent
        
        btnDashboard.ForeColor = Color.White
        btnAddVisit.ForeColor = Color.White
        btnInventory.ForeColor = Color.White
        btnAppointments.ForeColor = Color.White

        Dim highlightColor As Color = Color.Gold
        Dim darkText As Color = Color.Black

        Select Case stepNumber
            Case 2
                btnDashboard.BackColor = highlightColor
                btnDashboard.ForeColor = darkText
            Case 3
                btnAddVisit.BackColor = highlightColor
                btnAddVisit.ForeColor = darkText
            Case 4
                btnInventory.BackColor = highlightColor
                btnInventory.ForeColor = darkText
            Case 5
                btnAppointments.BackColor = highlightColor
                btnAppointments.ForeColor = darkText
        End Select
    End Sub

    Public Sub ShowUserGuide()
        Using guide As New UserGuideForm(Me)
            guide.ShowDialog(Me)
        End Using
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs)
        If dbHelper IsNot Nothing AndAlso Not String.IsNullOrEmpty(Me.CurrentUsername) Then
            Dim key As String = "ShowGuide_" & Me.CurrentUsername
            Dim showVal As String = dbHelper.GetConfig(key, "1")
            If showVal = "1" Then
                ShowUserGuide()
            End If
        End If
    End Sub

    Private Sub btnGuide_Click(sender As Object, e As EventArgs)
        ShowUserGuide()
    End Sub

End Class
