﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace mhcb.syd.DataService
{
	public class AuthTokenOperation : IDocumentFilter
	{
		void IDocumentFilter.Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
		{
			//throw new NotImplementedException();
			swaggerDoc.paths.Add("/token", new PathItem
			{
				post = new Operation
				{
					tags = new List<string> { "Auth" },
					consumes = new List<string>
					{
						"application/x-www-form-urlencoded"
					},
					parameters = new List<Parameter>
					{
						new Parameter
						{
							type ="string",
							name = "grant_type",
							required = true,
							@in = "formData",
							@default ="password"
						},
						new Parameter
						{
							type ="string",
							name = "username",
							required = true,
							@in = "formData"
						},
						new Parameter
						{
							type ="string",
							name = "password",
							required = true,
							@in = "formData"
						}


					}
				}
			});
		}
	}
}