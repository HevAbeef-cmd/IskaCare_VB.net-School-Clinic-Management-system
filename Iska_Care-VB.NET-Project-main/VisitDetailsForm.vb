Imports System.Windows.Forms
Imports System.Drawing

Public Class VisitDetailsForm
    Inherits Form
    ' eto yung form na ginagamit para mag-add o mag-edit ng visit record. Kapag nag-open ang form,
    ' iche-check nito kung may existing record na ibinigay sa constructor.
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property Record As VisitRecord
    Private dbHelper As DatabaseHelper
    Private pnlSicknessSuggestions As FlowLayoutPanel
    Private pnlMLEngine As GroupBox
    Private lblMLStatus As Label
    Private lblMLTokens As Label
    Private lblMLExplanation As Label
    Private WithEvents tmrDebounce As System.Windows.Forms.Timer

    Public Sub New(Optional existingRecord As VisitRecord = Nothing, Optional defaultAttending As String = "", Optional isTutorial As Boolean = False)
        InitializeComponent()
        dbHelper = New DatabaseHelper()
        Record = If(existingRecord, New VisitRecord() With {.TimeIn = DateTime.Now, .DoctorAssigned = defaultAttending})
        
        tmrDebounce = New System.Windows.Forms.Timer()
        tmrDebounce.Interval = 250
        
        LoadData()
        If isTutorial Then
            EnableTutorialMode()
        End If
        ' Kapag may existing record, i-po-populate nito yung form fields gamit yung data mula sa record.
        ' Kung wala naman, edi magse-set ito ng default values (e.g., current time para sa TimeIn or Timeout/checked out).
    End Sub

    Private Sub LoadData()
        ' Eto yung method na naglo-load ng data sa form fields.
        ' Iche-check nito kung may existing record at i-po-populate ang mga fields na walang laman sa record.
        If Not String.IsNullOrEmpty(Record.PatientType) Then cboType.SelectedItem = Record.PatientType Else cboType.SelectedIndex = 0
        txtName.Text = Record.PatientName
        numAge.Value = Record.Age
        numHeight.Value = CDec(Record.Height)
        numWeight.Value = CDec(Record.Weight)
        If Not String.IsNullOrEmpty(Record.Course) Then cboCourse.SelectedItem = Record.Course
        txtJob.Text = Record.Job
        txtContact.Text = Record.ContactNumber
        txtReason.Text = Record.ReasonForVisit
        txtSickness.Text = Record.Sickness
        txtInjury.Text = Record.Injury
        txtPrescription.Text = Record.Prescription

        If Not String.IsNullOrEmpty(Record.Gender) Then cboGender.SelectedItem = Record.Gender
        txtStudentID.Text = Record.StudentID
        If Not String.IsNullOrEmpty(Record.Year) Then cboYear.SelectedItem = Record.Year
        txtDoctor.Text = Record.DoctorAssigned

        If Record.TimeIn <> DateTime.MinValue Then dtpTimeIn.Value = Record.TimeIn
        If Record.TimeOut.HasValue Then
            chkTimeOut.Checked = True
            dtpTimeOut.Value = Record.TimeOut.Value
        Else
            chkTimeOut.Checked = False
            dtpTimeOut.Enabled = False
        End If
    End Sub

    Private Sub chkTimeOut_CheckedChanged(sender As Object, e As EventArgs) Handles chkTimeOut.CheckedChanged
        dtpTimeOut.Enabled = chkTimeOut.Checked
    End Sub

    Private Sub cboType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboType.SelectedIndexChanged
        Dim selectedType As String = If(cboType.SelectedItem IsNot Nothing, cboType.SelectedItem.ToString(), "")
        ' dto ni iche-check kung ano yung napili sa cboType (Student,Employee, o iba pa)
        ' at i-enable/disable ang mga related fields (cboCourse, cboYear, txtStudentID, txtJob) base sa selection.
        If selectedType = "Student" Then
            cboCourse.Enabled = True
            cboYear.Enabled = True
            txtStudentID.Enabled = True
            txtJob.Enabled = False
            txtJob.Text = ""
        ElseIf selectedType = "Employee" Then
            cboCourse.Enabled = False
            cboCourse.SelectedIndex = -1
            cboYear.Enabled = False
            cboYear.SelectedIndex = -1
            txtStudentID.Enabled = False
            txtStudentID.Text = ""
            txtJob.Enabled = True
        Else
            cboCourse.Enabled = False
            cboCourse.SelectedIndex = -1
            cboYear.Enabled = False
            cboYear.SelectedIndex = -1
            txtStudentID.Enabled = False
            txtStudentID.Text = ""
            txtJob.Enabled = False
            txtJob.Text = ""
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If btnSave.Text = "Close Demo" Then
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
            Return
        End If

        If String.IsNullOrWhiteSpace(txtName.Text) Then
            MessageBox.Show("Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        ' Eto yung method na nagse-save ng data mula sa form fields pabalik sa Record property.
        ' Iche-check nito kung may mga required fields na walang laman (e.g., Patient Name)
        ' at ipapakita ang validation error kung meron.
        Record.PatientType = cboType.SelectedItem?.ToString()
        Record.PatientName = txtName.Text
        Record.Age = CInt(numAge.Value)
        Record.Height = CDbl(numHeight.Value)
        Record.Weight = CDbl(numWeight.Value)
        Record.Course = cboCourse.SelectedItem?.ToString()
        Record.Job = txtJob.Text
        Record.ContactNumber = txtContact.Text
        Record.ReasonForVisit = txtReason.Text
        Record.Sickness = txtSickness.Text
        Record.Injury = txtInjury.Text
        Dim prescriptionText As String = txtPrescription.Text.Trim()
        Dim parenIdx As Integer = prescriptionText.IndexOf(" (")
        If parenIdx > 0 Then
            prescriptionText = prescriptionText.Substring(0, parenIdx).Trim()
        End If
        Record.Prescription = prescriptionText
        Record.Gender = cboGender.SelectedItem?.ToString()
        Record.StudentID = txtStudentID.Text
        Record.Year = cboYear.SelectedItem?.ToString()
        Record.DoctorAssigned = txtDoctor.Text
        Record.TimeIn = dtpTimeIn.Value

        If chkTimeOut.Checked Then
            Record.TimeOut = dtpTimeOut.Value
        Else
            Record.TimeOut = Nothing
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub VisitDetailsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Dynamically calculate alignment using the runtime coordinates of scaled designer controls
        Dim startX As Integer = lblGender.Left
        Dim targetWidth As Integer = cboGender.Right - lblGender.Left

        ' Create dynamic ML engine panel
        pnlMLEngine = New GroupBox With {
            .Text = "🧠 Dynamic Learning Engine",
            .Location = New Point(startX, 220),
            .Size = New Size(targetWidth, 180),
            .Font = New Font("Segoe UI", 9.0!, FontStyle.Bold),
            .ForeColor = Color.FromArgb(128, 0, 0)
        }
        
        lblMLStatus = New Label With {
            .Location = New Point(15, 30),
            .Size = New Size(pnlMLEngine.Width - 30, 45),
            .Font = New Font("Segoe UI", 9.5!, FontStyle.Bold),
            .ForeColor = Color.DimGray,
            .Text = "Waiting for symptom input..."
        }
        
        lblMLTokens = New Label With {
            .Location = New Point(15, 80),
            .Size = New Size(pnlMLEngine.Width - 30, 40),
            .Font = New Font("Segoe UI", 8.5!, FontStyle.Italic),
            .ForeColor = Color.Gray,
            .Text = "Active tokens: none"
        }
        
        lblMLExplanation = New Label With {
            .Location = New Point(15, 125),
            .Size = New Size(pnlMLEngine.Width - 30, 50),
            .Font = New Font("Segoe UI", 8.0!),
            .ForeColor = Color.DarkGray,
            .Text = "How it works: The engine tokenizes inputs and uses Jaccard similarity. Mappings are automatically customized based on the logged-in nurse's past entries."
        }
        
        pnlMLEngine.Controls.Add(lblMLStatus)
        pnlMLEngine.Controls.Add(lblMLTokens)
        pnlMLEngine.Controls.Add(lblMLExplanation)
        Me.Controls.Add(pnlMLEngine)

        ' Create dynamic suggestion panel next to the Sickness label/textbox
        pnlSicknessSuggestions = New FlowLayoutPanel With {
            .Location = New Point(startX, 442),
            .Size = New Size(targetWidth, 40),
            .FlowDirection = FlowDirection.LeftToRight,
            .WrapContents = False
        }
        Me.Controls.Add(pnlSicknessSuggestions)

        UpdateDiagnosisSuggestions()
        UpdatePrescriptionSuggestions()
    End Sub

    Private Sub UpdateDiagnosisSuggestions()
        Try
            If pnlSicknessSuggestions Is Nothing Then Return
            pnlSicknessSuggestions.Controls.Clear()
            
            Dim reasonText As String = txtReason.Text.Trim()
            If String.IsNullOrEmpty(reasonText) Then
                If lblMLStatus IsNot Nothing Then
                    lblMLStatus.Text = "Waiting for symptom input..."
                    lblMLStatus.ForeColor = Color.DimGray
                End If
                If lblMLTokens IsNot Nothing Then
                    lblMLTokens.Text = "Active tokens: none"
                End If
                Return
            End If
            
            ' Tokenize words to display what words the engine is analyzing
            Dim rawWords = reasonText.Split(New Char() {" "c, ","c, ";"c, "."c, "-"c, "_"c}, StringSplitOptions.RemoveEmptyEntries)
            Dim activeTokens As New List(Of String)()
            Dim stopWords As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
                "and", "the", "for", "with", "from", "felt", "have", "been", "was", "has", "had", 
                "she", "him", "his", "her", "they", "them", "but", "not", "this", "that", "are", 
                "were", "you", "your", "its", "then", "their", "our", "about", "like", "just", "very", "felt", "having", "some"
            }
            For Each rw In rawWords
                Dim cw = rw.Trim().ToLower()
                If cw.Length > 2 AndAlso Not stopWords.Contains(cw) Then
                    activeTokens.Add(cw)
                End If
            Next
            
            If activeTokens.Count = 0 Then
                If lblMLStatus IsNot Nothing Then
                    lblMLStatus.Text = "Words too short or are common stop words..."
                    lblMLStatus.ForeColor = Color.DimGray
                End If
                If lblMLTokens IsNot Nothing Then
                    lblMLTokens.Text = "Active tokens: none"
                End If
                Return
            End If
            
            If lblMLTokens IsNot Nothing Then
                lblMLTokens.Text = "Active tokens: " & String.Join(", ", activeTokens)
            End If
            
            ' Fetch predictions, passing current attending user name (Record.DoctorAssigned)
            Dim suggestions = dbHelper.GetTopDiagnosesForReason(reasonText, Record?.DoctorAssigned)
            
            If suggestions.Count = 0 Then
                If lblMLStatus IsNot Nothing Then
                    lblMLStatus.Text = $"🔵 New symptom pattern '{reasonText}' detected. System will dynamically learn this when saved."
                    lblMLStatus.ForeColor = Color.FromArgb(0, 102, 204) ' Dynamic blue
                End If
                Return
            End If
            
            ' High-Confidence Auto-Fill (>80% confidence and >=33% similarity)
            Dim autoFilled As Boolean = False
            Dim maxSimilarity As Double = 0
            Dim topSug As DatabaseHelper.DiagnosisSuggestion = Nothing
            
            For Each sug In suggestions
                If sug.Similarity > maxSimilarity Then
                    maxSimilarity = sug.Similarity
                    topSug = sug
                End If
                If sug.Confidence >= 80 AndAlso sug.Similarity >= 0.33 AndAlso String.IsNullOrWhiteSpace(txtSickness.Text) Then
                    txtSickness.Text = sug.Sickness
                    autoFilled = True
                End If
            Next
            
            ' Display live feedback depending on similarity and confidence
            If lblMLStatus IsNot Nothing Then
                If autoFilled AndAlso topSug IsNot Nothing Then
                    Dim matchedPct As Integer = CInt(topSug.Similarity * 100)
                    If matchedPct > 100 Then matchedPct = 100
                    lblMLStatus.Text = $"🟢 Automated: Auto-filled sickness '{topSug.Sickness}' based on similar past logs ({matchedPct}% match)."
                    lblMLStatus.ForeColor = Color.FromArgb(0, 128, 0) ' Nice green
                ElseIf topSug IsNot Nothing AndAlso topSug.Similarity >= 0.33 Then
                    Dim matchedPct As Integer = CInt(topSug.Similarity * 100)
                    If matchedPct > 100 Then matchedPct = 100
                    lblMLStatus.Text = $"🟡 Similar pattern found ({matchedPct}% match). Select suggestions below."
                    lblMLStatus.ForeColor = Color.FromArgb(180, 120, 0) ' Orange/brown
                Else
                    lblMLStatus.Text = $"🔵 Low similarity match. System will adapt and learn when saved."
                    lblMLStatus.ForeColor = Color.FromArgb(0, 102, 204) ' Dynamic blue
                End If
            End If
            
            ' Display clickable buttons for suggestions
            Dim lblSuggested As New Label With {
                .Text = "Suggested:",
                .AutoSize = True,
                .Margin = New Padding(0, 8, 5, 0),
                .Font = New Font("Segoe UI", 9.0!, FontStyle.Bold),
                .ForeColor = Color.FromArgb(128, 0, 0)
            }
            pnlSicknessSuggestions.Controls.Add(lblSuggested)
            
            For Each sug In suggestions
                Dim btn As New Button With {
                    .Text = $"{sug.Sickness} ({sug.Confidence}%)",
                    .AutoSize = True,
                    .FlatStyle = FlatStyle.Flat,
                    .BackColor = Color.White,
                    .ForeColor = Color.FromArgb(128, 0, 0),
                    .Cursor = Cursors.Hand,
                    .Margin = New Padding(0, 2, 5, 0),
                    .Font = New Font("Segoe UI", 8.5!)
                }
                btn.FlatAppearance.BorderSize = 1
                btn.FlatAppearance.BorderColor = Color.FromArgb(128, 0, 0)
                
                Dim diagnosisVal = sug.Sickness
                AddHandler btn.Click, Sub(snd, ea)
                                          txtSickness.Text = diagnosisVal
                                          UpdatePrescriptionSuggestions()
                                      End Sub
                pnlSicknessSuggestions.Controls.Add(btn)
            Next
            
            ' If we auto-filled Sickness, immediately update prescription suggestions
            If autoFilled Then
                UpdatePrescriptionSuggestions()
            End If
        Catch ex As Exception
            ' Fail silently
        End Try
    End Sub

    Private Sub UpdatePrescriptionSuggestions()
        Try
            Dim sickness As String = txtSickness.Text.Trim()
            Dim inventory = dbHelper.GetInventory()
            
            Dim currentPrescriptionText As String = txtPrescription.Text
            txtPrescription.Items.Clear()
            
            Dim autoSelectTop As String = ""
            
            ' If there's a sickness, get suggestions based on history (adaptive learning)
            If Not String.IsNullOrEmpty(sickness) Then
                Dim suggestions = dbHelper.GetTopPrescriptionsForSickness(sickness)
                If suggestions.Count > 0 Then
                    Dim hasAddedSuggestions As Boolean = False
                    For Each sug In suggestions
                        ' Find if this suggested item is in inventory and what its stock is
                        Dim invItem = inventory.Find(Function(i) i.MedicineName.Equals(sug.Prescription, StringComparison.OrdinalIgnoreCase))
                        If invItem IsNot Nothing Then
                            ' Check stock quantity: skip if out of stock
                            If invItem.StockQuantity > 0 Then
                                Dim suffix As String = ""
                                If invItem.StockQuantity < 10 Then
                                    suffix = " (LOW STOCK!)"
                                End If
                                Dim itemText As String = $"{invItem.MedicineName} ({invItem.StockQuantity} left) — SUGGESTED (Prescribed {sug.Count} times){suffix}"
                                txtPrescription.Items.Add(itemText)
                                If String.IsNullOrEmpty(autoSelectTop) Then
                                    autoSelectTop = itemText
                                End If
                                hasAddedSuggestions = True
                            End If
                        End If
                    Next
                    
                    If hasAddedSuggestions Then
                        txtPrescription.Items.Add("-----------------------------")
                    End If
                End If
            End If
            
            ' Add all inventory items as normal
            For Each item In inventory
                If item.StockQuantity <= 0 Then
                    txtPrescription.Items.Add(item.MedicineName & " (Not Available)")
                Else
                    txtPrescription.Items.Add(item.MedicineName & " (" & item.StockQuantity & " left)")
                End If
            Next

            If Not String.IsNullOrWhiteSpace(currentPrescriptionText) Then
                txtPrescription.Text = currentPrescriptionText
            ElseIf Not String.IsNullOrEmpty(autoSelectTop) Then
                txtPrescription.Text = autoSelectTop
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading inventory medicines: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtReason_Leave(sender As Object, e As EventArgs) Handles txtReason.Leave
        UpdateDiagnosisSuggestions()
    End Sub

    Private Sub txtSickness_Leave(sender As Object, e As EventArgs) Handles txtSickness.Leave
        UpdatePrescriptionSuggestions()
    End Sub

    Private Sub txtPrescription_Enter(sender As Object, e As EventArgs) Handles txtPrescription.Enter
        UpdatePrescriptionSuggestions()
    End Sub

    Private Sub txtReason_TextChanged(sender As Object, e As EventArgs) Handles txtReason.TextChanged
        tmrDebounce.Stop()
        tmrDebounce.Start()
    End Sub

    Private Sub txtSickness_TextChanged(sender As Object, e As EventArgs) Handles txtSickness.TextChanged
        tmrDebounce.Stop()
        tmrDebounce.Start()
    End Sub

    Private Sub tmrDebounce_Tick(sender As Object, e As EventArgs) Handles tmrDebounce.Tick
        tmrDebounce.Stop()
        UpdateDiagnosisSuggestions()
        UpdatePrescriptionSuggestions()
    End Sub

    Private Sub EnableTutorialMode()
        ' Increase form height to accommodate banner
        Me.Height += 50
        
        ' Create the tutorial banner
        Dim pnlBanner As New Panel()
        pnlBanner.Dock = DockStyle.Top
        pnlBanner.Height = 50
        pnlBanner.BackColor = Color.Gold
        pnlBanner.ForeColor = Color.Black
        
        Dim lblTitle As New Label()
        lblTitle.Text = "💡 TUTORIAL DEMO: Enter patient details and type symptoms (like 'headache') to see ML suggested tags!"
        lblTitle.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblTitle.Dock = DockStyle.Fill
        lblTitle.TextAlign = ContentAlignment.MiddleCenter
        pnlBanner.Controls.Add(lblTitle)
        
        Me.Controls.Add(pnlBanner)
        
        ' Shift all other controls down by 50 pixels
        For Each ctrl As Control In Me.Controls
            If ctrl IsNot pnlBanner Then
                ctrl.Top += 50
            End If
        Next
        
        ' Configure buttons for demo exit
        btnSave.Text = "Close Demo"
        btnCancel.Visible = False
        
        ' Pre-populate demo values
        cboType.SelectedItem = "Student"
        txtName.Text = "Juan Dela Cruz"
        txtStudentID.Text = "2024-00123-LO-0"
        numAge.Value = 20
        cboGender.SelectedItem = "Male"
        cboCourse.SelectedItem = "BSIT"
        cboYear.SelectedItem = "1st"
        txtReason.Text = "headache and fever"
        
        ' Trigger suggestions update
        UpdateDiagnosisSuggestions()
    End Sub
End Class
