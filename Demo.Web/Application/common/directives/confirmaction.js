/* global angular: false */

(function(ng) {
    'use strict';

    ng.module('angularDemo.common')
        .directive('blConfirmAction', [
            'blModal',
            function(modal) {
                return {
                    scope: {
                        confirmationMessage: '@',
                        confirmationAction: '&'
                    },
                    link: function(scope, element) {
                        element.on('click', function(event) {
                            event.preventDefault();
                            modal.confirm(scope.confirmationMessage, scope.confirmationAction);
                        });
                    }
                };
            }
        ]);

}(angular));