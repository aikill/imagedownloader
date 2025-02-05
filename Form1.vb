Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports Microsoft.VisualBasic.FileIO

Public Class form1


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Browse.Click
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "CSV Files (*.csv)|*.csv"
        openFileDialog.Title = "Select a CSV File"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            ' Get the selected file path
            Dim filePath As String = openFileDialog.FileName
            TextBox1.Text = $"{filePath}"
            ' Read the CSV file and store columns in lists
            Dim columns As List(Of List(Of String)) = ReadCSVFile(filePath)

            ' Display the data in the DataGridView
            DisplayDataInGridView(columns)
        Else
            MessageBox.Show("No file selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub mainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Function ReadCSVFile(filePath As String) As List(Of List(Of String))
        ' Initialize a list of lists to store column data
        Dim columns As New List(Of List(Of String))()

        ' Use TextFieldParser to read the CSV file
        Using parser As New TextFieldParser(filePath)
            parser.TextFieldType = FieldType.Delimited
            parser.SetDelimiters(",")

            ' Read the header row and ignore it
            If Not parser.EndOfData Then
                parser.ReadFields()
            End If

            ' Read the rest of the file
            While Not parser.EndOfData
                Dim fields() As String = parser.ReadFields()

                ' Ensure columns are initialized
                If columns.Count = 0 Then
                    For i As Integer = 0 To fields.Length - 1
                        columns.Add(New List(Of String)())
                    Next
                End If

                ' Add each field to its corresponding column
                For i As Integer = 0 To fields.Length - 1
                    columns(i).Add(fields(i))
                Next
            End While
        End Using

        Return columns
    End Function

    ' Function to display column data in the DataGridView
    Private Sub DisplayDataInGridView(columns As List(Of List(Of String)))
        ' Clear existing data in the DataGridView
        DataGridView1.Columns.Clear()
        DataGridView1.Rows.Clear()

        ' Add columns to the DataGridView
        For i As Integer = 0 To columns.Count - 1
            DataGridView1.Columns.Add($"Column{i + 1}", $"Column {i + 1}")
        Next

        ' Add rows to the DataGridView
        For rowIndex As Integer = 0 To columns(0).Count - 1
            Dim rowValues(columns.Count - 1) As String
            For colIndex As Integer = 0 To columns.Count - 1
                rowValues(colIndex) = columns(colIndex)(rowIndex)
            Next
            DataGridView1.Rows.Add(rowValues)
        Next
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Async Sub download_Click(sender As Object, e As EventArgs) Handles download.Click
        ' Create a folder to save the downloaded files
        Dim dateTime As String = DateString + "_" + Format(Now, "hhmmss")
        Dim saveFolder As String = Path.Combine(Application.StartupPath, "DownloadedFiles_" + dateTime)
        'verify wheter a directory of the same name exists already or not 
        If Not Directory.Exists(saveFolder) Then
            Directory.CreateDirectory(saveFolder)
            'if a directory with the same name exists then a randomly generated integer will be appended to the end of the file to prevent an exception
        ElseIf Directory.Exists(saveFolder) Then
            Dim randomInt As Integer = Int((100 * Rnd()) + 1)
            Dim randomIntStr As String = randomInt.ToString
            Directory.CreateDirectory(saveFolder + randomIntStr)
        End If

        'get a  count of rows in the datagrid
        Dim rowCount As Integer = DataGridView1.Rows.Count
        Dim progressIter As Integer = 100 / rowCount - 1

        'reset the value of the progress bar to 0 
        downloadProgress.Value = 0

        'establish output file for errrors
        Dim fs = CreateObject("Scripting.FileSystemObject")
        Dim a = fs.CreateTextFile(Application.StartupPath + dateTime + "_failed downloads" + ".txt", True)


        ' Create an HttpClient instance
        Using client As New HttpClient()
            ' Iterate through each row in the DataGridView
            For Each row As DataGridViewRow In DataGridView1.Rows
                ' Get the URL from the first column
                Dim url As String = row.Cells(0).Value?.ToString()
                If Not String.IsNullOrEmpty(url) Then
                    Try
                        ' Download the file
                        Dim response As HttpResponseMessage = Await client.GetAsync(url)
                        response.EnsureSuccessStatusCode()

                        ' Get the file name from the second column if it exists
                        Dim fileName As String = row.Cells(1).Value?.ToString()
                        If String.IsNullOrEmpty(fileName) Then
                            ' Use the default file name from the URL
                            fileName = Path.GetFileName(url)
                        Else
                            ' Append the file extension from the URL
                            Dim extension As String = Path.GetExtension(url)
                            fileName = fileName & extension
                        End If

                        ' Save the file
                        Dim filePath As String = Path.Combine(saveFolder, fileName)
                        Dim fileBytes As Byte() = Await response.Content.ReadAsByteArrayAsync()
                        File.WriteAllBytes(filePath, fileBytes)

                        ' MessageBox.Show($"File downloaded and saved as: {filePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        downloadProgress.Value += progressIter
                    Catch ex As Exception
                        'inform the user that a download has failed and insert the url into a textfile with a similar naming convention to the download folder
                        MessageBox.Show($"Failed to download file from {url}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        a.WriteLine("Failed to download file: " + url)
                        downloadProgress.Value += progressIter
                    End Try
                End If
            Next
        End Using
        a.close()
        downloadProgress.Value = 100
        MessageBox.Show("Download Process Complete!")
    End Sub


End Class
