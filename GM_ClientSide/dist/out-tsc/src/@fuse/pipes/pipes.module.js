import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { KeysPipe } from './keys.pipe';
import { GetByIdPipe } from './getById.pipe';
import { HtmlToPlaintextPipe } from './htmlToPlaintext.pipe';
import { FilterPipe } from './filter.pipe';
import { CamelCaseToDashPipe } from './camelCaseToDash.pipe';
let FusePipesModule = class FusePipesModule {
};
FusePipesModule = tslib_1.__decorate([
    NgModule({
        declarations: [
            KeysPipe,
            GetByIdPipe,
            HtmlToPlaintextPipe,
            FilterPipe,
            CamelCaseToDashPipe
        ],
        imports: [],
        exports: [
            KeysPipe,
            GetByIdPipe,
            HtmlToPlaintextPipe,
            FilterPipe,
            CamelCaseToDashPipe
        ]
    })
], FusePipesModule);
export { FusePipesModule };
//# sourceMappingURL=pipes.module.js.map