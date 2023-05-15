''' <summary>
''' The class representing a user.
''' </summary>
Public Class User
    ''' <summary>
    ''' Gets or sets the Id.
    ''' </summary>
    Public Property Id As Long

    ''' <summary>
    ''' Gets or sets the username.
    ''' </summary>
    Public Property Username As String

    ''' <summary>
    ''' Gets or sets the password.
    ''' </summary>
    Public Property Password As String

    ''' <summary>
    ''' Gets or sets the username.
    ''' </summary>
    Public Property Favorites As IEnumerable(Of String)
End Class
