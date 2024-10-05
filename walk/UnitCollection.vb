Public Class UnitCollection
    Inherits List(Of UnitBase)

    Public Property Effectes As New List(Of UnitBase)
    Public Property EventObjectes As New List(Of UnitBase)
    Public Property Texts As New List(Of UnitBase)

    Public Overloads Sub Add(ByVal item As UnitBase)
        MyBase.Add(item)

        Select Case item.BelongTo
            Case BelongTo.Effect
                Effectes.Add(item)
            Case BelongTo.EventObject
                EventObjectes.Add(item)
            Case BelongTo.Text
                Texts.Add(item)
        End Select

    End Sub

    Public Overloads Sub Remove(ByVal item As UnitBase)
        MyBase.Remove(item)

        Select Case item.BelongTo
            Case BelongTo.Effect
                Effectes.Remove(item)
            Case BelongTo.EventObject
                EventObjectes.Remove(item)
            Case BelongTo.Text
                Texts.Remove(item)
        End Select
    End Sub

    Public Overloads Sub Clear()
        Effectes.Clear()
        EventObjectes.Clear()
        Texts.Clear()
        MyBase.Clear()
    End Sub


End Class
