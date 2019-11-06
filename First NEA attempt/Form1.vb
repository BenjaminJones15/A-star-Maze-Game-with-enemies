﻿Public Class Form1

    Public Function CreatePicBoxes() Handles Me.Load

        Dim i As Integer = 1
        For y = 0 To 950 Step 50
            For x = 0 To 950 Step 50
                Dim newPictureBox As New PictureBox()
                newPictureBox.Name = "PictureBox" & i
                newPictureBox.Location = New Point(x, y)
                newPictureBox.Size = New Size(50, 50)
                newPictureBox.BorderStyle = BorderStyle.None
                newPictureBox.Dock = DockStyle.None
                newPictureBox.Visible = True
                If x = 950 And y = 950 Then
                    newPictureBox.BackColor = Color.Green
                ElseIf Maze(x, y) = True Then
                    newPictureBox.BackColor = Color.White    'path = white
                ElseIf Maze(x, y) = False Then
                    newPictureBox.BackColor = Color.Black    'walls = black
                End If
                Controls.Add(newPictureBox)
                i += 1
            Next
        Next

    End Function

End Class

Module module1

    Public Maze(950, 950) As Boolean    'If True, path is present
    Dim FrontierX As New List(Of Integer)   'All cells that are adjacent to a cell in the maze X coords
    Dim FrontierY As New List(Of Integer)   'All cells that are adjacent to a cell in the maze Y coords
    Dim FrontierCount As Integer = 0
    Dim RandomNumber As Integer

    'Marks all cells as false, and then turns the paths true. Then go through, and for all squares false, put a wall.

    Sub Main()
        Randomize()

        For y = 0 To 950 Step 50
            For x = 0 To 950 Step 50
                Maze(x, y) = False
            Next
        Next

        MarkIn(0, 0)

        Do Until FrontierX.Count = 0
            RandomNumber = CInt(Int((FrontierX.Count) * Rnd()))          '.count starts at 1, the actual list starts at 0
            MarkIn(FrontierX(RandomNumber), FrontierY(RandomNumber))
            FrontierX.RemoveAt(RandomNumber)
            FrontierY.RemoveAt(RandomNumber)
        Loop
        Maze(950, 950) = True

    End Sub

    Function MarkIn(ByVal x As Integer, ByVal y As Integer)  'This function will mark a cell as in the maze, into the frontier list
        Dim check As Boolean
        check = False

        If x = 0 And y = 0 Then
            If Maze(x + 50, y) = False And Maze(x, y + 50) = False Then
                check = True
            End If
        ElseIf x = 950 And y = 0 Then
            If Maze(x - 50, y) = False And Maze(x, y + 50) = False Then
                check = True
            End If
        ElseIf x = 950 And y = 950 Then
            If Maze(x - 50, y) = False And Maze(x, y - 50) = False Then
                check = True
            End If
        ElseIf x = 0 And y = 950 Then
            If Maze(x, y - 50) = False And Maze(x + 50, y) = False Then
                check = True
            End If
        ElseIf 49 < x And x < 901 And y = 0 Then
            If Maze(x - 50, y) = False And Maze(x + 50, y) = False Then
                check = True
            ElseIf Maze(x - 50, y) = False And Maze(x, y + 50) = False Then
                check = True
            ElseIf Maze(x, y + 50) = False And Maze(x + 50, y) = False Then
                check = True
            End If
        ElseIf x = 950 And 49 < y And y < 901 Then
            If Maze(x, y - 50) = False And Maze(x - 50, y) = False Then
                check = True
            ElseIf Maze(x, y + 50) = False And Maze(x, y - 50) = False Then
                check = True
            ElseIf Maze(x - 50, y) = False And Maze(x, y + 50) = False Then
                check = True
            End If
        ElseIf 49 < x And x < 901 And y = 950 Then
            If Maze(x - 50, y) = False And Maze(x + 50, y) = False Then
                check = True
            ElseIf Maze(x, y - 50) = False And Maze(x + 50, y) = False Then
                check = True
            End If
        ElseIf x = 0 And 49 < y And y < 901 Then
            If Maze(x, y - 50) = False And Maze(x + 50, y) = False Then
                check = True
            ElseIf Maze(x, y + 50) = False And Maze(x + 50, y) = False Then
                check = True
            ElseIf Maze(x, y + 50) = False And Maze(x, y - 50) = False Then
                check = True
            End If
        Else
            If Maze(x, y - 50) = False And Maze(x + 50, y) = False And Maze(x, y + 50) = False Then
                check = True
            ElseIf Maze(x, y + 50) = False And Maze(x + 50, y) = False And Maze(x - 50, y) = False Then
                check = True
            ElseIf Maze(x, y + 50) = False And Maze(x, y - 50) = False And Maze(x - 50, y) = False Then
                check = True
            ElseIf Maze(x - 50, y) = False And Maze(x + 50, y) = False And Maze(x, y - 50) = False Then
                check = True
            End If
        End If

        If check = True Then
            Maze(x, y) = True
            MarkFrontier(x, y)
        End If

    End Function

    Function MarkFrontier(ByVal x As Integer, ByVal y As Integer)   'This function will mark the neighbours of the in cell as out, putting them in the frontier list, unless they are already processed.

        If x = 0 And y = 0 Then                       'Top left corner
            If Maze(x, y + 50) = False And Maze(x + 50, y + 50) = False And Maze(x, y + 100) = False Then
                FrontierX.Add(x)          'Down coordinate    X
                FrontierY.Add(y + 50)      'Down coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 50, y + 50) = False And Maze(x + 100, y) = False Then
                FrontierX.Add(x + 50)      'Right coordinate    X
                FrontierY.Add(y)         'Right coordinate   Y
                FrontierCount += 1
            End If

        ElseIf x = 0 And y = 950 Then                 'Bottom left corner
            If Maze(x, y - 50) = False And Maze(x, y - 100) = False And Maze(x + 50, y - 50) = False Then
                FrontierX.Add(x)          'Up coordinate    X
                FrontierY.Add(y - 50)     'Up coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 100, y) = False And Maze(x + 50, y - 50) = False Then
                FrontierX.Add(x + 50)      'Right coordinate  X
                FrontierY.Add(y)           'Right coordinate   Y
                FrontierCount += 1
            End If

        ElseIf x = 950 And y = 0 Then                 'Top right corner
            If Maze(x, y + 50) = False And Maze(x - 50, y + 50) = False And Maze(x, y + 100) = False Then
                FrontierX.Add(x)           'Down coordinate    X
                FrontierY.Add(y + 50)     'Down coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 100, y) = False And Maze(x - 50, y + 50) = False Then
                FrontierX.Add(x - 50)    'Left coordinate    X
                FrontierY.Add(y)          'Left coordinate   Y
                FrontierCount += 1
            End If

        ElseIf x = 950 And y = 950 Then               'Bottom right corner
            If Maze(x, y - 50) = False And Maze(x, y - 100) = False And Maze(x - 50, y - 50) = False Then
                FrontierX.Add(x)        'Up coordinate    X
                FrontierY.Add(y - 50)     'Up coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 50, y) = False And Maze(x - 50, y - 50) = False Then
                FrontierX.Add(x - 50)      'Left coordinate   X
                FrontierY.Add(y)         'Left coordinate   Y
                FrontierCount += 1
            End If

        ElseIf x = 0 And y = 50 Then                      'special case
            If Maze(x, y + 50) = False And Maze(x, y + 100) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x)           'Down coordinate    X
                FrontierY.Add(y + 50)      'Down coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 50, y - 50) = False And Maze(x + 100, y) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x + 50)     'Right coordinate    X
                FrontierY.Add(y)          'Right coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x, y - 50) = False And Maze(x + 50, y - 50) = False Then
                FrontierX.Add(x)             'Up coordinate   X
                FrontierY.Add(y - 50)        'Up coordinate   Y
                FrontierCount += 1
            End If

        ElseIf x = 0 And y = 900 Then           'special case
            If Maze(x, y + 50) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x)           'Down coordinate    X
                FrontierY.Add(y + 50)      'Down coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 50, y - 50) = False And Maze(x + 100, y) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x + 50)     'Right coordinate    X
                FrontierY.Add(y)          'Right coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x, y - 50) = False And Maze(x, y - 100) = False And Maze(x + 50, y - 50) = False Then
                FrontierX.Add(x)             'Up coordinate   X
                FrontierY.Add(y - 50)        'Up coordinate   Y
                FrontierCount += 1
            End If

        ElseIf x = 50 And y = 0 Then                       'special case
            If Maze(x, y + 50) = False And Maze(x - 50, y + 50) = False And Maze(x + 100, y) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x)          'Down coordinate   X
                FrontierY.Add(y + 50)     'Down coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 50, y + 50) = False Then
                FrontierX.Add(x - 50)         'Left coordinate X
                FrontierY.Add(y)             'Left coordinate Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 100, y) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x + 50)      'Right coordinate  X
                FrontierY.Add(y)           'Right coordinate   Y
                FrontierCount += 1
            End If

        ElseIf x = 50 And y = 50 Then              'special case
            If Maze(x, y - 50) = False And Maze(x - 50, y - 50) = False And Maze(x + 50, y - 50) = False Then
                FrontierX.Add(x)           'Up coordinate    X
                FrontierY.Add(y - 50)    'Up coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 50, y - 50) = False And Maze(x + 100, y) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x + 50)      'Right coordinate  X
                FrontierY.Add(y)           'Right coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 50, y - 50) = False And Maze(x - 50, y + 50) = False Then
                FrontierX.Add(x - 50)     'Left coordinate   X
                FrontierY.Add(y)          'Left coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x, y + 50) = False And Maze(x - 50, y + 50) = False And Maze(x, y + 100) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x)        'Down coordinate X
                FrontierY.Add(y + 50)      'Down coordinate Y
                FrontierCount += 1
            End If

        ElseIf x = 50 And 99 < y And y < 851 Then                     'special case
            If Maze(x, y - 50) = False And Maze(x - 50, y - 50) = False And Maze(x + 50, y - 50) = False And Maze(x, y - 100) = False Then
                FrontierX.Add(x)           'Up coordinate    X
                FrontierY.Add(y - 50)    'Up coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 50, y - 50) = False And Maze(x + 100, y) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x + 50)      'Right coordinate  X
                FrontierY.Add(y)           'Right coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 50, y - 50) = False And Maze(x - 50, y + 50) = False Then
                FrontierX.Add(x - 50)     'Left coordinate   X
                FrontierY.Add(y)          'Left coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x, y + 50) = False And Maze(x - 50, y + 50) = False And Maze(x, y + 100) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x)        'Down coordinate X
                FrontierY.Add(y + 50)      'Down coordinate Y
                FrontierCount += 1
            End If

        ElseIf x = 50 And y = 900 Then           'special case
            If Maze(x, y - 50) = False And Maze(x - 50, y - 50) = False And Maze(x + 50, y - 50) = False And Maze(x, y - 100) = False Then
                FrontierX.Add(x)           'Up coordinate    X
                FrontierY.Add(y - 50)    'Up coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 50, y - 50) = False And Maze(x + 100, y) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x + 50)      'Right coordinate  X
                FrontierY.Add(y)           'Right coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 50, y - 50) = False And Maze(x - 50, y + 50) = False Then
                FrontierX.Add(x - 50)     'Left coordinate   X
                FrontierY.Add(y)          'Left coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x, y + 50) = False And Maze(x - 50, y + 50) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x)        'Down coordinate X
                FrontierY.Add(y + 50)      'Down coordinate Y
                FrontierCount += 1
            End If

        ElseIf x = 50 And y = 950 Then          'special case
            If Maze(x, y - 50) = False And Maze(x - 50, y - 50) = False And Maze(x, y - 100) = False And Maze(x + 50, y - 50) = False Then
                FrontierX.Add(x)           'Up coordinate    X
                FrontierY.Add(y - 50)      'Up coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 50, y - 50) = False And Maze(x + 100, y) = False Then
                FrontierX.Add(x + 50)      'Right coordinate  X
                FrontierY.Add(y)         'Right coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 50, y - 50) = False Then
                FrontierX.Add(x - 50)      'Left coordinate   X
                FrontierY.Add(y)           'Left coordinate   Y
                FrontierCount += 1
            End If

        ElseIf 99 < x And x < 851 And y = 50 Then                 'special case
            If Maze(x, y - 50) = False And Maze(x - 50, y - 50) = False And Maze(x + 50, y - 50) = False Then
                FrontierX.Add(x)           'Up coordinate    X
                FrontierY.Add(y - 50)    'Up coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 50, y - 50) = False And Maze(x + 100, y) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x + 50)      'Right coordinate  X
                FrontierY.Add(y)           'Right coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 50, y - 50) = False And Maze(x - 50, y + 50) = False And Maze(x - 100, y) = False Then
                FrontierX.Add(x - 50)     'Left coordinate   X
                FrontierY.Add(y)          'Left coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x, y + 50) = False And Maze(x - 50, y + 50) = False And Maze(x + 50, y + 50) = False And Maze(x, y + 100) = False Then
                FrontierX.Add(x)        'Down coordinate X
                FrontierY.Add(y + 50)      'Down coordinate Y
                FrontierCount += 1
            End If

        ElseIf 99 < x And x < 851 And y = 900 Then         'special case
            If Maze(x, y - 50) = False And Maze(x - 50, y - 50) = False And Maze(x + 50, y - 50) = False And Maze(x, y - 100) = False Then
                FrontierX.Add(x)           'Up coordinate    X
                FrontierY.Add(y - 50)    'Up coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 50, y - 50) = False And Maze(x + 100, y) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x + 50)      'Right coordinate  X
                FrontierY.Add(y)           'Right coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 50, y - 50) = False And Maze(x - 50, y + 50) = False And Maze(x - 100, y) = False Then
                FrontierX.Add(x - 50)     'Left coordinate   X
                FrontierY.Add(y)          'Left coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x, y + 50) = False And Maze(x - 50, y + 50) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x)        'Down coordinate X
                FrontierY.Add(y + 50)      'Down coordinate Y
                FrontierCount += 1
            End If

        ElseIf x = 900 And y = 0 Then         'special case
            If Maze(x + 50, y) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x + 50)      'Right coordinate  X
                FrontierY.Add(y)           'Right coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 50, y + 50) = False And Maze(x - 100, y) = False Then
                FrontierX.Add(x - 50)     'Left coordinate   X
                FrontierY.Add(y)          'Left coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x, y + 50) = False And Maze(x - 50, y + 50) = False And Maze(x + 50, y + 50) = False And Maze(x, y + 100) = False Then
                FrontierX.Add(x)        'Down coordinate X
                FrontierY.Add(y + 50)      'Down coordinate Y
                FrontierCount += 1
            End If

        ElseIf x = 900 And y = 50 Then              'special case
            If Maze(x, y - 50) = False And Maze(x - 50, y - 50) = False And Maze(x + 50, y - 50) = False Then
                FrontierX.Add(x)           'Up coordinate    X
                FrontierY.Add(y - 50)    'Up coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 50, y - 50) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x + 50)      'Right coordinate  X
                FrontierY.Add(y)           'Right coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 50, y - 50) = False And Maze(x - 50, y + 50) = False And Maze(x - 100, y) = False Then
                FrontierX.Add(x - 50)     'Left coordinate   X
                FrontierY.Add(y)          'Left coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x, y + 50) = False And Maze(x - 50, y + 50) = False And Maze(x + 50, y + 50) = False And Maze(x, y + 100) = False Then
                FrontierX.Add(x)        'Down coordinate X
                FrontierY.Add(y + 50)      'Down coordinate Y
                FrontierCount += 1
            End If

        ElseIf x = 900 And 99 < y And y < 851 Then            'special case
            If Maze(x, y - 50) = False And Maze(x - 50, y - 50) = False And Maze(x + 50, y - 50) = False And Maze(x, y - 100) = False Then
                FrontierX.Add(x)           'Up coordinate    X
                FrontierY.Add(y - 50)    'Up coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 50, y - 50) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x + 50)      'Right coordinate  X
                FrontierY.Add(y)           'Right coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 50, y - 50) = False And Maze(x - 50, y + 50) = False And Maze(x - 100, y) = False Then
                FrontierX.Add(x - 50)     'Left coordinate   X
                FrontierY.Add(y)          'Left coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x, y + 50) = False And Maze(x - 50, y + 50) = False And Maze(x + 50, y + 50) = False And Maze(x, y + 100) = False Then
                FrontierX.Add(x)        'Down coordinate X
                FrontierY.Add(y + 50)      'Down coordinate Y
                FrontierCount += 1
            End If

        ElseIf x = 900 And y = 900 Then                     'special case
            If Maze(x, y - 50) = False And Maze(x - 50, y - 50) = False And Maze(x + 50, y - 50) = False And Maze(x, y - 100) = False Then
                FrontierX.Add(x)           'Up coordinate    X
                FrontierY.Add(y - 50)    'Up coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 50, y - 50) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x + 50)      'Right coordinate  X
                FrontierY.Add(y)           'Right coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 50, y - 50) = False And Maze(x - 50, y + 50) = False And Maze(x - 100, y) = False Then
                FrontierX.Add(x - 50)     'Left coordinate   X
                FrontierY.Add(y)          'Left coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x, y + 50) = False And Maze(x - 50, y + 50) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x)        'Down coordinate X
                FrontierY.Add(y + 50)      'Down coordinate Y
                FrontierCount += 1
            End If

        ElseIf x = 900 And y = 950 Then                'special case
            If Maze(x, y - 50) = False And Maze(x - 50, y - 50) = False And Maze(x + 50, y - 50) = False And Maze(x, y - 100) = False Then
                FrontierX.Add(x)           'Up coordinate    X
                FrontierY.Add(y - 50)    'Up coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 50, y - 50) = False Then
                FrontierX.Add(x + 50)      'Right coordinate  X
                FrontierY.Add(y)           'Right coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 50, y - 50) = False And Maze(x - 100, y) = False Then
                FrontierX.Add(x - 50)     'Left coordinate   X
                FrontierY.Add(y)          'Left coordinate   Y
                FrontierCount += 1
            End If

        ElseIf x = 950 And y = 50 Then                'special case
            If Maze(x, y - 50) = False And Maze(x - 50, y - 50) = False Then
                FrontierX.Add(x)           'Up coordinate    X
                FrontierY.Add(y - 50)    'Up coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 50, y - 50) = False And Maze(x - 100, y) = False And Maze(x - 50, y + 50) = False Then
                FrontierX.Add(x - 50)     'Left coordinate   X
                FrontierY.Add(y)          'Left coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x, y + 50) = False And Maze(x - 50, y + 50) = False And Maze(x, y + 100) = False Then
                FrontierX.Add(x)        'Down coordinate X
                FrontierY.Add(y + 50)      'Down coordinate Y
                FrontierCount += 1
            End If

        ElseIf x = 950 And y = 900 Then             'special case
            If Maze(x, y - 50) = False And Maze(x - 50, y - 50) = False And Maze(x, y - 100) = False Then
                FrontierX.Add(x)           'Up coordinate    X
                FrontierY.Add(y - 50)    'Up coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 50, y - 50) = False And Maze(x - 100, y) = False And Maze(x - 50, y + 50) = False Then
                FrontierX.Add(x - 50)     'Left coordinate   X
                FrontierY.Add(y)          'Left coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x, y + 50) = False And Maze(x - 50, y + 50) = False Then
                FrontierX.Add(x)        'Down coordinate X
                FrontierY.Add(y + 50)      'Down coordinate Y
                FrontierCount += 1
            End If

        ElseIf x = 0 Then                             'If the current square is at the left border
            If Maze(x, y + 50) = False And Maze(x, y + 100) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x)           'Down coordinate    X
                FrontierY.Add(y + 50)      'Down coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 100, y) = False And Maze(x + 50, y - 50) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x + 50)     'Right coordinate    X
                FrontierY.Add(y)          'Right coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x, y - 50) = False And Maze(x, y - 100) = False And Maze(x + 50, y - 50) = False Then
                FrontierX.Add(x)             'Up coordinate   X
                FrontierY.Add(y - 50)        'Up coordinate   Y
                FrontierCount += 1
            End If

        ElseIf y = 0 Then                             'If the current square is at the top border
            If Maze(x, y + 50) = False And Maze(x, y + 100) = False And Maze(x + 50, y + 50) = False And Maze(x - 50, y + 50) = False Then
                FrontierX.Add(x)          'Down coordinate   X
                FrontierY.Add(y + 50)     'Down coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 100, y) = False And Maze(x - 50, y + 50) = False Then
                FrontierX.Add(x - 50)         'Left coordinate X
                FrontierY.Add(y)             'Left coordinate Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 100, y) = False And Maze(x + 50, y + 50) = False Then
                FrontierX.Add(x + 50)      'Right coordinate  X
                FrontierY.Add(y)           'Right coordinate   Y
                FrontierCount += 1
            End If

        ElseIf x = 950 Then                           'If the current square is at the right border
            If Maze(x, y - 50) = False And Maze(x, y - 100) = False And Maze(x - 50, y - 50) = False Then
                FrontierX.Add(x)          'Up coordinate    X
                FrontierY.Add(y - 50)     'Up coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x, y + 50) = False And Maze(x, y + 50) = False And Maze(x - 50, y + 50) = False Then
                FrontierX.Add(x)          'Down coordinate X
                FrontierY.Add(y + 50)      'Down coordinate Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 100, y) = False And Maze(x - 50, y - 50) = False And Maze(x - 50, y + 50) = False Then
                FrontierX.Add(x - 50)      'Left coordinate   X
                FrontierY.Add(y)          'Left coordinate   Y
                FrontierCount += 1
            End If

        ElseIf y = 950 Then                           'If the current square is at the bottom border
            If Maze(x, y - 50) = False And Maze(x, y - 100) = False And Maze(x - 50, y - 50) = False And Maze(x + 50, y - 50) = False Then
                FrontierX.Add(x)           'Up coordinate    X
                FrontierY.Add(y - 50)      'Up coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 50, y - 50) = False And Maze(x + 100, y) = False Then
                FrontierX.Add(x + 50)      'Right coordinate  X
                FrontierY.Add(y)         'Right coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 50, y - 50) = False And Maze(x - 100, y) = False Then
                FrontierX.Add(x - 50)      'Left coordinate   X
                FrontierY.Add(y)           'Left coordinate   Y
                FrontierCount += 1
            End If

        Else                'anywhere else in the square
            If Maze(x, y - 50) = False And Maze(x - 50, y - 50) = False And Maze(x + 50, y - 50) = False And Maze(x, y - 100) = False Then
                FrontierX.Add(x)           'Up coordinate    X
                FrontierY.Add(y - 50)    'Up coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x + 50, y) = False And Maze(x + 50, y - 50) = False And Maze(x + 50, y + 50) = False And Maze(x + 100, y) = False Then
                FrontierX.Add(x + 50)      'Right coordinate  X
                FrontierY.Add(y)           'Right coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x - 50, y) = False And Maze(x - 50, y - 50) = False And Maze(x - 50, y + 50) = False And Maze(x - 100, y) = False Then
                FrontierX.Add(x - 50)     'Left coordinate   X
                FrontierY.Add(y)          'Left coordinate   Y
                FrontierCount += 1
            End If
            If Maze(x, y + 50) = False And Maze(x - 50, y + 50) = False And Maze(x + 50, y + 50) = False And Maze(x, y + 100) = False Then
                FrontierX.Add(x)        'Down coordinate X
                FrontierY.Add(y + 50)      'Down coordinate Y
                FrontierCount += 1
            End If

        End If
    End Function

    Dim OpenListX As New List(Of Integer)
    Dim OpenListY As New List(Of Integer)
    Dim ClosedListX As New List(Of Integer)
    Dim ClosedListY As New List(Of Integer)
    Dim fmin As Integer = 0
    Dim CurrentX As Integer = 0
    Dim CurrentY As Integer = 0
    Dim count As Integer = 0        'holds the highest item number
    Public Item() As Node

    Public Class Node
        Public ParentX As Integer
        Public ParentY As Integer
        Public ChildX() As Integer
        Public ChildY() As Integer
        Public f As Integer         'total cost of node  = g+h
        Public g As Integer         'distance from the start
        Public h As Integer         'heuristic best guess
    End Class

    Function AStar()
        Dim i As Integer = 0
        Dim found As Boolean = False
        Dim PreviousItemLength As Integer

        OpenListX.Add(0)
        OpenListY.Add(0)

        Do
            Item(i).g = i
            Item(i).h = CurrentX + CurrentY
            Item(i).f = Item(i).h + Item(i).g

            For b = 0 To Item.Length
                If Item(b).f <= fmin Then
                    fmin = b
                End If
            Next

            CurrentX = OpenListX(fmin)
            CurrentY = OpenListY(fmin)
            ClosedListX.Add(CurrentX)
            ClosedListY.Add(CurrentY)
            OpenListX.RemoveAt(fmin)
            OpenListY.RemoveAt(fmin)

            If CurrentX = 950 And CurrentY = 950 Then
                found = True
            End If

            PreviousItemLength = Item.Length

            Child(CurrentX, CurrentY)

            MakeChoice(PreviousItemLength)


            i += 1
        Loop While OpenListX.Count <> 0 Or found = False

    End Function

    Function MakeChoice(ByVal PrevLength As Integer)

        For c = PrevLength To Item(3).ChildX.Length

            For a = 0 To Item(c).ChildX.Length - 1
                If Item(c).ChildX(a) = 950 And Item(c).ChildY(a) = 950 Then
                    Return True
                End If
            Next

            Item(c).g = Item(c).ParentX







        Next



    End Function


    Function CheckInClosed()
        For a = 0 To ClosedListX.Count
            For b = 0 To ChildListX.Count
                If ClosedListX(a) = ChildListX(b) And ClosedListY(a) = ChildListY(b) Then
                    Return True
                End If
            Next
        Next
    End Function



    Function Child(ByVal x As Integer, ByVal y As Integer)

        If x = 0 And y = 0 Then                       'Top left corner
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(0) = x          'Down coordinate    X
                Item(count).ChildY(0) = (y + 50)      'Down coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(1) = x + 50      'Right coordinate    X
                Item(count).ChildY(1) = (y)         'Right coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            count += 1
        ElseIf x = 0 And y = 950 Then                 'Bottom left corner
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(0) = x          'Up coordinate    X
                Item(count).ChildY(0) = (y - 50)     'Up coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(1) = x + 50      'Right coordinate  X
                Item(count).ChildY(1) = (y)           'Right coordinate Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            count += 1
        ElseIf x = 950 And y = 0 Then                 'Top right corner
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(0) = x           'Down coordinate    X
                Item(count).ChildY(0) = (y + 50)     'Down coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(1) = (x - 50)    'Left coordinate    X
                Item(count).ChildY(1) = (y)          'Left coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            count += 1
        ElseIf x = 950 And y = 950 Then               'Bottom right corner
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(0) = (x)        'Up coordinate    X
                Item(count).ChildY(0) = (y - 50)     'Up coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(1) = (x - 50)      'Left coordinate   X
                Item(count).ChildY(1) = (y)         'Left coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            count += 1
        ElseIf x = 0 And y = 50 Then                      'special case
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(0) = (x)           'Down coordinate    X
                Item(count).ChildY(0) = (y + 50)      'Down coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(1) = (x + 50)     'Right coordinate    X
                Item(count).ChildY(1) = (y)          'Right coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(2) = (x)             'Up coordinate   X
                Item(count).ChildY(2) = (y - 50)        'Up coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            count += 1
        ElseIf x = 0 And y = 900 Then           'special case
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(0) = (x)           'Down coordinate    X
                Item(count).ChildY(0) = (y + 50)      'Down coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(1) = (x + 50)     'Right coordinate    X
                Item(count).ChildY(1) = (y)          'Right coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(2) = (x)             'Up coordinate   X
                Item(count).ChildY(2) = (y - 50)        'Up coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            count += 1
        ElseIf x = 50 And y = 0 Then                       'special case
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(0) = (x)          'Down coordinate   X
                Item(count).ChildY(0) = (y + 50)     'Down coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(1) = (x - 50)         'Left coordinate X
                Item(count).ChildY(1) = (y)             'Left coordinate Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(2) = (x + 50)      'Right coordinate  X
                Item(count).ChildY(2) = (y)           'Right coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            count += 1
        ElseIf x = 50 And y = 50 Then              'special case
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(0) = (x)           'Up coordinate    X
                Item(count).ChildY(0) = (y - 50)    'Up coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(1) = (x + 50)      'Right coordinate  X
                Item(count).ChildY(1) = (y)           'Right coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(2) = (x - 50)     'Left coordinate   X
                Item(count).ChildY(2) = (y)          'Left coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(3) = (x)        'Down coordinate X
                Item(count).ChildY(3) = (y + 50)      'Down coordinate Y
                Item(count + 4).ParentX = x
                Item(count + 4).ParentY = y
            End If
            count += 1
        ElseIf x = 50 And 99 < y And y < 851 Then                     'special case
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(0) = (x)           'Up coordinat    X
                Item(count).ChildY(0) = (y - 50)    'Up coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(1) = (x + 50)      'Right coordinate  X
                Item(count).ChildY(1) = (y)           'Right coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(2) = (x - 50)     'Left coordinate   X
                Item(count).ChildY(2) = (y)          'Left coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(3) = x        'own coordinate X
                Item(count).ChildY(3) = (y + 50)      'Down coordinate Y
                Item(count + 4).ParentX = x
                Item(count + 4).ParentY = y
            End If
            count += 1
        ElseIf x = 50 And y = 900 Then           'special case
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(0) = (x)           'Up coordinate    X
                Item(count).ChildY(0) = (y - 50)    'Up coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(1) = (x + 50)      'Right coordinate  X
                Item(count).ChildY(1) = (y)           'Right coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(2) = (x - 50)     'Left coordinate   X
                Item(count).ChildY(2) = (y)          'Left coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(3) = (x)        'Down coordinate X
                Item(count).ChildY(3) = (y + 50)      'Down coordinate Y
                Item(count + 4).ParentX = x
                Item(count + 4).ParentY = y
            End If
            count += 1
        ElseIf x = 50 And y = 950 Then          'special case
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(0) = (x)           'Up coordinate    X
                Item(count).ChildY(0) = (y - 50)      'Up coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(1) = (x + 50)      'Right coordinate  X
                Item(count).ChildY(1) = (y)         'Right coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(2) = (x - 50)      'Left coordinate   X
                Item(count).ChildY(2) = (y)           'Left coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            count += 1
        ElseIf 99 < x And x < 851 And y = 50 Then                 'special case
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(0) = (x)           'Up coordinate    X
                Item(count).ChildY(0) = (y - 50)    'Up coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(1) = (x + 50)      'Right coordinate  X
                Item(count).ChildY(1) = (y)           'Right coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(2) = (x - 50)     'Left coordinate   X
                Item(count).ChildY(2) = (y)          'Left coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(3) = (x)        'Down coordinate X
                Item(count).ChildY(3) = (y + 50)      'Down coordinate Y
                Item(count + 4).ParentX = x
                Item(count + 4).ParentY = y
            End If
            count += 1
        ElseIf 99 < x And x < 851 And y = 900 Then         'special case
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(0) = (x)           'Up coordinate    X
                Item(count).ChildY(0) = (y - 50)    'Up coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(1) = (x + 50)      'Right coordinate  X
                Item(count).ChildY(1) = (y)           'Right coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(2) = (x - 50)     'Left coordinate   X
                Item(count).ChildY(2) = (y)          'Left coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(3) = (x)        'Down coordinate X
                Item(count).ChildY(3) = (y + 50)      'Down coordinate Y
                Item(count + 4).ParentX = x
                Item(count + 4).ParentY = y
            End If
            count += 1
        ElseIf x = 900 And y = 0 Then         'special case
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(0) = (x + 50)      'Right coordinate  X
                Item(count).ChildY(0) = (y)           'Right coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(1) = (x - 50)     'Left coordinate   X
                Item(count).ChildY(1) = (y)          'Left coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(2) = (x)        'Down coordinate X
                Item(count).ChildY(2) = (y + 50)      'Down coordinate Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            count += 1
        ElseIf x = 900 And y = 50 Then              'special case
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(0) = (x)           'Up coordinate    X
                Item(count).ChildY(0) = (y - 50)    'Up coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(1) = (x + 50)      'Right coordinate  X
                Item(count).ChildY(1) = (y)           'Right coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(2) = (x - 50)     'Left coordinate   X
                Item(count).ChildY(2) = (y)          'Left coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(3) = (x)        'Down coordinate X
                Item(count).ChildY(3) = (y + 50)      'Down coordinate Y
                Item(count + 4).ParentX = x
                Item(count + 4).ParentY = y
            End If
            count += 1
        ElseIf x = 900 And 99 < y And y < 851 Then            'special case
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(0) = (x)           'Up coordinate    X
                Item(count).ChildY(0) = (y - 50)    'Up coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(1) = (x + 50)      'Right coordinate  X
                Item(count).ChildY(1) = (y)           'Right coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(2) = (x - 50)     'Left coordinate   X
                Item(count).ChildY(2) = (y)          'Left coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(3) = x        'Down coordinate X
                Item(count).ChildY(3) = (y + 50)      'Down coordinate Y
                Item(count + 4).ParentX = x
                Item(count + 4).ParentY = y
            End If
            count += 1
        ElseIf x = 900 And y = 900 Then                     'special case
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(0) = (x)           'Up coordinate    X
                Item(count).ChildY(0) = (y - 50)    'Up coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(1) = (x + 50)      'Right coordinate  X
                Item(count).ChildY(1) = (y)           'Right coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(2) = (x - 50)     'Left coordinate   X
                Item(count).ChildY(2) = y         'Left coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(3) = (x)        'Down coordinate X
                Item(count).ChildY(3) = (y + 50)      'Down coordinate Y
                Item(count + 4).ParentX = x
                Item(count + 4).ParentY = y
            End If
            count += 1
        ElseIf x = 900 And y = 950 Then                'special case
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(0) = (x)           'Up coordinate    X
                Item(count).ChildY(0) = (y - 50)    'Up coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(1) = (x + 50)      'Right coordinate  X
                Item(count).ChildY(1) = (y)           'Right coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(2) = (x - 50)     'Left coordinate   X
                Item(count).ChildY(2) = (y)          'Left coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            count += 1
        ElseIf x = 950 And y = 50 Then                'special case
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(0) = (x)           'Up coordinate    X
                Item(count).ChildY(0) = (y - 50)    'Up coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(1) = (x - 50)     'Left coordinate   X
                Item(count).ChildY(1) = (y)          'Left coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(2) = (x)        'Down coordinate X
                Item(count).ChildY(2) = (y + 50)      'Down coordinate Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            count += 1
        ElseIf x = 950 And y = 900 Then             'special case
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(0) = x           'Up coordinate    X
                Item(count).ChildY(0) = (y - 50)    'Up coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(1) = (x - 50)     'Left coordinate   X
                Item(count).ChildY(1) = (y)          'Left coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(2) = (x)        'Down coordinate X
                Item(count).ChildY(2) = (y + 50)      'Down coordinate Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            count += 1
        ElseIf x = 0 Then                             'If the current square is at the left border
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(0) = (x)           'Down coordinate    X
                Item(count).ChildY(0) = (y + 50)      'Down coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(1) = (x + 50)     'Right coordinate    X
                Item(count).ChildY(1) = (y)          'Right coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(2) = (x)             'Up coordinate   X
                Item(count).ChildY(2) = (y - 50)        'Up coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            count += 1
        ElseIf y = 0 Then                             'If the current square is at the top border
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(0) = (x)          'Down coordinate   X
                Item(count).ChildY(0) = (y + 50)     'Down coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(1) = (x - 50)         'Left coordinate X
                Item(count).ChildY(1) = (y)             'Left coordinate Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(2) = (x + 50)      'Right coordinate  X
                Item(count).ChildY(2) = (y)           'Right coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            count += 1
        ElseIf x = 950 Then                           'If the current square is at the right border
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(0) = (x)          'Up coordinate    X
                Item(count).ChildY(0) = (y - 50)     'Up coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(1) = (x)          'Down coordinate X
                Item(count).ChildY(1) = (y + 50)      'Down coordinate Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(2) = x - 50      'Left coordinate   X
                Item(count).ChildY(2) = y          'Left coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            count += 1
        ElseIf y = 950 Then                           'If the current square is at the bottom border
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(0) = x           'Up coordinate    X
                Item(count).ChildY(0) = y - 50      'Up coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(1) = x + 50      'Right coordinate  X
                Item(count).ChildY(1) = y         'Right coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(2) = x - 50      'Left coordinate   X
                Item(count).ChildY(2) = y           'Left coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            count += 1
        Else                'anywhere else in the square
            If Maze(x, y - 50) = False Then
                Item(count).ChildX(0) = x           'Up coordinate    X
                Item(count).ChildY(0) = y - 50    'Up coordinate   Y
                Item(count + 1).ParentX = x
                Item(count + 1).ParentY = y
            End If
            If Maze(x + 50, y) = False Then
                Item(count).ChildX(1) = x + 50      'Right coordinate  X
                Item(count).ChildY(1) = y           'Right coordinate   Y
                Item(count + 2).ParentX = x
                Item(count + 2).ParentY = y
            End If
            If Maze(x - 50, y) = False Then
                Item(count).ChildX(2) = x - 50     'Left coordinate   X
                Item(count).ChildY(2) = y          'Left coordinate   Y
                Item(count + 3).ParentX = x
                Item(count + 3).ParentY = y
            End If
            If Maze(x, y + 50) = False Then
                Item(count).ChildX(3) = x        'Down coordinate X
                Item(count).ChildY(3) = y + 50      'Down coordinate Y
                Item(count + 4).ParentX = x
                Item(count + 4).ParentY = y
            End If
            count += 1
        End If

    End Function

End Module