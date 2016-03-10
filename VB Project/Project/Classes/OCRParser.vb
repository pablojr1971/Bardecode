Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Linq
Imports Clock.Hocr
Imports System.Xml.XPath
Imports System.IO
Imports System.Diagnostics
Imports HtmlAgilityPack


Public Class OCRParser
    Shared doc As HtmlDocument
    Shared hDoc As hDocument
    Shared currentPage As hPage
    Shared currentPara As hParagraph
    Shared currentLine As hLine

    Public Shared Function ParseHOCR(hOrcDoc As hDocument, hOcrFile As String, Append As Boolean) As hDocument
        hDoc = hOrcDoc

        If (IsNothing(doc)) Then
            doc = New HtmlDocument()
        End If

        currentPage = Nothing
        currentPara = Nothing
        currentLine = Nothing

        doc.LoadHtml(hOcrFile)

        Dim body As HtmlNode = doc.DocumentNode.SelectNodes("//body")(0)
        hDoc.ClassName = "body"
        Dim nodes As HtmlNodeCollection = body.SelectNodes("//div[@class='ocr_page']")

        ParseNodes(nodes)

        Return hDoc
    End Function

    Private Shared Sub ParseNodes(nodes As HtmlNodeCollection)

        For Each node As HtmlNode In nodes
            If node.HasAttributes Then
                Dim className As String = String.Empty
                Dim title As String = String.Empty
                Dim id As String = String.Empty

                If Not IsNothing(node.Attributes("class")) Then
                    className = node.Attributes("class").Value
                End If
                If Not IsNothing(node.Attributes("title")) Then
                    title = node.Attributes("title").Value
                End If
                If Not IsNothing(node.Attributes("Id")) Then
                    id = node.Attributes("Id").Value
                End If

                Select Case className
                    Case "ocr_page"
                        currentPage = New hPage()
                        currentPage.ClassName = className
                        currentPage.Id = id
                        ParseTitle(title, currentPage)
                        currentPage.Text = node.InnerText
                        hDoc.Pages.Add(currentPage)

                    Case "ocr_par"
                        currentPara = New hParagraph()
                        currentPara.ClassName = className
                        currentPara.Id = id
                        ParseTitle(title, currentPara)
                        currentPara.Text = node.InnerText
                        currentPage.Paragraphs.Add(currentPara)

                    Case "ocr_line"
                        currentLine = New hLine()
                        currentLine.ClassName = className
                        currentLine.Id = id
                        ParseTitle(title, currentLine)
                        currentLine.Text = node.InnerText
                        If IsNothing(currentPage) Then
                            currentPage = New hPage()
                        End If
                        If IsNothing(currentPara) Then
                            currentPara = New hParagraph()
                            currentPage.Paragraphs.Add(currentPara)
                        End If
                        currentPara.Lines.Add(currentLine)


                    Case "ocrx_word"
                        Dim w As hWord = New hWord()
                        w.ClassName = className
                        w.Id = id
                        ParseTitle(title, w)
                        w.Text = node.InnerText
                        currentLine.Words.Add(w)

                    Case "ocr_word"
                        Dim w1 As hWord = New hWord()
                        w1.ClassName = className
                        w1.Id = id
                        ParseTitle(title, w1)
                        w1.Text = node.InnerText
                        currentLine.Words.Add(w1)
                End Select
            End If
            ParseNodes(node.ChildNodes)
        Next
    End Sub

    Private Shared Sub ParseTitle(Title As String, ocrclass As HOcrClass)
        If String.IsNullOrEmpty(Title) Then
            Exit Sub
        End If

        Dim values As String() = Title.Split(New Char() {";"})

        For Each s As String In values
            If s.Contains("ppageno") Then
                Dim frame As Integer = 0
                If Int32.TryParse(s.Replace("ppageno", ""), frame) Then
                    currentPage.ImageFrameNumber = frame
                End If
            End If

            If s.Contains("bbox") Then
                Dim coords As String = s.Replace("bbox", "")
                Dim box As BBox = New BBox(coords)
                ocrclass.BBox = box
            End If
        Next
    End Sub
End Class
