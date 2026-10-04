<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        pnlSidebar = New Panel()
        btnLogout = New Button()
        btnManageUsers = New Button()
        btnSettings = New Button()
        btnAbout = New Button()
        btnAppointments = New Button()
        btnInventory = New Button()
        btnAddVisit = New Button()
        btnDashboard = New Button()
        pnlSidebarHeader = New Panel()
        picSidebarLogo = New PictureBox()
        lblTitle = New Label()
        pnlHeader = New Panel()
        btnCheckOut = New Button()
        chkQueueOnly = New CheckBox()
        btnHistory = New Button()
        btnPrint = New Button()
        btnExport = New Button()
        btnRefresh = New Button()
        btnDelete = New Button()
        btnEdit = New Button()
        txtSearch = New TextBox()
        lblSearch = New Label()
        lblHeaderTitle = New Label()
        lblUserProfile = New Label()
        pnlContent = New Panel()
        dgvVisits = New DataGridView()
        pnlRightSidebar = New Panel()
        pnlTodayAppointments = New FlowLayoutPanel()
        pnlRecent = New FlowLayoutPanel()
        pnlCharts = New Panel()
        pnlStats = New Panel()
        pnlStatAlerts = New Panel()
        lblStatAlertsTitle = New Label()
        lblStatAlertsCount = New Label()
        pnlStatVisits = New Panel()
        lblStatVisitsTitle = New Label()
        lblStatVisitsCount = New Label()
        pnlStatStudents = New Panel()
        lblStatStudentsTitle = New Label()
        lblStatStudentsCount = New Label()
        pnlStatEmployees = New Panel()
        lblStatEmployeesTitle = New Label()
        lblStatEmployeesCount = New Label()
        pnlSidebar.SuspendLayout()
        pnlSidebarHeader.SuspendLayout()
        CType(picSidebarLogo, ComponentModel.ISupportInitialize).BeginInit()
        pnlHeader.SuspendLayout()
        pnlContent.SuspendLayout()
        CType(dgvVisits, ComponentModel.ISupportInitialize).BeginInit()
        pnlRightSidebar.SuspendLayout()
        pnlStats.SuspendLayout()
        pnlStatAlerts.SuspendLayout()
        pnlStatVisits.SuspendLayout()
        pnlStatStudents.SuspendLayout()
        pnlStatEmployees.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlSidebar
        ' 
        pnlSidebar.BackColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        pnlSidebar.Controls.Add(btnLogout)
        pnlSidebar.Controls.Add(btnManageUsers)
        pnlSidebar.Controls.Add(btnSettings)
        pnlSidebar.Controls.Add(btnAbout)
        pnlSidebar.Controls.Add(btnAppointments)
        pnlSidebar.Controls.Add(btnInventory)
        pnlSidebar.Controls.Add(btnAddVisit)
        pnlSidebar.Controls.Add(btnDashboard)
        pnlSidebar.Controls.Add(pnlSidebarHeader)
        pnlSidebar.Dock = DockStyle.Left
        pnlSidebar.Location = New Point(0, 0)
        pnlSidebar.Name = "pnlSidebar"
        pnlSidebar.Size = New Size(220, 775)
        pnlSidebar.TabIndex = 0
        ' 
        ' btnLogout
        ' 
        btnLogout.Cursor = Cursors.Hand
        btnLogout.Dock = DockStyle.Bottom
        btnLogout.FlatAppearance.BorderSize = 0
        btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(160), CByte(0), CByte(0))
        btnLogout.FlatStyle = FlatStyle.Flat
        btnLogout.Font = New Font("Segoe UI", 11F)
        btnLogout.ForeColor = Color.White
        btnLogout.Location = New Point(0, 625)
        btnLogout.Margin = New Padding(3, 2, 3, 2)
        btnLogout.Name = "btnLogout"
        btnLogout.Padding = New Padding(20, 0, 0, 0)
        btnLogout.Size = New Size(220, 50)
        btnLogout.TabIndex = 13
        btnLogout.Text = "Logout"
        btnLogout.TextAlign = ContentAlignment.MiddleLeft
        btnLogout.UseVisualStyleBackColor = True
        ' 
        ' btnManageUsers
        ' 
        btnManageUsers.Cursor = Cursors.Hand
        btnManageUsers.Dock = DockStyle.Bottom
        btnManageUsers.FlatAppearance.BorderSize = 0
        btnManageUsers.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(160), CByte(0), CByte(0))
        btnManageUsers.FlatStyle = FlatStyle.Flat
        btnManageUsers.Font = New Font("Segoe UI", 11F)
        btnManageUsers.ForeColor = Color.White
        btnManageUsers.Location = New Point(0, 675)
        btnManageUsers.Margin = New Padding(3, 2, 3, 2)
        btnManageUsers.Name = "btnManageUsers"
        btnManageUsers.Padding = New Padding(20, 0, 0, 0)
        btnManageUsers.Size = New Size(220, 50)
        btnManageUsers.TabIndex = 12
        btnManageUsers.Text = "Manage Users"
        btnManageUsers.TextAlign = ContentAlignment.MiddleLeft
        btnManageUsers.UseVisualStyleBackColor = True
        ' 
        ' btnSettings
        ' 
        btnSettings.Cursor = Cursors.Hand
        btnSettings.Dock = DockStyle.Bottom
        btnSettings.FlatAppearance.BorderSize = 0
        btnSettings.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(160), CByte(0), CByte(0))
        btnSettings.FlatStyle = FlatStyle.Flat
        btnSettings.Font = New Font("Segoe UI", 11F)
        btnSettings.ForeColor = Color.White
        btnSettings.Location = New Point(0, 725)
        btnSettings.Name = "btnSettings"
        btnSettings.Padding = New Padding(20, 0, 0, 0)
        btnSettings.Size = New Size(220, 50)
        btnSettings.TabIndex = 4
        btnSettings.Text = "Settings"
        btnSettings.TextAlign = ContentAlignment.MiddleLeft
        btnSettings.UseVisualStyleBackColor = True
        ' 
        ' btnAbout
        ' 
        btnAbout.Cursor = Cursors.Hand
        btnAbout.Dock = DockStyle.Top
        btnAbout.FlatAppearance.BorderSize = 0
        btnAbout.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(160), CByte(0), CByte(0))
        btnAbout.FlatStyle = FlatStyle.Flat
        btnAbout.Font = New Font("Segoe UI", 11F)
        btnAbout.ForeColor = Color.White
        btnAbout.Location = New Point(0, 335)
        btnAbout.Margin = New Padding(3, 2, 3, 2)
        btnAbout.Name = "btnAbout"
        btnAbout.Padding = New Padding(20, 0, 0, 0)
        btnAbout.Size = New Size(220, 50)
        btnAbout.TabIndex = 6
        btnAbout.Text = "About Us"
        btnAbout.TextAlign = ContentAlignment.MiddleLeft
        btnAbout.UseVisualStyleBackColor = True
        ' 
        ' btnAppointments
        ' 
        btnAppointments.Cursor = Cursors.Hand
        btnAppointments.Dock = DockStyle.Top
        btnAppointments.FlatAppearance.BorderSize = 0
        btnAppointments.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(160), CByte(0), CByte(0))
        btnAppointments.FlatStyle = FlatStyle.Flat
        btnAppointments.Font = New Font("Segoe UI", 11F)
        btnAppointments.ForeColor = Color.White
        btnAppointments.Location = New Point(0, 285)
        btnAppointments.Margin = New Padding(3, 2, 3, 2)
        btnAppointments.Name = "btnAppointments"
        btnAppointments.Padding = New Padding(20, 0, 0, 0)
        btnAppointments.Size = New Size(220, 50)
        btnAppointments.TabIndex = 5
        btnAppointments.Text = "Appointments"
        btnAppointments.TextAlign = ContentAlignment.MiddleLeft
        btnAppointments.UseVisualStyleBackColor = True
        ' 
        ' btnInventory
        ' 
        btnInventory.Cursor = Cursors.Hand
        btnInventory.Dock = DockStyle.Top
        btnInventory.FlatAppearance.BorderSize = 0
        btnInventory.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(160), CByte(0), CByte(0))
        btnInventory.FlatStyle = FlatStyle.Flat
        btnInventory.Font = New Font("Segoe UI", 11F)
        btnInventory.ForeColor = Color.White
        btnInventory.Location = New Point(0, 235)
        btnInventory.Name = "btnInventory"
        btnInventory.Padding = New Padding(20, 0, 0, 0)
        btnInventory.Size = New Size(220, 50)
        btnInventory.TabIndex = 3
        btnInventory.Text = "Inventory"
        btnInventory.TextAlign = ContentAlignment.MiddleLeft
        btnInventory.UseVisualStyleBackColor = True
        ' 
        ' btnAddVisit
        ' 
        btnAddVisit.Cursor = Cursors.Hand
        btnAddVisit.Dock = DockStyle.Top
        btnAddVisit.FlatAppearance.BorderSize = 0
        btnAddVisit.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(160), CByte(0), CByte(0))
        btnAddVisit.FlatStyle = FlatStyle.Flat
        btnAddVisit.Font = New Font("Segoe UI", 11F)
        btnAddVisit.ForeColor = Color.White
        btnAddVisit.Location = New Point(0, 185)
        btnAddVisit.Name = "btnAddVisit"
        btnAddVisit.Padding = New Padding(20, 0, 0, 0)
        btnAddVisit.Size = New Size(220, 50)
        btnAddVisit.TabIndex = 2
        btnAddVisit.Text = "Add New Visit"
        btnAddVisit.TextAlign = ContentAlignment.MiddleLeft
        btnAddVisit.UseVisualStyleBackColor = True
        ' 
        ' btnDashboard
        ' 
        btnDashboard.Cursor = Cursors.Hand
        btnDashboard.Dock = DockStyle.Top
        btnDashboard.FlatAppearance.BorderSize = 0
        btnDashboard.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(160), CByte(0), CByte(0))
        btnDashboard.FlatStyle = FlatStyle.Flat
        btnDashboard.Font = New Font("Segoe UI", 11F)
        btnDashboard.ForeColor = Color.White
        btnDashboard.Location = New Point(0, 135)
        btnDashboard.Name = "btnDashboard"
        btnDashboard.Padding = New Padding(20, 0, 0, 0)
        btnDashboard.Size = New Size(220, 50)
        btnDashboard.TabIndex = 1
        btnDashboard.Text = "Dashboard"
        btnDashboard.TextAlign = ContentAlignment.MiddleLeft
        btnDashboard.UseVisualStyleBackColor = True
        ' 
        ' pnlSidebarHeader
        ' 
        pnlSidebarHeader.BackColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        pnlSidebarHeader.Controls.Add(picSidebarLogo)
        pnlSidebarHeader.Controls.Add(lblTitle)
        pnlSidebarHeader.Dock = DockStyle.Top
        pnlSidebarHeader.Location = New Point(0, 0)
        pnlSidebarHeader.Margin = New Padding(3, 2, 3, 2)
        pnlSidebarHeader.Name = "pnlSidebarHeader"
        pnlSidebarHeader.Size = New Size(220, 135)
        pnlSidebarHeader.TabIndex = 15
        ' 
        ' picSidebarLogo
        ' 
        picSidebarLogo.Location = New Point(0, 15)
        picSidebarLogo.Margin = New Padding(3, 2, 3, 2)
        picSidebarLogo.Name = "picSidebarLogo"
        picSidebarLogo.Size = New Size(220, 75)
        picSidebarLogo.SizeMode = PictureBoxSizeMode.Zoom
        picSidebarLogo.TabIndex = 14
        picSidebarLogo.TabStop = False
        ' 
        ' lblTitle
        ' 
        lblTitle.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(0, 98)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(220, 30)
        lblTitle.TabIndex = 0
        lblTitle.Text = "ISKA CARE"
        lblTitle.TextAlign = ContentAlignment.TopCenter
        ' 
        ' pnlHeader
        ' 
        pnlHeader.BackColor = Color.White
        pnlHeader.Controls.Add(btnCheckOut)
        pnlHeader.Controls.Add(chkQueueOnly)
        pnlHeader.Controls.Add(btnHistory)
        pnlHeader.Controls.Add(btnPrint)
        pnlHeader.Controls.Add(btnExport)
        pnlHeader.Controls.Add(btnRefresh)
        pnlHeader.Controls.Add(btnDelete)
        pnlHeader.Controls.Add(btnEdit)
        pnlHeader.Controls.Add(txtSearch)
        pnlHeader.Controls.Add(lblSearch)
        pnlHeader.Controls.Add(lblHeaderTitle)
        pnlHeader.Controls.Add(lblUserProfile)
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Location = New Point(220, 0)
        pnlHeader.Name = "pnlHeader"
        pnlHeader.Size = New Size(1444, 70)
        pnlHeader.TabIndex = 1
        ' 
        ' btnCheckOut
        ' 
        btnCheckOut.BackColor = Color.FromArgb(CByte(40), CByte(167), CByte(69))
        btnCheckOut.Cursor = Cursors.Hand
        btnCheckOut.FlatAppearance.BorderSize = 0
        btnCheckOut.FlatStyle = FlatStyle.Flat
        btnCheckOut.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnCheckOut.ForeColor = Color.White
        btnCheckOut.Location = New Point(862, 15)
        btnCheckOut.Margin = New Padding(3, 2, 3, 2)
        btnCheckOut.Name = "btnCheckOut"
        btnCheckOut.Size = New Size(83, 24)
        btnCheckOut.TabIndex = 9
        btnCheckOut.Text = "Check Out"
        btnCheckOut.UseVisualStyleBackColor = False
        ' 
        ' chkQueueOnly
        ' 
        chkQueueOnly.AutoSize = True
        chkQueueOnly.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        chkQueueOnly.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        chkQueueOnly.Location = New Point(954, 19)
        chkQueueOnly.Margin = New Padding(3, 2, 3, 2)
        chkQueueOnly.Name = "chkQueueOnly"
        chkQueueOnly.Size = New Size(91, 19)
        chkQueueOnly.TabIndex = 10
        chkQueueOnly.Text = "Queue Only"
        chkQueueOnly.UseVisualStyleBackColor = True
        ' 
        ' btnHistory
        ' 
        btnHistory.BackColor = Color.White
        btnHistory.Cursor = Cursors.Hand
        btnHistory.FlatAppearance.BorderColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnHistory.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(240), CByte(240), CByte(240))
        btnHistory.FlatStyle = FlatStyle.Flat
        btnHistory.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnHistory.ForeColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnHistory.Location = New Point(774, 15)
        btnHistory.Margin = New Padding(3, 2, 3, 2)
        btnHistory.Name = "btnHistory"
        btnHistory.Size = New Size(83, 24)
        btnHistory.TabIndex = 8
        btnHistory.Text = "View History"
        btnHistory.UseVisualStyleBackColor = False
        ' 
        ' btnPrint
        ' 
        btnPrint.BackColor = Color.White
        btnPrint.Cursor = Cursors.Hand
        btnPrint.FlatAppearance.BorderColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnPrint.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(240), CByte(240), CByte(240))
        btnPrint.FlatStyle = FlatStyle.Flat
        btnPrint.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnPrint.ForeColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnPrint.Location = New Point(691, 15)
        btnPrint.Margin = New Padding(3, 2, 3, 2)
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New Size(79, 24)
        btnPrint.TabIndex = 7
        btnPrint.Text = "Print Slip"
        btnPrint.UseVisualStyleBackColor = False
        ' 
        ' btnExport
        ' 
        btnExport.BackColor = Color.White
        btnExport.Cursor = Cursors.Hand
        btnExport.FlatAppearance.BorderColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnExport.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(240), CByte(240), CByte(240))
        btnExport.FlatStyle = FlatStyle.Flat
        btnExport.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnExport.ForeColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnExport.Location = New Point(608, 15)
        btnExport.Margin = New Padding(3, 2, 3, 2)
        btnExport.Name = "btnExport"
        btnExport.Size = New Size(79, 24)
        btnExport.TabIndex = 6
        btnExport.Text = "Export CSV"
        btnExport.UseVisualStyleBackColor = False
        ' 
        ' btnRefresh
        ' 
        btnRefresh.BackColor = Color.White
        btnRefresh.Cursor = Cursors.Hand
        btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnRefresh.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(240), CByte(240), CByte(240))
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnRefresh.ForeColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnRefresh.Location = New Point(534, 15)
        btnRefresh.Margin = New Padding(3, 2, 3, 2)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New Size(70, 24)
        btnRefresh.TabIndex = 5
        btnRefresh.Text = "Refresh"
        btnRefresh.UseVisualStyleBackColor = False
        ' 
        ' btnDelete
        ' 
        btnDelete.BackColor = Color.White
        btnDelete.Cursor = Cursors.Hand
        btnDelete.FlatAppearance.BorderColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnDelete.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(240), CByte(240), CByte(240))
        btnDelete.FlatStyle = FlatStyle.Flat
        btnDelete.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnDelete.ForeColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnDelete.Location = New Point(459, 15)
        btnDelete.Margin = New Padding(3, 2, 3, 2)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(70, 24)
        btnDelete.TabIndex = 4
        btnDelete.Text = "Delete"
        btnDelete.UseVisualStyleBackColor = False
        ' 
        ' btnEdit
        ' 
        btnEdit.BackColor = Color.White
        btnEdit.Cursor = Cursors.Hand
        btnEdit.FlatAppearance.BorderColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnEdit.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(240), CByte(240), CByte(240))
        btnEdit.FlatStyle = FlatStyle.Flat
        btnEdit.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnEdit.ForeColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnEdit.Location = New Point(376, 15)
        btnEdit.Margin = New Padding(3, 2, 3, 2)
        btnEdit.Name = "btnEdit"
        btnEdit.Size = New Size(79, 24)
        btnEdit.TabIndex = 3
        btnEdit.Text = "Edit Selected"
        btnEdit.UseVisualStyleBackColor = False
        ' 
        ' txtSearch
        ' 
        txtSearch.Font = New Font("Segoe UI", 10F)
        txtSearch.Location = New Point(219, 16)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(140, 25)
        txtSearch.TabIndex = 2
        ' 
        ' lblSearch
        ' 
        lblSearch.AutoSize = True
        lblSearch.Font = New Font("Segoe UI", 10F)
        lblSearch.Location = New Point(148, 18)
        lblSearch.Name = "lblSearch"
        lblSearch.Size = New Size(52, 19)
        lblSearch.TabIndex = 1
        lblSearch.Text = "Search:"
        ' 
        ' lblHeaderTitle
        ' 
        lblHeaderTitle.AutoSize = True
        lblHeaderTitle.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblHeaderTitle.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        lblHeaderTitle.Location = New Point(5, 22)
        lblHeaderTitle.Name = "lblHeaderTitle"
        lblHeaderTitle.Size = New Size(126, 30)
        lblHeaderTitle.TabIndex = 0
        lblHeaderTitle.Text = "Dashboard"
        ' 
        ' lblUserProfile
        ' 
        lblUserProfile.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        lblUserProfile.ForeColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        lblUserProfile.Location = New Point(1094, 19)
        lblUserProfile.Name = "lblUserProfile"
        lblUserProfile.Size = New Size(332, 17)
        lblUserProfile.TabIndex = 11
        lblUserProfile.Text = "Welcome"
        lblUserProfile.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' pnlContent
        ' 
        pnlContent.Controls.Add(dgvVisits)
        pnlContent.Controls.Add(pnlRightSidebar)
        pnlContent.Controls.Add(pnlCharts)
        pnlContent.Controls.Add(pnlStats)
        pnlContent.Dock = DockStyle.Fill
        pnlContent.Location = New Point(220, 70)
        pnlContent.Margin = New Padding(3, 2, 3, 2)
        pnlContent.Name = "pnlContent"
        pnlContent.Padding = New Padding(20, 20, 20, 20)
        pnlContent.Size = New Size(1444, 705)
        pnlContent.TabIndex = 2
        ' 
        ' dgvVisits
        ' 
        dgvVisits.AllowUserToAddRows = False
        dgvVisits.AllowUserToDeleteRows = False
        dgvVisits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvVisits.BackgroundColor = Color.White
        dgvVisits.BorderStyle = BorderStyle.None
        dgvVisits.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvVisits.Dock = DockStyle.Fill
        dgvVisits.Location = New Point(20, 372)
        dgvVisits.Margin = New Padding(3, 2, 3, 2)
        dgvVisits.Name = "dgvVisits"
        dgvVisits.ReadOnly = True
        dgvVisits.RowHeadersVisible = False
        dgvVisits.RowHeadersWidth = 51
        dgvVisits.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvVisits.Size = New Size(1115, 313)
        dgvVisits.TabIndex = 0
        ' 
        ' pnlRightSidebar
        ' 
        pnlRightSidebar.Controls.Add(pnlTodayAppointments)
        pnlRightSidebar.Controls.Add(pnlRecent)
        pnlRightSidebar.Dock = DockStyle.Right
        pnlRightSidebar.Location = New Point(1135, 372)
        pnlRightSidebar.Margin = New Padding(3, 2, 3, 2)
        pnlRightSidebar.Name = "pnlRightSidebar"
        pnlRightSidebar.Size = New Size(289, 313)
        pnlRightSidebar.TabIndex = 1
        ' 
        ' pnlTodayAppointments
        ' 
        pnlTodayAppointments.AutoScroll = True
        pnlTodayAppointments.BackColor = Color.WhiteSmoke
        pnlTodayAppointments.Dock = DockStyle.Fill
        pnlTodayAppointments.FlowDirection = FlowDirection.TopDown
        pnlTodayAppointments.Location = New Point(0, 311)
        pnlTodayAppointments.Margin = New Padding(3, 2, 3, 2)
        pnlTodayAppointments.Name = "pnlTodayAppointments"
        pnlTodayAppointments.Padding = New Padding(9, 8, 9, 8)
        pnlTodayAppointments.Size = New Size(289, 2)
        pnlTodayAppointments.TabIndex = 0
        pnlTodayAppointments.WrapContents = False
        ' 
        ' pnlRecent
        ' 
        pnlRecent.AutoScroll = True
        pnlRecent.BackColor = Color.White
        pnlRecent.Dock = DockStyle.Top
        pnlRecent.FlowDirection = FlowDirection.TopDown
        pnlRecent.Location = New Point(0, 0)
        pnlRecent.Margin = New Padding(3, 2, 3, 2)
        pnlRecent.Name = "pnlRecent"
        pnlRecent.Padding = New Padding(9, 8, 9, 8)
        pnlRecent.Size = New Size(289, 311)
        pnlRecent.TabIndex = 1
        pnlRecent.WrapContents = False
        ' 
        ' pnlCharts
        ' 
        pnlCharts.Dock = DockStyle.Top
        pnlCharts.Location = New Point(20, 110)
        pnlCharts.Margin = New Padding(3, 2, 3, 2)
        pnlCharts.Name = "pnlCharts"
        pnlCharts.Padding = New Padding(0, 0, 0, 15)
        pnlCharts.Size = New Size(1404, 262)
        pnlCharts.TabIndex = 2
        ' 
        ' pnlStats
        ' 
        pnlStats.Controls.Add(pnlStatAlerts)
        pnlStats.Controls.Add(pnlStatVisits)
        pnlStats.Controls.Add(pnlStatStudents)
        pnlStats.Controls.Add(pnlStatEmployees)
        pnlStats.Dock = DockStyle.Top
        pnlStats.Location = New Point(20, 20)
        pnlStats.Margin = New Padding(3, 2, 3, 2)
        pnlStats.Name = "pnlStats"
        pnlStats.Padding = New Padding(0, 0, 0, 15)
        pnlStats.Size = New Size(1404, 90)
        pnlStats.TabIndex = 1
        ' 
        ' pnlStatAlerts
        ' 
        pnlStatAlerts.BackColor = Color.White
        pnlStatAlerts.Controls.Add(lblStatAlertsTitle)
        pnlStatAlerts.Controls.Add(lblStatAlertsCount)
        pnlStatAlerts.Dock = DockStyle.Left
        pnlStatAlerts.Location = New Point(684, 0)
        pnlStatAlerts.Margin = New Padding(18, 0, 0, 0)
        pnlStatAlerts.Name = "pnlStatAlerts"
        pnlStatAlerts.Size = New Size(228, 75)
        pnlStatAlerts.TabIndex = 3
        ' 
        ' lblStatAlertsTitle
        ' 
        lblStatAlertsTitle.Dock = DockStyle.Top
        lblStatAlertsTitle.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblStatAlertsTitle.ForeColor = Color.Red
        lblStatAlertsTitle.Location = New Point(0, 0)
        lblStatAlertsTitle.Name = "lblStatAlertsTitle"
        lblStatAlertsTitle.Size = New Size(228, 22)
        lblStatAlertsTitle.TabIndex = 0
        lblStatAlertsTitle.Text = "LOW INVENTORY ALERTS"
        lblStatAlertsTitle.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblStatAlertsCount
        ' 
        lblStatAlertsCount.Dock = DockStyle.Fill
        lblStatAlertsCount.Font = New Font("Segoe UI", 32F, FontStyle.Bold)
        lblStatAlertsCount.ForeColor = Color.Red
        lblStatAlertsCount.Location = New Point(0, 0)
        lblStatAlertsCount.Name = "lblStatAlertsCount"
        lblStatAlertsCount.Size = New Size(228, 75)
        lblStatAlertsCount.TabIndex = 1
        lblStatAlertsCount.Text = "0"
        lblStatAlertsCount.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' pnlStatVisits
        ' 
        pnlStatVisits.BackColor = Color.White
        pnlStatVisits.Controls.Add(lblStatVisitsTitle)
        pnlStatVisits.Controls.Add(lblStatVisitsCount)
        pnlStatVisits.Dock = DockStyle.Left
        pnlStatVisits.Location = New Point(456, 0)
        pnlStatVisits.Margin = New Padding(3, 2, 3, 2)
        pnlStatVisits.Name = "pnlStatVisits"
        pnlStatVisits.Size = New Size(228, 75)
        pnlStatVisits.TabIndex = 0
        ' 
        ' lblStatVisitsTitle
        ' 
        lblStatVisitsTitle.Dock = DockStyle.Top
        lblStatVisitsTitle.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblStatVisitsTitle.ForeColor = Color.Gray
        lblStatVisitsTitle.Location = New Point(0, 0)
        lblStatVisitsTitle.Name = "lblStatVisitsTitle"
        lblStatVisitsTitle.Size = New Size(228, 22)
        lblStatVisitsTitle.TabIndex = 0
        lblStatVisitsTitle.Text = "TOTAL VISITS TODAY"
        lblStatVisitsTitle.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblStatVisitsCount
        ' 
        lblStatVisitsCount.Dock = DockStyle.Fill
        lblStatVisitsCount.Font = New Font("Segoe UI", 32F, FontStyle.Bold)
        lblStatVisitsCount.ForeColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        lblStatVisitsCount.Location = New Point(0, 0)
        lblStatVisitsCount.Name = "lblStatVisitsCount"
        lblStatVisitsCount.Size = New Size(228, 75)
        lblStatVisitsCount.TabIndex = 1
        lblStatVisitsCount.Text = "0"
        lblStatVisitsCount.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' pnlStatStudents
        ' 
        pnlStatStudents.BackColor = Color.White
        pnlStatStudents.Controls.Add(lblStatStudentsTitle)
        pnlStatStudents.Controls.Add(lblStatStudentsCount)
        pnlStatStudents.Dock = DockStyle.Left
        pnlStatStudents.Location = New Point(228, 0)
        pnlStatStudents.Margin = New Padding(18, 0, 0, 0)
        pnlStatStudents.Name = "pnlStatStudents"
        pnlStatStudents.Size = New Size(228, 75)
        pnlStatStudents.TabIndex = 1
        ' 
        ' lblStatStudentsTitle
        ' 
        lblStatStudentsTitle.Dock = DockStyle.Top
        lblStatStudentsTitle.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblStatStudentsTitle.ForeColor = Color.Gray
        lblStatStudentsTitle.Location = New Point(0, 0)
        lblStatStudentsTitle.Name = "lblStatStudentsTitle"
        lblStatStudentsTitle.Size = New Size(228, 22)
        lblStatStudentsTitle.TabIndex = 0
        lblStatStudentsTitle.Text = "TOTAL STUDENT VISITS"
        lblStatStudentsTitle.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblStatStudentsCount
        ' 
        lblStatStudentsCount.Dock = DockStyle.Fill
        lblStatStudentsCount.Font = New Font("Segoe UI", 32F, FontStyle.Bold)
        lblStatStudentsCount.ForeColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        lblStatStudentsCount.Location = New Point(0, 0)
        lblStatStudentsCount.Name = "lblStatStudentsCount"
        lblStatStudentsCount.Size = New Size(228, 75)
        lblStatStudentsCount.TabIndex = 1
        lblStatStudentsCount.Text = "0"
        lblStatStudentsCount.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' pnlStatEmployees
        ' 
        pnlStatEmployees.BackColor = Color.White
        pnlStatEmployees.Controls.Add(lblStatEmployeesTitle)
        pnlStatEmployees.Controls.Add(lblStatEmployeesCount)
        pnlStatEmployees.Dock = DockStyle.Left
        pnlStatEmployees.Location = New Point(0, 0)
        pnlStatEmployees.Margin = New Padding(18, 0, 0, 0)
        pnlStatEmployees.Name = "pnlStatEmployees"
        pnlStatEmployees.Size = New Size(228, 75)
        pnlStatEmployees.TabIndex = 2
        ' 
        ' lblStatEmployeesTitle
        ' 
        lblStatEmployeesTitle.Dock = DockStyle.Top
        lblStatEmployeesTitle.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        lblStatEmployeesTitle.ForeColor = Color.Gray
        lblStatEmployeesTitle.Location = New Point(0, 0)
        lblStatEmployeesTitle.Name = "lblStatEmployeesTitle"
        lblStatEmployeesTitle.Size = New Size(228, 22)
        lblStatEmployeesTitle.TabIndex = 0
        lblStatEmployeesTitle.Text = "TOTAL EMPLOYEE VISITS"
        lblStatEmployeesTitle.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblStatEmployeesCount
        ' 
        lblStatEmployeesCount.Dock = DockStyle.Fill
        lblStatEmployeesCount.Font = New Font("Segoe UI", 32F, FontStyle.Bold)
        lblStatEmployeesCount.ForeColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        lblStatEmployeesCount.Location = New Point(0, 0)
        lblStatEmployeesCount.Name = "lblStatEmployeesCount"
        lblStatEmployeesCount.Size = New Size(228, 75)
        lblStatEmployeesCount.TabIndex = 1
        lblStatEmployeesCount.Text = "0"
        lblStatEmployeesCount.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.WhiteSmoke
        ClientSize = New Size(1664, 775)
        Controls.Add(pnlContent)
        Controls.Add(pnlHeader)
        Controls.Add(pnlSidebar)
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "PUP Lopez Medical and Dental Services"
        pnlSidebar.ResumeLayout(False)
        pnlSidebarHeader.ResumeLayout(False)
        CType(picSidebarLogo, ComponentModel.ISupportInitialize).EndInit()
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        pnlContent.ResumeLayout(False)
        CType(dgvVisits, ComponentModel.ISupportInitialize).EndInit()
        pnlRightSidebar.ResumeLayout(False)
        pnlStats.ResumeLayout(False)
        pnlStatAlerts.ResumeLayout(False)
        pnlStatVisits.ResumeLayout(False)
        pnlStatStudents.ResumeLayout(False)
        pnlStatEmployees.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

    Friend WithEvents pnlSidebar As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnDashboard As System.Windows.Forms.Button
    Friend WithEvents btnAddVisit As System.Windows.Forms.Button
    Friend WithEvents btnInventory As System.Windows.Forms.Button
    Friend WithEvents btnAppointments As System.Windows.Forms.Button
    Friend WithEvents btnAbout As System.Windows.Forms.Button
    Friend WithEvents btnSettings As System.Windows.Forms.Button
    Friend WithEvents btnManageUsers As System.Windows.Forms.Button
    Friend WithEvents btnLogout As System.Windows.Forms.Button
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeaderTitle As System.Windows.Forms.Label
    Friend WithEvents lblUserProfile As System.Windows.Forms.Label
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnHistory As System.Windows.Forms.Button
    Friend WithEvents btnCheckOut As System.Windows.Forms.Button
    Friend WithEvents chkQueueOnly As System.Windows.Forms.CheckBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents pnlContent As System.Windows.Forms.Panel
    Friend WithEvents pnlCharts As System.Windows.Forms.Panel
    Friend WithEvents pnlRightSidebar As System.Windows.Forms.Panel
    Friend WithEvents pnlRecent As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlTodayAppointments As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlStats As System.Windows.Forms.Panel
    Friend WithEvents pnlStatAlerts As System.Windows.Forms.Panel
    Friend WithEvents lblStatAlertsTitle As System.Windows.Forms.Label
    Friend WithEvents lblStatAlertsCount As System.Windows.Forms.Label
    Friend WithEvents pnlStatVisits As System.Windows.Forms.Panel
    Friend WithEvents lblStatVisitsTitle As System.Windows.Forms.Label
    Friend WithEvents lblStatVisitsCount As System.Windows.Forms.Label
    Friend WithEvents pnlStatStudents As System.Windows.Forms.Panel
    Friend WithEvents lblStatStudentsTitle As System.Windows.Forms.Label
    Friend WithEvents lblStatStudentsCount As System.Windows.Forms.Label
    Friend WithEvents pnlStatEmployees As System.Windows.Forms.Panel
    Friend WithEvents lblStatEmployeesTitle As System.Windows.Forms.Label
    Friend WithEvents lblStatEmployeesCount As System.Windows.Forms.Label
    Friend WithEvents dgvVisits As System.Windows.Forms.DataGridView
    Friend WithEvents picSidebarLogo As System.Windows.Forms.PictureBox
    Friend WithEvents pnlSidebarHeader As System.Windows.Forms.Panel
End Class
