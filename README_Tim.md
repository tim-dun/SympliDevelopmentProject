# Finished Product Screenshots
	- See image files included in Screenshots folder

# Super-Simple Test Suite

	- Testing Using Dependency Injection
		- I have added dependency injection to the project to allow search engine "get" requests to be read from text (html) files included in the project rather than an HttpClient. This was done for the following reasons:-
			- To provide consistent results during development
			- To reduce the number of requests made to search engines in order to avoid being blocked (no robots)
			- To provide a means of performing unit testing
		- In order to run a unit test using included text files:-
			- Add a Singleton Service when the Web Application is built injecting class SearchEngineResponseFile:-
				- builder.Services.AddSingleton<ISearchEngineResponse, SearchEngineResponseFile>();
			- Result:-
				    // Google: 2 (from top 100 listing/s)
					// Bing: 4 (from top 4 listing/s)
			- The correct class SearchEngineResponseWeb should be injected (reverted) in a production environment:-
				- builder.Services.AddSingleton<ISearchEngineResponse, SearchEngineResponseWeb>();

# How to test / demo / run

	- Given I cannot publish this application to a non-Production web server on the Sympli network the best way for somebody to test it is to run it in Visual Studio in debug mode. Load the project, build it, then run it using default web server IISExpress / Kestrel.
	- Memory caching is in place and is currently set to 60 minutes. During testing it would be advisable to reduce this number to 1 by updating the following line of code in the SearchController class GetResult method:-
		- int memoryCacheMinutes = 60; // Can be reduced for unit testing
	- The project profile currently sets ASPNETCORE_ENVIRONMENT to "Development". This causes the application to be built using Swagger and to launch it on start up. Testing can then be carried out by interacting with Swagger:- 
		- Swagger opens in a web browser session on start up
		- Click "GET" to expand the Get controller section
		- Click "Try it out" to allow for the input of text in controls
		- Type "e-settlements" in to text box "keywords"
		- Click "Execute" to send a request
		- Inspect the response displayed in "Response body"
	- Alternatively a "get" requests can be sent from a new web browser session
		- Open a new browser session
		- Enter the following URI:-	
			- http://localhost:5180/Search/keywords?keywords=e-settlements
		- The response will be written to the browser

# Documentation

	- This application receives a "Get" request from a user taking a string "keywords" 
	- The application stores metadata for one or more search engines. For each search engine it then issues a "Get" request searching for the string passed in using string "keywords"
	- The "get" request issued to each engine includes a maximum number of listings to be returned. This value is set internally to 100
	- It parses the response body html and creates a list of all the listings found
	- For each listing where the web server host is "sympli.com.au" it compiles a comma separated list of each listing's rank. Each engine result is prepended with the name of the search engine to distinguish it in the list of results
	- Each search engine result is suffixed with the total number of listings returned. While the number requested is set to 100 this does not guarantee that 100 will be returned. Displaying the total number makes the user aware of how successful the application was in retrieving up to 100 listings
	- Memory caching has been implemented in the application and is currently set to 60 minutes. For each new keyword seen the result will be cached. If the same keyword is seen within 60 minutes the cache will be used to return a result. When 60 minutes have passed the cache will expire and a keyword that has already been seen will be searched using an HttpClient request for each search engine
	- Results are sent back to the calling client

# Additions / Enhancements

	- Add exception handling throughout
	- Load start up, environment and static application values using the standard configuration .json files
	- Enhance the Listing class:-
		- To store a list of listings keyed by engine. 
		- To return the result using a single method rather than concatenating a string whilst looping each search engine
	- Use the search engine provider APIs to perform searching
	- Use Html Agility Pack to parse html responses

# Supplementary Information

	- Most major search engines explicitly prohibit web scraping applications such as this one. This includes Google and Bing. They do allow for consumers to access their APIs to perform searches however
	- Search engines exposing APIs for consumption publish guides on how to form requests in order to receive good responses. Google and Bing do not publish guides on how to construct search uris to be used outside their APIs. Attempts to do so generate erratic results
	- While I was able to construct a search uri that Google responded to favourably I was less successful with Bing (and DuckDuckGo). I spent some time trying but was not successful. (I ran out of time which is why I did not add exception handling and .json configuration loading)
	- Several years ago I wrote an application that compiles property sales data for NSW. I spent time attempting to read web pages from sites such as www.realestate.com.au only to conclude that such entities really don't want robots scraping their data! The application aggregates data from 3 different sources:- 
		- www.onthehouse.com.au (web scraper - does allow scraping with throttling)
		- www.valuergeneral.nsw.gov.au (batch files)
		- G-NAF database (Australian address database - free to download)
	- If I were to build this application for real I would want to do so using the recommended APIs
	- I would also recommend using the Http Agility Pack. Html text is notoriously difficult to parse. Which is probably why Microsoft have not invested time in including their own Html class in .NET. Despite having created one for XML documents. But hey that's the beauty of OOPs like C#. Provide the foundation then let someone else come along and build the house!
