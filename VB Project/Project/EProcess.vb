'------------------------------------------------------------------------------
' <auto-generated>
'    This code was generated from a template.
'
'    Manual changes to this file may cause unexpected behavior in your application.
'    Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class EProcess
    Public Property Id As Integer
    Public Property Description As String
    Public Property Number As String

    Public Overridable Property Steps As ICollection(Of EStep) = New HashSet(Of EStep)

End Class
