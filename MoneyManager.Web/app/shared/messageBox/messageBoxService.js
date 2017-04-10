'use script';

messageBoxService.$inject = ['$uibModal'];
function messageBoxService($uibModal) {

    return {
        showMessage: showMessage
    }

    function showMessage(message, title, messageBoxType) {
        if (messageBoxType == null) {
            messageBoxType = 'info';
        }

        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/app/shared/messageBox/messageBox.html',
            controller: 'messageBoxController',
            resolve: {
                params: {
                    message: message, title: title, type: messageBoxType
                }
            }
        });
        return modalInstance.result;
    }
}

module.exports = messageBoxService;