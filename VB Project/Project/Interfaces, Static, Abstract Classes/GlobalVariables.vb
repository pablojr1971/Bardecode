' This is supposed to be an Static class but we don't have it in VB
' So NotInheritable to assure that this class will not be Inherited
' And Shared Methods to just call is by ClassReference and don't need to create an object of the class
Public NotInheritable Class GlobalVariables
    Public Const BardecodeExe = "C:\Program Files (x86)\Softek Software\BardecodeFiler\BardecodeFiler.exe"

    Private Shared Transaction As System.Data.Entity.DbContextTransaction = Nothing

End Class
