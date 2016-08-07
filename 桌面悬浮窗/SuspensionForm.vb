Imports Microsoft.VisualBasic.Devices
'绘制曲线、进度条
Public Class SuspensionForm
    Private Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As IntPtr, ByVal nIndex As Integer) As Integer
    Private Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer

    Private Declare Function ReleaseCapture Lib "user32" () As Integer
    Private Declare Function SendMessageA Lib "user32" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, lParam As VariantType) As Integer

    Dim CPUCounter As New PerformanceCounter("Processor", "% Processor Time", "_Total") '获取CPU使用率
    Dim DiskReadCounter As New PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total") '获取硬盘读速度
    Dim DiskWriteCounter As New PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total") '获取硬盘写速度
    Dim CmptInfo As New ComputerInfo() '获取内存相关
    'Dim MemoryCounter As New PerformanceCounter("Memory", "Available MBytes", vbNullString) '获取可用内存
    Dim MemoryUsageRate As Integer '内存使用率

    '获取下载和上传网速相关（全部定义为ULong(64位无符号整形)以最大限度储存整数）
    Dim PCCategory As New PerformanceCounterCategory("Network Interface") '定义网络接口的性能计数器
    Dim LBoundOfArray As UInteger = 0, UBoundOfArray As UInteger = PCCategory.GetInstanceNames.Count - 1 '网卡和性能计数器集合的上标和下界
    Dim DownloadCounter(UBoundOfArray) As PerformanceCounter '为每块网卡绑定一个下载性能计数器
    Dim UploadCounter(UBoundOfArray) As PerformanceCounter '为每块网卡绑定一个上传性能计数器
    Dim DownloadSpeed(UBoundOfArray) As ULong, UploadSpeed(UBoundOfArray) As ULong '下载和上传速度
    Dim DownloadValue(UBoundOfArray) As ULong, UploadValue(UBoundOfArray) As ULong '已经下载和上传的字节
    Dim DownloadValueOld(UBoundOfArray) As ULong, UploadValueOld(UBoundOfArray) As ULong '上次记录的下载和上传的字节
    Dim DownloadSpeedCount As ULong, UploadSpeedCount As ULong '总下载和上传速度

    Private Sub SuspensionForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Location = New Point(My.Computer.Screen.Bounds.Width - Me.Width,
                                My.Computer.Screen.Bounds.Height - Me.Height)
        '遍历网卡
        For Index As Integer = LBoundOfArray To UBoundOfArray
            '初始化性能计数器
            DownloadCounter(Index) = New PerformanceCounter("Network Interface", "Bytes Received/sec", PCCategory.GetInstanceNames(Index))
            UploadCounter(Index) = New PerformanceCounter("Network Interface", "Bytes Sent/sec", PCCategory.GetInstanceNames(Index))
            '默认记录上次下载和上传的字节
            DownloadValueOld(Index) = DownloadCounter(Index).NextSample().RawValue
            UploadValueOld(Index) = UploadCounter(Index).NextSample().RawValue
        Next
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles PerformanceCounterTimer.Tick
        Me.TopMost = True
        MemoryUsageRate = (CmptInfo.TotalPhysicalMemory - CmptInfo.AvailablePhysicalMemory) / CmptInfo.TotalPhysicalMemory * 100
        '初始化目前已经上传和下载的字节
        DownloadSpeedCount = 0 : UploadSpeedCount = 0
        '遍历所有网卡
        For Index As Integer = LBoundOfArray To UBoundOfArray
            '累计所有网卡已经下载和上传的字节
            DownloadValue(Index) = DownloadCounter(Index).NextSample().RawValue
            UploadValue(Index) = UploadCounter(Index).NextSample().RawValue
            '每块网卡独立计算，防止禁用某网卡时出现速度为负值的情况，无符号整形变量冲突
            If DownloadValue(Index) > 0 Then DownloadSpeed(Index) = DownloadValue(Index) - DownloadValueOld(Index) Else DownloadSpeed(Index) = 0
            If UploadValue(Index) > 0 Then UploadSpeed(Index) = UploadValue(Index) - UploadValueOld(Index) Else UploadSpeed(Index) = 0
            '计算总的下载和上传速度
            DownloadSpeedCount += DownloadSpeed(Index)
            UploadSpeedCount += UploadSpeed(Index)
            '更新上次记录
            DownloadValueOld(Index) = DownloadValue(Index)
            UploadValueOld(Index) = UploadValue(Index)
        Next
        '输出
        InfoData.Text = Int(CPUCounter.NextValue) & "%" & vbCrLf & Int(MemoryUsageRate) & "%" & vbCrLf & FormatSpeedString(DiskReadCounter.NextValue) & vbCrLf & FormatSpeedString(DiskWriteCounter.NextValue) & vbCrLf & FormatSpeedString(UploadSpeedCount) & vbCrLf & FormatSpeedString(DownloadSpeedCount)
    End Sub

    '格式化速度
    Private Function FormatSpeedString(ByVal LoadSpeed As ULong) As String
        If LoadSpeed < 1048576 Then
            Return [String].Format("{0:n} KB/S", LoadSpeed / 1024)
        Else
            Return [String].Format("{0:n} MB/S", LoadSpeed / 1048576)
        End If
        '1073741824'超过这个数据就要用GB/S作为单位了...但是我不知道多少钱的计算机可以达到这个...
    End Function

    Private Sub Info_MouseDown(sender As Object, e As MouseEventArgs) Handles InfoData.MouseDown, InfoTitle.MouseDown
        ReleaseCapture()
        SendMessageA(Me.Handle, &HA1, 2, 0&)
    End Sub

    Private Sub SuspensionForm_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        SetWindowLong(Me.Handle, -20, GetWindowLong(Me.Handle, -20) Or &H8000000)
    End Sub

    Private Sub InfoTitle_MouseUp(sender As Object, e As MouseEventArgs) Handles InfoTitle.MouseUp
        If e.Button = MouseButtons.Right Then
            Application.Exit()
        End If
    End Sub
End Class
