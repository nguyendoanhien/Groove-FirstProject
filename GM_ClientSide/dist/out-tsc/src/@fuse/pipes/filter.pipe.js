import * as tslib_1 from "tslib";
import { Pipe } from '@angular/core';
import { FuseUtils } from '@fuse/utils';
let FilterPipe = class FilterPipe {
    /**
     * Transform
     *
     * @param {any[]} mainArr
     * @param {string} searchText
     * @param {string} property
     * @returns {any}
     */
    transform(mainArr, searchText, property) {
        return FuseUtils.filterArrayByString(mainArr, searchText);
    }
};
FilterPipe = tslib_1.__decorate([
    Pipe({ name: 'filter' })
], FilterPipe);
export { FilterPipe };
//# sourceMappingURL=filter.pipe.js.map