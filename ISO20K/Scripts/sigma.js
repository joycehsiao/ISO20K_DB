/**
	 讀資料
	 */




//console.log("以下是測試html的data value是否可以用JSON格式讀取");
//console.log($("#customInput").attr('data-value'));
var ci_family_nodes = JSON.parse($("#customInput").attr('data-value'));
//console.log(ci_family_nodes.nodes[0].id);

//"nodes": [
//	{
//		"id": "n0",
//		"label": "A node",
//		"x": 0,
//		"y": 0,
//		"size": 3
//	},
//	{
//		"id": "n1",
//		"label": "Another node",
//		"x": 3,
//		"y": 1,
//		"size": 2
//	},
//	{
//		"id": "n2",
//		"label": "And a last one",
//		"x": 1,
//		"y": 3,
//		"size": 1
//	}
//],
//	"edges": [
//		{
//			"id": "e0",
//			"source": "n0",
//			"target": "n1"
//		},
//		{
//			"id": "e1",
//			"source": "n1",
//			"target": "n2"
//		},
//		{
//			"id": "e2",
//			"source": "n2",
//			"target": "n0"
//		}
//	] 
//	}
function getJsonLength(jsonData) {
	var jsonLength = 0;
	for (var item in jsonData) {
		jsonLength++;
	}
	return jsonLength;
}
var i,
	s,
	N = getJsonLength(ci_family_nodes.nodes);
	E = N-2,
	g = {
		nodes: [],
		edges: []
	};

// Generate a random graph:
for (i = 0; i < N; i++)
	g.nodes.push({
		id: ci_family_nodes.nodes[i].id,
		label: ci_family_nodes.nodes[i].label,
		x: Math.random(),
		y: Math.random(),
		size: getJsonLength(ci_family_nodes.nodes[i]),
		color: '#d8f2da'
	});
g.edges.push({
	id: 'e' + 1,
	source: 1,
	target: 2,
	size: 20,
	color: '#d8f2da'
}, {
		id: 'e' + 2,
		source: 3,
		target: 4,
		size: 20,
		color: '#d8f2da'}
);
//for (i = 0; i < E; i++)
//	g.edges.push({
//		id: 'e' + i,
//		source: (Math.random() * N | 0),
//		target: (Math.random() * N | 0),
//		size: Math.random(),
//		color: '#ccc'
//	});


// Instantiate sigma:
s = new sigma({
	graph: g,
	settings: {
		enableHovering: false
	}
});

s.addRenderer({
	id: 'main',
	type: 'svg',
	container: document.getElementById('graph-container'),
	freeStyle: true
});

s.refresh();

// Binding silly interactions
function mute(node) {
	if (!~node.getAttribute('class').search(/muted/))
		node.setAttributeNS(null, 'class', node.getAttribute('class') + ' muted');
}

function unmute(node) {
	node.setAttributeNS(null, 'class', node.getAttribute('class').replace(/(\s|^)muted(\s|$)/g, '$2'));
}

$('.sigma-node').click(function () {

	// Muting
	$('.sigma-node, .sigma-edge').each(function () {
		mute(this);
	});

	// Unmuting neighbors
	var neighbors = s.graph.neighborhood($(this).attr('data-node-id'));
	neighbors.nodes.forEach(function (node) {
		unmute($('[data-node-id="' + node.id + '"]')[0]);
	});

	neighbors.edges.forEach(function (edge) {
		unmute($('[data-edge-id="' + edge.id + '"]')[0]);
	});
});

s.bind('clickStage', function () {
	$('.sigma-node, .sigma-edge').each(function () {
		unmute(this);
	});
});