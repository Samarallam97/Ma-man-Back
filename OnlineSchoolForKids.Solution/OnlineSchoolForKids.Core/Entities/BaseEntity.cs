﻿namespace OnlineSchoolForKids.Core.Entities;

public class BaseEntity
{
	public string Id { get; set; } = Guid.NewGuid().ToString();

}
