'use strict';

popupService.$inject = ['$uibModal'];
function popupService($uibModal) {

    return { popup: popup };

    function popup(mode, options, model) {
        var modalInstance = $uibModal.open({
            animation: true,
            windowClass: options.windowClass,
            templateUrl: options.templateUrl,
            controller: options.controller,
            resolve: {
                params: { title: options.title, dialogMode: mode, model: model, saveFunc: options.saveFunc }
            }
        });

        modalInstance.result.then(function () {
            if (options.callback != null) {
                options.callback();
            }
        });
    }
}

module.exports = popupService;