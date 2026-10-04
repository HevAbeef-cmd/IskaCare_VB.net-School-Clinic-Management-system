<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class InventoryForm
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
        pnlHeader = New Panel()
        lblTitle = New Label()
        pnlAdd = New Panel()
        lblMedicine = New Label()
        txtMedicine = New TextBox()
        lblQuantity = New Label()
        numQuantity = New NumericUpDown()
        btnAdd = New Button()
        btnEdit = New Button()
        btnDelete = New Button()
        btnClear = New Button()
        dgvInventory = New DataGridView()
        pnlHeader.SuspendLayout()
        pnlAdd.SuspendLayout()
        CType(numQuantity, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvInventory, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' pnlHeader
        ' 
        pnlHeader.BackColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        pnlHeader.Controls.Add(lblTitle)
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Location = New Point(0, 0)
        pnlHeader.Name = "pnlHeader"
        pnlHeader.Size = New Size(1107, 60)
        pnlHeader.TabIndex = 2
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(20, 15)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(268, 37)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Medicine Inventory"
        ' 
        ' 
        ' pnlAdd
        ' 
        pnlAdd.BackColor = Color.White
        pnlAdd.Controls.Add(lblMedicine)
        pnlAdd.Controls.Add(txtMedicine)
        pnlAdd.Controls.Add(lblQuantity)
        pnlAdd.Controls.Add(numQuantity)
        pnlAdd.Controls.Add(btnAdd)
        pnlAdd.Controls.Add(btnEdit)
        pnlAdd.Controls.Add(btnDelete)
        pnlAdd.Controls.Add(btnClear)
        pnlAdd.Dock = DockStyle.Top
        pnlAdd.Location = New Point(0, 60)
        pnlAdd.Name = "pnlAdd"
        pnlAdd.Size = New Size(1107, 70)
        pnlAdd.TabIndex = 1
        ' 
        ' lblMedicine
        ' 
        lblMedicine.AutoSize = True
        lblMedicine.Location = New Point(43, 24)
        lblMedicine.Name = "lblMedicine"
        lblMedicine.Size = New Size(117, 20)
        lblMedicine.TabIndex = 0
        lblMedicine.Text = "Medicine Name:"
        ' 
        ' txtMedicine
        ' 
        txtMedicine.Location = New Point(166, 21)
        txtMedicine.Name = "txtMedicine"
        txtMedicine.Size = New Size(200, 27)
        txtMedicine.TabIndex = 1
        ' 
        ' lblQuantity
        ' 
        lblQuantity.AutoSize = True
        lblQuantity.Location = New Point(400, 25)
        lblQuantity.Name = "lblQuantity"
        lblQuantity.Size = New Size(100, 20)
        lblQuantity.TabIndex = 2
        lblQuantity.Text = "Add Quantity:"
        ' 
        ' numQuantity
        ' 
        numQuantity.Location = New Point(510, 23)
        numQuantity.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        numQuantity.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        numQuantity.Name = "numQuantity"
        numQuantity.Size = New Size(80, 27)
        numQuantity.TabIndex = 3
        numQuantity.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' btnAdd
        ' 
        btnAdd.BackColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.ForeColor = Color.White
        btnAdd.Location = New Point(610, 20)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New Size(110, 30)
        btnAdd.TabIndex = 4
        btnAdd.Text = "Add Stock"
        btnAdd.UseVisualStyleBackColor = False
        ' 
        ' btnEdit
        ' 
        btnEdit.BackColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnEdit.FlatStyle = FlatStyle.Flat
        btnEdit.ForeColor = Color.White
        btnEdit.Location = New Point(730, 20)
        btnEdit.Name = "btnEdit"
        btnEdit.Size = New Size(110, 30)
        btnEdit.TabIndex = 6
        btnEdit.Text = "Update Item"
        btnEdit.UseVisualStyleBackColor = False
        ' 
        ' btnDelete
        ' 
        btnDelete.BackColor = Color.White
        btnDelete.FlatStyle = FlatStyle.Flat
        btnDelete.ForeColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnDelete.Location = New Point(850, 20)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(120, 30)
        btnDelete.TabIndex = 5
        btnDelete.Text = "Delete Selected"
        btnDelete.UseVisualStyleBackColor = False
        ' 
        ' btnClear
        ' 
        btnClear.BackColor = Color.White
        btnClear.FlatStyle = FlatStyle.Flat
        btnClear.ForeColor = Color.FromArgb(CByte(128), CByte(0), CByte(0))
        btnClear.Location = New Point(980, 20)
        btnClear.Name = "btnClear"
        btnClear.Size = New Size(110, 30)
        btnClear.TabIndex = 7
        btnClear.Text = "Clear Selection"
        btnClear.UseVisualStyleBackColor = False
        ' 
        ' dgvInventory
        ' 
        dgvInventory.AllowUserToAddRows = False
        dgvInventory.AllowUserToDeleteRows = False
        dgvInventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvInventory.BackgroundColor = Color.WhiteSmoke
        dgvInventory.ColumnHeadersHeight = 29
        dgvInventory.Dock = DockStyle.Fill
        dgvInventory.Location = New Point(0, 130)
        dgvInventory.Name = "dgvInventory"
        dgvInventory.ReadOnly = True
        dgvInventory.RowHeadersWidth = 51
        dgvInventory.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvInventory.Size = New Size(1107, 599)
        dgvInventory.TabIndex = 0
        ' 
        ' InventoryForm
        ' 
        ClientSize = New Size(1107, 729)
        Controls.Add(dgvInventory)
        Controls.Add(pnlAdd)
        Controls.Add(pnlHeader)
        Name = "InventoryForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Inventory Management"
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        pnlAdd.ResumeLayout(False)
        pnlAdd.PerformLayout()
        CType(numQuantity, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvInventory, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlAdd As System.Windows.Forms.Panel
    Friend WithEvents lblMedicine As System.Windows.Forms.Label
    Friend WithEvents txtMedicine As System.Windows.Forms.TextBox
    Friend WithEvents lblQuantity As System.Windows.Forms.Label
    Friend WithEvents numQuantity As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents dgvInventory As System.Windows.Forms.DataGridView
End Class
