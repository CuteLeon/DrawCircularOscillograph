Imports System.Drawing.Drawing2D

Public Class Form1
    Dim UnityBitmap As Bitmap
    Dim UnityRandom As Random = New Random()
    Dim UnityContext As BufferedGraphicsContext = BufferedGraphicsManager.Current
    Dim UnityColor As Color = Color.White
    Dim UnityPen As Pen = New Pen(UnityColor, 3)
    ReadOnly CircleMinRadius As Integer = 80
    ReadOnly CircleMaxRadius As Integer = 150
    ReadOnly CircleCenter As Point = New Point(150, 150)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub CreateBitmap()
        Dim StartPoint As Point, EndPoint As Point, Value As Integer
        Using UnityBuffer As BufferedGraphics = UnityContext.Allocate(Me.CreateGraphics, Me.ClientRectangle)
            Using UnityGraphics As Graphics = UnityBuffer.Graphics
                UnityGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality
                UnityGraphics.SmoothingMode = SmoothingMode.HighQuality
                UnityGraphics.Clear(Color.DarkGray)

                UnityGraphics.FillEllipse(Brushes.LightGray, 0, 0, 300, 300)
                For Index As Integer = CircleMinRadius To CircleMaxRadius Step 10
                    UnityGraphics.DrawEllipse(Pens.White, New Rectangle(150 - Index, 150 - Index, Index * 2, Index * 2))
                Next

                For Angle As Integer = 0 To 359
                    If Angle Mod 10 = 0 Then
                        EndPoint = New Point(Math.Sin(Angle / Math.PI / 16) * CircleMaxRadius, Math.Cos(Angle / Math.PI / 16) * CircleMaxRadius)
                        EndPoint.Offset(CircleCenter)
                        UnityGraphics.DrawLine(Pens.White, EndPoint, CircleCenter)
                    End If

                    Value = UnityRandom.Next(CircleMinRadius, CircleMaxRadius)
                    UnityColor = Color.FromArgb(UnityRandom.Next(127, 256), UnityRandom.Next(256), UnityRandom.Next(256), UnityRandom.Next(256))
                    StartPoint = New Point(Math.Sin(Angle) * CircleMinRadius, Math.Cos(Angle) * CircleMinRadius)
                    StartPoint.Offset(CircleCenter)
                    EndPoint = New Point(Math.Sin(Angle) * Value, Math.Cos(Angle) * Value)
                    EndPoint.Offset(CircleCenter)
                    UnityPen = New Pen(UnityColor, 3)
                    UnityGraphics.DrawLine(UnityPen, StartPoint, EndPoint)
                Next
                UnityGraphics.FillEllipse(Brushes.WhiteSmoke, 150 - CircleMinRadius, 150 - CircleMinRadius, CircleMinRadius * 2, CircleMinRadius * 2)
                UnityBuffer.Render(Me.CreateGraphics)
            End Using
        End Using
        GC.Collect()
    End Sub

    Private Sub Form1_Click(sender As Object, e As EventArgs) Handles Me.Click
        CreateBitmap()
    End Sub

End Class