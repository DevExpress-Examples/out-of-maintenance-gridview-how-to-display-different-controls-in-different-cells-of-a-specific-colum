@ModelType System.Collections.IEnumerable

@Html.DevExpress().GridView( _
    Sub(settings)
            settings.Name = "GridView"
            settings.KeyFieldName = "ProductID"
            
            settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "GridViewPartial"}
            settings.CustomActionRouteValues = New With {.Controller = "Home", .Action = "GridViewCallbackPartial"}
            settings.SettingsEditing.AddNewRowRouteValues = New With {.Controller = "Home", .Action = "GridViewAddNewPartial"}
            settings.SettingsEditing.UpdateRowRouteValues = New With {.Controller = "Home", .Action = "GridViewUpdatePartial"}
            settings.SettingsEditing.DeleteRowRouteValues = New With {.Controller = "Home", .Action = "GridViewDeletePartial"}
   
            settings.CommandColumn.Visible = True
            settings.CommandColumn.ShowEditButton = True
            settings.CommandColumn.ShowNewButton = True
            settings.CommandColumn.ShowDeleteButton = True

            settings.Columns.Add( _
                Sub(column)
                        column.FieldName = "ProductID"
                        column.ReadOnly = True
                        column.EditFormSettings.Visible = DefaultBoolean.False
                End Sub)
            
            settings.Columns.Add("ProductName")

            settings.Columns.Add( _
    Sub(column)
            column.FieldName = "UnitPrice"
            column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left
            column.CellStyle.BackColor = System.Drawing.Color.LightGray
            column.CellStyle.Paddings.Padding = 0

            column.SetDataItemTemplateContent( _
                Sub(container)
                        Dim units As Integer = Convert.ToInt32(DataBinder.Eval(container.DataItem, "UnitsOnOrder"))
                            
                        If units = 0 Then
                            Html.DevExpress().Label( _
                                Sub(label)
                                        label.Name = "Label" + container.KeyValue.ToString()
                                        label.Width = 100
                                End Sub).Bind(DataBinder.Eval(container.DataItem, "UnitPrice")).GetHtml()
                        Else
                            Html.DevExpress().SpinEdit( _
                                Sub(spinEdit)
                                        spinEdit.Name = "SpinEdit" + container.KeyValue.ToString()
                                        spinEdit.Width = 100
                                        spinEdit.Properties.ClientSideEvents.LostFocus = String.Format("function(s, e) {{ GridView.PerformCallback({{key:  {0}, unitPrice: s.GetValue()}}); }}", container.KeyValue)
                                End Sub).Bind(DataBinder.Eval(container.DataItem, "UnitPrice")).GetHtml()
                        End If
                End Sub)
    End Sub)
            
            settings.Columns.Add("UnitsOnOrder")
    End Sub).SetEditErrorText(CType(ViewData("EditError"), String)).Bind(Model).GetHtml()