import * as tslib_1 from "tslib";
import { Pipe } from '@angular/core';
let GetByIdPipe = class GetByIdPipe {
    /**
     * Transform
     *
     * @param {any[]} value
     * @param {number} id
     * @param {string} property
     * @returns {any}
     */
    transform(value, id, property) {
        const foundItem = value.find(item => {
            if (item.id !== undefined) {
                return item.id === id;
            }
            return false;
        });
        if (foundItem) {
            return foundItem[property];
        }
    }
};
GetByIdPipe = tslib_1.__decorate([
    Pipe({
        name: 'getById',
        pure: false
    })
], GetByIdPipe);
export { GetByIdPipe };
//# sourceMappingURL=getById.pipe.js.map