Public Class Article
    Private _Id As Integer
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            _Id = value
        End Set
    End Property

    Private _Title As String
    Public Property Title() As String
        Get
            Return _Title
        End Get
        Set(ByVal value As String)
            _Title = value
        End Set
    End Property

    Private _Category As Category
    Public Property Category() As Category
        Get
            Return _Category
        End Get
        Set(ByVal value As Category)
            _Category = value
        End Set
    End Property
End Class
