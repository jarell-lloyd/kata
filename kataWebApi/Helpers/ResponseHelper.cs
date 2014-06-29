using Microsoft.Data.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace kataWebApi.Helpers
{
	public static class ResponseHelper
	{
		public static HttpResponseException ResourceNotFound(HttpRequestMessage request)
		{
			HttpResponseException httpException;
			HttpResponseMessage response;
			ODataError error;

			error = new ODataError
			{
				Message = "Resource Not Found - 404",
				ErrorCode = "NotFound"
			};

			response = request.CreateResponse(HttpStatusCode.NotFound, error);

			httpException = new HttpResponseException(response);

			return httpException;
		}
	}
}