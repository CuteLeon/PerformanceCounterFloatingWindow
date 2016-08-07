<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SuspensionForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.PerformanceCounterTimer = New System.Windows.Forms.Timer(Me.components)
        Me.InfoTitle = New System.Windows.Forms.Label()
        Me.InfoData = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'PerformanceCounterTimer
        '
        Me.PerformanceCounterTimer.Enabled = True
        Me.PerformanceCounterTimer.Interval = 1000
        '
        'InfoTitle
        '
        Me.InfoTitle.BackColor = System.Drawing.Color.Transparent
        Me.InfoTitle.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.InfoTitle.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.InfoTitle.ForeColor = System.Drawing.Color.White
        Me.InfoTitle.Location = New System.Drawing.Point(0, 0)
        Me.InfoTitle.Name = "InfoTitle"
        Me.InfoTitle.Size = New System.Drawing.Size(93, 120)
        Me.InfoTitle.TabIndex = 0
        Me.InfoTitle.Text = "CPU：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "RAM：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "DiskRead：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "DiskWrite：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Upload：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Download："
        Me.InfoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'InfoData
        '
        Me.InfoData.BackColor = System.Drawing.Color.Transparent
        Me.InfoData.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.InfoData.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.InfoData.ForeColor = System.Drawing.Color.White
        Me.InfoData.Location = New System.Drawing.Point(93, 0)
        Me.InfoData.Name = "InfoData"
        Me.InfoData.Size = New System.Drawing.Size(92, 120)
        Me.InfoData.TabIndex = 1
        Me.InfoData.Text = "0%" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "0%" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "0.00 MB/S" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "0.00 MB/S" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "0.00 KB/S" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "0.00 KB/S"
        Me.InfoData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SuspensionForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(184, 120)
        Me.Controls.Add(Me.InfoData)
        Me.Controls.Add(Me.InfoTitle)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SuspensionForm"
        Me.Opacity = 0.6R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PerformanceCounterTimer As Timer
    Friend WithEvents InfoTitle As Label
    Friend WithEvents InfoData As Label
End Class
