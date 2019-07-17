import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
let FuseConfirmDialogModule = class FuseConfirmDialogModule {
};
FuseConfirmDialogModule = tslib_1.__decorate([
    NgModule({
        declarations: [
            FuseConfirmDialogComponent
        ],
        imports: [
            MatDialogModule,
            MatButtonModule
        ],
        entryComponents: [
            FuseConfirmDialogComponent
        ],
    })
], FuseConfirmDialogModule);
export { FuseConfirmDialogModule };
//# sourceMappingURL=confirm-dialog.module.js.map