import * as tslib_1 from "tslib";
import { Pipe } from '@angular/core';
let CamelCaseToDashPipe = class CamelCaseToDashPipe {
    /**
     * Transform
     *
     * @param {string} value
     * @param {any[]} args
     * @returns {string}
     */
    transform(value, args = []) {
        return value ? String(value).replace(/([A-Z])/g, (g) => `-${g[0].toLowerCase()}`) : '';
    }
};
CamelCaseToDashPipe = tslib_1.__decorate([
    Pipe({ name: 'camelCaseToDash' })
], CamelCaseToDashPipe);
export { CamelCaseToDashPipe };
//# sourceMappingURL=camelCaseToDash.pipe.js.map