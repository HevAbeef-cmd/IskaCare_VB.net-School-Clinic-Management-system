Imports Microsoft.Data.Sqlite
Imports Dapper

Public Class DatabaseHelper
    ' Database storage path in AppData\Local\IskaCare\school_clinic.db
    Public Shared ReadOnly DatabasePath As String = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "IskaCare", "school_clinic.db")
    Private ReadOnly ConnectionString As String = $"Data Source={DatabasePath};"

    Public Sub New()
        InitializeDatabase()
    End Sub

    Public Class InventoryItem
        Public Property Id As Integer
        Public Property MedicineName As String
        Public Property StockQuantity As Integer
    End Class

    Private Sub InitializeDatabase()
        ' Ensure the local application directory exists
        Dim directoryPath As String = System.IO.Path.GetDirectoryName(DatabasePath)
        If Not System.IO.Directory.Exists(directoryPath) Then
            System.IO.Directory.CreateDirectory(directoryPath)
        End If

        ' In SQLite, opening a connection to the file automatically creates the database if it doesn't exist.
        Using connection As New SqliteConnection(ConnectionString)
            connection.Open()

            ' Create table Visits if it doesn't exist
            Dim createVisitsQuery As String = "
                CREATE TABLE IF NOT EXISTS Visits (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    PatientType VARCHAR(50),
                    PatientName VARCHAR(100),
                    Age INT,
                    Height DOUBLE,
                    Weight DOUBLE,
                    Course VARCHAR(100),
                    Job VARCHAR(100),
                    ContactNumber VARCHAR(50),
                    ReasonForVisit VARCHAR(200),
                    Sickness VARCHAR(200),
                    Injury VARCHAR(200),
                    Prescription VARCHAR(200),
                    Gender VARCHAR(50),
                    StudentID VARCHAR(100),
                    Year VARCHAR(50),
                    DoctorAssigned VARCHAR(100),
                    TimeIn DATETIME,
                    TimeOut DATETIME NULL
                )"
            connection.Execute(createVisitsQuery)

            Dim createInventoryQuery As String = "
                CREATE TABLE IF NOT EXISTS Inventory (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    MedicineName VARCHAR(100) UNIQUE,
                    StockQuantity INT
                )"
            connection.Execute(createInventoryQuery)

            Dim createUsersQuery As String = "
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username VARCHAR(50) UNIQUE,
                    Password VARCHAR(255),
                    Role VARCHAR(50)
                )"
            connection.Execute(createUsersQuery)

            ' Seed default admin/staff users if not exists
            Dim count = connection.ExecuteScalar(Of Integer)("SELECT COUNT(*) FROM Users")
            If count = 0 Then
                connection.Execute("INSERT INTO Users (Username, Password, Role) VALUES ('admin', 'admin123', 'Admin')")
                connection.Execute("INSERT INTO Users (Username, Password, Role) VALUES ('nurse', 'nurse123', 'Nurse')")
            Else
                ' Migrate any old 'Staff' accounts to 'Nurse'
                connection.Execute("UPDATE Users SET Role = 'Nurse' WHERE Role = 'Staff'")
            End If

            Dim createAppointmentsQuery As String = "
                CREATE TABLE IF NOT EXISTS Appointments (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    PatientName VARCHAR(100),
                    AppointmentDate DATETIME,
                    Reason VARCHAR(200)
                )"
            connection.Execute(createAppointmentsQuery)

            Dim createConfigQuery As String = "
                CREATE TABLE IF NOT EXISTS Config (
                    Key VARCHAR(50) PRIMARY KEY,
                    Value VARCHAR(200)
                )"
            connection.Execute(createConfigQuery)
        End Using
    End Sub

    ' --- CONFIG METHODS ---
    Public Function GetConfig(key As String, defaultValue As String) As String
        Using connection As New SqliteConnection(ConnectionString)
            Try
                Dim val = connection.ExecuteScalar(Of String)("SELECT Value FROM Config WHERE Key = @Key", New With {.Key = key})
                If val Is Nothing Then Return defaultValue
                Return val
            Catch ex As Exception
                Return defaultValue
            End Try
        End Using
    End Function

    Public Sub SetConfig(key As String, value As String)
        Using connection As New SqliteConnection(ConnectionString)
            Try
                Dim query As String = "REPLACE INTO Config (Key, Value) VALUES (@Key, @Val)"
                connection.Execute(query, New With {.Key = key, .Val = value})
            Catch ex As Exception
                ' Silent failure to avoid breaking app if DB has issues
            End Try
        End Using
    End Sub

    ' --- AUTHENTICATION ---
    Public Function AuthenticateUser(username As String, password As String) As String
        Using connection As New SqliteConnection(ConnectionString)
            Dim role = connection.ExecuteScalar(Of String)("SELECT Role FROM Users WHERE Username = @User AND Password = @Pass", New With {.User = username, .Pass = password})
            Return role
        End Using
    End Function

    Public Function CreateUser(username As String, password As String, role As String) As Boolean
        Using connection As New SqliteConnection(ConnectionString)
            Try
                Dim query As String = "INSERT INTO Users (Username, Password, Role) VALUES (@User, @Pass, @Role)"
                connection.Execute(query, New With {.User = username, .Pass = password, .Role = role})
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Using
    End Function

    ' --- INVENTORY METHODS ---
    Public Function GetInventory() As System.Collections.Generic.List(Of InventoryItem)
        Using connection As New SqliteConnection(ConnectionString)
            Return connection.Query(Of InventoryItem)("SELECT * FROM Inventory ORDER BY MedicineName").AsList()
        End Using
    End Function

    Public Sub AddOrUpdateInventory(itemName As String, quantity As Integer)
        Using connection As New SqliteConnection(ConnectionString)
            ' Check case-insensitively using COLLATE NOCASE
            Dim existing = connection.QueryFirstOrDefault(Of InventoryItem)(
                "SELECT * FROM Inventory WHERE MedicineName = @Name COLLATE NOCASE", 
                New With {.Name = itemName})
            
            If existing IsNot Nothing Then
                connection.Execute(
                    "UPDATE Inventory SET StockQuantity = StockQuantity + @Qty WHERE Id = @Id", 
                    New With {.Qty = quantity, .Id = existing.Id})
            Else
                connection.Execute(
                    "INSERT INTO Inventory (MedicineName, StockQuantity) VALUES (@Name, @Qty)", 
                    New With {.Name = itemName, .Qty = quantity})
            End If
        End Using
    End Sub

    Public Function UpdateInventoryItem(id As Integer, newName As String, newQuantity As Integer) As Boolean
        Using connection As New SqliteConnection(ConnectionString)
            ' Check case-insensitively if name is already taken by another item
            Dim duplicateCount As Integer = connection.ExecuteScalar(Of Integer)(
                "SELECT COUNT(*) FROM Inventory WHERE MedicineName = @Name COLLATE NOCASE AND Id <> @Id", 
                New With {.Name = newName, .Id = id})
            If duplicateCount > 0 Then
                Return False
            End If

            Dim query As String = "UPDATE Inventory SET MedicineName = @Name, StockQuantity = @Qty WHERE Id = @Id"
            connection.Execute(query, New With {.Name = newName, .Qty = newQuantity, .Id = id})
            Return True
        End Using
    End Function

    Public Sub DeductInventory(itemName As String, quantity As Integer)
        Using connection As New SqliteConnection(ConnectionString)
            Dim query As String = "
                UPDATE Inventory 
                SET StockQuantity = StockQuantity - @Qty 
                WHERE MedicineName = @Name AND StockQuantity >= @Qty"
            connection.Execute(query, New With {.Name = itemName, .Qty = quantity})
        End Using
    End Sub
    
    Public Sub DeleteInventoryItem(id As Integer)
        Using connection As New SqliteConnection(ConnectionString)
            connection.Execute("DELETE FROM Inventory WHERE Id = @Id", New With { Key .Id = id })
        End Using
    End Sub

    Public Function GetDatabaseConnectionString() As String
        Return ConnectionString
    End Function

    Public Function GetAllVisits() As System.Collections.Generic.List(Of VisitRecord)
        Using connection As New SqliteConnection(ConnectionString)
            Return connection.Query(Of VisitRecord)("SELECT * FROM Visits ORDER BY TimeIn DESC").AsList()
        End Using
    End Function

    Public Sub InsertVisit(visit As VisitRecord)
        Using connection As New SqliteConnection(ConnectionString)
            Dim query As String = "
                INSERT INTO Visits (PatientType, PatientName, Age, Height, Weight, Course, Job, ContactNumber, ReasonForVisit, Sickness, Injury, Prescription, Gender, StudentID, Year, DoctorAssigned, TimeIn, TimeOut)
                VALUES (@PatientType, @PatientName, @Age, @Height, @Weight, @Course, @Job, @ContactNumber, @ReasonForVisit, @Sickness, @Injury, @Prescription, @Gender, @StudentID, @Year, @DoctorAssigned, @TimeIn, @TimeOut)"
            connection.Execute(query, visit)
        End Using
    End Sub

    Public Sub UpdateVisit(visit As VisitRecord)
        Using connection As New SqliteConnection(ConnectionString)
            Dim query As String = "
                UPDATE Visits SET 
                    PatientType = @PatientType,
                    PatientName = @PatientName,
                    Age = @Age,
                    Height = @Height,
                    Weight = @Weight,
                    Course = @Course,
                    Job = @Job,
                    ContactNumber = @ContactNumber,
                    ReasonForVisit = @ReasonForVisit,
                    Sickness = @Sickness,
                    Injury = @Injury,
                    Prescription = @Prescription,
                    Gender = @Gender,
                    StudentID = @StudentID,
                    Year = @Year,
                    DoctorAssigned = @DoctorAssigned,
                    TimeIn = @TimeIn,
                    TimeOut = @TimeOut
                WHERE Id = @Id"
            connection.Execute(query, visit)
        End Using
    End Sub

    Public Sub DeleteVisit(id As Integer)
        Using connection As New SqliteConnection(ConnectionString)
            connection.Execute("DELETE FROM Visits WHERE Id = @Id", New With { Key .Id = id })
        End Using
    End Sub

    Public Function GetTotalVisitsToday() As Integer
        Using connection As New SqliteConnection(ConnectionString)
            ' SQLite dialect for today's visits checking local time
            Return connection.ExecuteScalar(Of Integer)("SELECT COUNT(*) FROM Visits WHERE date(TimeIn) = date('now', 'localtime')")
        End Using
    End Function

    Public Function GetTotalStudents() As Integer
        Using connection As New SqliteConnection(ConnectionString)
            Return connection.ExecuteScalar(Of Integer)("SELECT COUNT(*) FROM Visits WHERE PatientType = 'Student'")
        End Using
    End Function

    Public Function GetTotalEmployees() As Integer
        Using connection As New SqliteConnection(ConnectionString)
            Return connection.ExecuteScalar(Of Integer)("SELECT COUNT(*) FROM Visits WHERE PatientType = 'Employee'")
        End Using
    End Function

    ' --- APPOINTMENT METHODS ---
    Public Class AppointmentItem
        Public Property Id As Integer
        Public Property PatientName As String
        Public Property AppointmentDate As DateTime
        Public Property Reason As String
    End Class

    Public Function GetAppointments(selectedDate As DateTime) As System.Collections.Generic.List(Of AppointmentItem)
        Using connection As New SqliteConnection(ConnectionString)
            ' SQLite dialect for date matching
            Dim query As String = "SELECT * FROM Appointments WHERE date(AppointmentDate) = date(@SelectedDate) ORDER BY AppointmentDate"
            Return connection.Query(Of AppointmentItem)(query, New With { .SelectedDate = selectedDate.Date }).AsList()
        End Using
    End Function

    Public Sub AddAppointment(appointment As AppointmentItem)
        Using connection As New SqliteConnection(ConnectionString)
            Dim query As String = "INSERT INTO Appointments (PatientName, AppointmentDate, Reason) VALUES (@PatientName, @AppointmentDate, @Reason)"
            connection.Execute(query, appointment)
        End Using
    End Sub

    Public Sub DeleteAppointment(id As Integer)
        Using connection As New SqliteConnection(ConnectionString)
            connection.Execute("DELETE FROM Appointments WHERE Id = @Id", New With { Key .Id = id })
        End Using
    End Sub

    ' --- USER MANAGEMENT METHODS ---
    Public Class UserItem
        Public Property Id As Integer
        Public Property Username As String
        Public Property Password As String
        Public Property Role As String
    End Class

    Public Function GetAllUsers() As System.Collections.Generic.List(Of UserItem)
        Using connection As New SqliteConnection(ConnectionString)
            Return connection.Query(Of UserItem)("SELECT Id, Username, Password, Role FROM Users ORDER BY Username").AsList()
        End Using
    End Function

    Public Sub DeleteUser(id As Integer)
        Using connection As New SqliteConnection(ConnectionString)
            connection.Execute("DELETE FROM Users WHERE Id = @Id", New With { Key .Id = id })
        End Using
    End Sub

    Public Sub UpdateUserPassword(id As Integer, newPassword As String)
        Using connection As New SqliteConnection(ConnectionString)
            connection.Execute("UPDATE Users SET Password = @Pass WHERE Id = @Id", New With {.Pass = newPassword, .Id = id})
        End Using
    End Sub

    Public Class DiagnosisSuggestion
        Public Property Sickness As String
        Public Property Count As Integer
        Public Property Confidence As Integer
        Public Property Similarity As Double
    End Class

    Public Class PrescriptionSuggestion
        Public Property Prescription As String
        Public Property Count As Integer
    End Class

    Public Function GetTopDiagnosesForReason(reasonText As String, Optional attendingUser As String = "") As List(Of DiagnosisSuggestion)
        If String.IsNullOrWhiteSpace(reasonText) Then Return New List(Of DiagnosisSuggestion)()
        
        ' Tokenize input symptoms
        Dim inputWords = TokenizeText(reasonText)
        If inputWords.Count = 0 Then Return New List(Of DiagnosisSuggestion)()
        
        ' Fetch all visits with a sickness
        Dim historicalVisits As List(Of VisitRecord)
        Using connection As New SqliteConnection(ConnectionString)
            Dim sql As String = "SELECT ReasonForVisit, Sickness, DoctorAssigned FROM Visits WHERE Sickness IS NOT NULL AND Sickness <> ''"
            historicalVisits = connection.Query(Of VisitRecord)(sql).AsList()
        End Using
        
        If historicalVisits.Count = 0 Then Return New List(Of DiagnosisSuggestion)()
        
        ' Score each sickness using Jaccard Similarity on symptoms (k-NN matching)
        Dim sicknessScores As New Dictionary(Of String, Double)(StringComparer.OrdinalIgnoreCase)
        Dim sicknessCounts As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        Dim sicknessMaxSimilarity As New Dictionary(Of String, Double)(StringComparer.OrdinalIgnoreCase)
        
        For Each visit In historicalVisits
            Dim recordWords = TokenizeText(visit.ReasonForVisit)
            If recordWords.Count = 0 Then Continue For
            
            ' Jaccard Similarity: Intersection / Union
            Dim intersectCount As Integer = 0
            For Each w In inputWords
                If recordWords.Contains(w) Then
                    intersectCount += 1
                End If
            Next
            Dim unionCount As Integer = inputWords.Count + recordWords.Count - intersectCount
            
            If intersectCount > 0 Then
                Dim similarity As Double = intersectCount / CDbl(unionCount)
                Dim sickness As String = visit.Sickness.Trim()
                
                ' Apply nurse-specific weighting boost
                Dim isSameNurse As Boolean = False
                If Not String.IsNullOrEmpty(attendingUser) AndAlso Not String.IsNullOrEmpty(visit.DoctorAssigned) Then
                    If visit.DoctorAssigned.Trim().Equals(attendingUser.Trim(), StringComparison.OrdinalIgnoreCase) Then
                        isSameNurse = True
                    End If
                End If
                
                Dim weightedSimilarity As Double = similarity
                If isSameNurse Then
                    weightedSimilarity = similarity * 1.5
                    If weightedSimilarity > 1.0 Then weightedSimilarity = 1.0
                End If
                
                If Not sicknessScores.ContainsKey(sickness) Then
                    sicknessScores(sickness) = 0
                    sicknessCounts(sickness) = 0
                    sicknessMaxSimilarity(sickness) = 0
                End If
                
                ' Accumulate scores to favor both similarity and frequency
                sicknessScores(sickness) += weightedSimilarity
                sicknessCounts(sickness) += 1
                If weightedSimilarity > sicknessMaxSimilarity(sickness) Then
                    sicknessMaxSimilarity(sickness) = weightedSimilarity
                End If
            End If
        Next
        
        If sicknessScores.Count = 0 Then Return New List(Of DiagnosisSuggestion)()
        
        ' Order results by score
        Dim sortedSicknesses = (From kvp In sicknessScores
                               Order By kvp.Value Descending
                               Select New With {
                                   .Sickness = kvp.Key,
                                   .Score = kvp.Value,
                                   .Count = sicknessCounts(kvp.Key)
                               }).Take(3).ToList()
                               
        ' Calculate relative confidence percentage
        Dim totalScore As Double = 0
        For Each item In sortedSicknesses
            totalScore += item.Score
        Next
        
        Dim results As New List(Of DiagnosisSuggestion)()
        For Each item In sortedSicknesses
            Dim confidence As Integer = 100
            If totalScore > 0 Then
                confidence = CInt((item.Score * 100) / totalScore)
            End If
            
            results.Add(New DiagnosisSuggestion() With {
                .Sickness = item.Sickness,
                .Count = item.Count,
                .Confidence = confidence,
                .Similarity = sicknessMaxSimilarity(item.Sickness)
            })
        Next
        
        Return results
    End Function

    Private Function TokenizeText(text As String) As HashSet(Of String)
        Dim words As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        If String.IsNullOrWhiteSpace(text) Then Return words
        
        Dim rawWords = text.Split(New Char() {" "c, ","c, ";"c, "."c, "-"c, "_"c}, StringSplitOptions.RemoveEmptyEntries)
        
        Dim stopWords As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
            "and", "the", "for", "with", "from", "felt", "have", "been", "was", "has", "had", 
            "she", "him", "his", "her", "they", "them", "but", "not", "this", "that", "are", 
            "were", "you", "your", "its", "then", "their", "our", "about", "like", "just", "very", "felt", "having", "some"
        }
        
        For Each rw In rawWords
            Dim cw = rw.Trim().ToLower()
            cw = cw.Replace("'", "").Replace("""", "")
            
            If cw.Length > 2 AndAlso Not stopWords.Contains(cw) Then
                words.Add(cw)
                
                ' Apply NLP Stemming and compound word splitting sequentially
                Dim stem As String = cw
                
                ' 1. Plural suffix 's' (e.g. headaches -> headache, cramps -> cramp)
                ' Exclude "ss" words like sickness, dizziness, illness
                If cw.EndsWith("s") AndAlso Not cw.EndsWith("ss") AndAlso cw.Length > 3 Then
                    stem = cw.Substring(0, cw.Length - 1)
                    words.Add(stem)
                End If
                
                ' 2. Progressive suffix 'ing' (e.g. coughing -> cough, vomiting -> vomit)
                If stem.EndsWith("ing") AndAlso stem.Length > 5 Then
                    Dim stemIng As String = stem.Substring(0, stem.Length - 3)
                    words.Add(stemIng)
                    stem = stemIng ' Update stem for subsequent compound checks
                End If
                
                ' 3. Splitting compound "ache" words (e.g. tummyache -> tummy + ache)
                If stem.EndsWith("ache") AndAlso stem.Length > 4 Then
                    words.Add("ache")
                    words.Add(stem.Substring(0, stem.Length - 4))
                End If
                
                ' Also check compound on raw word if it was skipped during stemming
                If cw.EndsWith("ache") AndAlso cw.Length > 4 Then
                    words.Add("ache")
                    words.Add(cw.Substring(0, cw.Length - 4))
                End If
            End If
        Next
        
        Return words
    End Function

    Public Function GetTopPrescriptionsForSickness(sicknessText As String) As List(Of PrescriptionSuggestion)
        If String.IsNullOrWhiteSpace(sicknessText) Then Return New List(Of PrescriptionSuggestion)()
        
        Using connection As New SqliteConnection(ConnectionString)
            Dim query As String = "
                SELECT Prescription, COUNT(*) as Count 
                FROM Visits 
                WHERE Sickness = @Sickness COLLATE NOCASE 
                  AND Prescription IS NOT NULL 
                  AND Prescription <> '' 
                GROUP BY Prescription 
                ORDER BY Count DESC 
                LIMIT 5"
            Return connection.Query(Of PrescriptionSuggestion)(query, New With {.Sickness = sicknessText.Trim()}).AsList()
        End Using
    End Function
End Class
