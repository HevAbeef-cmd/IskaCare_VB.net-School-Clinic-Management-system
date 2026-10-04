Imports System.Windows.Forms

Public Class AddVisitForm
    Inherits VisitDetailsForm

    Public Sub New(Optional defaultAttending As String = "")
        MyBase.New(Nothing, defaultAttending)
        Me.Text = "Add New Visit Record"
        Me.btnSave.Text = "Save Record"
    End Sub
End Class
