$(document).ready(function () {
    var validator = TaxCalculator.Validator;
    TaxCalculator.Process.LoadPage(validator);
});

var TaxCalculator = TaxCalculator || (function ($) {

    "use strict";

    //Constructor
    var taxCalculator = function () {
    };

    taxCalculator.prototype.Process = (function ($) {

        //Constructor
        var process = function () {
        };

        process.prototype.SetupDropDown = function () {
            setup();

            function setup() {
                var parameterSettings = {};
                parameterSettings.urls = taxCalculatorSettings.dropDownSettings.urls;
                parameterSettings.pageSize = 2;
                parameterSettings.defaultTaxCalculationType = {
                    id: "1",
                    text: "7441 - Progressive"
                };

                DropDown.Setup('#selectedCalculationType',
                    "Please choose calculation type",
                    parameterSettings.urls.getTaxCaculatorTypes + '?pageSize=' + parameterSettings.pageSize,
                    parameterSettings.pageSize,
                    parameterSettings.defaultTaxCalculationType, true);
            }
        };

        process.prototype.CalculateTax = function () {

            var taxCalculationType = $("#selectedCalculationType").val();
            var annualSalary = $("#annualSalary").val();
            var alertEl = "#statusAlert";

            var alertMsg = "There was an error while submitting request.";
            var query = JSON.stringify({
                postalCode: taxCalculationType,
                annualIncome: annualSalary
            });

            $.ajax({
                data: query,
                dataType: "json",
                type: 'post',
                contentType: 'application/json',
                url: taxCalculatorSettings.urls.taxCalculation,
                success: function (data) {

                    if (data) {
                        if (data.errorMsg) {
                            alertMsg = alertMsg + data.errorMsg;
                        }
                        else {
                            if (data.taxDue) {
                                alertMsg = "Tax Due: ";
                                alertMsg = "<b>" + alertMsg + "</b>" + data.taxDue;
                            }
                            else {
                                alertMsg = "There was an error while calculating the tax due.";
                            }
                        }                        
                    }

                    Utils.DisplayAlert(alertEl, alertMsg, !data || data.errorMsg);
                },
                error: function (xhr, status, error) {
                    if (!error || error.length === 0) {
                        error = "Network Error";
                    }
                    Utils.DisplayAlert(alertEl, error, true);
                    console.log("error: ", error);
                }
            });
        };
        
        process.prototype.LoadPage = function (validator) {
            this.SetupDropDown();

            var self;
            self = this;

            this.SetValidationEvents(validator, frmSubmitCallback);

            function frmSubmitCallback() {
                self.CalculateTax();
            }
        };

        process.prototype.SetValidationEvents = function (validator, frmSubmitCallback) {
            var formElId = "form#taxCalcForm";
            var form = document.querySelector(formElId);
            var constraints = validator.FormConstraints;
            var annualSalaryElId = "#annualSalary";

            $(annualSalaryElId).on("change", function (e) {
                var errors = validate(form, constraints) || {};
                validator.ShowErrorsForInput(this, errors[this.name], constraints);
            });

            $(formElId).on("submit", function (e) {
                e.preventDefault();
                validator.HandleFormSubmit(form, annualSalaryElId, constraints, frmSubmitCallback);
            });
        };

        return new process();
    }($));

    taxCalculator.prototype.Validator = (function ($) {

        //Constructor
        var validator = function () {

            validate.validators.decimalGreaterThanZero = function (value, options, key, attributes) {
                var decimalGreaterThanZero = /\d+\.?\d*/i;

                if (!value) {
                    return "value is required";
                }
                else if (value) {
                    if (value < 0) {
                        return "value must be greater than or equal to 0";
                    }
                    else if (!decimalGreaterThanZero.test(value)) {
                        return "value must be decimal";
                    }
                }

                return null;
            };
        };

        validator.prototype.FormConstraints = {

            annualSalary: {
                numericality: {
                    presence: true,
                },
                decimalGreaterThanZero: true
            }
        };

        validator.prototype.HandleFormSubmit = function (form, elId, constraints, callback) {
            var errors = validate(form, constraints);
            this.ShowErrors(form, errors || {}, elId);
            if (!errors) {
                callback();
            }
        };

        validator.prototype.ShowErrors = function (form, errors, elId) {
            var self = this;
            var input = form.querySelector(elId);
            self.ShowErrorsForInput(input, errors && errors[input.name]);
        };

        validator.prototype.ShowErrorsForInput = function (input, errors) {
            var self = this;
            var formGroup = self.ClosestParent(input.parentNode, "form-group");
            var messages = formGroup.querySelector(".messages");

            self.ResetFormGroup(formGroup);

            if (errors) {
                formGroup.classList.add("has-error");
                _.each(errors, function (error) {
                    self.AddError(messages, error);
                });
            } else {
                formGroup.classList.add("has-success");
            }
        };

        validator.prototype.ClosestParent = function (child, className) {
            if (!child || child == document) {
                return null;
            }
            if (child.classList.contains(className)) {
                return child;
            } else {
                return this.ClosestParent(child.parentNode, className);
            }
        };

        validator.prototype.ResetFormGroup = function (formGroup) {
            formGroup.classList.remove("has-error");
            formGroup.classList.remove("has-success");
            _.each(formGroup.querySelectorAll(".help-block.error"), function (el) {
                el.parentNode.removeChild(el);
            });
        };

        validator.prototype.AddError = function (messages, error) {
            var block = document.createElement("p");
            block.classList.add("help-block");
            block.classList.add("error");
            block.innerText = error;
            messages.appendChild(block);
        };

        return new validator();
    }($));

    return new taxCalculator();
}($));