
1. Coding with HtmlSharp. 

The first step in creating a web application is designing and creating web pages. This is done using HtmlSharp library. 
The code looks very much like real HTML, but it’s using C# functions and classes instead. Every HTML element has a corresponding function which ultimately generates the exact HTML tag that is targeting.
For example:
    
    public static HtmlElement div(…)
will generate the `<div>` element.

    public static HtmlElement ul(…)
will generate `<ul>` element and so on

Almost all functions have two arguments: first one is the attributes of the element (can be null) and the second is a list of nested elements (using params keyword). When rendered, they will generate the element tag, the attributes and all nested elements
Ex:

    public static HtmlElement div(HtmlAttributes? attrs = null, params HtmlElement[] elements)

A piece of code can look like this:

    div(new() { className = "some class", id = “ID1” },
	h1(new() { className = "text-xs-center" }, "Sign in"
        )
    )

The first argument is an HtmlAttributes object instantiated and initialized on the fly. Every HTML attribute is a property of the HtmlAttributes class. So this element has 2 attributes: a class and an ID.
The second argument is a list of two nested elements: a `<h1>` and a text
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

As an alternative to manually coding every element, an utility (called HtmlConvert) is provided that will take a piece of existing HTML and will generate C# code. You can design the pages in your favorite web designer, copy the produced HTML and paste into HtmlConvert and you have now the C# code to continue with the development.
Actually this is how the pages in RealWorld app were produced: the HTML code was copied from the provided templates and the C# code was generated for me.

Using HtmlAttributes has some big advantages:
-	Type safe HTML attributes
-	Attributes can be any type: string, tuple, bool are used but any other type is possible
-	Syntetic attributes: group of attributes set once

If the element has no attributes, you must pass “null” in its place. Alternatively you can use the special null attribute : “_”, which is almost exclusively used in RealWorld instead of null 

2. Adding data.

