namespace RealWorldSharp.UI;

public class AppHead
{
	public HtmlElement Build1()
	{
		var root = body();
		root.Head = $@"
	<script src = ""https://unpkg.com/htmx.org@2.0.3"" integrity=""sha384-0895/pl2MU10Hqc6jd4RvrthNlDiE9U1tWmX7WRESftEDRosgxNsQG/Ze9YMRzHq"" crossorigin=""anonymous""></script>
	<script defer src=""https://cdn.jsdelivr.net/npm/alpinejs@3.x.x/dist/cdn.min.js""></script>
 
";


		return root;
	}

	public HtmlElement Build()
	{
		var noFormScript = @"

(function() {
  let api
  htmx.defineExtension('hx-noformdata', {
    init: function(apiRef) {
      api = apiRef
    },

    onEvent: function(name, evt) {
      if (name === 'htmx:configRequest') {
        evt.detail.headers['Content-Type'] = 'application/json'
      }
    },

    encodeParameters: function(xhr, parameters, elt) {
      xhr.overrideMimeType('text/json')

      const vals = api.getExpressionVars(elt)
      return (JSON.stringify(vals))
    }
  })
})()
";

		var noCacheScript = @"
(function () {
    window.onpageshow = function(event) {
        if (event.persisted) {
            window.location.reload();
        }
    };
})();
";

		var hmxHistory = @"
        document.addEventListener('htmx:beforeHistorySave', (evt) => {
            document.querySelectorAll('[x-for]').forEach((item) => {
                item._x_lookup && Object.values(item._x_lookup).forEach((el) => el.remove())
            })
            document.querySelectorAll('[x-if]').forEach((item) => {
                item._x_currentIfEl && item._x_currentIfEl.remove()
            })
        })
";

		var root = body();
		root.Head = $@"

	<script src = ""https://unpkg.com/htmx.org@2.0.3"" integrity=""sha384-0895/pl2MU10Hqc6jd4RvrthNlDiE9U1tWmX7WRESftEDRosgxNsQG/Ze9YMRzHq"" crossorigin=""anonymous""></script>
	<script defer src=""https://cdn.jsdelivr.net/npm/alpinejs@3.x.x/dist/cdn.min.js""></script>
	<script>{noFormScript}</script>
	<script>{hmxHistory}</script>

	<meta charset=""utf-8"" />
	<title>Conduit</title>
	<link href=""https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css"" rel=""stylesheet"" type=""text/css"">
	<link href=""https://fonts.googleapis.com/css?family=Titillium+Web:700|Source+Serif+Pro:400,700|Merriweather+Sans:400,700|Source+Sans+Pro:400,300,600,700,300italic,400italic,600italic,700italic"" rel=""stylesheet"" type=""text/css"">

	<!-- Import the custom Bootstrap 4 theme from our hosted CDN -->
	<link rel = ""stylesheet"" href=""./main.css"" />
  
";


		return root;
	}

	//	<script src = ""https://unpkg.com/htmx-ext-json-enc@2.0.1/json-enc.js""></script>
	//<link rel = ""stylesheet"" href=""/main.css"" />
	//<link rel = ""stylesheet"" href= 'file:///c:/TFSProjects_BV/RealWorldSharp/RealWorldSharp/StaticFiles/main.css' />

}
