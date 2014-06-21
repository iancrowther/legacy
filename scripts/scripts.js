YUI().use("node", "anim", function(Y) { 
var _carousel,  
	_anim, 
	WINDOW_WIDTH, 
	TOTAL_PAGES, 
	_currentXPosition, 
	_startIndex = 0, 
	_lastIndex, 
	_currentIndex = 0;

function init() {
	_body = Y.get(document.body);
	_carousel = _body.query(".carousel-list"),
	_anim = new Y.Anim({ node: _carousel, duration: 1, easing: Y.Easing.easeBothStrong });
	WINDOW_WIDTH = _carousel.get("children").item(0).get("offsetWidth");
	TOTAL_PAGES = _carousel.get("children").size(); 
	_lastIndex = TOTAL_PAGES -1;
	_currentXPosition = _carousel.getX();
}
function scrollControl(e) {
	e.halt();
	var leftButton = e.target.hasClass('carousel-left-button'),
		rightButton = e.target.hasClass('carousel-right-button');
	
	if(!_carousel.hasClass('animating')){
		if (leftButton) {
			scrollLeft();
		} else if(rightButton) {
			scrollRight();
		}	
		
		if (leftButton || rightButton) {
			_carousel.addClass('animating');
			_anim.set("to", { xy: [_currentXPosition, _carousel.getY()] });
			_anim.run();
			_anim.on("end", function(){
				_carousel.removeClass('animating');
			});			
		}
	}
}
function scrollLeft() {
	if (_currentIndex === _startIndex) {
		_currentXPosition = -1 * WINDOW_WIDTH * (TOTAL_PAGES - 1) + _carousel.getX();
		_currentIndex = TOTAL_PAGES - 1; 

	} else {
		_currentXPosition = _carousel.getX() + WINDOW_WIDTH;
		--_currentIndex;
	}
}
function scrollRight() {
	if (_currentIndex === _lastIndex) {
		_currentXPosition = WINDOW_WIDTH * (TOTAL_PAGES -1) + _carousel.getX();
		_currentIndex = _startIndex;
	} else {
		_currentXPosition = _carousel.getX() - WINDOW_WIDTH;
		++_currentIndex;
	}
}
Y.on("domready", init); 
Y.on("click", scrollControl, ".carousel-container");
});