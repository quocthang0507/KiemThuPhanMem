﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace WideWorldImporters.API.Models
{
#pragma warning disable CS1591

	public interface IResponse
	{
		string Message { get; set; }

		bool DidError { get; set; }

		string ErrorMessage { get; set; }
	}

	/// <summary>
	/// ISingleResponse đại diện cho phản hồi trả về một thực thể
	/// </summary>
	/// <typeparam name="TModel"></typeparam>
	public interface ISingleResponse<TModel> : IResponse
	{
		TModel Model { get; set; }
	}

	/// <summary>
	/// IListResponse đại diện cho phản hồi trả về một danh sách các thực thể, không phân trang
	/// </summary>
	/// <typeparam name="TModel"></typeparam>
	public interface IListResponse<TModel> : IResponse
	{
		IEnumerable<TModel> Model { get; set; }
	}

	/// <summary>
	/// IPagedResponse trả về một danh sách các thực thể có phân trang
	/// </summary>
	/// <typeparam name="TModel"></typeparam>
	public interface IPagedResponse<TModel> : IListResponse<TModel>
	{
		int ItemsCount { get; set; }

		double PageCount { get; }
	}

	public class Response : IResponse
	{
		public string Message { get; set; }

		public bool DidError { get; set; }

		public string ErrorMessage { get; set; }
	}

	public class SingleResponse<TModel> : ISingleResponse<TModel>
	{
		public string Message { get; set; }

		public bool DidError { get; set; }

		public string ErrorMessage { get; set; }

		public TModel Model { get; set; }
	}

	public class ListResponse<TModel> : IListResponse<TModel>
	{
		public string Message { get; set; }

		public bool DidError { get; set; }

		public string ErrorMessage { get; set; }

		public IEnumerable<TModel> Model { get; set; }
	}

	public class PagedResponse<TModel> : IPagedResponse<TModel>
	{
		public string Message { get; set; }

		public bool DidError { get; set; }

		public string ErrorMessage { get; set; }

		public IEnumerable<TModel> Model { get; set; }

		public int PageSize { get; set; }

		public int PageNumber { get; set; }

		public int ItemsCount { get; set; }

		public double PageCount
			=> ItemsCount < PageSize ? 1 : (int)(((double)ItemsCount / PageSize) + 1);
	}

	/// <summary>
	/// ResponseExtensions chuyển đổi một phản hồi thành phản hồi Http, trả về InternalServerError (500), OK (200), NotFound (404), NoContent (204)
	/// </summary>
	public static class ResponseExtensions
	{
		public static IActionResult ToHttpResponse(this IResponse response)
		{
			var status = response.DidError ? HttpStatusCode.InternalServerError : HttpStatusCode.OK;

			return new ObjectResult(response)
			{
				StatusCode = (int)status
			};
		}

		public static IActionResult ToHttpResponse<TModel>(this ISingleResponse<TModel> response)
		{
			var status = HttpStatusCode.OK;

			if (response.DidError)
				status = HttpStatusCode.InternalServerError;
			else if (response.Model == null)
				status = HttpStatusCode.NotFound;

			return new ObjectResult(response)
			{
				StatusCode = (int)status
			};
		}

		public static IActionResult ToHttpResponse<TModel>(this IListResponse<TModel> response)
		{
			var status = HttpStatusCode.OK;

			if (response.DidError)
				status = HttpStatusCode.InternalServerError;
			else if (response.Model == null)
				status = HttpStatusCode.NoContent;

			return new ObjectResult(response)
			{
				StatusCode = (int)status
			};
		}
	}
#pragma warning restore CS1591

}
