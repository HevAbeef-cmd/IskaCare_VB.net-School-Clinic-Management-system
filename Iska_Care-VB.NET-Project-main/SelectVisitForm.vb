Imports System.Windows.Forms
Imports System.Drawing
Imports System.Collections.Generic

Public Class SelectVisitForm
    Inherits Form

    Private pnlTop As Panel
    Private lblTitle As Label
    Private lblSearch As Label
    Private txtSearch As TextBox
    Private chkSelectAll As CheckBox
    Private dgvVisits As DataGridView
    Private btnSelect As Button
    Private btnCancel As Button
    Private lblSelCount As Label
    Private dbHelper As DatabaseHelper
    Private allVisits As List(Of VisitRecord)
    Private _suppressCheckChange As Boolean = False

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property SelectedRecord As VisitRecord

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property SelectedRecords As New List(Of VisitRecord)

    Private preselectedRecord As VisitRecord

    Public Sub New(Optional preselected As VisitRecord = Nothing)
        InitializeComponent()
        dbHelper = New DatabaseHelper()
        preselectedRecord = preselected
    End Sub

    Private Sub InitializeComponent()
        Me.Text = "Select Record(s) to Print Medical Slip"
        Me.Size = New Size(820, 600)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.BackColor = Color.White

        ' Top Panel
        pnlTop = New Panel With {
            .BackColor = Color.FromArgb(128, 0, 0),
            .Dock = DockStyle.Top,
            .Height = 70
        }

        lblTitle = New Label With {
            .Text = "Select Patient Record(s) — Print Medical Excuse Slip",
            .Font = New Font("Segoe UI", 13.0!, FontStyle.Bold),
            .ForeColor = Color.White,
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter
        }
        pnlTop.Controls.Add(lblTitle)

        ' Search Label
        lblSearch = New Label With {
            .Text = "Search Patient Name / Condition:",
            .Location = New Point(30, 95),
            .Size = New Size(220, 22),
            .Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)
        }

        ' Search TextBox
        txtSearch = New TextBox With {
            .Location = New Point(258, 92),
            .Size = New Size(300, 25),
            .Font = New Font("Segoe UI", 10.0!)
        }
        AddHandler txtSearch.TextChanged, AddressOf txtSearch_TextChanged

        ' Select All Checkbox
        chkSelectAll = New CheckBox With {
            .Text = "Select All",
            .Location = New Point(30, 128),
            .Size = New Size(110, 22),
            .Font = New Font("Segoe UI", 9.5!, FontStyle.Bold),
            .ForeColor = Color.FromArgb(128, 0, 0)
        }
        AddHandler chkSelectAll.CheckedChanged, AddressOf chkSelectAll_CheckedChanged

        ' Selection count label
        lblSelCount = New Label With {
            .Text = "0 selected",
            .Location = New Point(580, 128),
            .Size = New Size(200, 22),
            .Font = New Font("Segoe UI", 9.5!, FontStyle.Italic),
            .ForeColor = Color.Gray,
            .TextAlign = ContentAlignment.MiddleRight
        }

        ' DataGridView — checkbox column for multi-select
        dgvVisits = New DataGridView With {
            .Location = New Point(30, 155),
            .Size = New Size(745, 330),
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .BackgroundColor = Color.WhiteSmoke,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .RowHeadersVisible = False,
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            .ReadOnly = False,
            .EnableHeadersVisualStyles = False,
            .MultiSelect = True
        }
        dgvVisits.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(128, 0, 0)
        dgvVisits.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvVisits.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.5!, FontStyle.Bold)
        dgvVisits.ColumnHeadersHeight = 35
        AddHandler dgvVisits.DoubleClick, AddressOf dgvVisits_DoubleClick
        AddHandler dgvVisits.CellValueChanged, AddressOf dgvVisits_CellValueChanged
        AddHandler dgvVisits.CurrentCellDirtyStateChanged, AddressOf dgvVisits_CurrentCellDirtyStateChanged

        ' Buttons
        btnSelect = New Button With {
            .Text = "🖨  Print Selected Slip(s)",
            .Location = New Point(230, 505),
            .Size = New Size(200, 42),
            .BackColor = Color.FromArgb(128, 0, 0),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI", 9.5!, FontStyle.Bold),
            .Cursor = Cursors.Hand
        }
        btnSelect.FlatAppearance.BorderSize = 0
        AddHandler btnSelect.Click, AddressOf btnSelect_Click

        btnCancel = New Button With {
            .Text = "Cancel",
            .Location = New Point(450, 505),
            .Size = New Size(120, 42),
            .FlatStyle = FlatStyle.Flat,
            .ForeColor = Color.FromArgb(128, 0, 0),
            .Font = New Font("Segoe UI", 9.5!, FontStyle.Bold),
            .Cursor = Cursors.Hand
        }
        AddHandler btnCancel.Click, Sub()
                                        Me.DialogResult = DialogResult.Cancel
                                        Me.Close()
                                    End Sub

        Me.Controls.Add(pnlTop)
        Me.Controls.Add(lblSearch)
        Me.Controls.Add(txtSearch)
        Me.Controls.Add(chkSelectAll)
        Me.Controls.Add(lblSelCount)
        Me.Controls.Add(dgvVisits)
        Me.Controls.Add(btnSelect)
        Me.Controls.Add(btnCancel)

        AddHandler Me.Load, AddressOf SelectVisitForm_Load
    End Sub

    Private Sub SelectVisitForm_Load(sender As Object, e As EventArgs)
        LoadVisits()
    End Sub

    Private Sub LoadVisits()
        Try
            allVisits = dbHelper.GetAllVisits()
            FilterAndBindGrid()

            ' Pre-select the passed record checkbox
            If preselectedRecord IsNot Nothing AndAlso dgvVisits.Rows.Count > 0 Then
                For Each row As DataGridViewRow In dgvVisits.Rows
                    Dim record As VisitRecord = TryCast(row.Tag, VisitRecord)
                    If record IsNot Nothing AndAlso record.Id = preselectedRecord.Id Then
                        row.Cells(0).Value = True
                        row.Selected = True
                        dgvVisits.CurrentCell = row.Cells(1)
                        Exit For
                    End If
                Next
            End If
            UpdateSelectionCount()
        Catch ex As Exception
            MessageBox.Show("Error loading visits: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FilterAndBindGrid()
        If allVisits Is Nothing Then Return
        Dim filterText As String = txtSearch.Text.Trim().ToLower()

        Dim filtered As List(Of VisitRecord)
        If Not String.IsNullOrWhiteSpace(filterText) Then
            filtered = allVisits.FindAll(Function(v)
                Return (v.PatientName IsNot Nothing AndAlso v.PatientName.ToLower().Contains(filterText)) OrElse
                       (v.Sickness IsNot Nothing AndAlso v.Sickness.ToLower().Contains(filterText)) OrElse
                       (v.PatientType IsNot Nothing AndAlso v.PatientType.ToLower().Contains(filterText))
            End Function)
        Else
            filtered = New List(Of VisitRecord)(allVisits)
        End If

        ' Rebuild the grid manually with a checkbox column
        dgvVisits.Columns.Clear()
        dgvVisits.Rows.Clear()

        ' Checkbox column
        Dim chkCol As New DataGridViewCheckBoxColumn With {
            .HeaderText = "✓",
            .Width = 40,
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
            .ReadOnly = False
        }
        dgvVisits.Columns.Add(chkCol)

        ' Data columns
        Dim colId As New DataGridViewTextBoxColumn With {.HeaderText = "Visit ID", .Width = 70, .ReadOnly = True, .AutoSizeMode = DataGridViewAutoSizeColumnMode.None}
        Dim colName As New DataGridViewTextBoxColumn With {.HeaderText = "Patient Name", .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, .ReadOnly = True}
        Dim colType As New DataGridViewTextBoxColumn With {.HeaderText = "Type", .Width = 90, .ReadOnly = True, .AutoSizeMode = DataGridViewAutoSizeColumnMode.None}
        Dim colSick As New DataGridViewTextBoxColumn With {.HeaderText = "Condition", .Width = 150, .ReadOnly = True, .AutoSizeMode = DataGridViewAutoSizeColumnMode.None}
        Dim colTime As New DataGridViewTextBoxColumn With {.HeaderText = "Time In", .Width = 140, .ReadOnly = True, .AutoSizeMode = DataGridViewAutoSizeColumnMode.None}
        Dim colDoc As New DataGridViewTextBoxColumn With {.HeaderText = "Assigned To", .Width = 130, .ReadOnly = True, .AutoSizeMode = DataGridViewAutoSizeColumnMode.None}

        dgvVisits.Columns.Add(colId)
        dgvVisits.Columns.Add(colName)
        dgvVisits.Columns.Add(colType)
        dgvVisits.Columns.Add(colSick)
        dgvVisits.Columns.Add(colTime)
        dgvVisits.Columns.Add(colDoc)

        For Each v In filtered
            Dim rowIdx As Integer = dgvVisits.Rows.Add()
            Dim row As DataGridViewRow = dgvVisits.Rows(rowIdx)
            row.Tag = v
            row.Cells(0).Value = False
            row.Cells(1).Value = v.Id
            row.Cells(2).Value = v.PatientName
            row.Cells(3).Value = v.PatientType
            row.Cells(4).Value = v.Sickness
            row.Cells(5).Value = v.TimeIn.ToString("MMM dd, hh:mm tt")
            row.Cells(6).Value = v.DoctorAssigned
        Next

        UpdateSelectionCount()
    End Sub

    Private Sub dgvVisits_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs)
        If dgvVisits.IsCurrentCellDirty AndAlso dgvVisits.CurrentCell IsNot Nothing AndAlso TypeOf dgvVisits.CurrentCell Is DataGridViewCheckBoxCell Then
            dgvVisits.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub dgvVisits_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs)
        If e.ColumnIndex = 0 AndAlso Not _suppressCheckChange Then
            UpdateSelectionCount()
            ' Sync select-all state
            _suppressCheckChange = True
            Dim allChecked As Boolean = dgvVisits.Rows.Cast(Of DataGridViewRow).All(Function(r) CBool(r.Cells(0).Value) = True)
            chkSelectAll.Checked = allChecked
            _suppressCheckChange = False
        End If
    End Sub

    Private Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        If _suppressCheckChange Then Return
        _suppressCheckChange = True
        For Each row As DataGridViewRow In dgvVisits.Rows
            row.Cells(0).Value = chkSelectAll.Checked
        Next
        _suppressCheckChange = False
        UpdateSelectionCount()
    End Sub

    Private Sub UpdateSelectionCount()
        Dim count As Integer = dgvVisits.Rows.Cast(Of DataGridViewRow).Count(Function(r) CBool(r.Cells(0).Value) = True)
        lblSelCount.Text = $"{count} record(s) selected"
        lblSelCount.ForeColor = If(count > 0, Color.FromArgb(128, 0, 0), Color.Gray)
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs)
        FilterAndBindGrid()
    End Sub

    Private Sub dgvVisits_DoubleClick(sender As Object, e As EventArgs)
        ' Toggle checkbox of clicked row on double-click
        If dgvVisits.CurrentRow IsNot Nothing Then
            Dim currentVal As Boolean = CBool(dgvVisits.CurrentRow.Cells(0).Value)
            dgvVisits.CurrentRow.Cells(0).Value = Not currentVal
            UpdateSelectionCount()
        End If
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs)
        SelectRecordsAndClose()
    End Sub

    Private Sub SelectRecordsAndClose()
        Dim checkedRecords As New List(Of VisitRecord)()
        For Each row As DataGridViewRow In dgvVisits.Rows
            If CBool(row.Cells(0).Value) = True Then
                Dim record As VisitRecord = TryCast(row.Tag, VisitRecord)
                If record IsNot Nothing Then
                    checkedRecords.Add(record)
                End If
            End If
        Next

        If checkedRecords.Count = 0 Then
            ' Fall back to highlighted row selection
            If dgvVisits.CurrentRow IsNot Nothing Then
                Dim record As VisitRecord = TryCast(dgvVisits.CurrentRow.Tag, VisitRecord)
                If record IsNot Nothing Then
                    checkedRecords.Add(record)
                End If
            End If
        End If

        If checkedRecords.Count = 0 Then
            MessageBox.Show("Please check at least one patient record to print.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Me.SelectedRecords = checkedRecords
        Me.SelectedRecord = checkedRecords(0)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class
