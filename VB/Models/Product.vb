Imports System.ComponentModel.DataAnnotations

Public Class Product
	Public Property ProductID() As Integer
	<Required(ErrorMessage := "Product Name is required")>
	Public Property ProductName() As String
	Public Property UnitPrice() As Decimal?
	Public Property UnitsOnOrder() As Short?
End Class
