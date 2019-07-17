import * as tslib_1 from "tslib";
import { Component, ViewEncapsulation } from '@angular/core';
let QuickPanelComponent = class QuickPanelComponent {
    /**
     * Constructor
     */
    constructor() {
        // Set the defaults
        this.date = new Date();
        this.settings = {
            notify: true,
            cloud: false,
            retro: true
        };
    }
};
QuickPanelComponent = tslib_1.__decorate([
    Component({
        selector: 'quick-panel',
        templateUrl: './quick-panel.component.html',
        styleUrls: ['./quick-panel.component.scss'],
        encapsulation: ViewEncapsulation.None
    }),
    tslib_1.__metadata("design:paramtypes", [])
], QuickPanelComponent);
export { QuickPanelComponent };
//# sourceMappingURL=quick-panel.component.js.map