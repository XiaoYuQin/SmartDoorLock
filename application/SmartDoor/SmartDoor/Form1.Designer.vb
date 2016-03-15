<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
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

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.portnamebox = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.baudratebox = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.openbtn = New System.Windows.Forms.Button()
        Me.closebtn = New System.Windows.Forms.Button()
        Me.statuslabel = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.receivebox = New System.Windows.Forms.TextBox()
        Me.sendbox = New System.Windows.Forms.TextBox()
        Me.receivebytes = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'SerialPort1
        '
        '
        'portnamebox
        '
        Me.portnamebox.FormattingEnabled = True
        Me.portnamebox.Location = New System.Drawing.Point(56, 28)
        Me.portnamebox.Name = "portnamebox"
        Me.portnamebox.Size = New System.Drawing.Size(121, 20)
        Me.portnamebox.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "串口"
        '
        'baudratebox
        '
        Me.baudratebox.FormattingEnabled = True
        Me.baudratebox.Items.AddRange(New Object() {"9600", "38400", "115200"})
        Me.baudratebox.Location = New System.Drawing.Point(56, 66)
        Me.baudratebox.Name = "baudratebox"
        Me.baudratebox.Size = New System.Drawing.Size(121, 20)
        Me.baudratebox.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "波特率"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 109)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "串口状态"
        '
        'openbtn
        '
        Me.openbtn.Location = New System.Drawing.Point(205, 28)
        Me.openbtn.Name = "openbtn"
        Me.openbtn.Size = New System.Drawing.Size(75, 23)
        Me.openbtn.TabIndex = 5
        Me.openbtn.Text = "连接串口"
        Me.openbtn.UseVisualStyleBackColor = True
        '
        'closebtn
        '
        Me.closebtn.Location = New System.Drawing.Point(205, 66)
        Me.closebtn.Name = "closebtn"
        Me.closebtn.Size = New System.Drawing.Size(75, 23)
        Me.closebtn.TabIndex = 6
        Me.closebtn.Text = "断开串口"
        Me.closebtn.UseVisualStyleBackColor = True
        '
        'statuslabel
        '
        Me.statuslabel.AutoSize = True
        Me.statuslabel.Location = New System.Drawing.Point(71, 109)
        Me.statuslabel.Name = "statuslabel"
        Me.statuslabel.Size = New System.Drawing.Size(53, 12)
        Me.statuslabel.TabIndex = 7
        Me.statuslabel.Text = "串口状态"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(205, 104)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 8
        Me.Button3.Text = "发送数据"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'receivebox
        '
        Me.receivebox.Location = New System.Drawing.Point(56, 162)
        Me.receivebox.Name = "receivebox"
        Me.receivebox.Size = New System.Drawing.Size(100, 21)
        Me.receivebox.TabIndex = 9
        '
        'sendbox
        '
        Me.sendbox.Location = New System.Drawing.Point(56, 206)
        Me.sendbox.Name = "sendbox"
        Me.sendbox.Size = New System.Drawing.Size(100, 21)
        Me.sendbox.TabIndex = 10
        '
        'receivebytes
        '
        Me.receivebytes.AutoSize = True
        Me.receivebytes.Location = New System.Drawing.Point(258, 165)
        Me.receivebytes.Name = "receivebytes"
        Me.receivebytes.Size = New System.Drawing.Size(41, 12)
        Me.receivebytes.TabIndex = 11
        Me.receivebytes.Text = "Label4"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(416, 324)
        Me.Controls.Add(Me.receivebytes)
        Me.Controls.Add(Me.sendbox)
        Me.Controls.Add(Me.receivebox)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.statuslabel)
        Me.Controls.Add(Me.closebtn)
        Me.Controls.Add(Me.openbtn)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.baudratebox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.portnamebox)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents portnamebox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents baudratebox As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents openbtn As System.Windows.Forms.Button
    Friend WithEvents closebtn As System.Windows.Forms.Button
    Friend WithEvents statuslabel As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents receivebox As System.Windows.Forms.TextBox
    Friend WithEvents sendbox As System.Windows.Forms.TextBox
    Friend WithEvents receivebytes As System.Windows.Forms.Label

End Class
