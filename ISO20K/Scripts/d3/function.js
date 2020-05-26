/*利用ci_family做示範
 view : views/ci_family/Index.cshtml
 controller : controllers/ci_familyController
 */


var ci_family_nodes = JSON.parse($("#customInput").attr('data-value'));

//d3_2
var colors = d3.scaleOrdinal(d3.schemeCategory10);

var svg = d3.select("#test2"),
	width = +svg.attr("width"),
	height = +svg.attr("height"),
	node,
	link;

svg.append('defs').append('marker')
	.attrs({
		'id': 'arrowhead',
		'viewBox': '-0 -5 10 10',
		'refX': 13,
		'refY': 0,
		'orient': 'auto',
		'markerWidth': 13,
		'markerHeight': 13,
		'xoverflow': 'visible'
	})
	.append('svg:path')
	.attr('d', 'M 0,-5 L 10 ,0 L 0,5')
	.attr('fill', '#999')
	.style('stroke', 'none');

var simulation = d3.forceSimulation()
	.force("link", d3.forceLink().id(function (d) { return d.id; }).distance(200).strength(1))
	.force("charge", d3.forceManyBody())
	.force("center", d3.forceCenter(width / 2, height / 2));

//links
var json2 = {
	
	"links": [
		{
			"source": 1,
			"target": 2,
			"type": "KNOWS",
			"since": 2010
		},
		{
			"source": 1,
			"target": 3,
			"type": "FOUNDED"
		},
		{
			"source": 2,
			"target": 3,
			"type": "WORKS_ON"
		},
		{
			"source": 3,
			"target": 4,
			"type": "IS_A"
		}
	]
}

update(json2.links, ci_family_nodes.nodes)
function update(links, nodes) {
	link = svg.selectAll(".link")
		.data(links)
		.enter()
		.append("line")
		.attr("class", "link")
		.attr('marker-end', 'url(#arrowhead)')

	link.append("title")
		.text(function (d) { return d.type; });

	edgepaths = svg.selectAll(".edgepath")
		.data(links)
		.enter()
		.append('path')
		.attrs({
			'class': 'edgepath',
			'fill-opacity': 0,
			'stroke-opacity': 0,
			'id': function (d, i) { return 'edgepath' + i }
		})
		.style("pointer-events", "none");

	edgelabels = svg.selectAll(".edgelabel")
		.data(links)
		.enter()
		.append('text')
		.style("pointer-events", "none")
		.attrs({
			'class': 'edgelabel',
			'id': function (d, i) { return 'edgelabel' + i },
			'font-size': 13,
			'fill': '#aaa'
		});

	edgelabels.append('textPath')
		.attr('xlink:href', function (d, i) { return '#edgepath' + i })
		.style("text-anchor", "middle")
		.style("pointer-events", "none")
		.attr("startOffset", "60%")
		.text(function (d) { return d.type });

	node = svg.selectAll(".node")
		.data(nodes)
		.enter()
		.append("g")
		.attr("class", "node")
		.call(d3.drag()
			.on("start", dragstarted)
			.on("drag", dragged)
			//.on("end", dragended)
		);

	node.append("circle")
		.attr("r", 13)
		.style("fill", function (d, i) { return colors(i); })

	node.append("title")
		.text(function (d) { return d.id; });

	node.append("text")
		.attr("dy", -15)
		.style("font-size", "20px")
		.text(function (d) { return d.name + ":" + d.label; });

	simulation
		.nodes(nodes)
		.on("tick", ticked);

	simulation.force("link")
		.links(links);
}

function ticked() {
	link
		.attr("x1", function (d) { return d.source.x; })
		.attr("y1", function (d) { return d.source.y; })
		.attr("x2", function (d) { return d.target.x+7; })
		.attr("y2", function (d) { return d.target.y+7; });

	node
		.attr("transform", function (d) { return "translate(" + d.x + ", " + d.y + ")"; });

	edgepaths.attr('d', function (d) {
		return 'M ' + d.source.x + ' ' + d.source.y + ' L ' + d.target.x-7 + ' ' + d.target.y-7;
	});

	edgelabels.attr('transform', function (d) {
		if (d.target.x < d.source.x) {
			var bbox = this.getBBox();

			rx = bbox.x + bbox.width / 2;
			ry = bbox.y + bbox.height / 2;
			return 'rotate(180 ' + rx + ' ' + ry + ')';
		}
		else {
			return 'rotate(0)';
		}
	});
}

function dragstarted(d) {
	if (!d3.event.active) simulation.alphaTarget(0.3).restart()
	d.fx = d.x;
	d.fy = d.y;
}

function dragged(d) {
	d.fx = d3.event.x;
	d.fy = d3.event.y;
}

function getRelation () {//取得下拉式選單資料
	var user_source = document.getElementById("source").value;
	var user_target = document.getElementById("target").value;
	var user_type = document.getElementById("type").value;
	setLink(user_source, user_target, user_type)
}

function setLink(src, tar, tp){
	json2.links.push({
		source: src,
		target: tar,
		type:tp
	})
	
	update(json2.links, ci_family_nodes.nodes);
}