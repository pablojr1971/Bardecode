Public Class FrmProcess


    Public Sub New(ParentForm As System.Windows.Forms.IWin32Window)
        InitializeComponent()

    End Sub

    Public Sub New(Id As Integer, ParentForm As System.Windows.Forms.IWin32Window)
        InitializeComponent()

        ' se vier valor no id eu devo buscar do banco de dados com este objeto da entidade 
        ' e dai colocar tudo num grande objeto so e boa vai ficar bom
        ' engracado como as coisas sao automaticas eu nunca penso em cada letra
        ' eu so penso na palavra e meus dedos ja escrevem isso automaticamente
        ' agora eu nem preciso pensar em qual tecla eu estou apertando eu so aperto




    End Sub


    Private Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        ' Save the changes in the database
    End Sub

    Private Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        ' Cancel the changes, and rollback the database
    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub
End Class