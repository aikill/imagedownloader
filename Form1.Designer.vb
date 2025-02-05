<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form1))
        Browse = New Button()
        download = New Button()
        downloadProgress = New ProgressBar()
        DataGridView1 = New DataGridView()
        TextBox1 = New TextBox()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Browse
        ' 
        Browse.Location = New Point(12, 12)
        Browse.Name = "Browse"
        Browse.Size = New Size(94, 29)
        Browse.TabIndex = 0
        Browse.Text = "Browse..."
        Browse.UseVisualStyleBackColor = True
        ' 
        ' download
        ' 
        download.Location = New Point(542, 12)
        download.Name = "download"
        download.Size = New Size(94, 29)
        download.TabIndex = 1
        download.Text = "Download"
        download.UseVisualStyleBackColor = True
        ' 
        ' downloadProgress
        ' 
        downloadProgress.Location = New Point(12, 49)
        downloadProgress.Name = "downloadProgress"
        downloadProgress.Size = New Size(624, 29)
        downloadProgress.TabIndex = 2
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(12, 84)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 51
        DataGridView1.Size = New Size(624, 384)
        DataGridView1.TabIndex = 4
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(112, 13)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(424, 27)
        TextBox1.TabIndex = 5
        ' 
        ' form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(649, 480)
        Controls.Add(TextBox1)
        Controls.Add(DataGridView1)
        Controls.Add(downloadProgress)
        Controls.Add(download)
        Controls.Add(Browse)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "form1"
        Text = "Bulk Image Downloader"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Browse As Button
    Friend WithEvents download As Button
    Friend WithEvents downloadProgress As ProgressBar
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents TextBox1 As TextBox

End Class
