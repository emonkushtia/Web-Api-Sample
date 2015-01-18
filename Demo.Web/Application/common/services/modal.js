/* global angular: false */

(function(ng,_) {
    'use strict';

    var ModalInstanceCtrl = [
        '$scope',
        '$modalInstance',
        'title',
        'icon',
        'message',
        function($scope, $modalInstance, title, icon, message) {

            $scope.title = title;
            $scope.icon = icon;
            $scope.message = message;

            $scope.ok = function() {
                $modalInstance.close();
            };

            $scope.cancel = function() {
                $modalInstance.dismiss('cancel');
            };
        }
    ];
    var MultiViewModalInstanceCtrl = [
        '$scope',
        '$modalInstance',
        'views',
        'data',
        function ($scope, $modalInstance, views, data) {
            $scope.views = views;
            $scope.data = data;
            $scope.currentStep = 1;
            function next(stepNumber) {
                var nextStep = $scope.currentStep + 1;
                console.log('Step Number ' + stepNumber);
                if (_.isNumber(stepNumber)) {
                    nextStep = stepNumber;
                }
                if (nextStep <= $scope.views.length)
                    $scope.currentStep = nextStep;
                else close();
            }


            function close() {
                console.log('Modal Closed');
                    $modalInstance.close();
            }

            $scope.btnClick = function(ac) {
                ac.action(next, close);
            };

            $scope.cancel = function() {
                $modalInstance.dismiss('cancel');
            };

        }
    ];

    function Modal() {

        this.$get = [
            '$modal',
            function ($modal) {
                function show(template, title, icon, message, action) {
                    var dialog = $modal.open({
                            templateUrl: template,
                            controller: ModalInstanceCtrl,
                            resolve: {
                                title: function () {
                                    return title;
                                },
                                icon: function() {
                                    return icon;
                                },
                                message: function () {
                                    return message;
                                }
                            }
                        });

                    return dialog.result.then(function () {

                        if (ng.isFunction(action)) {

                            action();
                        }
                    });
                }

                return {
                    confirm: function() {
                        var title, message, action;

                        if (arguments.length < 2) {
                            throw new Error('Both message and action are required.');
                        }

                        if (arguments.length > 2) {
                            title = arguments[0];
                            message = arguments[1];
                            action = arguments[2];
                        } else {
                            title = 'Confirm';
                            message = arguments[0];
                            action = arguments[1];
                        }

                       return show('views/directives/confirmdialog.html', title, 'fa-question-circle', message, action);
                    },

                    notify: function() {
                        var DEFAULT_TITLE = 'Notification',
                            title,
                            message,
                            action;

                        if (arguments.length < 1) {
                            throw new Error('Message is required.');
                        }

                        if (arguments.length > 2) {
                            title = arguments[0];
                            message = arguments[1];
                            action = arguments[2];
                        } else if (arguments.length === 2) {
                            if (ng.isFunction(arguments[1])) {
                                title = DEFAULT_TITLE;
                                message = arguments[0];
                                action = arguments[1];
                            } else {
                                title = arguments[0];
                                message = arguments[1];
                                action = ng.noop;
                            }
                        } else {
                            title = DEFAULT_TITLE;
                            message = arguments[0];
                            action = ng.noop;
                        }

                       return show('views/directives/messagedialog.html', title, 'fa-info-circle', message, action);
                    },
                    multiview: function (modalItems) {
                       $modal.open({
                            size: modalItems.size||'lg',
                            templateUrl: 'views/directives/multiviewdialog.html',
                            controller: MultiViewModalInstanceCtrl,
                           resolve: {
                               views: function() {
                                   return modalItems.views;
                               },
                               data: function () {
                                   return modalItems.data;
                               }
                           }
                        });

                    },
                    notifySession: function() {
                        var DEFAULT_TITLE = 'Notification',
                            title,
                            message,
                            action;

                        if (arguments.length < 1) {
                            throw new Error('Message is required.');
                        }

                        if (arguments.length > 2) {
                            title = arguments[0];
                            message = arguments[1];
                            action = arguments[2];
                        } else if (arguments.length === 2) {
                            if (ng.isFunction(arguments[1])) {
                                title = DEFAULT_TITLE;
                                message = arguments[0];
                                action = arguments[1];
                            } else {
                                title = arguments[0];
                                message = arguments[1];
                                action = ng.noop;
                            }
                        } else {
                            title = DEFAULT_TITLE;
                            message = arguments[0];
                            action = ng.noop;
                        }

                        return show('views/directives/notifysessiondialog.html', title, 'fa-info-circle', message, action);
                    },

                    error: function() {
                        var DEFAULT_TITLE = 'Error',
                            title,
                            message,
                            action;

                        if (arguments.length < 1) {
                            throw new Error('Message is required.');
                        }

                        if (arguments.length > 2) {
                            title = arguments[0];
                            message = arguments[1];
                            action = arguments[2];
                        } else if (arguments.length === 2) {
                            if (ng.isFunction(arguments[1])) {
                                title = DEFAULT_TITLE;
                                message = arguments[0];
                                action = arguments[1];
                            } else {
                                title = arguments[0];
                                message = arguments[1];
                                action = ng.noop;
                            }
                        } else {
                            title = DEFAULT_TITLE;
                            message = arguments[0];
                            action = ng.noop;
                        }

                        return show('views/directives/messagedialog.html', title, 'fa-exclamation-circle', message, action);
                    }
                };
            }
        ];
    }

    ng.module('angularDemo.common').provider('blModal', [Modal]);

})(angular,_);