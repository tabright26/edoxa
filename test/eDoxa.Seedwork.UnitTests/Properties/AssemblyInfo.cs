// Filename: AssemblyInfo.cs
// Date Created: 2019-07-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: Parallelize(Scope = ExecutionScope.MethodLevel, Workers = 0)]
