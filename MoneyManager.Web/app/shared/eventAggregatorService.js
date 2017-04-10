'use strict';

function eventAggregatorService() {
    var events = [];

    var eventNames = {
        loadingStarted: 'loadingStarted',
        loadingFinished: 'loadingFinished',
        expensePeriodChanged: 'expensePeriodChanged'
    };

    // Return public API.
    return ({
        subscribeToEvent: subscribeToEvent,
        unsubscribe: unsubscribe,
        publishEvent: publishEvent,
        eventNames: eventNames
    });

    function unsubscribe(eventName, handler) {
        events.forEach(function (item) {
            if (item.eventName == eventName) {
                item.handlers.pop(handler);
            }
        });
    }

    function subscribeToEvent(eventName, handler) {
        if (handler == undefined) {
            return;
        }
        if (events.length > 0) {
            var added = false;
            events.forEach(function (item) {
                //add listener to already registered event
                if (item.eventName == eventName && item.handlers.indexOf(handler) < 0) {
                    item.handlers.push(handler);
                    added = true;
                    return;
                }
            });
            //register new event
            if (!added) {
                events.push({ eventName: eventName, handlers: [handler] });
            }
        } else {
            events = [{ eventName: eventName, handlers: [handler] }];
        }
    }

    function publishEvent(eventName, args) {
        events.forEach(function (item) {
            if (item.eventName == eventName) {
                //notify all registered listeners
                item.handlers.forEach(function (handler) {
                    handler(args);
                });
                return;
            }
        });
    }
}

module.exports = eventAggregatorService;