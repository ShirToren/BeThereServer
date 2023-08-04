﻿using System;
using BeTherServer.Models;

namespace BeTherServer.Services.Utils
{
	public class ResultUnit<T>
	{
		private bool m_IsSuccess = true;

		public bool IsSuccess
        {
			get { return m_IsSuccess; }
			set { m_IsSuccess = value; }
		}

        public string? ResultMessage { get; set; }
		public T? ReturnValue { get; set; } = default!;

	}
}

