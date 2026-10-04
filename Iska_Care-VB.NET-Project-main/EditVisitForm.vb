Imports System.Windows.Forms

Public Class EditVisitForm
    Inherits VisitDetailsForm

    Public Sub New(existingRecord As VisitRecord)
        MyBase.New(existingRecord, "")
        Me.Text = "Edit Visit Record"
        Me.btnSave.Text = "Update Record"
    End Sub
End Class
