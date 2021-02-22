"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
exports.__esModule = true;
exports.MemberMessagesComponent = void 0;
var core_1 = require("@angular/core");
var MemberMessagesComponent = /** @class */ (function () {
    function MemberMessagesComponent(messageService) {
        this.messageService = messageService;
        this.messageContent = '';
    }
    MemberMessagesComponent.prototype.ngOnInit = function () {
    };
    MemberMessagesComponent.prototype.sendMessage = function () {
        var _this = this;
        this.messageService.sendMessage(this.username, this.messageContent).then(function () {
            _this.messageForm.reset();
        });
    };
    __decorate([
        core_1.ViewChild('messageForm')
    ], MemberMessagesComponent.prototype, "messageForm");
    __decorate([
        core_1.Input()
    ], MemberMessagesComponent.prototype, "messages");
    __decorate([
        core_1.Input()
    ], MemberMessagesComponent.prototype, "username");
    MemberMessagesComponent = __decorate([
        core_1.Component({
            changeDetection: core_1.ChangeDetectionStrategy.OnPush,
            selector: 'app-member-messages',
            templateUrl: './member-messages.component.html',
            styleUrls: ['./member-messages.component.scss']
        })
    ], MemberMessagesComponent);
    return MemberMessagesComponent;
}());
exports.MemberMessagesComponent = MemberMessagesComponent;
