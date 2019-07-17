import * as tslib_1 from "tslib";
import { Component, EventEmitter, forwardRef, Input, Output, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { MatColors } from '@fuse/mat-colors';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
export const FUSE_MATERIAL_COLOR_PICKER_VALUE_ACCESSOR = {
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => FuseMaterialColorPickerComponent),
    multi: true
};
let FuseMaterialColorPickerComponent = class FuseMaterialColorPickerComponent {
    /**
     * Constructor
     */
    constructor() {
        // Set the defaults
        this.colorChanged = new EventEmitter();
        this.colors = MatColors.all;
        this.hues = ['50', '100', '200', '300', '400', '500', '600', '700', '800', '900', 'A100', 'A200', 'A400', 'A700'];
        this.selectedHue = '500';
        this.view = 'palettes';
        // Set the private defaults
        this._color = '';
        this._modelChange = () => {
        };
        this._modelTouched = () => {
        };
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------
    /**
     * Selected class
     *
     * @param value
     */
    set color(value) {
        if (!value || value === '' || this._color === value) {
            return;
        }
        // Split the color value (red-400, blue-500, fuse-navy-700 etc.)
        const colorParts = value.split('-');
        // Take the very last part as the selected hue value
        this.selectedHue = colorParts[colorParts.length - 1];
        // Remove the last part
        colorParts.pop();
        // Rejoin the remaining parts as the selected palette name
        this.selectedPalette = colorParts.join('-');
        // Store the color value
        this._color = value;
    }
    get color() {
        return this._color;
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Control Value Accessor implementation
    // -----------------------------------------------------------------------------------------------------
    /**
     * Register on change function
     *
     * @param fn
     */
    registerOnChange(fn) {
        this._modelChange = fn;
    }
    /**
     * Register on touched function
     *
     * @param fn
     */
    registerOnTouched(fn) {
        this._modelTouched = fn;
    }
    /**
     * Write value to the view from model
     *
     * @param color
     */
    writeValue(color) {
        // Return if null
        if (!color) {
            return;
        }
        // Set the color
        this.color = color;
        // Update the selected color
        this.updateSelectedColor();
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------
    /**
     * Select palette
     *
     * @param event
     * @param palette
     */
    selectPalette(event, palette) {
        // Stop propagation
        event.stopPropagation();
        // Go to 'hues' view
        this.view = 'hues';
        // Update the selected palette
        this.selectedPalette = palette;
        // Update the selected color
        this.updateSelectedColor();
    }
    /**
     * Select hue
     *
     * @param event
     * @param hue
     */
    selectHue(event, hue) {
        // Stop propagation
        event.stopPropagation();
        // Update the selected huse
        this.selectedHue = hue;
        // Update the selected color
        this.updateSelectedColor();
    }
    /**
     * Remove color
     *
     * @param event
     */
    removeColor(event) {
        // Stop propagation
        event.stopPropagation();
        // Return to the 'palettes' view
        this.view = 'palettes';
        // Clear the selected palette and hue
        this.selectedPalette = '';
        this.selectedHue = '';
        // Update the selected color
        this.updateSelectedColor();
    }
    /**
     * Update selected color
     */
    updateSelectedColor() {
        if (this.selectedColor && this.selectedColor.palette === this.selectedPalette && this.selectedColor.hue === this.selectedHue) {
            return;
        }
        // Set the selected color object
        this.selectedColor = {
            palette: this.selectedPalette,
            hue: this.selectedHue,
            class: this.selectedPalette + '-' + this.selectedHue,
            bg: this.selectedPalette === '' ? '' : MatColors.getColor(this.selectedPalette)[this.selectedHue],
            fg: this.selectedPalette === '' ? '' : MatColors.getColor(this.selectedPalette).contrast[this.selectedHue]
        };
        // Emit the color changed event
        this.colorChanged.emit(this.selectedColor);
        // Mark the model as touched
        this._modelTouched(this.selectedColor.class);
        // Update the model
        this._modelChange(this.selectedColor.class);
    }
    /**
     * Go to palettes view
     *
     * @param event
     */
    goToPalettesView(event) {
        // Stop propagation
        event.stopPropagation();
        this.view = 'palettes';
    }
    /**
     * On menu open
     */
    onMenuOpen() {
        if (this.selectedPalette === '') {
            this.view = 'palettes';
        }
        else {
            this.view = 'hues';
        }
    }
};
tslib_1.__decorate([
    Output(),
    tslib_1.__metadata("design:type", EventEmitter)
], FuseMaterialColorPickerComponent.prototype, "colorChanged", void 0);
tslib_1.__decorate([
    Input(),
    tslib_1.__metadata("design:type", String),
    tslib_1.__metadata("design:paramtypes", [Object])
], FuseMaterialColorPickerComponent.prototype, "color", null);
FuseMaterialColorPickerComponent = tslib_1.__decorate([
    Component({
        selector: 'fuse-material-color-picker',
        templateUrl: './material-color-picker.component.html',
        styleUrls: ['./material-color-picker.component.scss'],
        animations: fuseAnimations,
        encapsulation: ViewEncapsulation.None,
        providers: [FUSE_MATERIAL_COLOR_PICKER_VALUE_ACCESSOR]
    }),
    tslib_1.__metadata("design:paramtypes", [])
], FuseMaterialColorPickerComponent);
export { FuseMaterialColorPickerComponent };
//# sourceMappingURL=material-color-picker.component.js.map