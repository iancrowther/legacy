
var ev = new CustomEvent();

var headerContainer = document.getElementById("header-container");
var mainContainer = document.getElementById("main-container");
var logo = document.getElementById("logo");

ev.subscribe( function(e) {
	// console.log(window.pageYOffset);
	if ( window.pageYOffset === 0 ) {
		logo.style.top = '50px';
	} else if ( window.pageYOffset >= 50 && window.pageYOffset <= 280 ){
		logo.style.top = window.pageYOffset + 'px';
	} else if ( window.pageYOffset > 370 ) {
		headerContainer.className = "fixed";
		mainContainer.className = "fixed";
		logo.className = "hidden";
	} else {
		headerContainer.className = "";
		mainContainer.className = "";
		logo.className = "";
	}
});

// var panels = document.querySelectorAll("[data-type='bg']");
// for (var i = 0; i < panels.length; i++ ) {
// (function(panel){
// 		ev.subscribe( function(e) {
//     	var topPanel = window.pageYOffset + window.innerHeight;
//     	// console.log(topPanel, panel.offsetTop, topPanel > panel.offsetTop);
//     	var bottomPanel = panel.offsetTop + panel.offsetHeight;
//     	// console.log(bottomPanel, window.pageYOffset, bottomPanel > window.pageYOffset);

// 			// if not scrolled past top & bottom 
// 			if ( ( topPanel > panel.offsetTop ) && ( bottomPanel > window.pageYOffset ) ) {
// 				var yPos = ( window.pageYOffset / panel.getAttribute('data-speed') );
// 				var offset = panel.getAttribute("data-offsetY");
// 				if (offset) { yPos += parseInt(offset);	}
// 				panel.style.top = yPos + 'px';
// 			}
// 		});
// })(panels[ i ]);
// }

window.onscroll = function ( event ) {
	ev.publish();
}
