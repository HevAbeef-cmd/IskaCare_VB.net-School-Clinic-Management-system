Imports System.Windows.Forms
Imports System.Drawing

Public Class UserManagementForm
    Inherits Form

    Private pnlTop As Panel
    Private lblTitle As Label
    Private dgvUsers As DataGridView
    Private btnAddUser As Button
    Private btnResetPassword As Button
    Private btnDeleteUser As Button
    Private btnClose As Button
    Private dbHelper As DatabaseHelper

    Public Sub New()
        InitializeComponent()
        dbHelper = New DatabaseHelper()
    End Sub

    Private Sub InitializeComponent()
        Me.Text = "Account Management"
        Me.Size = New Size(650, 500)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        ' Register form Load event to perform binding and layout safely
        AddHandler Me.Load, AddressOf UserManagementForm_Load

        ' Top Panel
        pnlTop = New Panel With {
            .BackColor = Color.FromArgb(128, 0, 0),
            .Dock = DockStyle.Top,
            .Height = 70
        }

        lblTitle = New Label With {
            .Text = "System User Accounts",
            .Font = New Font("Segoe UI", 14.0!, FontStyle.Bold),
            .ForeColor = Color.White,
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter
        }
        pnlTop.Controls.Add(lblTitle)

        ' DataGridView
        dgvUsers = New DataGridView With {
            .Location = New Point(30, 90),
            .Size = New Size(570, 260),
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .BackgroundColor = Color.White,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .RowHeadersVisible = False,
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            .ReadOnly = True,
            .EnableHeadersVisualStyles = False
        }
        dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(128, 0, 0)
        dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvUsers.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.5!, FontStyle.Bold)
        dgvUsers.ColumnHeadersHeight = 35

        ' Buttons
        btnAddUser = New Button With {
            .Text = "Add Account",
            .Location = New Point(30, 380),
            .Size = New Size(120, 40),
            .BackColor = Color.FromArgb(128, 0, 0),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
        }
        AddHandler btnAddUser.Click, AddressOf btnAddUser_Click

        btnResetPassword = New Button With {
            .Text = "Reset Password",
            .Location = New Point(165, 380),
            .Size = New Size(130, 40),
            .FlatStyle = FlatStyle.Flat,
            .ForeColor = Color.FromArgb(128, 0, 0),
            .Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
        }
        AddHandler btnResetPassword.Click, AddressOf btnResetPassword_Click

        btnDeleteUser = New Button With {
            .Text = "Delete Account",
            .Location = New Point(310, 380),
            .Size = New Size(130, 40),
            .FlatStyle = FlatStyle.Flat,
            .ForeColor = Color.FromArgb(128, 0, 0),
            .Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
        }
        AddHandler btnDeleteUser.Click, AddressOf btnDeleteUser_Click

        btnClose = New Button With {
            .Text = "Close",
            .Location = New Point(470, 380),
            .Size = New Size(110, 40),
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
        }
        AddHandler btnClose.Click, Sub() Me.Close()

        Me.Controls.Add(pnlTop)
        Me.Controls.Add(dgvUsers)
        Me.Controls.Add(btnAddUser)
        Me.Controls.Add(btnResetPassword)
        Me.Controls.Add(btnDeleteUser)
        Me.Controls.Add(btnClose)
    End Sub

    Private Sub UserManagementForm_Load(sender As Object, e As EventArgs)
        LoadUsers()
    End Sub

    Private Sub LoadUsers()
        Try
            Dim users = dbHelper.GetAllUsers()
            dgvUsers.DataSource = Nothing
            dgvUsers.DataSource = users

            ' Safely style generated columns after grid is rendered in Load event
            For Each col As DataGridViewColumn In dgvUsers.Columns
                If Not String.IsNullOrEmpty(col.Name) Then
                    Select Case col.Name.ToLower()
                        Case "id"
                            col.HeaderText = "User ID"
                            col.Width = 80
                        Case "username"
                            col.HeaderText = "Username"
                        Case "password"
                            col.HeaderText = "Password (Plain Text)"
                        Case "role"
                            col.HeaderText = "Role"
                    End Select
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Error loading accounts: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAddUser_Click(sender As Object, e As EventArgs)
        Using registerDlg As New Form()
            registerDlg.Text = "Add Account"
            registerDlg.Size = New Size(320, 260)
            registerDlg.StartPosition = FormStartPosition.CenterParent
            registerDlg.FormBorderStyle = FormBorderStyle.FixedDialog
            registerDlg.MaximizeBox = False
            registerDlg.MinimizeBox = False

            Dim lblUser As New Label With {.Text = "Username:", .Location = New Point(20, 20), .Size = New Size(80, 20)}
            Dim txtUser As New TextBox With {.Location = New Point(110, 20), .Size = New Size(160, 20)}

            Dim lblPass As New Label With {.Text = "Password:", .Location = New Point(20, 60), .Size = New Size(80, 20)}
            Dim txtPass As New TextBox With {.Location = New Point(110, 60), .Size = New Size(160, 20)}

            Dim lblRole As New Label With {.Text = "Role:", .Location = New Point(20, 100), .Size = New Size(80, 20)}
            Dim cmbRole As New ComboBox With {.Location = New Point(110, 100), .Size = New Size(160, 20), .DropDownStyle = ComboBoxStyle.DropDownList}
            cmbRole.Items.AddRange({"Admin", "Doctor", "Nurse", "Dentist"})
            cmbRole.SelectedIndex = 2 ' Defaults to Nurse

            Dim btnAdd As New Button With {
                .Text = "Add",
                .Location = New Point(50, 160),
                .Size = New Size(90, 30),
                .BackColor = Color.FromArgb(128, 0, 0),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat
            }

            Dim btnCancel As New Button With {
                .Text = "Cancel",
                .Location = New Point(160, 160),
                .Size = New Size(90, 30),
                .FlatStyle = FlatStyle.Flat
            }

            AddHandler btnCancel.Click, Sub() registerDlg.Close()
            AddHandler btnAdd.Click, Sub()
                                         Dim uName As String = txtUser.Text.Trim()
                                         Dim pWord As String = txtPass.Text
                                         Dim uRole As String = cmbRole.SelectedItem.ToString()

                                         If String.IsNullOrWhiteSpace(uName) OrElse String.IsNullOrWhiteSpace(pWord) Then
                                             MessageBox.Show("Please fill out all fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                             Return
                                         End If

                                         If dbHelper.CreateUser(uName, pWord, uRole) Then
                                             MessageBox.Show("User account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                             registerDlg.DialogResult = DialogResult.OK
                                             registerDlg.Close()
                                         Else
                                             MessageBox.Show("Username already exists or creation failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                         End If
                                     End Sub

            registerDlg.Controls.Add(lblUser)
            registerDlg.Controls.Add(txtUser)
            registerDlg.Controls.Add(lblPass)
            registerDlg.Controls.Add(txtPass)
            registerDlg.Controls.Add(lblRole)
            registerDlg.Controls.Add(cmbRole)
            registerDlg.Controls.Add(btnAdd)
            registerDlg.Controls.Add(btnCancel)

            If registerDlg.ShowDialog(Me) = DialogResult.OK Then
                LoadUsers()
            End If
        End Using
    End Sub

    Private Sub btnResetPassword_Click(sender As Object, e As EventArgs)
        If dgvUsers.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a user account to reset the password.", "Select Account", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedUser As DatabaseHelper.UserItem = CType(dgvUsers.SelectedRows(0).DataBoundItem, DatabaseHelper.UserItem)

        Using resetDlg As New Form()
            resetDlg.Text = "Reset Password"
            resetDlg.Size = New Size(320, 180)
            resetDlg.StartPosition = FormStartPosition.CenterParent
            resetDlg.FormBorderStyle = FormBorderStyle.FixedDialog
            resetDlg.MaximizeBox = False
            resetDlg.MinimizeBox = False

            Dim lblInfo As New Label With {
                .Text = $"Enter new password for '{selectedUser.Username}':",
                .Location = New Point(20, 20),
                .Size = New Size(260, 20)
            }
            Dim txtNewPass As New TextBox With {
                .Location = New Point(20, 50),
                .Size = New Size(260, 20)
            }

            Dim btnSave As New Button With {
                .Text = "Save",
                .Location = New Point(50, 90),
                .Size = New Size(90, 30),
                .BackColor = Color.FromArgb(128, 0, 0),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat
            }

            Dim btnCancel As New Button With {
                .Text = "Cancel",
                .Location = New Point(160, 90),
                .Size = New Size(90, 30),
                .FlatStyle = FlatStyle.Flat
            }

            AddHandler btnCancel.Click, Sub() resetDlg.Close()
            AddHandler btnSave.Click, Sub()
                                          Dim newPass As String = txtNewPass.Text
                                          If String.IsNullOrWhiteSpace(newPass) Then
                                              MessageBox.Show("Password cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                              Return
                                          End If

                                          Try
                                              dbHelper.UpdateUserPassword(selectedUser.Id, newPass)
                                              MessageBox.Show("Password successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                              resetDlg.DialogResult = DialogResult.OK
                                              resetDlg.Close()
                                          Catch ex As Exception
                                              MessageBox.Show("Failed to reset password: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                          End Try
                                      End Sub

            resetDlg.Controls.Add(lblInfo)
            resetDlg.Controls.Add(txtNewPass)
            resetDlg.Controls.Add(btnSave)
            resetDlg.Controls.Add(btnCancel)

            If resetDlg.ShowDialog(Me) = DialogResult.OK Then
                LoadUsers()
            End If
        End Using
    End Sub

    Private Sub btnDeleteUser_Click(sender As Object, e As EventArgs)
        If dgvUsers.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a user account to delete.", "Select Account", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedUser As DatabaseHelper.UserItem = CType(dgvUsers.SelectedRows(0).DataBoundItem, DatabaseHelper.UserItem)

        If selectedUser.Username.ToLower() = "admin" Then
            MessageBox.Show("The default 'admin' account cannot be deleted to prevent locking out the system.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim activeUser As String = ""
        For Each openForm As Form In Application.OpenForms
            If TypeOf openForm Is Form1 Then
                activeUser = CType(openForm, Form1).CurrentUsername
                Exit For
            End If
        Next

        If selectedUser.Username.Equals(activeUser, StringComparison.OrdinalIgnoreCase) Then
            MessageBox.Show("You cannot delete the account you are currently logged in with.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show($"Are you sure you want to permanently delete the user '{selectedUser.Username}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                dbHelper.DeleteUser(selectedUser.Id)
                MessageBox.Show("User successfully deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadUsers()
            Catch ex As Exception
                MessageBox.Show("Failed to delete user: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
End Class
