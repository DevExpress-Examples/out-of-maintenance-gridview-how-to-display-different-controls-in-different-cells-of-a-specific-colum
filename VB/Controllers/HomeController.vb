Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc

Namespace GridDynamicTemplateMvc.Controllers
	Public Class HomeController
		Inherits Controller

		Public Function Index() As ActionResult
			Return View()
		End Function

		Public Function GridViewPartial() As ActionResult
			Return PartialView(NorthwindDataProvider.GetProducts())
		End Function

		Public Function GridViewCallbackPartial(ByVal key As Integer, ByVal unitPrice As Decimal) As ActionResult
			NorthwindDataProvider.UpdateProductUnitPrice(key, unitPrice)

			Return PartialView("GridViewPartial", NorthwindDataProvider.GetProducts())
		End Function

		Public Function GridViewAddNewPartial(ByVal product As Product) As ActionResult
			If ModelState.IsValid Then
				Try
					NorthwindDataProvider.InsertProduct(product)
				Catch e As Exception
					ViewData("EditError") = e.Message
				End Try
			Else
				ViewData("EditError") = "Please, correct all errors."
			End If

			Return PartialView("GridViewPartial", NorthwindDataProvider.GetProducts())
		End Function

		Public Function GridViewUpdatePartial(ByVal product As Product) As ActionResult
			If ModelState.IsValid Then
				Try
					NorthwindDataProvider.UpdateProduct(product)
				Catch e As Exception
					ViewData("EditError") = e.Message
				End Try
			Else
				ViewData("EditError") = "Please, correct all errors."
			End If

			Return PartialView("GridViewPartial", NorthwindDataProvider.GetProducts())
		End Function

		Public Function GridViewDeletePartial(ByVal productID As Integer) As ActionResult
			Try
				NorthwindDataProvider.DeleteProduct(productID)
			Catch e As Exception
				ViewData("EditError") = e.Message
			End Try

			Return PartialView("GridViewPartial", NorthwindDataProvider.GetProducts())
		End Function
	End Class
End Namespace
