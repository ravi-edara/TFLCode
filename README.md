TFL Code Challenge:
--------------------



1) Made into three individual projects for code, unit test and acceptance tests.
2) all can run on command line. (Assumption - D:\Ravi is the drive location where code has been cloned from Github)
	o	How to build the code:
			cd TFLCodeChallenge
			cd TFLCodeChallenge.Console
			dotnet build

	o	How to run the output
			dotnet run
			   The above will prompt as 'Enter Road id'
					Enter the data as 'A2'
					O/P: 
								Road Display name  is A2
					            Road Status is Good
								Road Status Description is No Exceptional Delays
								----> Enter 'Y' to continue, any other key to exit <----
					   enter 'Y' to continue which repeats from line 12.
			(Tested with A233,A406)

	o	How to run any tests that you have written
		   
		   Unit tests:
		   -----------

		   Be in the path 'D:\Ravi\TFLCodeChallenge'
		   cd TFLCodeChallenge.Tests.Unit
		   dotnet test
		      you should see the last line after executing the above statement as 
			  'Passed!  - Failed:     0, Passed:     5, Skipped:     0, Total:     5, 
			     Duration: 4 s - TFLCodeChallenge.Tests.Unit.dll (netcoreapp3.1)'
		   
		  Assumptions: 
		    Unit tests were targeted just to test the service method 'GetRoadStatusById' only, rather than Main function in program.cs file. Made an effort in the code to 
			demonstrate the dependency injection, configuring services, using interfaces in unit testing. 

		  Acceptance tests:
		  -----------------
		  Be in the path 'D:\Ravi\TFLCodeChallenge' (cd..)
		  cd TFLCodeChallenge.Tests.Acceptance
		  dotnet test
			you should see the last line after executing the above statement as 
			 'Passed!  - Failed:     0, Passed:     4, Skipped:     0, Total:     4, Duration: 1 s - TFLCodeChallenge.Tests.Acceptance.dll (net6.0)

		  Assumptions:
			Taken the same scenarios mentioned in the challenge document.

