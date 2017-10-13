var yearData;
var salesData;

var data =
	[
		{ 'Year': '2012', 'Sale': 2000 },
		{ 'Year': '2014', 'Sale': 3000 },
		{ 'Year': '2015', 'Sale': 5000 }
	]

yearData = GetYears(data)
salesData = GetSales(data)

function GetYears(data) {
	var result = [];
	for (var i in data)
		result.push(data[i].Year);

	return result;
}

function GetSales(data) {
	var result = [];
	var max = 0.0;
	result.push(max);
	for (var i in data) {
		if (max & lt; data[i].Sale) {
			max = data[i].Sale
		}
	}
	result.push(max + 50);


	return result;
}
var margin = { top: 20, right: 30, bottom: 30, left: 40 },
	width = 1000 - margin.left - margin.right,
	height = 500 - margin.top - margin.bottom;

var chart = d3.select("#chart")
	.append("svg")  //append svg element inside #chart
	.attr("width", width + (2 * margin.left) + margin.right)    //set width
	.attr("height", height + margin.top + margin.bottom);  //set height

var x = d3.scale.ordinal().domain(yearData).rangePoints([margin.left, width]);
var y = d3.scale.linear().domain(salesData).range([height, 0]);

var xAxis = d3.svg.axis()
	.scale(x)
	.orient("bottom").ticks(data.Year);  //orient bottom because x-axis will appear below the bars
var yAxis = d3.svg.axis()
	.scale(y)
	.orient("left").ticks(10);

var valueline = d3.svg.line()
	.x(function (d) { return x(d.Year); })
	.y(function (d) { return y(d.Sale); });

chart.append("g")
	.attr("class", "x axis")
	.attr("transform", "translate(0," + height + ")")
	.call(xAxis);

chart.append("g")
	.attr("class", "y axis")
	.attr("transform", "translate(" + margin.left + ",0)")
	.call(yAxis)
	.append("text")
	.attr("transform", "rotate(-90)")
	.attr("y", 6)
	.attr("dy", ".71em")
	.style("text-anchor", "end")
	.text("Sales Data");

// Add the valueline path.
chart.append("path")
	.attr("class", "line")
	.attr("d", valueline(data));