Data is added to pages using attributes. For this, AlpineJS library is used, specifically the attributes coming with it.
The attribute x-data (xData in c#) is made precisely for this: <https://alpinejs.dev/directives/data>
It contains a JavaScript object literal ([JSOL](https://playcode.io/javascript/object-literal)) that can be accessed by nested elements to display or update. This is done using two more attributes: x-text (xText): <https://alpinejs.dev/directives/text> and x-model (xModel): <https://alpinejs.dev/directives/model>
For a typical C# component, this mechanism is totally automatic and wrapped in C#, no JS is required. This is done using a helper class called JSBuilder which converts a C# class into a JSOL and provides type safe access to its members.
This is the equivalent of data aware programming in WinForms with the HTML elements as data aware controls and x-data as data source.

Ex (note that following examples are not actual code from RealWorld app):

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

While this is a simple but common usage of data, more complex scenario are possible: use of nested x-data, adding JS functions to x-data, use of x-for attribute to iterate through lists. These can be found in the RealWorld app, either actively used or just presented as proof of concept

More about x-data

If LoginModel is a C# class like this:

    public class LoginModel
    {
                public string Email { get; set; } = null!;

                public string Password { get; set; } = null!;
    }

Then x-data created by JSBuilder is a string that looks like this:

item: {Email: null, Password: null}

It’s a JSOL with one variable (item) which is initialized with the provided values of the LoginModel (null in this case)
x-text is actually this: “item.Email”
x-model is: “item.Email”

JSBuilder has functionality to change the default variable (“item”) to something else. You can also add additional JS code (in the form of C# strings) to x-data

However not all data is processed using x-data. Only data that is handled and changed by the client is using x-data (we call this client data). Ex: Login credentials, article content etc.
Some data is not handled by client, is actually determined by the context and is rendered by server (server data). Ex: Favorite count, Follow count. The difference is very important because client data (x-data) and server data are processed differently.

3. Saving data.

After data is updated, it’s time to send it to server for save. This is done using a Button action and HTMX attributes or a combination of HTMX and AlpineJs attributes. (But note that any HTML element can be used, it's just that Buttons are the most familiar).
First of all, data must be some sort of serialized JSON. That means that server data must be a class, even if it has only one member. Client data (x-data) is already JSON but must be handled is a specific way, as seen below
In order to save data, several attributes must be set:

hx-post (hxPost): <https://htmx.org/attributes/hx-post/> : the url of the endpoint that will process the action (you can also use hx-put or hx-patch)
hx-vals(jsonVals): <https://htmx.org/attributes/hx-vals/> : a string containing serialized JSON. 

If data is server data, the value of hx-value is the result of calling JsonConvert.SerializeObject(obj), where obj is an instance of the class defining the data. See FavoriteCounter and FollowCounter components for examples

If data is x-data, an additional attribute is used: x-bind (AlpineJs): <https://alpinejs.dev/directives/bind>
The format of this attribute is  
“x-bind:hx-vals=value” 
or a shorter form:  
“:hx-vals=value”.
This attribute basically gives access to x-data to other attributes (not part of AlpineJs). In this case it gives access to x-data to hx-vals.
And the value is this JS expression: “JSON.stringify($data.item)”. 
“$data” is the AlpineJS global name for x-data, “item” is the JSOL variable created by JSBuilder and “JSON.stringify” is the JS way or serializing JSON.
So the above attribute would look like this:
“:hx-vals= JSON.stringify($data.item)”.

RealWorld app combines some of these attributes which always appear in the same order and have same values into custom (or syntetic) attributes, reducing repetitions and enhancing clarity.

VERY IMPORTANT
Using hx-vals is not the default way of handling data for HTMX. It uses form data instead. In order for hx-vals to work, a custom extension (<https://htmx.org/extensions/>) called “hx-noformdata” was added in the Head of the HTML document. Every time hx-vals is used, an additional attribute must be set (<https://htmx.org/attributes/hx-ext/>): 
hx-ext= "hx-noformdata";
However because they are always used together, the framework automatically add this attribute whenever hx-post is used so you don’t have to add it yourself. You still have to use the extension present in HTML Head


4.Navigation and page swap.

FSS is an SPA building framework. The server is sending only pieces of HTML, not full pages, to the browser, which will replace the existing HTML with the new ones.
For example, RealWorld app has a header, a footer and some content (called main content) in between. Most navigation inside the site involves only changing the main content, while header and footer remain unchanged. 
(However the HTML being swapped may be much smaller, maybe as small as a single button or label)
So any time an internal link is clicked, only the main content is sent to browser, replacing the existing content. This is called HTML swapping and is accomplished using the HTMX library. 
For more details of how swapping works, please see this: <https://htmx.org/docs/>
Using the library means setting two attributes of the element that triggers the navigation: usually an anchor (`<a>` tag) or a button, but any element can be used for this
1)	hx-target (hxTarget): set to the ID of the element that must be replaced. However the actual format is: hx-target = #ID, note that the ID must be prefixed with # char
2)	hx-swap (hxSwap): set to the method used to swap. For details see: link

While these attributes can be used any time, HTMX has another attribute to simplify things: hx-boost (<https://htmx.org/attributes/hx-boost/>). When this is set on some element, the other two attributes mentioned above don’t have to be used for any nested anchor , they will use the target and swap indicated by the boost set on parent. So for example you can set it at the top of main content element (as is done in RealWorld) and benefit almost everywhere.
Note though that there is a catch when using hx-boost. If a nested element is a form and the form doesn’t have any anchors or submit buttons, HTMX will signal an error. For this reason, in RealWorld, any element with a form has hx-boost disabled (hx-boost = false). 

Of course, any anchor element usually also needs a href attribute. If the boost mechanism is not used, hx-get attribute (<https://htmx.org/attributes/hx-get/>) must be used together with href, both pointing to same link (Technically when hx-get is used, href is not required, however using it will make the anchor look normal when hover – cursor change, underlink etc)
Links can be fixed (constant) or dynamic which in turn can be: server generated (C# expression but constant at runtime) or client generated (from x-data)
Fixed and server links are just regular C# strings.
However client links have a special format and need one more attribute:
1)	Format
For example for this link pointing to “/profile/{username}”, the link is like this:
    `$"'{Routes.Profile}' + {js.Field(x => x.Author.Username)}"`

This is actually a JS expression with several parts, some fixed and some variable:
Routes.Profile is a C# constant and is the fixed part. It must be surrounded by single quotes
js.Field(x => x.Author.Username) is the JSBuilder property expression that will return the Username from x-data
The parts are separated by the “+” sign which will be processed by JS in the browser, concatenating the two parts.
A link can have more than two parts but all must follow the same rules: constant parts surrounded by single quotes and all parts separated by “+”

2)	In order to use x-data, the x-bind attribute must be used, binding the href (or hx-get if not boosted) attribute:
HTML:
  	`:href = link`

To do this in C#, you can do

    xBind = (“href”, link)
or use the synthetic attribute xBindRef:
    
    xBindRef = link

RealWorld app doesn’t use client links, however there is a proof of concept function called ArticleListAlternate() in file ArticleList.cs which shows how shd be done


Sometimes more than one piece of HTML must be swapped. In this case, the server prepares the required pieces and send them together, side-by-side. 
In RealWorld there is function called RenderPages in UIBuilder.cs which does exactly this.
Only one piece is swapped normally (using hx-target). The rest use a mechanism called Out of Band swap (https://htmx.org/attributes/hx-swap-oob/) and must have the attribute hx-swap-oob = “true” (hxOob = true in HtmlSharp). Look for example in FollowHandler.cs to see how is done. 

5. Backend.
The backend is using Minimal API , EF Core and a Command - CommandHandler architecture. 
It has the following components:
-	Endpoints:
o	Handles the requests from web page MinimalAPI endpoints: MapGet, MapPost etc. 
o	Injects (actually uses the injected) main service and request parameters; 
-	RealWorldService - the main service:
o	Injects or creates and initializes all the other components used by the backend
o	Sets up the Htmx flag
o	Creates the repository
o	Creates the authentication service
o	Sets up the current user
o	Creates and initializes the Commands, which keep the request parameters, the html produced as a result of the request and additional data.
o	Creates, initializes and runs the CommandHandlers 
o	Renders a web page/fragment which is returned to the browser by the ASP NET system
-	AuthService: authentication service
-	UIBuilder: assembles various parts of the page and renders the final HTML sent to the browser
-	Repository: data storage faced service. It’s using EFCore but it can be easily changed to other ORM

A few words about testing. 
All tests are done against CommandHandlers. There are 3 types of testing:
-	Visual testing: displays the HTML sent to the browser. Can be used before most of the backend is implemented, it just needs a Command, CommandHandler and UIBuilder. Save the HTML from Command.Result somewhere and display in your current browser. Make sure that HTML includes the document HEAD and deploy the local css (if any) to same location where you place the HTML
-	Unit testing: unit tests the web pages. Similar to unit testing in React. Unfortunately there is no testing library in C# for this, something like react-testing-library. I used HtmlAgilityPack (<https://html-agility-pack.net/>) to help creating some tests.
-	Integration testing: tests the data /repository used by web pages


6. Summary
Putting all together

The process of creating a web app, page by page, is like this:
-	Design and implement your page in C# using HtmlSharp
-	Alternatively, design you page in a separate web designer app or copy the html from other web site or template and generate C# code using HtmlConverter app
-	Create the command handler that renders the page
-	Test the page using the CommandBase.Result which contains the page’s html. You can load the page in a browser or do unit tests before the rest of the app is even started
-	Add data
-	Add endpoints and the rest of the backend



