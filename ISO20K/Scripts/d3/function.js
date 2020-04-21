var ci_family_nodes = JSON.parse($("#customInput").attr('data-value'));
var node_number = getJsonLength(ci_family_nodes.nodes)
var edges = {
	"links": [
		{
			"source": 0,
			"target": 1,
			"type":"HAS"
		},
		{
			"source": 0,
			"target": 2,
			"type": "HAS"
		},
		{
			"source": 0,
			"target": 3,
			"type": "HAS"
		}
	]
}



var friendJSON = {
	"nodes": [
		{
			"name": "王曉明",
			"group": 1
		},
		{
			"name": "豬三櫃",
			"group": 1
		},
		{
			"name": "張西西",
			"group": 1
		},
		{
			"name": "龍不悔",
			"group": 1
		},
		{
			"name": "安三蓋",
			"group": 1
		},
		{
			"name": "雷想想",
			"group": 2
		},
		{
			"name": "呂輕輕",
			"group": 2
		},
		{
			"name": "蘭柯孟",
			"group": 2
		},
		{
			"name": "武單單",
			"group": 3
		},
		{
			"name": "花籃藍",
			"group": 3
		},
		{
			"name": "謝謝尼",
			"group": 3
		}
	],
	"links": [
		{
			"source": 0,
			"target": 1
		},
		{
			"source": 0,
			"target": 2
		},
		{
			"source": 0,
			"target": 3
		},
		{
			"source": 0,
			"target": 4
		},
		{
			"source": 1,
			"target": 2
		},
		{
			"source": 1,
			"target": 3
		},
		{
			"source": 1,
			"target": 4
		},
		{
			"source": 2,
			"target": 3
		},
		{
			"source": 2,
			"target": 4
		},
		{
			"source": 0,
			"target": 5
		},
		{
			"source": 5,
			"target": 6
		},
		{
			"source": 5,
			"target": 7
		},
		{
			"source": 0,
			"target": 8
		},
		{
			"source": 0,
			"target": 9
		},
		{
			"source": 0,
			"target": 10
		},
		{
			"source": 8,
			"target": 7
		}
	]
}
var width = 560,
	height = 300
var svg = d3.select("#test3")
	.attr("width", width)
	.attr("height", height);
var force = d3.forceSimulation()
	.force("charge", d3.forceManyBody().strength(-700).distanceMin(100).distanceMax(1000))
	.force("link", d3.forceLink().id(function (d) { return d.index }))
	.force("center", d3.forceCenter(width / 2, height / 2))
	.force("y", d3.forceY(0.001))
	.force("x", d3.forceX(0.001))
var color = function (group) {
	if (group == 1) {
		return "#aaa"
	} else if (group == 2) {
		return "#fbc280"
	} else {
		return "#405275"
	}
}
function getJsonLength(jsonData) {
	var jsonLength = 0;
	for (var item in jsonData) {
		jsonLength++;
	}
	return jsonLength;
}
function dragstarted(d) {
	if (!d3.event.active) force.alphaTarget(0.5).restart();
	d.fx = d.x;
	d.fy = d.y;
}

function dragged(d) {
	d.fx = d3.event.x;
	d.fy = d3.event.y;
}

function dragended(d) {
	if (!d3.event.active) force.alphaTarget(0.5);
	d.fx = null;
	d.fy = null;
}
function friendJSON_function(json) {
	force
		.nodes(json.nodes)
		.force("link").links(json.links)
	var link = svg.selectAll(".link")
		.data(json.links)
		.enter()
		.append("line")
		.attr("class", "link");
	var node = svg.selectAll(".node")
		.data(json.nodes)
		.enter().append("g")
		.attr("class", "node")
		.call(d3.drag()
			.on("start", dragstarted)
			.on("drag", dragged)
			.on("end", dragended));
	node.append('circle')
		.attr('r', 13)
		.attr('fill', function (d) {
			return color(d.group);
		});
	node.append("text")
		.attr("dx", -18)
		.attr("dy", 8)
		.style("font-family", "overwatch")
		.style("font-size", "18px")
		.text(function (d) {
			return d.name
		});

	force.on("tick", function () {
		link.attr("x1", function (d) {
			return d.source.x;
		})
			.attr("y1", function (d) {
				return d.source.y;
			})
			.attr("x2", function (d) {
				return d.target.x;
			})
			.attr("y2", function (d) {
				return d.target.y;
			});
		node.attr("transform", function (d) {
			return "translate(" + d.x + "," + d.y + ")";
		});
	});

}
function ciJSON_function(n,l) {
	force
		.nodes(n)
		.force("link").links(l)
	var link = svg.selectAll(".link")
		.data(l)
		.enter()
		.append("line")
		.attr("class", "link");
	var node = svg.selectAll(".node")
		.data(n)
		.enter().append("g")
		.attr("class", "node")
		.call(d3.drag()
			.on("start", dragstarted)
			.on("drag", dragged)
			.on("end", dragended));
	node.append('circle')
		.attr('r', 13)
		.attr('fill', function (d) {
			return color(d.group);
		});
	node.append("text")
		.attr("dx", -18)
		.attr("dy", 8)
		.style("font-family", "overwatch")
		.style("font-size", "18px")
		.text(function (d) {
			return d.name
		});

	force.on("tick", function () {
		link.attr("x1", function (d) {
			return d.source.x;
		})
			.attr("y1", function (d) {
				return d.source.y;
			})
			.attr("x2", function (d) {
				return d.target.x;
			})
			.attr("y2", function (d) {
				return d.target.y;
			});
		node.attr("transform", function (d) {
			return "translate(" + d.x + "," + d.y + ")";
		});
	});

}
//friendJSON_function(friendJSON)
ciJSON_function(ci_family_nodes.nodes, edges.links)

// Toggle children on click.
function click(d) {
	if (d3.event.defaultPrevented) return; // ignore drag
	if (d.children) {
		d._children = d.children;
		d.children = null;
	} else {
		d.children = d._children;
		d._children = null;
	}
	update3();
}