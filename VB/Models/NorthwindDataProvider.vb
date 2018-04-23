Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.Configuration

Public Class NorthwindDataProvider
	Public Shared Function GetProducts() As IEnumerable
		Dim products As New List(Of Product)()

		Using connection As New OleDbConnection(WebConfigurationManager.ConnectionStrings("Northwind").ConnectionString)
			Dim selectCommand As New OleDbCommand("SELECT * FROM Products ORDER BY ProductID", connection)

			connection.Open()

			Dim reader As OleDbDataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection)

			Do While reader.Read()
                products.Add(New Product() With {.ProductID = DirectCast(reader("ProductID"), Integer), .ProductName = DirectCast(reader("ProductName"), String), .UnitPrice = (If(reader("UnitPrice") Is DBNull.Value, Nothing, DirectCast(reader("UnitPrice"), Decimal))), .UnitsOnOrder = (If(reader("UnitsOnOrder") Is DBNull.Value, Nothing, DirectCast(reader("UnitsOnOrder"), Short?)))})
			Loop

			reader.Close()
		End Using

		Return products
	End Function

	Public Shared Sub InsertProduct(ByVal product As Product)
		Using connection As New OleDbConnection(WebConfigurationManager.ConnectionStrings("Northwind").ConnectionString)
			Dim insertCommand As New OleDbCommand("INSERT INTO [Products] ([ProductName], [UnitPrice], [UnitsOnOrder]) VALUES (?, ?, ?)", connection)

			insertCommand.Parameters.AddWithValue("ProductName", product.ProductName)

			If product.UnitPrice.HasValue Then
				insertCommand.Parameters.AddWithValue("UnitPrice", product.UnitPrice)
			Else
				insertCommand.Parameters.AddWithValue("UnitPrice", DBNull.Value)
			End If

			If product.UnitsOnOrder.HasValue Then
				insertCommand.Parameters.AddWithValue("UnitsOnOrder", product.UnitsOnOrder)
			Else
				insertCommand.Parameters.AddWithValue("UnitsOnOrder", DBNull.Value)
			End If

			connection.Open()
			insertCommand.ExecuteNonQuery()
		End Using
	End Sub

	Public Shared Sub UpdateProduct(ByVal product As Product)
		Using connection As New OleDbConnection(WebConfigurationManager.ConnectionStrings("Northwind").ConnectionString)
			Dim updateCommand As New OleDbCommand("UPDATE [Products] SET [ProductName] = ?, [UnitPrice] = ?, [UnitsOnOrder] = ? WHERE [ProductID] = ?", connection)

			updateCommand.Parameters.AddWithValue("ProductName", product.ProductName)
			updateCommand.Parameters.AddWithValue("UnitPrice", product.UnitPrice)
			updateCommand.Parameters.AddWithValue("UnitsOnOrder", product.UnitsOnOrder)
			updateCommand.Parameters.AddWithValue("ProductID", product.ProductID)

			connection.Open()
			updateCommand.ExecuteNonQuery()
		End Using
	End Sub

	Public Shared Sub DeleteProduct(ByVal productID As Integer)
		Using connection As New OleDbConnection(WebConfigurationManager.ConnectionStrings("Northwind").ConnectionString)
			Dim deleteCommand As New OleDbCommand("DELETE FROM Products WHERE ProductID = " & productID.ToString(), connection)

			connection.Open()
			deleteCommand.ExecuteNonQuery()
		End Using
	End Sub

	Public Shared Sub UpdateProductUnitPrice(ByVal productID As Integer, ByVal unitPrice As Decimal)
		Using connection As New OleDbConnection(WebConfigurationManager.ConnectionStrings("Northwind").ConnectionString)
			Dim updateCommand As New OleDbCommand("UPDATE [Products] SET [UnitPrice] = ? WHERE [ProductID] = ?", connection)

			updateCommand.Parameters.AddWithValue("UnitPrice", unitPrice)
			updateCommand.Parameters.AddWithValue("ProductID", productID)

			connection.Open()
			updateCommand.ExecuteNonQuery()
		End Using
	End Sub

End Class
