Imports System.Windows.Forms
Imports System.Drawing

Public Class InventoryForm
    Private dbHelper As New DatabaseHelper()
    Private selectedItemId As Integer? = Nothing

    Public Sub New(Optional isTutorial As Boolean = False)
        InitializeComponent()
        If isTutorial Then
            EnableTutorialMode()
        End If
    End Sub

    Private Sub InventoryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadInventory()
    End Sub

    Private Sub LoadInventory()
        Try
            Dim inventory = dbHelper.GetInventory()
            dgvInventory.DataSource = inventory
            dgvInventory.ClearSelection()
        Catch ex As Exception
            MessageBox.Show("Error loading inventory: " & ex.Message)
        End Try
    End Sub

    Private Sub dgvInventory_SelectionChanged(sender As Object, e As EventArgs) Handles dgvInventory.SelectionChanged
        If dgvInventory.SelectedRows.Count = 1 Then
            Dim selectedItem As DatabaseHelper.InventoryItem = CType(dgvInventory.SelectedRows(0).DataBoundItem, DatabaseHelper.InventoryItem)
            If selectedItem IsNot Nothing Then
                selectedItemId = selectedItem.Id
                txtMedicine.Text = selectedItem.MedicineName
                numQuantity.Value = selectedItem.StockQuantity
            End If
        Else
            ClearSelectionState()
        End If
    End Sub

    Private Sub ClearSelectionState()
        selectedItemId = Nothing
        txtMedicine.Clear()
        numQuantity.Value = 1
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If btnAdd.Text = "Close Demo" Then
            Me.Close()
            Return
        End If

        Dim medicineName As String = txtMedicine.Text.Trim()
        Dim quantity As Integer = Convert.ToInt32(numQuantity.Value)

        If String.IsNullOrWhiteSpace(medicineName) Then
            MessageBox.Show("Please enter a medicine name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            dbHelper.AddOrUpdateInventory(medicineName, quantity)
            MessageBox.Show("Inventory item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            dgvInventory.ClearSelection()
            ClearSelectionState()
            LoadInventory()
        Catch ex As Exception
            MessageBox.Show("Error saving to inventory: " & ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim medicineName As String = txtMedicine.Text.Trim()
        Dim quantity As Integer = Convert.ToInt32(numQuantity.Value)

        If Not selectedItemId.HasValue Then
            MessageBox.Show("Please select a medicine from the list to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(medicineName) Then
            MessageBox.Show("Please enter a medicine name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            If dbHelper.UpdateInventoryItem(selectedItemId.Value, medicineName, quantity) Then
                MessageBox.Show("Inventory item updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                dgvInventory.ClearSelection()
                ClearSelectionState()
                LoadInventory()
            Else
                MessageBox.Show("A medicine with that name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error updating inventory: " & ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        dgvInventory.ClearSelection()
        ClearSelectionState()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvInventory.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an item to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedItem As DatabaseHelper.InventoryItem = CType(dgvInventory.SelectedRows(0).DataBoundItem, DatabaseHelper.InventoryItem)
        If MessageBox.Show($"Are you sure you want to delete {selectedItem.MedicineName} from inventory?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                dbHelper.DeleteInventoryItem(selectedItem.Id)
                dgvInventory.ClearSelection()
                ClearSelectionState()
                LoadInventory()
            Catch ex As Exception
                MessageBox.Show("Error deleting item: " & ex.Message)
            End Try
        End If
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
        lblTitle.Text = "💡 TUTORIAL DEMO: This is the Inventory. Select medicines, view stock level, or type medicine and quantity to update!"
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
        
        ' In tutorial mode, intercept database saving
        btnAdd.Text = "Close Demo"
        btnEdit.Visible = False
        btnDelete.Visible = False
        btnClear.Visible = False
        
        ' Pre-populate a demo selection
        txtMedicine.Text = "Amoxicillin"
        numQuantity.Value = 50
    End Sub
End Class
