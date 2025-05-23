﻿namespace OnlineSchoolForKids.API.Helpers;

public class PaginationResponse<T>
{
	public int PageSize { get; set; }
	public int PageIndex { get; set; }
	public int Count { get; set; }

	public IReadOnlyList<T> Data { get; set; }

	public PaginationResponse(int pageSize, int pageIndex, int count, IReadOnlyList<T> data)
	{
		PageSize = pageSize;
		PageIndex = pageIndex;
		Count = count;
		Data = data;
	}
}
