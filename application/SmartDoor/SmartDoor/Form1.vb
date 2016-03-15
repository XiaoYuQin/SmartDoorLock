Imports System
Imports System.IO.Ports
Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '获取计算机有效串口
        Dim ports As String() = SerialPort.GetPortNames() '必须用命名空间，用SerialPort,获取计算机的有效串口
        Dim port As String
        For Each port In ports
            portnamebox.Items.Add(port) '向combobox中添加项
        Next port
        baudratebox.SelectedIndex() = 0
        portnamebox.SelectedIndex() = 0
        statuslabel.Text = "串口未连接"
        statuslabel.ForeColor = Color.Red

    End Sub

    Private Sub Serial_Port1() '设置串口参数
        SerialPort1.BaudRate = Val(baudratebox.Text) '波特率
        SerialPort1.PortName = portnamebox.Text '串口名称
        SerialPort1.DataBits = 8 '数据位
        SerialPort1.StopBits = IO.Ports.StopBits.One '停止位
        SerialPort1.Parity = IO.Ports.Parity.None '校验位
    End Sub


    Private Sub openbtn_Click(sender As Object, e As EventArgs) Handles openbtn.Click
        Try
            SerialPort1.Open() '打开串口
            Label3.Text = SerialPort1.IsOpen
            If SerialPort1.IsOpen = True Then
                statuslabel.Text = "串口已连接"
                statuslabel.ForeColor = Color.Green
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub closebtn_Click(sender As Object, e As EventArgs) Handles closebtn.Click
        Try
            SerialPort1.Close() '关闭串口
            Label3.Text = SerialPort1.IsOpen
            If SerialPort1.IsOpen = False Then
                statuslabel.Text = "串口未连接"
                statuslabel.ForeColor = Color.Red
                receivebox.Text = ""
                receivebytes.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Delegate Sub SetTextCallback(ByVal InputString As String)
    Private Sub ShowString(ByVal comData As String)
        receivebox.Text += comData   '将收到的数据入接收文字框中
        receivebox.SelectionStart = receivebox.Text.Length
        receivebox.ScrollToCaret()
    End Sub

    '触发接收事件
    Public Sub Sp_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        Me.Invoke(New EventHandler(AddressOf Sp_Receiving)) '调用接收数据函数
        Debug.Print("Sp_DataReceived")
        'Dim inData As String = SerialPort1.ReadExisting
        'Dim d As New SetTextCallback(AddressOf ShowString)
        'BeginInvoke(d, inData)
    End Sub
    ' Private Sub SerialPort1_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
    ' Dim inData As String = SerialPort1.ReadExisting
    'Dim d As New SetTextCallback(AddressOf ShowString)
    '   BeginInvoke(d, inData)
    'End Sub
    '接收数据
    Private Sub Sp_Receiving(ByVal sender As Object, ByVal e As EventArgs)
        Dim strIncoming As String
        Debug.Print("Sp_Receiving")
        Try
            receivebytes.Text = Str(Val(receivebytes.Text) + SerialPort1.BytesToRead)
            If SerialPort1.BytesToRead > 0 Then
                Threading.Thread.Sleep(100) '添加的延时
                strIncoming = SerialPort1.ReadExisting.ToString '读取缓冲区中的数据
                SerialPort1.DiscardInBuffer()
                Debug.Print(strIncoming.ToString)
                receivebox.Text = strIncoming
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Debug.Print(sendbox.Text)
            SerialPort1.Write("123456" & vbCr)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
