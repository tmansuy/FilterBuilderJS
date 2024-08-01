# Horizon Reports filterbuilder.js


![Filter Builder](https://github.com/tmansuy/FilterBuilderJS/blob/45269681f71256fdb947a8f9743bedc3af00c5d1/filterbuilder.png)

> A javscript library that turns an html div element in to a filter builder control similar to the one found in [Horizon Reports](https://www.horizon-reports.com).

## Dependencies

* [HorizonReports.Core Nuget Package](https://www.nuget.org/packages/HorizonReports.Core/)
* [jQuery](https://jquery.com/)
* [jquery-ui](https://jqueryui.com/)
* [underscore.js](https://underscorejs.org/)
* [knockout](https://knockoutjs.com/)
* [Moment.js](https://momentjs.com/)
* [font-awesome](https://fontawesome.com/)
* [Bootstrap 4](https://getbootstrap.com/docs/4.6/getting-started/introduction/)

## Getting started

* Clone the repo: `git@github.com:tmansuy/FilterBuilder.git`
* Copy filterbuilder/dist/filterbuilder.js to your project

Supported loading methods.

* Include directly:
```html
<script src="~Scripts/filterbuilder/filterbuilder.js"></script>
```

* **AMD** module

```javascript
define('module', ['jquery', 'knockout', 'filterbuilder'], 
function($, ko, filterbuilder) {
...
});
```

* **CommonJS** module

```javascript
var filterbuilder = require('filterbuilder'); 
```

## Usage

Add a div to act as the container for the filterbuilder control. If you're using ASP.NET MVC, this div can be loaded from a partial view.

```html
<div id="filter-builder"></div>
```

Show the control:

```javascript
filterbuilder.show('filter-builder', filterConfig);
```

Unload the control:

```javascript
filterbuilder.destroy('filter-builder');
```

Get an array of the filter conditions representing the current state of the control:

```javascript
var filters = filterbuilder.getCurrentFilter();
```

[Full example](https://github.com/tmansuy/FilterBuilderJS/blob/45269681f71256fdb947a8f9743bedc3af00c5d1/FilterBuilderExample/Views/Home/Index.cshtml)

## Options

Available settings:

```javascript
$('filter-builder').show({
	tables: [],
	fields: [],
	initialFilter: '',
	module: '',
	availableFilters: [],
	onShowExpressionBuilder: expressionBuilderFunction,
	onClose: onFilterBuilderClose,
	onLoad: onLoadFilter,
	onSave: onSaveFilter,
	allowExpressions: true,
	allowDatabaseExpressions: true,
	allowExpressionBuilder: true,
	allowPersistance: true,
	allowAskAtRuntimeFilters: true,
	allowFilterDescriptions: true,
	captions: {
	    AndConnection: 'AND'
	}
	
});
```

Attribute			        | Type			                        | Default		| Description
---						    | ---			                        | ---			| ---
`tables`		            | *Array*		                        | `empty`		| An array of table objects. **required** See Index.cshtml for an example of how to populate this array in ASP.NET MVC.
`fields`		            | *Array*		                        | `empty`		| An array of field objects. **required** See Index.cshtml for an example of how to populate this array in ASP.NET MVC.
`initialFilter`	            | *String*		                        | `null`		| The serialized JSON of a filter to load when the control is shown.
`module`	                | *String*		                        | `null`		| An option module name to filter the available tables.
`availableFilters`	        | *Array*		                        | `empty`		| An array of filter names that can be loaded to display to the user. 
`onShowExpressionBuilder`   | *function (callback)* 		        | `null`		| Called on expression builder button click. Pass an expression string to the **callback** to populate the expression.
`onClose`	                | *function (filter)*		            | `null`		| Called on close button click. Passed the current **filter** array.
`onLoad`	                | *function (filtername, callback)*		| `null`		| Called on load filter button click. **filtername** contains the filter to load. Pass the loaded filter to the **callback** to populate the control.
`onSave`	                | *function (filtername, filter)*		| `null`		| Called on save filter button click. **filtername** contains the filter name to save under. **filter** contains the filter array to save. 
`allowAskAtRuntimeFilters`	| *Boolean*		                        | `true`		| True to allow filters to be designated as ask-at-runtime filters.
`allowDatabaseExpressions`	| *Boolean*		                        | `true`		| True to allow database expression based filter conditions. These are SQL expressions that are injected directly in the filter. 
`allowFilterDescriptions`   | *Boolean*		                        | `true`		| True to allow controls for customizing the filter description text.
`allowExpressions`	        | *Boolean*		                        | `true`		| True to allow expression based filter conditions. These user defined values can then be evaluated at a later time.
`allowExpressionBuilder`    | *Boolean*		                        | `true`		| True to show the expression builder button for expression based conditions.
`allowPersistance`          | *Boolean*		                        | `true`		| True to allow loading and saving filters. 
`captions`             	    | *Object*		                        | `null`  		| Localizable resources. Set the value of the associated captions property to override the default value. The available properties are listed in the next table.


Property					| Default			
---							| ---
`AndConnection`            	| `And`
`FilterGroupRootCaption`    | `Filter`
`FilterGroupNodeCaption`    | `Filter Group`
`LoadFilter`               	| `Load Filter`
`SaveFilter`               	| `Save Filter`
`AskAtRuntime`              | `Ask at runtime`
`ValuesFor`                	| `Values For:`
`Close`                    	| `Close`
`FilterName`                | `Filter name ...`
`CustomFilterPrompt`        | `Custom filter prompt`
`AddFilterCaption`          | `To add a filter item, use the + buttons above, or drag an existing filter item to the space below.`


## Examples

Populate available filters from an array called *AvailableFilters* passed via the ASP.NET MVC ViewBag.

```javascript
var options = {
    availableFilters = [],
};
var array = @Html.Raw(Json.Encode(@ViewBag.AvailableFilters));

for(var i =0; i<array.length;i++){
    options.availableFilters[i] = array[i];
}
```

Load field objects for the filter control using jquery. For an example server MVC controller, see [FilterController.cs](https://github.com/tmansuy/FilterBuilderJS/blob/45269681f71256fdb947a8f9743bedc3af00c5d1/FilterBuilderExample/Controllers/FilterController.cs) :

```javascript
var options = {
    fields = [],
};
$.getJSON("/filter/filterfields/", function (fields) {
    options.fields = fields;
});
```

Load table objects for the filter control using jquery. For an example server MVC controller, see [FilterController.cs](https://github.com/tmansuy/FilterBuilderJS/blob/45269681f71256fdb947a8f9743bedc3af00c5d1/FilterBuilderExample/Controllers/FilterController.cs) :

```javascript
var options = {
    tables = [],
};
$.getJSON("/filter/filtertables/", function (tables) {
    options.tables = tables;
});
```

Handle the expression builder click event.

```javascript
var expressionBuilderFunction = function (callback) {
    alert("Show a dialog for building expressions here.");
    if (callback) {
        // Inject the expression string in to the callback to pass it to the condition.
        callback("Injected expression.");
    }
};

var options = {
    onShowExpressionBuilder = expressionBuilderFunction,
};

```

Handle the filter builder close event. For an example server MVC controller, see [FilterController.cs](https://github.com/tmansuy/FilterBuilderJS/blob/45269681f71256fdb947a8f9743bedc3af00c5d1/FilterBuilderExample/Controllers/FilterController.cs) :

```javascript
var onFilterBuilderClose = function (filter) {
    // Do something with the filter
};

var options = {
    onClose = onFilterBuilderClose,
};

```

Handle the save button event. For an example server MVC controller, see [FilterController.cs](https://github.com/tmansuy/FilterBuilderJS/blob/45269681f71256fdb947a8f9743bedc3af00c5d1/FilterBuilderExample/Controllers/FilterController.cs) :

```javascript
var onSaveFilter = function (filtername, filter) {
    // Persist the filter. The control expects to be able to load this filter with the filtername as a key. 
};

var options = {
    onSave = onSaveFilter,
};

```

Handle the load button event. For an example server MVC controller, see [FilterController.cs](https://github.com/tmansuy/FilterBuilderJS/blob/45269681f71256fdb947a8f9743bedc3af00c5d1/FilterBuilderExample/Controllers/FilterController.cs) :

```javascript
var onLoadFilter = function (filtername, callback) {
    // lookup the filter stored under filtername, and 
	// pass it to the callback function when finished.
};

var options = {
    onLoad = onLoadFilter,
};

```

## Licenses

**select2**
https://github.com/select2/select2/blob/master/LICENSE.md
Copyright (c) 2012-2017 Kevin Brown, Igor Vaynberg, and Select2 contributors

 **bootstrap-datetimejs**
 https://github.com/Eonasdan/bootstrap-datetimepicker
 Copyright (c) 2015 Jonathan Peterson

## Author

**Trevor Mansuy**
* [github/tmansuy](https://github.com/tmansuy)
* <tmansuy@horizon-reports.com>

## Copyright

Copyright (c) 2024, Trevor Mansuy