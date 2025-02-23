## Fullstack Sharp Framework - React/Blazor alternative

### Build end to end web applications using C# only

Fullstack Sharp Framework is a set of libraries, utilities and guidelines for building Single Page web applications using only C#, ASP.Net and Visual Studio, no JS framework required and also no Blazor/razor pages.

The main component of the framework is SharpHtml, an inhouse library for creating user interfaces that generates HTML from C# code in the form of SSR pages or fragments of page. Coding with SharpHtml resembles very much the HTML that it generates and is in some ways similar with how React coding is done: pseudo HTML mixed with actual code, functions that act as reusable components, AJAX calls. However, unlike React, there is no state management, no hooks, no props, no npm, no nodejs.

SharpHtml uses two third party JavaScript libraries with distinct and very important roles: HTMX(https://htmx.org/) and Alpine.js(https://alpinejs.dev/).

HTMX is in charge of AJAX requests and two-way communication between browser and server: data from browser to server and HTML from server to browser.

AlpineJs is handling client interactivity and bidirectional data-binding in the browser.

The usage of these libraries is almost entirely transparent for the user and actual JavaScript code required is minimal or even nonexistent. Total size of these two libraries is very small and because there are no other dependencies the web pages load very fast.

## How it works:

## Prerequisites

Web pages created with the framework must include scripts to install HTMX and AlpineJs libraries, usually in the Head section of the web page.
See RealWorldSharp AppHead.cs file for an example.

## 1. Coding with SharpHtml 

First step in creating a web application is designing and creating web pages. This is done using SharpHtml library. 

Coding with HtmlSharp is very much like working with actual HTML and is in some ways similar with how React coding is done: pseudo HTML mixed with  code, functions that act as reusable components, AJAX calls. However, unlike React, there is no state management, no hooks, no props, no npm, no nodejs.  
In SharpHtml, every HTML element has a corresponding function which ultimately generates the exact HTML tag that is targeting.  
For example:
    
>    public static HtmlElement div(…)

will generate the `<div>` element.

>    public static HtmlElement ul(…)

will generate `<ul>` element and so on.
	
Most functions have two arguments: first one is the attributes of the element (can be null) and the second is a list of nested elements (using params keyword). When rendered, they will generate the element tag, the attributes and all nested elements.  
Ex:

    public static HtmlElement div(HtmlAttributes? attrs = null, params HtmlElement[] elements)

A piece of code can look like this:

    div(new() { className = "some class", id = “ID1” },
        h1(new() { className = "text-xs-center" }, "Sign in"
        )
    )

> The first argument is an HtmlAttributes object instantiated and initialized on the fly. Every HTML attribute is a property of the HtmlAttributes class. So this element has 2 attributes: a class and an ID.

> The second argument is a list of two nested elements: a `<h1>` and a text.

This code will generate this HTML:

    <div class="some class" id="ID1"> <h1 class="text-xs-center">Sign in</h1>
    </div>

Functions are nested/grouped so that they will generate full pages or fragments of page (as small as a single `<button>` for example). Such groups are then placed inside regular C# functions which act as reusable components: can be called and reused everywhere, including in other components.
Ex:

    public static HtmlElement LoginPage()
    {
        return
        div(new() { className = "some class", id = “ID1” },
            h1(new() { className = "text-xs-center" }, "Sign in"
            )
        )
    }

Function LoginPage returns a HTML div element that can be included in other pages or can be rendered and sent directly to browser.

This approach has considerable advantages as opposed to working with real HTML. Perhaps the biggest one is that you can break large pages into smaller, much easier to manage pieces. These pieces can be reused in other parts of your application, or can be shared with other developers as re-useable components.

Also, using HtmlAttributes has some big advantages:
-	Type safe HTML attributes
-	Attributes can be any type: string, tuple, bool are used but any other type is possible
-	Synthetic attributes: group of attributes set once

If the element has no attributes, you must pass “null” in its place. Alternatively you can use the special null attribute : “_”, which is almost exclusively used in RealWorldSharp instead of null. 

As an alternative to manually coding every element, an utility (called HtmlConverter) is provided that will take a piece of existing HTML and will generate C# code. You can design the pages in your favorite web designer, copy the produced HTML and paste into HtmlConverter and you have now the C# code to continue with the development.  
Actually this is how the pages in RealWorldSharp app were produced: the HTML code was copied from the provided templates and the C# code was generated for me.

Note that not all HTML tags are yet implemented in SharpHtml library. If you encounter one that is missing you can add to your local copy of SharpHtml, or raise an Issue in GitHub and I will add it.

## 2. Adding data

Data is added to pages using attributes. For this, AlpineJS library is used, specifically the attributes coming with it.  
The attribute x-data (xData in C#) is made precisely for this: <https://alpinejs.dev/directives/data>  
It contains a JavaScript object literal ([JSOL](https://playcode.io/javascript/object-literal)) that can be accessed by nested elements to display or edit data. This is done using two more attributes:  
> x-text (xText): <https://alpinejs.dev/directives/text> - used to display data (readonly)

> x-model (xModel): <https://alpinejs.dev/directives/model> - used to edit data

For a typical SharpHtml component, this mechanism is totally automatic and wrapped in C#, no JS is required. This is done using a helper class called JSBuilder which converts a C# class into a JSOL and provides type safe access to its members.  
This is the equivalent of data aware programming in WinForms with the HTML elements as data aware controls and x-data as data source.

Ex:

    public static HtmlElement LoginPage(LoginModel login) // login is data passed as argument to this component
    {
        var js = new JSBuilder<LoginModel>(login); // JSBuilder created using the single item constructor
                                                   // there is also a constructor for a List
        var xdata = js.Build(); // JSOL created here

       return
       div(new() { xData = xdata }, // JSOL passed to x-data attribute
		label(new() { xText = js.Field(x => x.Email) }  //  x-text attribute is set to Email field from LoginModel class;  
                                                                // every time Email changes, the text of this label will also change
								//note that there is no innerText for this element
		)

		input(new() { xModel = js.Field(x => x.Email) } // x-model is set to Email field from LoginModel class;  
                                                        	// every change made by the user will update Email field which in turn will update any element bound to this field
								//note that there is no innerText for this element

		)
        )
      }

While this is a simple but common usage of data, more complex scenarios are possible: use of nested x-data, adding JS functions to x-data, use of x-for (<https://alpinejs.dev/directives/for>) attribute to iterate through lists. These can be found in the RealWorldSharp app, either actively used or just presented as proof of concept.

### More about x-data

If LoginModel is a C# class like this:

    public class LoginModel
    {
                public string Email { get; set; }
                public string Password { get; set; }
    }

Then x-data created by JSBuilder is a string that looks like this:

> item: {Email: "user@google.com", Password: "abc"}

It’s a JSOL with one variable (item) which is initialized with the provided values of the LoginModel.  
x-text/x-model contains the name of the field that is bound to that HTML element. For ex, in HTML it will look like this:
> x-model="item.Email"

JSBuilder provides a field accessor function so the attributes can be set typesafe, with intelisense, without using strings. The above HTML expression becomes in C#:
> xModel = js.Field(x => x.Email)

The field accessor function works with nested objects too, so you can do for ex:
> xModel = js.Field(x => x.SomeComplexField.NestedField)

JSBuilder has functionality to change the default variable name (“item”) to something else. You can also add additional JS code (in the form of C# strings) to x-data.

However not all data is processed using x-data. Only data that is handled and changed by the client (for ex. via x-model) is using x-data. We call this client data. Ex: Login credentials, article content etc.
Some data is not handled by client, is actually determined by the context and is rendered by server. We call this server data. Ex: Favorite count, Follow count.  
The difference is very important because client data (x-data) and server data are processed differently.

## 3. Saving data

After data is updated, it’s time to send it to server for save.  
This is done using a Button action and HTMX attributes or a combination of HTMX and AlpineJs attributes. (But note that any HTML element can be used, it's just that Buttons are the most familiar).  
First of all, data must be some sort of serialized JSON. That means that server data must be a class, even if it has only one member. Client data (x-data) is already JSON but must be handled is a specific way, as seen below.  
In order to save data, several attributes must be set:

The first one is
> hx-post (hxPost): <https://htmx.org/attributes/hx-post/> : the url of the endpoint that will process the action (you can also use hx-put or hx-patch)

The next one depends if data is server data or client data.  
If data is server data, a synthetic attribute called jsonVals is used:
> jsonVals

The value of this attribute is the result of calling JsonConvert.SerializeObject(obj), where obj is an instance of the class defining the data. See FavoriteCounter and FollowCounter components for examples.  

If data is client data, two more attributes are needed:
1. hx-vals: <https://htmx.org/attributes/hx-vals/> : a string containing serialized JSON.

2. x-bind (AlpineJs): <https://alpinejs.dev/directives/bind>  

About x-bind, the HTML format is  
> x-bind:hx-vals=value

or a shorter form:  
> :hx-vals=value

This attribute basically gives access to x-data to other attributes (not part of AlpineJs). In this case it gives access to x-data to hx-vals.
And the value is this JS expression:  
>  `“JSON.stringify($data.item)”. `  

where “$data” is the AlpineJS global name for x-data, “item” is the JSOL variable created by JSBuilder and “JSON.stringify” is the JS way of serializing JSON.  
So the above attribute would look like this in HTML:  
> :hx-vals= JSON.stringify($data.item)

However, instead of setting these two attributes manually, you can use a syntetic attribute called xBindVals, which sets both attributes in one call.
Furthermore, using a JSBuilder property which gets the value from xData, there is only one line of code:
> hxBindVals = js.JsonData

### VERY IMPORTANT

Using hx-vals/jsonVals is not the default way of handling data for HTMX, which uses form data instead. In order for hx-vals to work, a custom extension (<https://htmx.org/extensions/>) called “hx-noformdata” was added in the Head of the HTML document.
So, when the above attributes jsonVals and hxBindVals are used, an additional attribute must be set (<https://htmx.org/attributes/hx-ext/>): 
> hx-ext= "hx-noformdata";  

However because they are always used together, the framework automatically add this attribute whenever jsonVals or hxBindVals are used so you don’t have to add it yourself. 

## 4. Navigation and page swapping  

FSS is an SPA building framework. The server, most of the time, is sending only pieces of HTML, not full pages, to the browser, which will replace the existing HTML with the new ones.  
For example, RealWorldSharp app has a header, a footer and some content (called main content) in between. Most navigation inside the site involves only changing the main content, while header and footer remain unchanged. (However the HTML being swapped may be much smaller, maybe as small as a single button or label). Any time an internal link is clicked, only the main content is sent to browser, replacing the existing content.  
This is called HTML swapping and is accomplished using the HTMX library. 
For more details of how swapping works, please see this: <https://htmx.org/docs/>  
With SharpHtml this is done by setting two attributes of the element that triggers the navigation (usually an anchor (`<a>` tag) or a button, but any element can be used for this):
> hx-target (hxTarget) <https://htmx.org/attributes/hx-target/>: set to the ID of the element that must be replaced. The actual format is:  
    `hx-target = #ID`, note that the ID must be prefixed with # char

> hx-swap (hxSwap): set to the method used to swap. For details see: <https://htmx.org/attributes/hx-swap/>

While these attributes can be used any time, HTMX has another attribute to simplify things: 
> hx-boost (<https://htmx.org/attributes/hx-boost/>).

When this is set on some element, the other two attributes mentioned above don’t have to be used for any nested anchor , they will use the target and swap indicated by the boost set on parent. So for example you can set it at the top of main content element (as is done in RealWorldSharp) and benefit almost everywhere.  
Note though that there is a catch when using hx-boost. If a nested element is a form and the form doesn’t have any anchors or submit buttons, HTMX will signal an error. For this reason, in RealWorldSharp, any element with a form has hx-boost disabled (hx-boost = false). 

Of course, any anchor element usually also needs a href attribute.  
If the boost mechanism is not used, hx-get attribute (<https://htmx.org/attributes/hx-get/>) must be used together with href, both pointing to same link (Technically when hx-get is used, href is not required, however using it will make the anchor look normal when hover: cursor change, underline etc).  
Links can be fixed (constant) or dynamic which in turn can be: server generated (C# expression but constant at runtime) or client generated (from x-data).  
Fixed and server links are just regular C# strings.  
However client links have a special format and need one more attribute:
1)	Format  
For example for this link pointing to “/profile/{username}”, the link is like this:  
> $"'{Routes.Profile}' + {js.Field(x => x.Author.Username)}"

This is actually a JS expression with several parts, some fixed and some variable:  
> Routes.Profile is a C# constant and is the fixed part. It must be surrounded by single quotes  

> js.Field(x => x.Author.Username) is the JSBuilder property expression that will return the Username from x-data  

> The parts are separated by the “+” sign which will be processed by JS in the browser, concatenating the two parts.  

A link can have more than two parts but all must follow the same rules: constant parts surrounded by single quotes and all parts separated by “+”

2)	In order to use x-data, the x-bind attribute must be used, binding the href attribute:  
HTML:  
  	`:href = link`

To do this in C#, you can do

    xBind = (“href”, link)
or use the synthetic attribute xBindRef:
    
    xBindRef = link

RealWorldSharp app doesn’t use client links, however there is a proof of concept function called ArticleListAlternate in file ArticleList.cs which shows how shd be done.

Sometimes more than one piece of HTML must be swapped. In this case, the server prepares the required pieces and send them together, side-by-side. 
In RealWorldSharp there is function called RenderPages in UIBuilder.cs which does exactly this.  
Only one piece is swapped normally (using hx-target). The rest use a mechanism called Out of Band swap (https://htmx.org/attributes/hx-swap-oob/) and must have the attribute hx-swap-oob = “true” (hxOob = true in HtmlSharp). Look for example in FollowHandler.cs to see how is done. 

### IMPORTANT

Because this is a SPA, page swapping usually involves only fractions of the full page.
However a full page must be sent to browser in at least 2 cases:
1. When the site is first accessed
2. When an internal link is opened in a new tab/window

To find out when one of the two cases above is happening, HTTP headers must be consulted. If they include the header "HX-Request" (see <https://htmx.org/reference/#request_headers>), you are not in one of these cases. Otherwise, the full page must be sent to browser .
RealWorldSharp app implements this mechanism in the backend.

## 5. Backend

The backend is using Minimal API , EF Core and a Command - CommandHandler architecture. 
It has the following components:
-	Endpoints:
    -	Handle the requests from web page MinimalAPI endpoints: MapGet, MapPost etc. 
    -	Inject (actually uses the injected) main service and request parameters
-	RealWorldService - the main service:
    -	Injects or creates and initializes all the other components used by the backend
    -	Sets up the Htmx flag (true for internal links, false when the link is opened in different tab/window)
    -	Creates the repository
    -	Creates the authentication service
    -	Sets up the current user
    -	Creates and initializes the Commands
    -	Creates, initializes and runs the CommandHandlers 
    -	Renders a web page/fragment which is returned to the browser by the ASP NET system
    -	Provides automatic logging, exception handling and profiling
-	Commands: keep the request parameters, the html produced as a result of the request and additional data
-	CommandHandlers: do the actual processing of the requests; designed to be autonomous pieces of code, only depending on lower level services (data, UIBuilder); this way they are better managed and can be tested in isolation
-	AuthService: authentication service
-	UIBuilder: assembles various parts of the page and renders the final HTML sent to the browser
-	Repository: data storage faced service. It’s using EFCore but it can be easily changed to other ORM

A few words about testing.  
All tests are done against CommandHandlers. There are 3 types of testing:
-	Visual testing: displays the HTML sent to the browser. Can be used before most of the backend is implemented, it just needs a Command and the CommandHandler. Save the HTML from Command.Result somewhere and display in your current browser. Make sure that HTML includes the document HEAD and deploy the local css (if any) to same location where you place the HTML
-	Unit testing: unit tests the web pages. Similar to unit testing in React. Unfortunately there is no testing library in C# for this, something like react-testing-library. I used HtmlAgilityPack (<https://html-agility-pack.net/>) to help creating some tests.
-	Integration testing: tests the data /repository used by web pages


## 6. Putting all together

All of the above may look a bit complicated a first but in the end it's mostly just a matter of setting some attributes and taking care of some quirks here and there.

The process of creating a web app, page by page, is like this:
-	Design and implement your page in C# using SharpHtml
-	Alternatively, design you page in a separate web designer app or copy the html from other web site or template and generate C# code using HtmlConverter app
-	Create the command handler that renders the page
-	Test the page using the CommandBase.Result which contains the page’s html. You can load the page in a browser or do unit tests before the rest of the app is even started
-	Add data
-	Add endpoints and the rest of the backend



