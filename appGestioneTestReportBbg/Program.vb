Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms



Public Class Program

    <STAThread()> _
    Shared Sub Main()
        'Avvio l'applicazione
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New frmMain)
    End Sub


End Class
