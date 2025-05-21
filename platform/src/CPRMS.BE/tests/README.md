# 🧪 Testing Strategy for Our Project

This document outlines our testing approach for ensuring the quality and stability of our system be.

---

## 📌 Testing Scopes

| Test Type        | Scope Tested                               | Purpose                                                  | Run in CI  |
---------------------------------------------------------------------------------------------------------------------------------------------------------
| Unit Test        | Individual classes/methods (App, Domain)   | Test business logic in isolation (no I/O or DB)          | ✅ Yes     |
| Integration Test | Application + Infrastructure               | Verify interaction with DB, file system, services        | ✅ Yes     |
| System Test      | Entire deployed system                     | Validate real-world user flows in deployment environment | ❌ No      |

> 🔸 **Note:** System tests are not part of CI testing — they are run manually in the deployed environment 

## 🧱 Test Responsibility per Layer

| Layer            | Should be Unit Tested  | Integration Tested | 
-------------------------------------------------------------------------------------------
| Domain           | ✅ Yes                 | ❌ Rarely           |   
| Application      | ✅ Yes                 | ✅ Sometimes        |   
| Infrastructure   | ❌ No                  | ✅ Yes              |   
| API              | ❌ No                  | ❌ Optional         |   

## 📌 How to write assign test scope
using trait
assign [Trait("Scope", "Unit")] for unit testing
assign [Trait("Scope", "Integration")] for integration testing
🧪 [Example](./Rms.Application.Tests/ApplicationTestExample.cs) 

## 🧪 Running Tests Locally
go to this path: platform/src/CPRMS.BE/
### all tests 
dotnet test CPRMS.BE.sln
### unit tests only
dotnet test CPRMS.BE.sln --filter "Scope=Unit"
### integration tests only 
dotnet test CPRMS.BE.sln --filter "Scope=Integration"