import * as tslib_1 from "tslib";
import { Pipe } from '@angular/core';
let HtmlToPlaintextPipe = class HtmlToPlaintextPipe {
    /**
     * Transform
     *
     * @param {string} value
     * @param {any[]} args
     * @returns {string}
     */
    transform(value, args = []) {
        return value ? String(value).replace(/<[^>]+>/gm, '') : '';
    }
};
HtmlToPlaintextPipe = tslib_1.__decorate([
    Pipe({ name: 'htmlToPlaintext' })
], HtmlToPlaintextPipe);
export { HtmlToPlaintextPipe };
//# sourceMappingURL=htmlToPlaintext.pipe.js.map