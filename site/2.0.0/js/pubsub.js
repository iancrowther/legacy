
/**
 * Typical publish subscribe object
 * @class
 * @constructor
 */
CustomEvent = function() {
    this.subscribers = [];
};

/**
 * Subscribe to an event
 * @param  {Function} fn The function that is called when the event is published
 * @return {[type]}      [description]
 */
CustomEvent.prototype.subscribe = function(fn) {
    this.subscribers.push(fn);
};

CustomEvent.prototype.unSubscribe = function(fn) {
    for (var i = 0; i < this.subscribers.length; i++) {
        if (this.subscribers[i] === fn) {
            this.subscribers.pop(i, 1);
            break;
        }
    }
};

CustomEvent.prototype.publish = function() {

  var subscribersLength = this.subscribers.length;

  for ( var i = 0; i < subscribersLength; i++ ) {

		this.subscribers[i].apply(this, arguments);

  }

};
