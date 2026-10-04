Imports System.Windows.Forms
Imports Microsoft.Data.Sqlite

Public Class SettingsForm
    ' Eto yung form para sa database backup at restore. May dalawang button ito: "Backup Database" at "Restore Database".
    ' Kapag nag-click ang user sa "Backup Database" button,
    ' magpapakita ito ng SaveFileDialog para pumili ng location at filename para sa backup file.

    ' pano ngaba nagana ang database backup at restore? Eto yung mga steps:
    ' 1. Kapag nag-click ang user sa "Backup Database" button,
    ' magpapakita ito ng SaveFileDialog para pumili ng location at filename para sa backup file.
    ' 2. Kapag pumili ang user ng location at filename at nag-click ng "Save",
    ' gagamitin natin ang System.IO.File.Copy para i-export ang database file sa napiling location.
    ' 3. Kapag nag-click ang user sa "Restore Database" button, magpapakita ito ng OpenFileDialog para pumili ng backup file na i-restore.
    ' 4. Kapag pumili ang user ng backup file at nag-click ng "Open",
    ' ipapakita nito ang confirmation dialog para i-confirm ang restore action.
    ' 5. Kapag ni-confirm ng user ang restore action, lilinisin natin ang connection pools at gagamitin ang System.IO.File.Copy
    ' para i-overwrite ang active database file gamit ang napiling backup file.
    ' 6. Pagkatapos ng backup o restore action, ipapakita nito ang success o error message depende sa resulta ng operation.
    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        Using sfd As New SaveFileDialog()
            sfd.Filter = "SQLite Database File|*.db;*.sqlite"
            sfd.Title = "Save Database Backup"
            sfd.FileName = "ClinicBackup_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".db"

            If sfd.ShowDialog() = DialogResult.OK Then
                Try
                    ' Copy database file to the selected backup path
                    System.IO.File.Copy(DatabaseHelper.DatabasePath, sfd.FileName, True)
                    MessageBox.Show("Backup successfully saved to " & sfd.FileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show("Backup failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    Private Sub btnRestore_Click(sender As Object, e As EventArgs) Handles btnRestore.Click
        Using ofd As New OpenFileDialog()
            ofd.Filter = "SQLite Database File|*.db;*.sqlite"
            ofd.Title = "Select Database Backup File"

            If ofd.ShowDialog() = DialogResult.OK Then
                If MessageBox.Show("WARNING: Restoring a backup will overwrite all current data. Are you sure you want to continue?", "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                    Try
                        ' Clear all connection pools to make sure SQLite connection locks are released
                        SqliteConnection.ClearAllPools()

                        ' Ensure parent directory of target database exists
                        Dim targetDir = System.IO.Path.GetDirectoryName(DatabaseHelper.DatabasePath)
                        If Not System.IO.Directory.Exists(targetDir) Then
                            System.IO.Directory.CreateDirectory(targetDir)
                        End If

                        ' Overwrite the current active database with the backup file
                        System.IO.File.Copy(ofd.FileName, DatabaseHelper.DatabasePath, True)
                        MessageBox.Show("Database successfully restored! Please restart the application.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As Exception
                        MessageBox.Show("Restore failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End If
        End Using
    End Sub

    Private Sub SettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
