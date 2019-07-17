import * as tslib_1 from "tslib";
var FuseModule_1;
import { NgModule, Optional, SkipSelf } from '@angular/core';
import { FUSE_CONFIG } from '@fuse/services/config.service';
let FuseModule = FuseModule_1 = class FuseModule {
    constructor(parentModule) {
        if (parentModule) {
            throw new Error('FuseModule is already loaded. Import it in the AppModule only!');
        }
    }
    static forRoot(config) {
        return {
            ngModule: FuseModule_1,
            providers: [
                {
                    provide: FUSE_CONFIG,
                    useValue: config
                }
            ]
        };
    }
};
FuseModule = FuseModule_1 = tslib_1.__decorate([
    NgModule(),
    tslib_1.__param(0, Optional()), tslib_1.__param(0, SkipSelf()),
    tslib_1.__metadata("design:paramtypes", [FuseModule])
], FuseModule);
export { FuseModule };
//# sourceMappingURL=fuse.module.js.map